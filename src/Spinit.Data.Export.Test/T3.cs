using System.Collections.Generic;
using System.ComponentModel;

namespace Spinit.Data.Export.UnitTest
{
    internal class T3
    {
        [DisplayName("Medlem1")]
        [ExcludeExport]
        public int Member1 { get; set; }

        [ExcludeExport]
        public int Member2 { get; set; }

        public static List<T3> Build()
        {
            return new List<T3>();
        }
    }
}