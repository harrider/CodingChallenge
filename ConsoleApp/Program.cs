using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using CodingChallengeLibrary;
using CodingChallengeLibrary.FormatReader;
using CodingChallengeLibrary.DataReader;
using CodingChallengeLibrary.DataPrinter;

namespace ConsoleApp
{
    class Program
    {
        // Private variables
        private const string _AppSettingsFileName = "appsettings.json";
        private const string _AppSetting_HierarchyFileName = "HierarchyFileName";
        private const string _AppSetting_SortHierarchyOutput = "SortHierarchyOutput";


        // Main
        static void Main(string[] args)
        {
            // Try to load AppSettings.json configuration file first
            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile(_AppSettingsFileName);

            var configs = builder.Build();


            // Get the Employee Hierarchy filename from the AppSettings file
            var employeeHierarchyFileName = configs[_AppSetting_HierarchyFileName];
           
           // Throw exception if there was a problem
            if(String.IsNullOrEmpty(employeeHierarchyFileName))
                throw new ApplicationException($"ERROR :: Problem reading App Setting '{_AppSetting_HierarchyFileName}'!");


            // Create filepath to Employee Hierarchy json file
            var employeeHierarchyFilePath = Path.Combine(Environment.CurrentDirectory, employeeHierarchyFileName);


            // Convert AppSettings parameter 'SortHierarchyOutput' to Boolean
            var castSortEmployeeHierarchySuccess = bool.TryParse(configs[_AppSetting_SortHierarchyOutput], out bool sortEmployeeHierarchy);

            if(castSortEmployeeHierarchySuccess == false)
                throw new ApplicationException($"ERROR :: Problem reading App Setting '{_AppSetting_SortHierarchyOutput}'!");

            
            // Composition Root & Dependency Injection patterns
            // IFormatReader formatReader = new MockFormatReader();
            IFormatReader formatReader = new JsonFormatReader(employeeHierarchyFilePath);
            IDataReader dataReader = new DataReader(formatReader);
            IDataPrinter hierarchyPrinter = null;


            // If AppSettings parameter 'SortHierarchyOutput' set to True: create 'SortedHierarchyPrinter'
            //  otherwise, create an unsorted 'HierarchyPrinter'
            if(sortEmployeeHierarchy)
            {
                hierarchyPrinter = new SortedHierarchyPrinter();
            }
            else
            {
                hierarchyPrinter = new HierarchyPrinter();
            }


            // Create the Salaray Requirement Printer
            IDataPrinter salaryRequirementPrinter = new SalaryRequirementPrinter();


            // Read Employee data from the data source
            var rootManager = dataReader.Read();


            // Get String representations for the Employee Hierarchy & Salary Requirement
            var hierarchyString = hierarchyPrinter.PrintString(rootManager);
            var salaryRequirementString = salaryRequirementPrinter.PrintString(rootManager);


            // Print the Employee Hierarchy & Salary Requirement
            Console.WriteLine(hierarchyString);
            Console.WriteLine(salaryRequirementString);
        }
    }
}
