using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
    [Serializable]
    public class BasElem
    {
        public readonly STTableau Sigma;
        public readonly STTableau Tau;
        public readonly Fraction Coeff;
        public readonly int SigmaInt;   //нумерация с единицы
        public readonly int TauInt;     //нумерация с единицы
        public readonly int MatrixNum;  //нумерация с нуля

        public BasElem(Basis bas, int poss, Fraction coeff)
        {
            int pos = poss + 1;
            Coeff = coeff;
            int sum = 0;
            if (pos == Common.Factorial(bas.Number))
            {
                Sigma = new STTableau(bas.SaveTabGroups[bas.SaveTabGroups.Count-1].TabGroupNumerations[0],
                    bas.SaveTabGroups[bas.SaveTabGroups.Count - 1].TabGroupPartition);
                Tau = Sigma;
                MatrixNum = bas.SaveTabGroups.Count - 1;
                SigmaInt = 1;
                TauInt = 1;
                return;
            }
            if (pos == 1)
            {
                Sigma = new STTableau(bas.SaveTabGroups[0].TabGroupNumerations[0], bas.SaveTabGroups[0].TabGroupPartition);
                TauInt = 1;
                SigmaInt = 1;
                Tau = Sigma;
                return;
            }
            for (int i = 0; i < (int)bas.SaveTabGroups.Count - 1; i++)
            {
                int current = bas.SaveTabGroups[i].TabGroupNumerations.Count;
                int next = bas.SaveTabGroups[i + 1].TabGroupNumerations.Count;
                sum += current * current;

                if ((pos > sum) && (pos <= sum + next * next))
                {
                    int pos1 = pos - sum;
                    if (pos1 % next == 0)
                    {
                        TauInt = next;
                        SigmaInt = pos1 / next;
                    }
                    else
                    {
                        SigmaInt = 1 + pos1 / next;
                        TauInt = pos1 % (next);
                    }
                    Sigma = new STTableau(bas.SaveTabGroups[i + 1].TabGroupNumerations[SigmaInt-1], bas.SaveTabGroups[i + 1].TabGroupPartition);
                    Tau = new STTableau(bas.SaveTabGroups[i + 1].TabGroupNumerations[TauInt-1], bas.SaveTabGroups[i + 1].TabGroupPartition);
                    MatrixNum = i + 1;
                    break;
                }
            }

        }
    }
}
