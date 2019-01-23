using System.Collections.Generic;

namespace Spinit.Data.Export.UnitTest
{
    internal class T4
    {
        internal int Member3 { get; set; }
        protected int Member2 { get; set; }
        private int Member1 { get; set; }

        public static List<T4> Build()
        {
            return new List<T4>();
        }
    }
}