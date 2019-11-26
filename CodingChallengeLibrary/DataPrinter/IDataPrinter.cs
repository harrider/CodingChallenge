using System;
using System.Collections.Generic;

namespace CodingChallengeLibrary.DataPrinter
{
    public interface IDataPrinter
    {
        void Print(IEmployee rootManager);
    }
}