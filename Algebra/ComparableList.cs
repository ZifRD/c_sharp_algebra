using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Algebra
{
    [Serializable]
    public class ComparableList
    {
        private List<int> permNumbers;

        public List<int> PermutationNumbers
        {
            get { return permNumbers; }
        }
        public int Count
        {
            get { return permNumbers.Count; }
        }
        

       public ComparableList()
        {
            permNumbers = new List<int>();
        }
       public ComparableList(int number)
        {
            permNumbers = new List<int>(number);
        }
        public ComparableList(List<int> z)
        {
            Init(z);
        }

        public ComparableList(string str)
        {
            List<int> z = new List<int>();
            int stop = -1;
            int offset = 0; 
            do
            {
                stop = str.IndexOf(',',offset);
                if (stop != -1 && offset < str.Length)
                {
                    z.Add(int.Parse(str.Substring(offset,stop-offset)));
                    offset = stop+1;
                }
                else break;
            }
            while (true);
            Init(z);
        }
        protected void Init(List<int> z)  //нужен для Partition
        {
            permNumbers = new List<int>(z.Count);
            for(int i = 0; i < (int)z.Count;i++)
            {
                permNumbers.Add(z[i]);
            }
        }
        public static bool operator >(ComparableList a, ComparableList b)
        {
            bool bigger = false;
            if (a.Count == b.Count)
            {
                int i;
                for (i = 0; i < a.Count; i++)
                {
                    if (a[i] <= b[i])
                    {
                        bigger = false;
                        break;
                    }
                }
                if (i == a.Count) bigger = true;
            }
            else
            {
                bigger = false;
            }
            return bigger;
        }
        public static bool operator <(ComparableList a, ComparableList b)
        {
            bool bigger = false;
            if (a.Count == b.Count)
            {
                int i;
                for (i = 0; i < a.Count; i++)
                {
                    if (a[i] >= b[i])
                    {
                        bigger = false;
                        break;
                    }
                }
                if (i == a.Count) bigger = true;
            }
            else
            {
                bigger = false;
            }
            return bigger;
        }
        public static bool operator ==(ComparableList a, ComparableList b)
        {
            bool result = false;
            if (a.Count == b.Count)
            {
                int i;
                for ( i = 0; i < a.Count; i++)
                {
                    if (a[i] != b[i]) break;
                }
                if (i == a.Count) result = true;
            }
            return result;
        }
        public static bool operator !=(ComparableList a, ComparableList b)
        {
            return (a == b) ? false : true;
        }
        
               
        public int this[int index]
        {
            get { return (int)permNumbers[index]; }
        }

        internal void Add(int c)
        {
            permNumbers.Add(c);
        }
        internal void Reverse(int a)
        {
            permNumbers.Reverse(0, a);
        }
        public override bool Equals(object obj)
        {
            return (this == (ComparableList)obj);
        }
        public override int GetHashCode()
        {
            return this.permNumbers.GetHashCode();
        }
        public override string ToString()
        {
            string result = "(";
            for (int i = 0; i < permNumbers.Count; i++)
            {
                result += permNumbers[i].ToString();
                if (i < permNumbers.Count - 1) result += ", ";
            }
            result += ")";
            return result;
        }
    }
}
