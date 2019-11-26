using System;

namespace CodingChallengeLibrary
{
    public class EmptyEmployeeDataException : Exception
    {
        public EmptyEmployeeDataException(string message) : base(message)
        {
            
        }
    }
}