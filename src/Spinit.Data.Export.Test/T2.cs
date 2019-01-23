using System.Collections.Generic;
using System.ComponentModel;

namespace Spinit.Data.Export.UnitTest
{
    internal class T2
    {
        [DisplayName("Medlem3")]
        public int Member3 { get; set; }

        [DisplayName("Medlem4")]
        public static List<T2> Build()
        {
            return new List<T2>();
        }
    }
}