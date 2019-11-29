using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CodingChallengeLibrary.FormatReader
{

    /*
        Description:  This class is responsible for reading json data from a JSON file and builds a
                        .NET represesntation of the JSON data.
    */
    public class JsonFormatReader : IFormatReader   // This class implements the 'IFormatReader' interface
    {
        // Public variables

        // Private variables
        private readonly string _dataFilePath;      // filepath to the employee hierarchy json file


        // Constructor
        public JsonFormatReader(string dataFilePath)
        {
            // Guard clauses to ensure that only a valid JsonFormatReader can be created
            if(String.IsNullOrEmpty(dataFilePath))
                throw new ArgumentException($"ERROR :: '{nameof(dataFilePath)}' is NULL or Empty!");
            
            if(Path.GetExtension(dataFilePath).ToLower().Equals(".json") == false)
                throw new ArgumentException($"ERROR :: Hierarchy file must be '.json' file type!");

            // Assign private variables
            this._dataFilePath = dataFilePath;
        }


        /*
            Description:  This method will try to open the 'Hierarchy.json' file and build .NET objects
                            representing the data in the 'Hierarchy.json' file
            Inputs:  
                N/A
            Output: 
                IEmployee - root 'IEmployee' object for employee hierarchy data
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
            Inputs:  
                JObject - Newtonsoft.Json .NET object matching JSON data representation
            Output: 
                IEmployee - 'IEmployee' object representing either an 'Employee' object or 'Manager' object
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

                    // If the current employeeJObject is of type 'Manager'; Recursive case
                    if(tempJsonObject.EmployeeType.Equals("Manager"))
                    {
                        // Call this method recursively to build the current manager's direct reports List
                        employeeToAdd = ReadRecursive(employeeJObject);

                        // Add the completed Direct Reporting Manager to the Current Manager's Direct Report List
                        employees.Add(employeeToAdd);
                    }
                    // Else if the current employeeJObject is of type 'Employee'; Base case
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
                    else
                        throw new UnhandledEmployeeTypeException($"ERROR :: Employee Type '{tempJsonObject.EmployeeType}' is unknown to application!");
                }

                // Create a .NET 'Employee' type object; 'Manager' type decorates 'Employee' type
                var managerEmployee = new Employee(
                    jsonObject.EmployeeData.Name, 
                    jsonObject.EmployeeData.Salary
                );

                // Create a .NET 'Manager' type object
                newEmployee = new Manager<IEmployee>(managerEmployee, employees);
            }
            // Else if the .NET anonymous type object is an employee of type 'Employee'; This is base case
            else if(jsonObject.EmployeeType.Equals("Employee"))
            {
                // Create a .NET 'Employee' type object
                newEmployee = new Employee(
                    jsonObject.EmployeeData.Name, 
                    jsonObject.EmployeeData.Salary
                );
            }
            else
                throw new UnhandledEmployeeTypeException($"ERROR :: Employee Type '{jsonObject.EmployeeType}' is unknown to application!");

            // Return the new IEmployee object 
            return newEmployee;
        }


    }
}