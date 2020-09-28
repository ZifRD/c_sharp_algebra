using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Algebra
{
    [Serializable]
    public class Partition : ComparableList
    {
        public Partition()
            : base()
        {
        }
        public Partition(int n)
            : base(n)
        {
        }
        public Partition(string str):base(str){}
        public Partition(List<int> z)
        {
            //загрузка элементов
            for (int i = 1; i < z.Count-1; i++)
            {
                if (z[i] < z[i + 1] || z[i] <= 0) throw new Exception("PartitionBug");
            }
            if (z[z.Count-1] <= 0) throw new Exception("PartitionBug");
            this.Init(z);

            //определение числа, для которого построено разбиение
            number = 0;
            for (int i = 0; i < PermutationNumbers.Count; i++)
            {
                number += PermutationNumbers[i];
            }
        }
        
        private int number;     //сумма членов = число для разбиения

        public int Number
        {
            get{ return number;  }
        }
    }
}

