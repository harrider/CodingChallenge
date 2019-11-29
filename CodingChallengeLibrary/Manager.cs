using System;
using System.Collections.Generic;

namespace CodingChallengeLibrary
{

    /*
        Description:  This class is responsible for extending the functionality of an 'IEmployee'
                        object by adding a collection of direct reports / employees using the 
                        'Decorator' and 'Composite' design patterns
    */
    public class Manager<T> : IEmployee where T : IEmployee // This class decorates 'IEmployee' type objects and also implements the 'IEmployee' interface itself
    {
        // Public variables
        public string Name      // Readonly property that gets the value of the 'Employee' type 'Name' property
        {
            get
            {
                return _manager.Name;
            }
        }

        public decimal Salary   // Readonly property that gets the value of the 'Employee' type 'Salary' property
        {
            get
            {
                return _manager.Salary;
            }
        }

        // Private variables
        private readonly IEmployee _manager;                    // readonly private variable for the decorated 'IEmployee' object
        private readonly ICollection<IEmployee> _employees;     // readonly private varaible for the list of employees / direct reports


        // Constructor
        public Manager(T manager, ICollection<IEmployee> employees)
        {
            // Guard clauses to ensure that only a valid Manager can be created
            if(manager == null)
                throw new ArgumentException($"ERROR :: '{nameof(manager)}' is NULL!");
            else if(employees == null)
                throw new ArgumentException($"ERROR :: '{nameof(employees)}' is NULL!");

            // Assign private variables
            this._manager = manager;
            this._employees = employees;
        }


        /*
            Description:  This method will return the collection of employees / direct reports for
                            the referenced Manager
            Inputs:  
                N/A
            Output: 
                ICollection<IEmployee> - collection of 'IEmployee' objects representing the 
                                            referenced manager's employees / direct reports
        */
        public ICollection<IEmployee> HierarchyDetails()
        {
            // Return the collection of employees / direct reports
            return _employees;
        }
        
    }
}