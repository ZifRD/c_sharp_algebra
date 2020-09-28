using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
    [Serializable]
    public class Diagram
    {
        private int width = 0;      //количество клеток в первой строке
        private int height = 0;     //количество клеток в первом столбце
        private int number = 0;     //количество клеток

        protected Partition diagElemRow;
        protected Partition diagElemCol;

        public Partition DiagElemRow
        {
            get { return diagElemRow; }
        }
        
        public Partition DiagElemCol
        {
            get { return diagElemCol; }
        }

        public int Width
        {
            get{ return width;}
        }
        public int Height
        {
            get { return height; }
        }
        public int Number
        {
            get { return number;}
        }
      
        public Diagram(Partition partition)
        {
            for (int i = 0; i < partition.Count; i++)
            {
                number += partition[i];
            }
            width = partition[0];
            height = partition.Count;
            diagElemRow = new Partition(partition.Count);
            
            for (int i = 0; i < (int)partition.Count; i++)
            {
                diagElemRow.Add((int)partition[i]);
            }
            diagElemCol = GetDiagElemCol();
        }
        public Diagram()
        {

        }
        internal int this[int index]
        {
            get { return (int)diagElemRow[index]; }
        }
        private Partition GetDiagElemCol()
        {
                Partition DiagCols = new Partition(10);
                for (int i = 0; i < this.Number; i++)
                {
                    int a = 0;
                    while ((a < Height) && (((int)diagElemRow[a] > i)))
                    { a++; }//получена высота столбца
                    if (a != 0) DiagCols.Add(a);
                }
                return DiagCols;
         }
        internal int Count
        {
            get { return diagElemRow.Count; }
        }

        protected void SetNumber(int p)
        {
            number = p;
        }

        protected void SetWidth(int p)
        {
            width = p;
        }

        protected void SetHeight(int p)
        {
            height = p;
        }



        public static bool operator ==(Diagram a, Diagram b)
        {
            bool result = false;
            if (a.Count == b.Count)
            {
                int i;
                for (i = 0; i < a.Count; i++)
                {
                    if (a[i] != b[i]) break;
                }
                if (i == a.Count) result = true;
            }
            return result;
        }
        public static bool operator !=(Diagram a, Diagram b)
        {
            return (a == b) ? false : true;
        }
        public override bool Equals(object obj)
        {
            return (this == (Diagram)obj);
        }
        public override int GetHashCode()
        {
            return this.diagElemRow.GetHashCode();
        }
    }
}
