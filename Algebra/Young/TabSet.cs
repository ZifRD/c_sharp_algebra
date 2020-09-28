using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
    [Serializable]
    public class TabSet
    {
        private Partition diagPart;
        private List<Permutation> numerations;

        public List<Permutation> TabGroupNumerations
        {
            get { return numerations; }
        }

        public Permutation this[int index]
        {
            get { return this.numerations[index]; }
        }
        public Partition TabGroupPartition
        {
            get { return this.diagPart; }
        }
        
        public TabSet(Partition part)
        {
            diagPart = part;
            numerations = new List<Permutation>();
        }

    }
}
