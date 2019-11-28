using System;
using System.Collections.Generic;
using Xunit;
using CodingChallengeLibrary;

namespace UnitTests
{
    public class EmployeeClassUnitTests
    {
        [Fact]
        public void EmployeeNullNameArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>( () => new Employee(null, 1) );
        }

        [Fact]
        public void EmployeeEmptyNameArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>( () => new Employee("", 1) );
        }


        [Fact]
        public void EmployeeNegativeSalaryArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>( () => new Employee("Test", -1) );
        }


        
        [Fact]
        public void ValidEmployeeCreationTest()
        {
            var testEmployee = new Employee("Test", 1);
            
            Assert.Equal("Test", testEmployee.Name);
            Assert.Equal(1, testEmployee.Salary);
        }
    }

// ========================================================================================
// ========================================================================================
    
    
    public class ManagerClassUnitTests
    {
        [Fact]
        public void ManagerNullManagerEmployeeArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>( () => {
                var testEmployees = new List<IEmployee>{
                    new Employee("Employee1", 1)
                };

                return new Manager<IEmployee>(null, testEmployees);
            });
        }


        [Fact]
        public void ManagerNullEmployeesArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>( () => {
                var managerEmployee = new Employee("Manager", 1);

                return new Manager<IEmployee>(managerEmployee, null);
            });
        }


        [Fact]
        public void ValidManagerCreationTest()
        {
                var managerEmployee = new Employee("Manager", 1);
                var testEmployees = new List<IEmployee>{
                    new Employee("Employee1", 1)
                };

                var testManager = new Manager<IEmployee>(managerEmployee, testEmployees);

                Assert.Equal("Manager", managerEmployee.Name);
                Assert.Equal(1, managerEmployee.Salary);
        }
    }
}
