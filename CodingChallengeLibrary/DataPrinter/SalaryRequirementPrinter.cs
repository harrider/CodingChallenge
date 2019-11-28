using System;

namespace CodingChallengeLibrary.DataPrinter
{
    public class SalaryRequirementPrinter : IDataPrinter
    {
        // Public variables

        // Private variables


        // Constructor
        public SalaryRequirementPrinter()
        {

        }


        public string PrintString(IEmployee rootManager)
        {
            if(rootManager == null)
                throw new ArgumentException($"ERROR :: '{nameof(rootManager)}' is NULL!");

            var result = PrintRecursive(rootManager);

            return $"Total salary: {result.ToString("C")}";
        }


        private decimal PrintRecursive(IEmployee employeeToPrint)
        {
            var sum = 0.0m;

            sum += employeeToPrint.Salary;

            foreach(var employee in employeeToPrint.HierarchyDetails())
            {
                if(employee is Employee)
                {
                    sum += employee.Salary;
                }
                else if(employee is Manager<IEmployee>)
                {
                    var result = PrintRecursive(employee);

                    sum += result;
                }
                else
                {
                    throw new UnhandledEmployeeTypeException($"ERROR :: Type '{employeeToPrint.GetType().ToString()}' is Unhandled!");
                }
            }

            return sum;
        }

    }
}