using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Algebra;

namespace Algebra
{
    [Serializable]  //   .bas files
    public class Basis
    {
        private int number;

        public int Number
        {
            get { return number; }
        }

        [NonSerialized] public int numberToAp = 0;
        public List<Permutation> order;
        public List<TabSet> SaveTabGroups;
        [NonSerialized] public List<int> Ap;  //для Umfpack
        [NonSerialized] public List<int> Ai;  //для Umfpack
        [NonSerialized] public List<int> Ax;  //для Umfpack

        public Basis(){}

        public Basis(int num)
        {
            number = num;
            SaveTabGroups = new List<TabSet>();
            order = new List<Permutation>();
        }

        //Cохранение order & SaveTabGroups
        internal void CreateForUMFmyFormat()
        {
            string name = this.number.ToString();
            FileStream myStream = File.Create(name + ".bas");
            BinaryFormatter myFormatter = new BinaryFormatter();
            myFormatter.Serialize(myStream, this);
            myStream.Close();
        }

        //сохранение матрицы базиса в текстовый файл для UMFPACK
        internal void CreateForUMF()
        {
            //Заполнение массивов для проекта UmfSolver 
            Ap = new List<int>();
            Ai = new List<int>();
            Ax = new List<int>();
            
            //отдельно для первой группы - вертикаль
            Ap.Add(numberToAp);
            for (int k = 0; k < this.order.Count; k++)
            {
                bool sign = order[k].IsEven();
                Ax.Add((sign == false) ? -1 : 1);
                Ai.Add(k);
                numberToAp++;
            }
            
            int columnmatr = 1;
            for (int ii = 1; ii < this.SaveTabGroups.Count - 1; ii++)//для каждой группы
            {
                Permutation perm = new Permutation(this.number);
                for (int i = 0; i < this.number; i++) perm.Add(i + 1);
                FSnCollection YSym = (new STTableau(perm,this.SaveTabGroups[ii].TabGroupPartition)).YoungSymmetrizer();
                //вычислен симметризатор тривиальной таблицы

                for (int gk = 0; gk < SaveTabGroups[ii].TabGroupNumerations.Count; gk++)
                {
                    for (int gg = 0; gg < SaveTabGroups[ii].TabGroupNumerations.Count; gg++)
                    {
                        Ap.Add(numberToAp);
                        Array st = Array.CreateInstance(typeof(int), Common.Factorial(this.number));
                        for (int ghH = 0; ghH < YSym.Count; ghH++)
                        {
                            //выбираем произвольно две стандартные таблицы, записанные подстановками, и перемножаем si*ed*(ta-1)
                            Permutation tt = this.SaveTabGroups[ii].TabGroupNumerations[gk] * YSym[ghH].SnElement *
                                this.SaveTabGroups[ii].TabGroupNumerations[gg].RevPerm();
                            //поиск подстановки в ордере и занеение в большую матрицу
                            Common.Factorial(this.number);
                            for (int i4 = 0; i4 < this.order.Count; i4++)
                            {
                                if (tt == this.order[i4])
                                {
                                    st.SetValue(YSym[ghH].FElement, i4);
                                    numberToAp++;
                                    break;
                                }
                            }
                        }
                        for (int i = 0; i < st.Length; i++)
                        {
                            if ((int)st.GetValue(i) != 0)
                            {
                                Ax.Add((int)st.GetValue(i));
                                Ai.Add(i);
                            }
                        }
                    }
                }

            }
            //отдельно для последней группы - горизонталь
            Ap.Add(numberToAp);
            for (int i = 0; i < Common.Factorial(this.number); i++)
            {
                Ax.Add(1);
                Ai.Add(i);
                numberToAp++;
            }
            Ap.Add(numberToAp);
            FileInfo f = new FileInfo("bas" + this.number.ToString() + ".txt");
            StreamWriter writer = f.CreateText();
            writer.WriteLine("{0},{1},{2}", Ap.Count, Ai.Count, Ax.Count);
            for (int i = 0; i < Ap.Count; i++)
            {
                writer.Write("{0},", Ap[i]);
            }
            writer.WriteLine();
            for (int i = 0; i < Ai.Count; i++)
            {
                writer.Write("{0},", Ai[i]);
            }
            writer.WriteLine();
            for (int i = 0; i < Ax.Count; i++)
            {
                writer.Write("{0}.,", Ax[i]);
            }
            writer.WriteLine();
            writer.Close();
        }
        //Построение (сортированного по нумерациям) списка диаграмма-нумерации SaveTabGroups
        internal void Core()
        {
            order = Common.GeneratePermutations(this.number);

            //оставляем только инволюции
            List<Permutation> Involutions = new List<Permutation>();
            for (int i = 0; i < this.order.Count; i++)
            {
                if (this.order[i].IsInvolution())
                {
                    Involutions.Add(this.order[i]);
                }
            }

            //генерация разбиений
            List<Partition> Partitions = Common.GeneratePartitions(this.number);
            for (int i = 0; i < Partitions.Count; i++)
            {
                SaveTabGroups.Add(new TabSet(Partitions[i]));
            }

            //алгоритм вставки Шенстеда и запись в SaveTabGroups
            for (int i = 0; i < (int)Involutions.Count; i++)
            {
                STTableau tab = new STTableau(Involutions[i]);
                for (int j = 0; j < (int)Partitions.Count; j++)
                  if (tab.DiagElemRow == SaveTabGroups[j].TabGroupPartition)
                      SaveTabGroups[j].TabGroupNumerations.Add(tab.Numeration);
            }

            //сортировка таблиц в SaveTabGroups
            for (int i = 1; i < SaveTabGroups.Count-1; i++)
            {
                int count = SaveTabGroups[i].TabGroupNumerations.Count;
                int[] array = new int[count];
                for (int j = 0; j < count; j++) //для одной группы записываем нумерации строкой цифр
                {
                    string st = "";
                    for (int h = 0; h < number; h++)
                      st += SaveTabGroups[i].TabGroupNumerations[j][h];
                    array[j] = int.Parse(st);
                }
                Common.RadixSorting(array, 10, number);
                for (int j = 0; j < count; j++)
                {
                    SaveTabGroups[i].TabGroupNumerations.RemoveAt(j);
                    string st = (array[j]).ToString();
                    Permutation arrForPerm = new Permutation();
                    for (int p = 0; p < this.number; p++)
                    {
                        arrForPerm.Add(int.Parse(st.Substring(p, 1)));
                    }
                    SaveTabGroups[i].TabGroupNumerations.Insert(j,arrForPerm);
                }
            }
        }
    }
}
