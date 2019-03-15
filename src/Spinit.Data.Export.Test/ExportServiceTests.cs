using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        [Fact]
        public void ColumnOrderCanBeCustomized()
        {
            var sut = new ExportService
            {
                ColumnIncluder = new ReversedColumnIncluder()
            };
            var result = sut.CreateTabularData(T1.Build());
            result.Columns[0].ColumnName.ShouldBe("Member2");
            result.Columns[1].ColumnName.ShouldBe("Member1");
        }

        private class ReversedColumnIncluder : DefaultColumnIncluder
        {
            public override IEnumerable<PropertyInfo> GetProperties(Type type) => base.GetProperties(type).Reverse();
        }
    }
}
