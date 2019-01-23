using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using FakeItEasy;

using Shouldly;

using Spinit.Data.Export.Implementations.Csv;
using Spinit.IO;
using Spinit.IO.Factories;
using Xunit;

namespace Spinit.Data.Export.UnitTest
{
    public class ExportCsvFileServiceTests
    {
        [Fact]
        public void CreateDocumentAsFileTest()
        {
            const string OutputPath = "outputpath.txt";
            var fileStreamFactory = A.Fake<IFileStreamFactory>();
            var fileStream = A.Fake<IFileStream>();
            A.CallTo(() => fileStreamFactory.New(OutputPath, FileMode.Create)).Returns(fileStream);

            var itemToExport = new List<T1> { new T1 { Member1 = 1, Member2 = "two" } };
            var expectedOutput = Encoding.Unicode.GetBytes("Member1,Member2" + Environment.NewLine + "1,\"two\"" + Environment.NewLine);
            var sut = new CsvFileExporter(new ExportService(), fileStreamFactory);
            sut.Write(itemToExport, OutputPath);

            A.CallTo(() => fileStream.Write(A<byte[]>.That.Matches(x => AreEqual(x, expectedOutput)), 0, expectedOutput.Length)).MustHaveHappened(1, Times.Exactly);
            A.CallTo(() => fileStream.Flush()).MustHaveHappened(1, Times.Exactly);
            A.CallTo(() => fileStream.Dispose()).MustHaveHappened(1, Times.Exactly);
        }

        [Fact]
        public void CreateDocumentAsByteArrayTest()
        {
            var itemToExport = new List<T1> { new T1 { Member1 = 1, Member2 = "two" } };
            var expectedOutput = Encoding.Unicode.GetBytes("Member1,Member2" + Environment.NewLine + "1,\"two\"" + Environment.NewLine);
            var sut = new CsvByteArrayExporter(new ExportService());
            var result = sut.Write(itemToExport);

            result.ShouldBe(expectedOutput);
        }

        private bool AreEqual(byte[] byteArray1, byte[] byteArray2)
        {
            return byteArray1.Length == byteArray2.Length && !byteArray1.Where((t, i) => t != byteArray2[i]).Any();
        }
    }
}