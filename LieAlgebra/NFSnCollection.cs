using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LieAlgebra
{
    public class NFSnCollection : List<NFSnElement>
    {
        public NFSnCollection() { }
        public static NFSnCollection operator *(NFSnCollection a, NPermutation b)
        {
            NFSnCollection result = new NFSnCollection();
            for (int i = 0; i < a.Count; i++)
            {
                NFSnElement el = new NFSnElement();
                el.FElement = a[i].FElement;
                el.SnElement = (NPermutation)(a[i].SnElement * b);
                result.Add(el);
            }
            return result;
        }
    }
}
