using System;
using System.Text;
using System.Linq;

namespace CodingChallengeLibrary.DataPrinter
{
    public class SortedHierarchyPrinter : IDataPrinter
    {
        // Public variables

        // Private variables


        // Constructor
        public SortedHierarchyPrinter()
        {

        }


        public string PrintString(IEmployee rootManager)
        {
            if(rootManager == null)
                throw new ArgumentException($"ERROR :: '{nameof(rootManager)}' is NULL!");

            var result = PrintRecursive(rootManager, 0);

            return result;
        }


        private string PrintRecursive(IEmployee employeeToPrint, int hierarchyLevel)
        {
            var builder = new StringBuilder();

            if(employeeToPrint is Employee)
            {
                for(int i = 0; i < hierarchyLevel; i++)
                {
                    builder.Append("\t");
                }

                builder.Append($"{employeeToPrint.Name}");
                builder.Append("\n");
            }
            else if(employeeToPrint is Manager<IEmployee>)
            {
                var managerEmployees = employeeToPrint.HierarchyDetails();

                if(managerEmployees.Count > 0)
                {
                    var sortedManagerEmployees = managerEmployees
                        .Select( x => x)
                        .OrderBy( x => x.Name );

                    for(int i = 0; i < hierarchyLevel; i++)
                    {
                        builder.Append("\t");
                    }

                    builder.Append($"{employeeToPrint.Name}");
                    builder.Append("\n");

                    for(int i = 0; i < hierarchyLevel; i++)
                    {
                        builder.Append("\t");
                    }

                    builder.Append($"Employees of: {employeeToPrint.Name}");
                    builder.Append("\n");

                    foreach(var employee in sortedManagerEmployees)
                    {
                        var result = PrintRecursive(employee, hierarchyLevel + 1);

                        builder.Append(result);
                    }
                }
            }
            else
            {
                throw new UnhandledEmployeeTypeException($"ERROR :: Type '{employeeToPrint.GetType().ToString()}' is Unhandled!");
            }
            

            return builder.ToString();
        }

    }
}