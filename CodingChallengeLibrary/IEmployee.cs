using System;
using System.Collections.Generic;

namespace CodingChallengeLibrary
{
    public interface IEmployee
    {
        // Attributes
        string Name { get; }
        decimal Salary { get; }

        // Operations
        ICollection<IEmployee> HierarchyDetails();
    }
}