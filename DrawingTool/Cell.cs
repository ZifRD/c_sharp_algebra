using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace DrawingTool
{
    [Serializable]
    public class Cell : ViewItem
    {
        internal int CellHeight;
        internal int CellWidth;
        internal int CellBorder;
        
        private void CreateRegion()
        {
            Rectangle rect = new Rectangle(column * (Master.BaseCellWidth + CellBorder), row * (Master.BaseCellHeight + CellBorder), CellWidth, CellHeight);
            Region = new Region(rect);
        }

        public Cell(Controller owner, int row, int column,int mul)
            : base(owner, row, column)
        {
            Master = owner;
            CellBorder = owner.BaseCellBorder;
            CellWidth = owner.BaseCellWidth * mul + (CellBorder-1)*mul;
            CellHeight = owner.BaseCellHeight * mul + (CellBorder - 1) * mul;
            this.CreateRegion();
        }

        public Cell(Controller owner,int row, int column)
            : base(owner,row,column)
        {
            Master = owner;
            CellWidth = owner.BaseCellWidth;
            CellHeight = owner.BaseCellHeight;
            CellBorder = owner.BaseCellBorder;
            this.CreateRegion();
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);
            g.FillRegion(Brushes.White, Region);
            Rectangle rect = Rectangle.Round(this.Region.GetBounds(g));
            g.DrawRectangle(Pens.OldLace, rect);
        }
     }
}
