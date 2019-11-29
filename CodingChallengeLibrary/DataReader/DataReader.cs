using System;
using CodingChallengeLibrary.FormatReader;

namespace CodingChallengeLibrary.DataReader
{

    /*
        Description:  This class is responsible for reading data from a given 'IFormatReader' 
    */
    public class DataReader : IDataReader
    {
        // Public variables


        // Private variables
        private readonly IFormatReader _reader;


        // Constructor
        public DataReader(IFormatReader reader)
        {
            // Guard clause to ensure that only a valid DataReader can be created
            if(reader == null)
                throw new ArgumentException($"ERROR :: '{nameof(reader)}' is NULL!");

            // Assign private variables
            this._reader = reader;
        }


        /*
            Description:  This method will try to read data from a given 'IFormatReader' 
                            and return the root 'IEmployee' object for the data.
            Inputs:  
                N/A
            Output: 
                IEmployee - root 'IEmployee' object for employee hierarchy data
        */
        public IEmployee Read()
        {
            // Call the 'Read()' method from the referenced 'IFormatReader' object
            var employeeData = _reader.Read();

            // Ensure that the result of the 'IFormatReader.Read()' action was not null
            if(employeeData == null)
                throw new EmptyEmployeeDataException("ERROR :: Employee Data is Empty!");

            // Return the root 'IEmployee' object
            return employeeData;
        }

    }
}