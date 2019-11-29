using System;
using Xunit;
using CodingChallengeLibrary;
using CodingChallengeLibrary.DataReader;
using CodingChallengeLibrary.FormatReader;

namespace UnitTests
{
    public class MockFormatReaderUnitTests
    {
        [Fact]
        public void MockFormatReaderCreationTest()
        {
            IFormatReader mockFormatReader = new MockFormatReader();
            
            Assert.NotNull(mockFormatReader);
            Assert.IsType<MockFormatReader>(mockFormatReader);
        }


        [Fact]
        public void MockFormatReaderReadTest()
        {
            IFormatReader mockFormatReader = new MockFormatReader();

            var rootManager = mockFormatReader.Read();

            Assert.NotNull(rootManager);
            Assert.IsType<Manager<IEmployee>>(rootManager);
        }
    }


// ===========================================================================================
// ===========================================================================================


    public class JsonFormatReaderUnitTests
    {
        [Fact]
        public void JsonFormatReaderEmptyArgumentException()
        {
            Assert.Throws<ArgumentException>( () => new JsonFormatReader(null) );
        }


        [Fact]
        public void JsonFormatReaderNullArgumentException()
        {
            Assert.Throws<ArgumentException>( () => new JsonFormatReader("") );
        }


        [Fact]
        public void JsonFormatReaderInvalidFileTypeArgumentException()
        {
            Assert.Throws<ArgumentException>( () => new JsonFormatReader("c:/TestFilePath.txt") );
        }


        [Fact]
        public void ValidJsonFormatReaderCreationTest()
        {
            IFormatReader jsonFormatReader = new JsonFormatReader("c:/TestFilePath.json");
            
            Assert.NotNull(jsonFormatReader);
            Assert.IsType<JsonFormatReader>(jsonFormatReader);
        }


        [Fact]
        public void JsonFormatReaderReadTest()
        {
            Assert.Throws<ArgumentException>( () => {
                var jsonFormatReader = new JsonFormatReader("");

                jsonFormatReader.Read(); 
            });
        }
    }
}
