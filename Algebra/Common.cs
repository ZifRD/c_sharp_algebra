using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Algebra
{
    public static class Common
    {
        [DllImport("UmfSolver.dll")]
        public static extern void UmfpackSolve();

        public static void CalculatePrimes(int start, int stop)
        {
            Primes pr = new Primes();
            pr.CreatePrimes(start, stop);
        }

        // обычная генерация перестановок указанной длины
        public static List<Permutation> GeneratePermutations(int capacity)
        {
            Permutation id = CreateIdPermutation(capacity); //тождественная перестановка
            List<Permutation> PermList = new List<Permutation>();
            PermAntilexBase(id, capacity - 1, PermList);
            return PermList;
        }

        //рекурсивная процедура для генерации перестановок
        public static void PermAntilexBase(Permutation permutation, int RecursVar, List<Permutation> PermList)
        {
            int m = RecursVar;
            if (m == 0)
            {
                int number = permutation.Count;
                Permutation temp = new Permutation(number);
                for (int h = 0; h < number; h++)
                {
                    temp.Add(permutation[h]);
                }
                PermList.Add(temp);
            }
            else
            {
                for (int i = 0; i <= m; ++i)
                {
                    PermAntilexBase(permutation, m - 1, PermList);
                    if (i < m)
                    {
                        int b = permutation[i];
                        permutation.PermutationNumbers[i] = permutation[m];
                        permutation.PermutationNumbers[m] = b;
                        permutation.Reverse(m);
                    }
                }
            }
        }


        //Генерация перестановок из элементов списка, где длина перестановки указывается дополнительно 
        //элементы, отсутствующие в списке занимают свои места в соответствии с нометом (тождественно)
        public static List<Permutation> GeneratePermutationsFromList(List<int> list, int capacity)
        {
            Permutation id = CreateIdPermutation(list.Count); //тождественная перестановка = длине списка
            List<Permutation> PermList = new List<Permutation>();
            PermAntilex(id, list, list.Count - 1, capacity, PermList);
            return PermList;
        }

        public static void GeneratePermutationsFromList(List<int> input, List<Permutation> output, int capacity)
        {
            Permutation id = CreateIdPermutation(input.Count); //тождественная перестановка = длине списка
            PermAntilex(id, input, input.Count - 1, capacity, output);
        }


        //Рекурсивная процедура для генерации перестановок в антилексикографическом порядке
        public static void PermAntilex(Permutation permutation, List<int> source, int RecursVar, int totalnumber, List<Permutation> permutationlist)
        {
            int h;
            int m1 = RecursVar;
            int a = source.Count;
            if (m1 == 0)
            {
                Permutation temp = new Permutation(a);
                for (h = 0; h < a; h++)
                {
                    temp.Add(source[permutation[h] - 1]);
                }
                Permutation result = new Permutation(totalnumber);
                List<int> tempsort = new List<int>(temp.Count);
                for (int j = 0; j < temp.Count; j++)
                    tempsort.Add((int)temp[j]);
                tempsort.Sort();
                for (int i = 0; i < totalnumber; i++)
                {
                    if (tempsort.Contains(i + 1)) result.Add(temp[tempsort.IndexOf(i + 1)]);
                    else result.Add(i + 1);
                }
                permutationlist.Add(result);//перестановка получена
            }
            else
            {
                for (h = 0; h <= m1; ++h)
                {
                    PermAntilex(permutation, source, m1 - 1, totalnumber, permutationlist);
                    if (h < m1)
                    {
                        int b = permutation[h];
                        permutation.PermutationNumbers[h] = permutation[m1];
                        permutation.PermutationNumbers[m1] = b;
                        permutation.Reverse(m1);
                    }
                }
            }
        }

        public static STTableau CreateIdSTTableau(Partition part)
        {
            return new STTableau(CreateIdPermutation(part.Number), part);
        }

        public static int Factorial(int number)
        {
            int y = 1;
            for (int i = 1; i <= number; y *= i, i++) ;
            return y;
        }

        public static Permutation CreateIdPermutation(int n)
        {
            Permutation p = new Permutation(n);
            for (int i = 0; i < n; i++)
                p.Add(i + 1);
            return p;
        }

        public static List<Partition> GeneratePartitions(int number)
        {
            List<Partition> result = new List<Partition>();
            int k = 2;
            int sum;
            Array z = Array.CreateInstance(typeof(int), 15);
            Array m = Array.CreateInstance(typeof(int), 15);
            z.SetValue(0, 0);
            m.SetValue(0, 0);
            z.SetValue(number + 1, 1);
            m.SetValue(0, 1);
            z.SetValue(1, 2);
            m.SetValue(number, 2);
            while (k != 1)
            {
                ArrayList big = new ArrayList();
                Partition go = new Partition();
                for (int i = 2; i <= k; i++)
                {
                    for (int h = 0; h < (int)m.GetValue(i); h++)
                        go.Add((int)z.GetValue(i));
                }
                result.Add(go);
                sum = ((int)m.GetValue(k)) * ((int)z.GetValue(k));
                if ((int)m.GetValue(k) == 1)
                {
                    k--;
                    sum += ((int)m.GetValue(k)) * ((int)z.GetValue(k));
                }
                if ((int)z.GetValue(k - 1) == (int)z.GetValue(k) + 1)
                {
                    k--;
                    m.SetValue((int)m.GetValue(k) + 1, k);
                }
                else
                {
                    z.SetValue((int)z.GetValue(k) + 1, k);
                    m.SetValue(1, k);
                }
                if (sum > (int)z.GetValue(k))
                {
                    z.SetValue(1, k + 1);
                    m.SetValue(sum - (int)z.GetValue(k), k + 1);
                    k++;
                }
            }
            return result;
        }
        public static ArrayList GeneratePartitionsForMatrix(int number)
        {
            ArrayList TabGroups = new ArrayList();
            int k = 2;
            int sum;
            Array z = Array.CreateInstance(typeof(int), 15);
            Array m = Array.CreateInstance(typeof(int), 15);
            z.SetValue(0, 0);
            m.SetValue(0, 0);
            z.SetValue(number + 1, 1);
            m.SetValue(0, 1);
            z.SetValue(1, 2);
            m.SetValue(number, 2);
            while (k != 1)
            {
                ArrayList big = new ArrayList();
                ArrayList go = new ArrayList();
                for (int i = 2; i <= k; i++)
                {
                    for (int h = 0; h < (int)m.GetValue(i); h++)
                        go.Add((int)z.GetValue(i));
                }
                big.Add(go);
                TabGroups.Add(big);
                sum = ((int)m.GetValue(k)) * ((int)z.GetValue(k));
                if ((int)m.GetValue(k) == 1)
                {
                    k--;
                    sum += ((int)m.GetValue(k)) * ((int)z.GetValue(k));
                }
                if ((int)z.GetValue(k - 1) == (int)z.GetValue(k) + 1)
                {
                    k--;
                    m.SetValue((int)m.GetValue(k) + 1, k);
                }
                else
                {
                    z.SetValue((int)z.GetValue(k) + 1, k);
                    m.SetValue(1, k);
                }
                if (sum > (int)z.GetValue(k))
                {
                    z.SetValue(1, k + 1);
                    m.SetValue(sum - (int)z.GetValue(k), k + 1);
                    k++;
                }
            }
            return TabGroups;
        }


        public static void RadixSorting(int[] arr, int range, int length)
        {
            ArrayList[] lists = new ArrayList[range];
            for (int i = 0; i < range; ++i)
                lists[i] = new ArrayList();

            for (int step = 0; step < length; ++step)
            {
                //распределение по спискам
                for (int i = 0; i < arr.Length; ++i)
                {
                    int temp = (arr[i] % (int)Math.Pow(range, step + 1)) /
                                                  (int)Math.Pow(range, step);
                    lists[temp].Add(arr[i]);
                }
                //сборка
                int k = 0;
                for (int i = 0; i < range; ++i)
                {
                    for (int j = 0; j < lists[i].Count; ++j)
                    {
                        arr[k++] = (int)lists[i][j];
                    }
                }
                for (int i = 0; i < range; ++i)
                    lists[i].Clear();
            }
        }

        public static void CreateBasisFiles(int num)
        {
            Basis b = new Basis(num);
            b.Core();
            b.CreateForUMFmyFormat();
            b.CreateForUMF();
        }
        private static void FromFSnCollectionToTXTFile(FSnCollection Collection, Basis bhigh)
        {
            Array right = Array.CreateInstance(typeof(int), Factorial(bhigh.Number));
            for (int i = 0; i < Collection.Count; i++)
            {
                for (int i4 = 0; i4 < bhigh.order.Count; i4++)
                {
                    if (Collection[i].SnElement == bhigh.order[i4])
                    {
                        right.SetValue((int)Collection[i].FElement, i4);
                        break;
                    }
                }
            }

            FileInfo f = new FileInfo("right.txt");
            StreamWriter writer = f.CreateText();
            writer.WriteLine(bhigh.Number.ToString() + ",bas" + bhigh.Number.ToString() + ".txt");
            for (int i = 0; i < Factorial(bhigh.Number); i++)
                writer.Write("{0}.,", (int)right.GetValue(i));
            writer.Close();
        }
        private static List<BasElem> FromTXTFileToBasElems(Basis bhigh)
        {
            FileInfo f = new FileInfo("out.txt");
            StreamReader reader = f.OpenText();
            string st = "";
            List<BasElem> bases = new List<BasElem>();
            for (int i = 0; ; i++)
            {
                st = reader.ReadLine();
                if (st != null)
                {
                    int p = st.IndexOf(',');
                    Fraction fr = new Fraction(int.Parse(st.Substring(p + 1)), Factorial(bhigh.Number));
                    int pos = int.Parse(st.Substring(0, p));
                    BasElem baselem = new BasElem(bhigh, pos, fr);
                    bases.Add(baselem);
                }
                else break;
            }
            reader.Close();
            return bases;
        }
        public static List<BasElem> TranslateEdTauToBasElems(STTableau tab)
        {
            int n = tab.Number + 1;
            FileStream v = File.OpenRead(n.ToString() + ".bas");
            BinaryFormatter b = new BinaryFormatter();
            Basis bhigh = (Basis)b.Deserialize(v);
            v.Close();
            return TranslateEdTauToBasElems(tab, bhigh);
        }

        private static List<BasElem> TranslateEdTauToBasElems(STTableau tab, Basis bhigh)
        {
            FSnCollection Sym = tab.YoungSymmetrizer();
            foreach (FSnElement fse in Sym)
            {
                fse.SnElement = fse.SnElement * tab.Numeration.RevPerm();
                fse.SnElement.Add(bhigh.Number);
            }
            FromFSnCollectionToTXTFile(Sym, bhigh);
            UmfpackSolve();
            return FromTXTFileToBasElems(bhigh);
        }

        public static void CreateAndSaveYTF(int number)
        {
            List<YTFClass> result = new List<YTFClass>();
            FileStream v1 = File.OpenRead(number.ToString() + ".bas");
            BinaryFormatter bin1 = new BinaryFormatter();
            Basis b = (Basis)bin1.Deserialize(v1);
            v1.Close();
            FileStream v2 = File.OpenRead((number+1).ToString() + ".bas");
            BinaryFormatter bin2 = new BinaryFormatter();
            Basis bhigh = (Basis)bin2.Deserialize(v2);
            v2.Close();
            foreach(TabSet tabset in b.SaveTabGroups)
            {
                foreach (Permutation perm in tabset.TabGroupNumerations)
                {
                    STTableau tab = new STTableau(perm,tabset.TabGroupPartition);
                    result.Add(new YTFClass(tab,TranslateEdTauToBasElems(tab,bhigh)));
                }
            }
            FileStream myStream = File.Create(number.ToString() + ".ytf");
            BinaryFormatter myFormatter = new BinaryFormatter();
            myFormatter.Serialize(myStream, result);
            myStream.Close();
        }

        public static Basis OpenBasFile(int n)
        {
            FileStream v = File.OpenRead(n.ToString() + ".bas");
            BinaryFormatter b = new BinaryFormatter();
            Basis bhigh = (Basis)b.Deserialize(v);
            v.Close();
            return bhigh;
        }

        public static List<YTFClass> FindTabsOnDiagInYTF(Basis bas, Diagram d)
        {
            FileStream v = File.OpenRead(bas.Number.ToString() + ".ytf");
            BinaryFormatter b = new BinaryFormatter();
            List<YTFClass> ytf = (List<YTFClass>)b.Deserialize(v);
            v.Close();
            List<YTFClass> result = new List<YTFClass>();
            foreach (YTFClass item in ytf)
            {
                if (item.Tab.DiagElemRow == d.DiagElemRow)
                {
                    result.Add(item);
                }
            }
            return result;
        }
        public static List<YTFClass> FindTabInYTF(Basis bas, STTableau t)
        {
            FileStream v = File.OpenRead(bas.Number.ToString() + ".ytf");
            BinaryFormatter b = new BinaryFormatter();
            List<YTFClass> ytf = (List<YTFClass>)b.Deserialize(v);
            v.Close();
            List<YTFClass> result = new List<YTFClass>();
            foreach (YTFClass item in ytf)
            {
                if (item.Tab.Equ(t))
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
