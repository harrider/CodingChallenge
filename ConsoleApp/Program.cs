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

            // Use command line argument of '-s' or '-sorted' to create a 'SortedHierarchyPrinter'
            //  otherwise, create an unsorted 'HierarchyPrinter'
            IDataPrinter hierarchyPrinter = null;

            if(args.Length > 0)
            {
                if(args[0].ToLower().Equals("-s") || args[0].ToLower().Equals("-sorted"))
                {
                    hierarchyPrinter = new SortedHierarchyPrinter();
                }
                else
                {
                    hierarchyPrinter = new HierarchyPrinter();
                }
            }
            else
            {
                hierarchyPrinter = new HierarchyPrinter();
            }

            // Create the Salaray Requirement Printer
            IDataPrinter salaryRequirementPrinter = new SalaryRequirementPrinter();

            // Read Employee data from the data source
            var rootManager = dataReader.Read();

            // Print the Employee Hierarchy & Salary Requirement
            hierarchyPrinter.Print(rootManager);
            salaryRequirementPrinter.Print(rootManager);
        }
    }
}
