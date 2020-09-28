using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Algebra
{
    public class FSnCollection: List<FSnElement>
    {
        public FSnCollection() {}
        public static FSnCollection operator *(FSnCollection a, Permutation b)
        {
            FSnCollection result = new FSnCollection();
            for (int i = 0; i < a.Count; i++)
            {
                FSnElement el = new FSnElement();
                el.FElement = a[i].FElement;
                el.SnElement = (Permutation)( a[i].SnElement * b);
                result.Add(el);
            }
            return result;
        }
    }
}
