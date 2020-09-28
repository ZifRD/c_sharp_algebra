using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algebra;
using System.Drawing;

namespace DrawingTool
{
    internal class CoeffCell:Cell
    {
        public readonly Fraction Value;

        public CoeffCell(Controller owner, int row, int column, Fraction value)
            : base(owner, row, column,2)
        {
           this.Value = value;
        }

        public override void Draw(Graphics g)
        {
            g.FillRegion(Brushes.LightGoldenrodYellow, this.Region);
            Rectangle rect = Rectangle.Round(this.Region.GetBounds(g));
            g.DrawString(this.Value.ToString(), this.Master.ControllerFont, Brushes.Black, this.Region.GetBounds(g), Master.Format);
        }
    }
}
