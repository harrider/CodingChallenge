using System;
using Xunit;
using CodingChallengeLibrary;
using CodingChallengeLibrary.FormatReader;
using CodingChallengeLibrary.DataReader;

namespace UnitTests
{
    public class DataReaderUnitTests
    {
        [Fact]
        public void DataReaderReadTest()
        {
            IFormatReader formatReader = new MockFormatReader();
            IDataReader dataReader = new DataReader(formatReader);

            var rootManager = dataReader.Read();

            Assert.NotNull(rootManager);
            Assert.IsType<Manager<IEmployee>>(rootManager);
        }
    }
}
