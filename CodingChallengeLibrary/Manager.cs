using System;
using System.Collections.Generic;

namespace CodingChallengeLibrary
{
    public class Manager<T> : IEmployee where T : IEmployee
    {
        // Public variables
        public string Name 
        {
            get
            {
                return _manager.Name;
            }
        }

        public decimal Salary
        {
            get
            {
                return _manager.Salary;
            }
        }

        // Private variables
        private readonly IEmployee _manager;
        private readonly ICollection<IEmployee> _employees;


        // Constructor
        public Manager(T manager, ICollection<IEmployee> employees)
        {
            if(manager == null)
                throw new ArgumentException($"ERROR :: '{nameof(manager)}' is NULL!");
            else if(employees == null)
                throw new ArgumentException($"ERROR :: '{nameof(employees)}' is NULL!");

            this._manager = manager;
            this._employees = employees;
        }


        public ICollection<IEmployee> HierarchyDetails()
        {
            return _employees;
        }
        
    }
}