using System;

namespace CodingChallengeLibrary.DataPrinter
{

    /*
        Description:  This class is responsible for formatting the total salary 
                        requirement for the company
    */
    public class SalaryRequirementPrinter : IDataPrinter
    {
        // Public variables

        // Private variables


        // Constructor
        public SalaryRequirementPrinter()
        {

        }


        /*
            Description:  This method will coordinate the building of the formatted string
            Inputs:  
                IEmployee - root 'IEmployee' object for the employee hierarchy
            Output: 
                String  - formatted total salary requirement string
        */
        public string PrintString(IEmployee rootManager)
        {            
            // Guard clauses to ensure root 'Iemployee' object is not null
            if(rootManager == null)
                throw new ArgumentException($"ERROR :: '{nameof(rootManager)}' is NULL!");

            // Build the formatted string
            var result = PrintRecursive(rootManager);

            // Format the sum in US dollars and return the total salary requirement string
            return $"Total salary: {result.ToString("C")}";
        }


        /*
            Description:  This recursive method calculates the sum salary requirement for the company
            Inputs:  
                IEmployee - represents a 'Manager' object
            Output: 
                decimal - the salary requirement sum
        */
        private decimal PrintRecursive(IEmployee employeeToPrint)
        {
            // Set the 'sum' for the current iteration to 0
            var sum = 0.0m;

            // Add the current Manager's salary to the sum
            sum += employeeToPrint.Salary;

            // Iterate through the Manager's list of employees / direct reports
            foreach(var employee in employeeToPrint.HierarchyDetails())
            {
                // If the current 'employee' is an 'Employee' type object
                if(employee is Employee)
                {
                    // Add the current 'employee' salary to the sum
                    sum += employee.Salary;
                }
                // Else if the current 'employee' is a 'Manager' type object
                else if(employee is Manager<IEmployee>)
                {
                    // Call this method recursively and pass current 'employee' (manager) as parameter
                    var result = PrintRecursive(employee);

                    // Add intermediate result sum to the running sum
                    sum += result;
                }
                // Else the referenced 'IEmployee' type is not one that is currently handled
                else
                {
                    throw new UnhandledEmployeeTypeException($"ERROR :: Type '{employeeToPrint.GetType().ToString()}' is Unhandled!");
                }
            }

            // return the calculated sum
            return sum;
        }

    }
}