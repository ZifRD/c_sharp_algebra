using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DrawingTool
{
    internal class GrossNumber:Cell
    {
        public GrossNumber(Controller owner, int row, int column)
            : base(owner, row, column,2){}

        public override void Draw(Graphics g)
        {
            g.FillRegion(Brushes.White, this.Region);
            Rectangle rect = Rectangle.Round(this.Region.GetBounds(g));
            g.DrawString("0", this.Master.ControllerFont, Brushes.Black, this.Region.GetBounds(g), Master.Format);
        }
    }
}
