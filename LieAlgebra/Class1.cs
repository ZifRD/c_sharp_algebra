using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algebra;

namespace LieAlgebra
{
    public static class Operator
    {
        public static NPermutation T(int k, NPermutation p)
        {
            NPermutation outp = new NPermutation();
            outp.Add(p[k]);
            for (int i = 1; i <= k; i++)
              outp.Add(p[i - 1]);
            for (int i = k + 1; i < p.Count;i++ )
              outp.Add(p[i]);
            return outp;
        }

        public static NFSnCollection T(int k,NFSnCollection fsn)
        {
            foreach (NFSnElement fsne in fsn)
            {
                T(k, fsne.SnElement);
            }
            return fsn;
        }
    }
}
