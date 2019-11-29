using System;
using System.Text;

namespace CodingChallengeLibrary.DataPrinter
{

    /*
        Description:  This class is responsible for formatting the employee hierarchy data
    */
    public class HierarchyPrinter : IDataPrinter
    {
        // Public variables

        // Private variables


        // Constructor
        public HierarchyPrinter()
        {

        }


        /*
            Description:  This method will coordinate the building of the formatted string
            Inputs:  
                IEmployee - root 'IEmployee' object for the employee hierarchy
            Output: 
                String  - formatted employee hierarchy string
        */
        public string PrintString(IEmployee rootManager)
        {
            // Guard clauses to ensure root 'Iemployee' object is not null
            if(rootManager == null)
                throw new ArgumentException($"ERROR :: '{nameof(rootManager)}' is NULL!");

            // Set the starting hierarchy level to 0
            var startingHierarchyLevel = 0;
            
            // Build the formatted string
            var result = PrintRecursive(rootManager, startingHierarchyLevel);

            // Return the formatted employee hierarchy string
            return result;
        }


        /*
            Description:  This recursive method builds a formatted string based on the 
                            .NET 'IEmployee' object
            Inputs:  
                IEmployee - represents either an 'Employee' object or a 'Manager' object
                int - the current level of the employee hierarchy tree (translates to amount of indentation in string)
            Output: 
                string - the formatted string
        */
        private string PrintRecursive(IEmployee employeeToPrint, int hierarchyLevel)
        {
            // Create a string builder to build a large string progressively
            var builder = new StringBuilder();

            // Use Pattern Matching to react accordingly based on the concrete type of the 'IEmployee' object
            // If the 'IEmployee' is an 'Employee' type object; This is the base case for recursion
            if(employeeToPrint is Employee)
            {
                // Append i number of tabs to the string
                for(int i = 0; i < hierarchyLevel; i++)
                {
                    builder.Append("\t");
                }

                // Append the Employee's name to the string
                builder.Append($"{employeeToPrint.Name}");
                builder.Append("\n");
            }
            // Else if the 'IEmployee' is a 'Manager' type object
            else if(employeeToPrint is Manager<IEmployee>)
            {
                // Retrieve the Manager's collection of employees / direct reports
                var managerEmployees = employeeToPrint.HierarchyDetails();

                // If the manager has any employees / direct reports
                if(managerEmployees.Count > 0)
                {
                    // Append i number of tabs to the string
                    for(int i = 0; i < hierarchyLevel; i++)
                    {
                        builder.Append("\t");
                    }

                    // Append the Manager's name to the string
                    builder.Append($"{employeeToPrint.Name}");
                    builder.Append("\n");

                    // Append i number of tabs to the string
                    for(int i = 0; i < hierarchyLevel; i++)
                    {
                        builder.Append("\t");
                    }

                    // Append a line to designate a Manager's list of direct reports
                    builder.Append($"Employees of: {employeeToPrint.Name}");
                    builder.Append("\n");

                    // Iterate through a Manager's list of direct reports
                    foreach(var employee in managerEmployees)
                    {
                        // Call this method recursively and increase hierarchy level by +1
                        var result = PrintRecursive(employee, hierarchyLevel + 1);

                        // Append string results to the current string
                        builder.Append(result);
                    }
                }
            }
            // Else the referenced 'IEmployee' type is not one that is currently handled
            else
            {
                throw new UnhandledEmployeeTypeException($"ERROR :: Type '{employeeToPrint.GetType().ToString()}' is Unhandled!");
            }

            // Return the current formatted string result
            return builder.ToString();
        }

    }
}