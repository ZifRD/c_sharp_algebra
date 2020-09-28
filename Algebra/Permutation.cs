using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Algebra
{
    [Serializable]
    public class Permutation : ComparableList
    {
        public Permutation():base(){}
        public Permutation(int n): base(n){}
        public Permutation(string str):base(str){}
        public Permutation(List<int> z)  //конструктор с инициализацией по списку
        {
            int counter = 0;

            //Проверка повторений
            for (int i = 1; i <= z.Count; i++)
            {
                int local = 0;
                foreach (int p in z)
                {
                    if (p == i) local++;
                }
                if (local != 1) throw new Exception("PermutationBug");
                else counter++;
            }
            if (counter == z.Count) this.Init(z);
        }

        //Обращение (возведение в степень -1)
        internal Permutation RevPerm()
        {
            Permutation tperm = new Permutation();
            for (int ar = 0; ar < this.Count; ar++)
            {
                int b1 = this.PermutationNumbers.IndexOf(ar + 1);
                tperm.PermutationNumbers.Add(b1 + 1);
            }
            return tperm;
        }

        //Групповая операция
        public static Permutation operator *(Permutation a, Permutation b)
        {
            Permutation perm = new Permutation(a.Count);
            for (int i = 0; i < a.Count; i++)
            {
                perm.PermutationNumbers.Add(a[b[i] - 1]);
            }
            return perm;
        }

        //проверка инволютивности a.RevPerm == a
        internal bool IsInvolution()
        {
            Permutation temp = this.RevPerm();
            if (temp == this) return true;
            else return false;
        }

        //проверка чётности
        internal bool IsEven()
        {
            int sign = 0;
            Array tempar = Array.CreateInstance(typeof(int), this.Count);
            for (int ii = 0; ii < this.Count; ii++)
            {
                tempar.SetValue(this[ii], ii);
            }
            int c1 = 1;
            int a2 = tempar.Length;
            while (c1 != 0)
            {
                c1 = 0;
                for (int r = 0; r < a2 - 1; r++)
                {
                    int b1 = (int)tempar.GetValue(r);
                    int b2 = (int)tempar.GetValue(r + 1);
                    if (b1 > b2)
                    {

                        tempar.SetValue(b2, r);
                        tempar.SetValue(b1, r + 1);
                        c1 = 1;
                        sign++;
                    }
                }
            }
            if (sign % 2 == 0) return true;
            else return false;
        }
    }
}
