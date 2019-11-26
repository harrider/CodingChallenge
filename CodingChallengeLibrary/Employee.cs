using System;
using System.Collections.Generic;

namespace CodingChallengeLibrary
{
    public class Employee : IEmployee
    {
        // Public variables
        public string Name 
        {
            get
            {
                return _name;
            }
        }


        public decimal Salary
        {
            get
            {
                return _salary;
            }
        }

        // Private variables
        private readonly string _name;
        private readonly decimal _salary;

        // Constructor
        public Employee(string name, decimal salary)
        {
            if(String.IsNullOrEmpty(name))
                throw new ArgumentException($"ERROR :: '{nameof(name)}' is NULL or Empty!");
            else if(salary < 0.0m)
                throw new ArgumentException($"ERROR :: '{nameof(salary)}' cannot be negative!");

            this._name = name;
            this._salary = salary;
        }


        public ICollection<IEmployee> HierarchyDetails()
        {
            var employeeCollection = new List<IEmployee>
            {
                this
            };

            return employeeCollection; 
        }
        
    }
}