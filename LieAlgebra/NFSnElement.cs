using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LieAlgebra
{
    public class NFSnElement
    {
        private int _FElement;
        private NPermutation _SnElement;

        public NPermutation SnElement
        {
            get {return _SnElement;}
            set {this._SnElement = value;}
        }
       public int FElement
        {
            get { return _FElement; }
            set { this._FElement = value; }
        }

        public NFSnElement(){}

        public NFSnElement(int num, NPermutation per)
        {
            this._FElement = num;
            this._SnElement = per;
        }
    }
}
