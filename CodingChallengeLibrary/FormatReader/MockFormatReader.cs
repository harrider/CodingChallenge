using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodingChallengeLibrary.FormatReader
{
    public class MockFormatReader : IFormatReader
    {
        // Public variables

        // Private variables


        // Constructor
        public MockFormatReader()
        {

        }


        public IEmployee Read()
        {
            var Jeff = new Employee("Jeff", 100000);
            var Dave = new Employee("Dave", 85000);
            var Cory = new Employee("Cory", 65000);
            var Andy = new Employee("Andy", 65000);
            var Dan = new Employee("Dan", 60000);
            var Jason = new Employee("Jason", 60000);
            var Rick = new Employee("Rick", 56000);
            var Suzanne = new Employee("Suzanne", 61000);

            var ManagerDave = new Manager<IEmployee>(Dave, new List<IEmployee>{
                Andy,
                Dan,
                Jason,
                Rick,
                Suzanne
            });
            
            var ManagerJeff = new Manager<IEmployee>(Jeff, new List<IEmployee>{
                ManagerDave,
                Cory
            });

            return ManagerJeff;
        }

    }
}