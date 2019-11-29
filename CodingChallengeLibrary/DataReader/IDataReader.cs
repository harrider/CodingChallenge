using System;

namespace CodingChallengeLibrary.DataReader
{

    /*
        Description:  This interface is responsible providing the interface 
                        necessary to obtain employee data
    */
    public interface IDataReader
    {
        IEmployee Read();   // Read() method returns the root 'Manager' object for the employee hierarchy tree
    }
}