using System;
using System.Collections.Generic;

namespace CodingChallengeLibrary.DataPrinter
{
    public interface IDataPrinter
    {
        string PrintString(IEmployee rootManager);
    }
}