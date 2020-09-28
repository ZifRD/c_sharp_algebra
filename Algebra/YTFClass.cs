using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Algebra
{
    [Serializable]
    public class YTFClass
    {
        public readonly STTableau Tab;
        public readonly List<BasElem> BasElemList;
        public YTFClass()
        {
        }
        public YTFClass(STTableau t, List<BasElem> bl)
        {
            Tab = t;
            BasElemList = bl;
        }
    }
}
