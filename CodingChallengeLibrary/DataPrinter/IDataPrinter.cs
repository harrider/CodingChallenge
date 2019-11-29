using System;
using System.Collections.Generic;

namespace CodingChallengeLibrary.DataPrinter
{

    /*
        Description:  This interface is responsible providing the interface 
                        necessary to format data strings
    */
    public interface IDataPrinter
    {
        string PrintString(IEmployee rootManager);  // PrintString() method returns a formatted string
    }
}