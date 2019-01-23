using System.Collections.Generic;

namespace Spinit.Data.Export.UnitTest
{
    internal class T5
    {
        public string MemberForExport { get; set; }
        public string AnotherMember { get; set; }

        public static List<T5> Build()
        {
            return new List<T5>();
        }
    }
}