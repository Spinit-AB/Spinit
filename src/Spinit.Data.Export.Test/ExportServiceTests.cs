using Shouldly;
using Xunit;

namespace Spinit.Data.Export.UnitTest
{
    public class ExportServiceTests
    {
        [Fact]
        public void PublicMembersAreRecognizedAsColumns()
        {
            var sut = new ExportService();
            var result = sut.CreateTabularData(T1.Build());

            result.Columns.ShouldContain(x => x.ColumnName == "Member1" && x.DataType == typeof(int));
            result.Columns.ShouldContain(x => x.ColumnName == "Member2" && x.DataType == typeof(string));
        }

        [Fact]
        public void DisplayNameIsHonoured()
        {
            var sut = new ExportService();
            var result = sut.CreateTabularData(T2.Build());
            result.Columns.ShouldContain(x => x.ColumnName == "Medlem3");
        }

        [Fact]
        public void ExcludeExportIsHonoured()
        {
            var sut = new ExportService();
            var result = sut.CreateTabularData(T3.Build());
            result.Columns.Count.ShouldBe(0);
        }

        [Fact]
        public void PrivateProtectedAndInternalMembersAreNotExported()
        {
            var sut = new ExportService();
            var result = sut.CreateTabularData(T4.Build());
            result.Columns.Count.ShouldBe(0);
        }

        [Fact]
        public void CustomColumnIncluderIsHonoured()
        {
            var sut = new ExportService();
            sut.ColumnIncluder = CustomColumnIncluder.Create(x => x.Name.EndsWith("Export"));
            var result = sut.CreateTabularData(T5.Build());
            result.Columns.Count.ShouldBe(1);
        }
    }
}
