using System;
using System.Collections.Generic;

namespace CodingChallengeLibrary
{
    public class Employee : IEmployee
    {
        // Public variables
        public string Name      // Readonly property that gets the value of '_name' variable
        {
            get
            {
                return _name;
            }
        }

        public decimal Salary     // Readonly property that gets the value of '_salary' variable
        {
            get
            {
                return _salary;
            }
        }

        // Private variables
        private readonly string _name;          // readonly private variable for employee's name
        private readonly decimal _salary;       // readonly private variable for employee's salary


        // Constructor
        public Employee(string name, decimal salary)
        {
            // Guard clauses to ensure that only a valid Employee can be created
            if(String.IsNullOrEmpty(name))
                throw new ArgumentException($"ERROR :: '{nameof(name)}' is NULL or Empty!");
            else if(salary < 0.0m)
                throw new ArgumentException($"ERROR :: '{nameof(salary)}' cannot be negative!");

            // Assign private variables
            this._name = name;
            this._salary = salary;
        }


        /*
            Description:  This method will return the collection of employees / direct reports for
                            the referenced Manager
            Inputs:  
                N/A
            Output: 
                ICollection<IEmployee> - collection of 'IEmployee' objects containing only 
                                            the current employee object
        */
        public ICollection<IEmployee> HierarchyDetails()
        {
            // Create a new List of IEmployee objects containing only a reference to the current 'Employee' object
            var employeeCollection = new List<IEmployee>
            {
                this
            };

            // Return the collection
            return employeeCollection; 
        }
        
    }
}