using System;
using System.Collections.Generic;

namespace CodingChallengeLibrary.FormatReader
{

    /*
        Description:  This interface is responsible providing the interface 
                        necessary to obtain employee data
    */
    public interface IFormatReader
    {
        IEmployee Read();   // Read() method returns the root 'Manager' object for the employee hierarchy tree
    }
}