using System;
using CodingChallengeLibrary.FormatReader;

namespace CodingChallengeLibrary.DataReader
{
    public class DataReader : IDataReader
    {
        // Public variables


        // Private variables
        private readonly IFormatReader _reader;


        // Constructor
        public DataReader(IFormatReader reader)
        {
            if(reader == null)
                throw new ArgumentException($"ERROR :: '{nameof(reader)}' is NULL!");

            this._reader = reader;
        }

        public IEmployee Read()
        {
            var employeeData = _reader.Read();

            if(employeeData == null)
                throw new EmptyEmployeeDataException("ERROR :: Employee Data is Empty!");

            return employeeData;
        }

    }
}