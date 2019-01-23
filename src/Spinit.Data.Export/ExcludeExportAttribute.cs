using System;

namespace Spinit.Data.Export
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ExcludeExportAttribute : Attribute
    {
    }
}