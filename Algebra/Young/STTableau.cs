using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Algebra;

namespace Algebra
{
    [Serializable]
    public class STTableau : Diagram
    {
        private Permutation numeration;
        
        public Permutation Numeration
        {
            get { return this.numeration; }
        }

        public STTableau(Permutation perm, Partition diagelem):base(diagelem)
        {
            numeration = perm;
        }
        //алгоритм Шенстеда
        public STTableau(Permutation perm):base()
        {
            if (perm.IsInvolution() != true) throw new Exception("Involution required for STTab");
            ArrayList temp1 = new ArrayList();
            ArrayList temp2 = new ArrayList();
            temp2.Add(perm[0]);
            temp1.Add(temp2);
            for (int number = 1; number < perm.Count; number++)
            {
                int a = -1;
                int b = ((ArrayList)temp1[0]).Count;
                int c = perm[number];
                do
                {
                    a++;
                    if (a == (int)temp1.Count)
                    {
                        ArrayList tmp = new ArrayList();
                        tmp.Add(100);
                        temp1.Add(tmp);
                    }
                    int c1 = 0;
                    while ((b > (int)((ArrayList)temp1[a]).Count) || ((b <= (int)((ArrayList)temp1[a]).Count) && (b > 0) && (c < (int)((ArrayList)temp1[a])[b - 1])))
                    {
                        b--;
                    }
                    int res = c;
                    if (b >= ((ArrayList)temp1[a]).Count)
                    { ((ArrayList)temp1[a]).Add(res); goto e; }
                    else c1 = (int)((ArrayList)temp1[a])[b];
                    ((ArrayList)temp1[a])[b] = res;
                    c = c1;
                    if (c == 100) goto e;

                }
                while (b <= ((ArrayList)temp1[a]).Count);
            e: ;
            }

            //temp1 - ArrayList ArrayList по строкам
            this.SetWidth(((ArrayList)temp1[0]).Count);
            this.SetHeight((int)temp1.Count);
            this.SetNumber(perm.Count);
            this.diagElemRow = new Partition();
            for (int i = 0; i < Height; i++)
            {
                DiagElemRow.Add(((ArrayList)temp1[i]).Count);
            }

            Array bla = Array.CreateInstance(typeof(int), Height + 1, Width + 1);
            for (int hh = 0; hh < (int)temp1.Count; hh++)
            {
                for (int kl = 0; kl < (int)((ArrayList)temp1[hh]).Count; kl++)
                {
                    bla.SetValue((int)((ArrayList)temp1[hh])[kl], hh, kl);
                }
            }


            this.numeration = new Permutation();
            int pp = 0;
            int stn = 0;
            while ((stn < Number) && ((int)bla.GetValue(0, pp) != 0))
            {
                for (int gh = 0; gh < Height; gh++)
                {
                    if ((int)bla.GetValue(gh, pp) == 0) break;
                    else
                    {
                        numeration.Add((int)bla.GetValue(gh, pp));
                        stn++;
                    }
                }
                pp++;
            }
        }

        public static bool operator >(STTableau a, STTableau b)
        {
            return (a.numeration > b.numeration);
        }
        public static bool operator <(STTableau a, STTableau b)
        {
            return (a.numeration < b.numeration);
        }
        public static bool operator ==(STTableau a, STTableau b)
        {
            int i = 0;
            if (a.Height == b.Height)
            {
                for (i = 0; i < a.Height; i++)
                {
                    if (a.diagElemRow[i] != a.diagElemRow[i]) break;
                }
            }
            if (i == a.Height - 1) return true;
            else return false;
        }
        public static bool operator !=(STTableau a, STTableau b)
        {
            return (a == b) ? false : true;
        }

        private Array ToMatrix()
        {
            Array matrix = Array.CreateInstance(typeof(int), Height, Width);
            Partition vert = this.DiagElemCol;
            int total = 0;
            for (int i = 0; i < vert.Count; i++)
            {
                for (int j = 0; j < (int)vert[i]; j++)
                {
                    matrix.SetValue(this.numeration[total], j, i);
                    total++;
                }
            }
            return matrix;
        }


        public override bool Equals(object obj)
        {
            return (this == (STTableau)obj);
        }
        public override int GetHashCode()
        {
            return this.Number.GetHashCode();
        }


        //аргумент
        internal bool IsHorizontal()
        {
            if (diagElemRow.Count == 1) return true;
            else return false;
        }
        
        internal bool IsVertical()
        {
            if (DiagElemCol.Count == 1) return true;
            else return false;
        }

        private FSnCollection IfHorizontalStabilizer()
        {
            if (this.IsHorizontal() == true) 
            {
                FSnCollection result = new FSnCollection(); 
                int a = (int)diagElemRow[0];
                List<Permutation> rs = Common.GeneratePermutations(this.Number);
                for (int j = 0; j < rs.Count; j++)
                {
                    FSnElement aa = new FSnElement();
                    aa.SnElement = (Permutation)rs[j];
                    aa.FElement = 1;
                    result.Add(aa);
                }
                return result;
            }
            else return null;
        }

        private FSnCollection IfVerticalStabilizer()
        {
            if (this.IsVertical() == true)
            {
                FSnCollection result = new FSnCollection(); 
                int a = DiagElemCol[0];
                List<Permutation> cs = Common.GeneratePermutations(this.Number);
                for (int j = 0; j < cs.Count; j++)
                {
                    FSnElement aa = new FSnElement();
                    aa.SnElement = cs[j];
                    if (cs[j].IsEven() == true )aa.FElement = 1;
                    else aa.FElement = -1;
                    result.Add(aa);
                }
                return result;
            }
            else return null;
        }

