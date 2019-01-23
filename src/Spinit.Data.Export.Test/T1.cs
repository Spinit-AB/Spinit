using System.Collections.Generic;

namespace Spinit.Data.Export.UnitTest
{
    internal class T1
    {
        public int Member1 { get; set; }
        public string Member2 { get; set; }

        public static List<T1> Build()
        {
            return new List<T1>();
        }
    }
}