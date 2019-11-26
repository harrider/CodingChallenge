using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodingChallengeLibrary.FormatReader
{
    public class JsonFormatReader : IFormatReader
    {
        // Public variables

        // Private variables
        private readonly string _dataFilePath;


        // Constructor
        public JsonFormatReader(string dataFilePath)
        {
            if(String.IsNullOrEmpty(dataFilePath))
                throw new ArgumentException($"ERROR :: '{nameof(dataFilePath)}' is NULL or Empty!");

            this._dataFilePath = dataFilePath;
        }


        public IEmployee Read()
        {
            throw new NotImplementedException($"'{nameof(Read)}' not implemented yet");
        }

    }
}