        internal FSnCollection YoungSymmetrizer()
        {
            Array source = this.ToMatrix();
            FSnCollection result = new FSnCollection();
            if (this.IsVertical() == true)
            {
                result = IfVerticalStabilizer();
                return result;
            }
            if (this.IsHorizontal() == true)
            {
                result = IfHorizontalStabilizer();
                return result;
            }
            List<Permutation> rs = this.RowStabilizer(source); 
            List<Permutation> cs = this.ColStabilizer(source);
            for (int i = 0; i < cs.Count; i++)
            {
                int sign = 0;
                if (cs[i].IsEven() == true) sign = 1;
                else sign = -1;
                for (int j = 0; j < rs.Count; j++)
                {
                    FSnElement a = new FSnElement();
                    a.SnElement = (Permutation)(rs[j] * cs[i]);
                    a.FElement = sign;
                    result.Add(a);
                }
            }
            return result;
        }

        private List<Permutation> RowStabilizer(Array matr)
        {
            List<int> partrs = new List<int>(); 
            List<Permutation> rs = new List<Permutation>();
            for (int b1 = 0; b1 < this.Height; b1++)
            {
                int a = diagElemRow[b1];
                if (a!=1)
                {
                    List<int> s = new List<int>();
                    for (int i = 0; i < a; i++)
                    {
                        s.Add((int)matr.GetValue(b1, i));
                    }
                    Common.GeneratePermutationsFromList(s, rs, this.Number);
                }
            }
            for (int i = 0; i < diagElemRow.Count; i++)
            {
                if (diagElemRow[i] != 1) partrs.Add(Common.Factorial(diagElemRow[i]));
            }
            if (diagElemRow[1] > 1)
            {
                for (int i = 0; i < (int)partrs[0]; i++)
                {
                    for (int c2 = (int)partrs[0]; c2 < (int)partrs[0] + (int)partrs[1]; c2++)
                    {
                        rs.Add((Permutation)(rs[i] * rs[c2]));
                    }
                }
                for (int i = 0; i < (int)partrs[0] + (int)partrs[1]; i++)
                {
                    rs.RemoveAt(0);
                }
                partrs.Add(((int)partrs[0]) * ((int)partrs[1]));
                partrs.RemoveAt(0);
                partrs.RemoveAt(1);
            }
            while (partrs.Count > 1)
            {
                int number3 = 0;
                for (int i = 0; i < (int)partrs[0]; i++)
                {
                    number3 = 0;
                    for (int p = 0; p < partrs.Count - 1; p++) number3 += (int)partrs[p];
                    for (int c2 = number3; c2 < (number3 + (int)partrs[partrs.Count - 1]); c2++)
                    {
                        rs.Add((Permutation)(rs[c2] * rs[i]));
                    }
                }
                for (int i = number3; i < (number3 + (int)partrs[partrs.Count - 1]); i++)
                {
                    rs.RemoveAt(number3);
                }
                for (int i = 0; i < (int)partrs[0]; i++)
                {
                    rs.RemoveAt(0);
                }

                partrs.Add(((int)partrs[0]) * ((int)partrs[partrs.Count - 1]));
                partrs.RemoveAt(0);
                partrs.RemoveAt(partrs.Count - 2);
            }
            partrs.Clear();
            return rs;
        }

        private List<Permutation> ColStabilizer(Array matr)
        {
            List<int> partcs = new List<int>();
            List<Permutation> cs = new List<Permutation>();
            for (int b1 = 0; b1 < this.Width; b1++)
            {
                int a = DiagElemCol[b1];
                if (a != 1)
                {
                    List<int> s = new List<int>();
                    for (int i = 0; i < a; i++)
                    {
                        s.Add((int)matr.GetValue(i, b1));
                    }
                    Common.GeneratePermutationsFromList(s, cs, this.Number);
                }
            }
            for (int i = 0; i < DiagElemCol.Count; i++)
            {
                if (DiagElemCol[i] != 1) partcs.Add(Common.Factorial(DiagElemCol[i]));
            }
            if (DiagElemCol[1] > 1)
            {
                for (int i = 0; i < (int)partcs[0]; i++)
                {
                    for (int c2 = (int)partcs[0]; c2 < (int)partcs[0] + (int)partcs[1]; c2++)
                    {
                        cs.Add((Permutation)(cs[i] * cs[c2]));
                    }
                }
                for (int i = 0; i < (int)partcs[0] + (int)partcs[1]; i++)
                {
                    cs.RemoveAt(0);
                }
                partcs.Add(((int)partcs[0]) * ((int)partcs[1]));
                partcs.RemoveAt(0);
                partcs.RemoveAt(1);
            }
            while (partcs.Count > 1)
            {
                int number3 = 0;
                for (int i = 0; i < (int)partcs[0]; i++)
                {
                    number3 = 0;
                    for (int p = 0; p < partcs.Count - 1; p++) number3 += (int)partcs[p];
                    for (int c2 = number3; c2 < (number3 + (int)partcs[partcs.Count - 1]); c2++)
                    {
                        cs.Add((Permutation)(cs[c2] * cs[i]));
                    }
                }
                for (int i = number3; i < (number3 + (int)partcs[partcs.Count - 1]); i++)
                {
                    cs.RemoveAt(number3);
                }
                for (int i = 0; i < (int)partcs[0]; i++)
                {
                    cs.RemoveAt(0);
                }

                partcs.Add(((int)partcs[0]) * ((int)partcs[partcs.Count - 1]));
                partcs.RemoveAt(0);
                partcs.RemoveAt(partcs.Count - 2);
            }
            partcs.Clear();
            return cs;
        }

        public bool Equ(STTableau b)
        {
            bool result = false;
            if (this.numeration == b.numeration && (Diagram)this == (Diagram)b)
                result = true;
            return result;
        }
    }
}


