using System;
using System.Text;
using Xunit;
using CodingChallengeLibrary;
using CodingChallengeLibrary.FormatReader;
using CodingChallengeLibrary.DataReader;
using CodingChallengeLibrary.DataPrinter;

namespace UnitTests
{
    public class DataPrinterUnitTests
    {
        // Private variables
        private readonly IEmployee _rootManager;


        public DataPrinterUnitTests()
        {
            IFormatReader formatReader = new MockFormatReader();
            IDataReader dataReader = new DataReader(formatReader);

            this._rootManager = dataReader.Read();
        }



        [Fact]
        public void HierarchyPrinterCreationTest()
        {
            IDataPrinter hierarchyPrinter = new HierarchyPrinter();

            Assert.NotNull(hierarchyPrinter);
            Assert.IsType<HierarchyPrinter>(hierarchyPrinter);
        }


        [Fact]
        public void HierarchyPrinterPrintStringArgumentExceptionTest()
        {
            IDataPrinter hierarchyPrinter = new HierarchyPrinter();

            Assert.Throws<ArgumentException>( () => hierarchyPrinter.PrintString(null) );
        }


        [Fact]
        public void HierarchyPrinterPrintStringNotNullTest()
        {
            IDataPrinter hierarchyPrinter = new HierarchyPrinter();

            var result = hierarchyPrinter.PrintString(_rootManager);

            Assert.NotNull(result);
        }


        [Fact]
        public void HierarchyPrinterPrintStringNotEmptyTest()
        {
            IDataPrinter hierarchyPrinter = new HierarchyPrinter();

            var result = hierarchyPrinter.PrintString(_rootManager);

            Assert.NotEmpty(result);
        }


        [Fact]
        public void HierarchyPrinterPrintStringTest()
        {
            IDataPrinter hierarchyPrinter = new HierarchyPrinter();

            var result = hierarchyPrinter.PrintString(_rootManager);

            // Expected Output:
            //-----------------
            // Jeff
            // Employees of: Jeff
            //     Dave
            //     Employees of: Dave
            //         Andy
            //         Dan
            //         Jason
            //         Rick
            //         Suzanne
            //     Cory
            
            var builder = new StringBuilder();
            builder.Append("Jeff\n");
            builder.Append("Employees of: Jeff\n");
            builder.Append("\tDave\n");
            builder.Append("\tEmployees of: Dave\n");
            builder.Append("\t\tAndy\n");
            builder.Append("\t\tDan\n");
            builder.Append("\t\tJason\n");
            builder.Append("\t\tRick\n");
            builder.Append("\t\tSuzanne\n");
            builder.Append("\tCory\n");

            var expectedString = builder.ToString();

            Assert.Equal(expectedString, result);
        }



        [Fact]
        public void SalaryRequirementPrinterCreationTest()
        {
            IDataPrinter salaryRequirementPrinter = new SalaryRequirementPrinter();

            Assert.NotNull(salaryRequirementPrinter);
            Assert.IsType<SalaryRequirementPrinter>(salaryRequirementPrinter);
        }


        [Fact]
        public void SalaryRequirementPrinterPrintStringArgumentExceptionTest()
        {
            IDataPrinter salaryRequirementPrinter = new SalaryRequirementPrinter();

            Assert.Throws<ArgumentException>( () => salaryRequirementPrinter.PrintString(null) );
        }


        [Fact]
        public void SalaryRequirementPrinterPrintStringNotNullTest()
        {
            IDataPrinter salaryRequirementPrinter = new SalaryRequirementPrinter();

            var result = salaryRequirementPrinter.PrintString(_rootManager);

            Assert.NotNull(result);
        }


        [Fact]
        public void SalaryRequirementPrinterPrintStringNotEmptyTest()
        {
            IDataPrinter salaryRequirementPrinter = new SalaryRequirementPrinter();

            var result = salaryRequirementPrinter.PrintString(_rootManager);

            Assert.NotEmpty(result);
        }


        [Fact]
        public void SalaryRequirementPrinterPrintStringTest()
        {
            IDataPrinter salaryRequirementPrinter = new SalaryRequirementPrinter();

            var result = salaryRequirementPrinter.PrintString(_rootManager);

            var expectedString = "Total salary: $552,000.00";

            Assert.Equal(expectedString, result);
        }
    }
}
