using System;

namespace CodingChallengeLibrary
{
    public class UnhandledEmployeeTypeException : Exception
    {
        public UnhandledEmployeeTypeException(string message) : base(message)
        {
            
        }
    }
}