using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodingChallengeLibrary.FormatReader
{

    /*
        Description:  This class is responsible for building a static .NET represesntation 
                        of the given employee data from the Coding Challenge Problem Statement.
    */
    public class MockFormatReader : IFormatReader   // This class implements the 'IFormatReader' interface
    {
        // Public variables

        // Private variables


        // Constructor
        public MockFormatReader()
        {

        }


        /*
            Description:  This method builds .NET objects representing the employee data 
                            given in the Coding Challenge Problem Statement file
            Inputs:  
                N/A
            Output: 
                IEmployee - root 'IEmployee' object for employee hierarchy data
        */
        public IEmployee Read()
        {
            // Create 'Employee' type objects for each employee
            var Jeff = new Employee("Jeff", 100000);
            var Dave = new Employee("Dave", 85000);
            var Cory = new Employee("Cory", 65000);
            var Andy = new Employee("Andy", 65000);
            var Dan = new Employee("Dan", 60000);
            var Jason = new Employee("Jason", 60000);
            var Rick = new Employee("Rick", 56000);
            var Suzanne = new Employee("Suzanne", 61000);

            // Create a 'Manager' type object for the manager Dave with his direct reports
            var ManagerDave = new Manager<IEmployee>(Dave, new List<IEmployee>{
                Andy,
                Dan,
                Jason,
                Rick,
                Suzanne
            });
            

            // Create a 'Manager' type object for the CEO Jeff with his direct reports
            var ManagerJeff = new Manager<IEmployee>(Jeff, new List<IEmployee>{
                ManagerDave,
                Cory
            });


            // Return the 'Manager' object representing the root node in the employee hierarchy tree
            return ManagerJeff;
        }

    }
}