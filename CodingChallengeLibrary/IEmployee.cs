using System;
using System.Collections.Generic;

namespace CodingChallengeLibrary
{
    
    /*
        Description:  This interface is responsible providing the interface 
                        necessary to describe an Employee
    */
    public interface IEmployee
    {
        // Attributes
        string Name { get; }        // Employee's name
        decimal Salary { get; }     // Employee's salary

        // Operations
        ICollection<IEmployee> HierarchyDetails();  // Returns a collection of an employees direct reports
    }
}