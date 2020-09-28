using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Algebra
{
    [Serializable]
    public class Primes
    {
        public List<int> Elems;
        public Primes()
        {
            Elems = new List<int>();
        }
        public void CreatePrimes(int start,int stop)
        {
            for (int i = start; i <= stop; i++)
            {
                int limit = (int) Math.Sqrt(i);
                int j;
                for (j = 2; j <= limit; j++)
                {
                    if (i % j == 0) break; 
                }
                if (j == limit+1) Elems.Add(i);
            }
            FileStream myStream = File.Create(start.ToString()+"_"+stop.ToString()+".prm");
            BinaryFormatter myFormatter = new BinaryFormatter();
            myFormatter.Serialize(myStream, this);
            myStream.Close();
         }
    }
}
