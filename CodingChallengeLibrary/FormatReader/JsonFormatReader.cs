using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CodingChallengeLibrary.FormatReader
{
    public class JsonFormatReader : IFormatReader
    {
        // Public variables

        // Private variables
        private readonly string _dataFilePath;


        // Constructor
        public JsonFormatReader(string dataFilePath)
        {
            if(String.IsNullOrEmpty(dataFilePath))
                throw new ArgumentException($"ERROR :: '{nameof(dataFilePath)}' is NULL or Empty!");

            this._dataFilePath = dataFilePath;
        }


        /*
            Description:  This method will try to open the 'Hierarchy.json' file and build .NET objects
                            representing the data in the 'Hierarchy.json' file
            Inputs:  N/A
            Output: IEmployee
        */
        public IEmployee Read()
        {
            IEmployee rootManager = null;

            // Create a Stream Reader for the file at the given filepath 
            using(StreamReader fileReader = File.OpenText(_dataFilePath))
            {
                // Create a Json Text Reader for the file stream reader
                using(JsonTextReader jsonReader = new JsonTextReader(fileReader))
                {
                    // Read the json data from the file
                    var jsonData = (JObject)JToken.ReadFrom(jsonReader);
                    
                    // Call 'ReadRecursive' to build .NET IEmployee representation of the JSON data
                    rootManager = ReadRecursive(jsonData);
                }
            }

            // Return the root IEmployee manager object
            return rootManager;
        }
        

        /*
            Description:  This recursive method builds the .NET objects and collections
                            representing the JSON data read from the 'Hierarchy.json' file
            Inputs:  JObject
            Output: IEmployee
        */
        private IEmployee ReadRecursive(JObject parentJsonObject)
        {
            // IEmployee type that will be returned by method
            IEmployee newEmployee = null;

            // Anonymous object to use as the model when deserializing JSON data
            var jsonEmployee = new {
                EmployeeType = "",
                EmployeeData = new {
                    Name = "",
                    Salary = 0
                },
                DirectReports = new List<object>()
            };

            // Deserialize Newtonson JObject type to .NET anonymous type
            var jsonObject = JsonConvert.DeserializeAnonymousType(parentJsonObject.ToString(), jsonEmployee);

            // If the .NET anonymous type object is an employee of type 'Manager'
            if(jsonObject.EmployeeType.Equals("Manager"))
            {
                // Create a .NET 'Employee' type object; 'Manager' type decorates 'Employee' type
                var managerEmployee = new Employee(
                    jsonObject.EmployeeData.Name, 
                    jsonObject.EmployeeData.Salary
                );

                // Create a List of IEmployee types that will hold the current Manager's direct reports
                var employees = new List<IEmployee>();

                // Iterate through the anonymous type object's Direct Reports 
                foreach(JObject employeeJObject in jsonObject.DirectReports)
                {
                    // Deserialize Newtonson JObject type to .NET anonymous type
                    var tempJsonObject = JsonConvert.DeserializeAnonymousType(
                        employeeJObject.ToString(), 
                        jsonEmployee
                    );

                    // Create a .NET IEmployee varaible that will hold either a 'Manager' or 'Employee'
                    IEmployee employeeToAdd = null;

                    // If the current employeeJObject is of type 'Manager'
                    if(tempJsonObject.EmployeeType.Equals("Manager"))
                    {
                        // Call this method recursively to build it's Direct Reports List
                        employeeToAdd = ReadRecursive(employeeJObject);

                        // Add the completed Direct Reporting Manager to the Current Manager's Direct Report List
                        employees.Add(employeeToAdd);
                    }
                    else if(tempJsonObject.EmployeeType.Equals("Employee"))
                    {
                        // Create a .NET 'Employee' type object
                        employeeToAdd = new Employee(
                            tempJsonObject.EmployeeData.Name,
                            tempJsonObject.EmployeeData.Salary
                        );

                        // Add the completed Direct Reporting Employee to the Current Manager's Direct Report List
                        employees.Add(employeeToAdd);
                    }
                }

                // Create a .NET 'Manager' type object
                newEmployee = new Manager<IEmployee>(managerEmployee, employees);
            }
            // Else if the .NET anonymous type object is an employee of type 'Employee'
            else if(jsonObject.EmployeeType.Equals("Employee"))
            {
                // Create a .NET 'Employee' type object
                newEmployee = new Employee(
                    jsonObject.EmployeeData.Name, 
                    jsonObject.EmployeeData.Salary
                );
            }

            // Return the new IEmployee object 
            return newEmployee;
        }


    }
}