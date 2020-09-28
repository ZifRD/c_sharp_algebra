using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algebra;

namespace LieAlgebra
{
    public class NPermutation: Permutation  //перестановка с нулевым элементом
    {
        public NPermutation():base(){}
        public NPermutation(int n): base(n){}
        public NPermutation(string str):base(str){}
        public NPermutation(List<int> z):base(z){}
        
        //Обращение (возведение в степень -1)
        internal NPermutation RevPerm()
        {
            NPermutation tperm = new NPermutation();
            for (int ar = 0; ar < this.Count; ar++)
            {
                int b1 = this.PermutationNumbers.IndexOf(ar);
                tperm.PermutationNumbers.Add(b1);
            }
            return tperm;
        }

        //Групповая операция
        public static NPermutation operator *(NPermutation a, NPermutation b)
        {
            NPermutation perm = new NPermutation(a.Count);
            for (int i = 0; i < a.Count; i++)
            {
                perm.PermutationNumbers.Add(a[b[i]]);
            }
            return perm;
        }

    }
}
