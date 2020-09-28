using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
    public class FSnElement
    {
        private int _FElement;
        private Permutation _SnElement;

        public Permutation SnElement
        {
            get {return _SnElement;}
            set {this._SnElement = value;}
        }
       public int FElement
        {
            get { return _FElement; }
            set { this._FElement = value; }
        }

        public FSnElement(){}

        public FSnElement(int num, Permutation per)
        {
            this._FElement = num;
            this._SnElement = per;
        }
    }
}
