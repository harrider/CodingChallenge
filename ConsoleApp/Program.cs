using System;
using System.Collections.Generic;   
using CodingChallengeLibrary;
using CodingChallengeLibrary.FormatReader;
using CodingChallengeLibrary.DataReader;
using CodingChallengeLibrary.DataPrinter;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Composition Root & Dependency Injection patterns
            IFormatReader formatReader = new MockFormatReader();
            IDataReader dataReader = new DataReader(formatReader);

            IDataPrinter hierarchyPrinter = new HierarchyPrinter();
            IDataPrinter salaryRequirementPrinter = new SalaryRequirementPrinter();

            // Read Employee data from the data source
            var rootManager = dataReader.Read();

            // Print the Employee Hierarchy & Salary Requirement
            hierarchyPrinter.Print(rootManager);
            salaryRequirementPrinter.Print(rootManager);
        }
    }
}
