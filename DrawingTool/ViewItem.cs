using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DrawingTool
{
    [Serializable]
    public abstract class ViewItem
    { 
        protected Region Region;
        internal Controller Master;
        protected int row;
        protected int column;
        
        internal int Row
        {
            get { return row;}
        }

        internal int Column
        {
            get { return column;}
        }

        protected ViewItem(Controller c,int row,int col)
        {
            this.row = row;
            this.column = col;
            Master = c;
        }

        public virtual void Draw(Graphics g)
        {
        }
    }
}
