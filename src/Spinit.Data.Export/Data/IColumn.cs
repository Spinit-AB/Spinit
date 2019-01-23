using System;

namespace Spinit.Data.Export.Data
{
    public interface IColumn
    {
        string ColumnName { get; }
        Type DataType { get; set; }
    }
}