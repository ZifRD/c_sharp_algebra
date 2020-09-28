using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DrawingTool
{
    internal class IndexNumber:Cell
    {
        public readonly int Value;

        public IndexNumber(Controller owner, int row, int column, int value)
            : base(owner, row, column,2)
        {
           this.Value = value;
        }

        public override void Draw(Graphics g)
        {
            g.FillRegion(Brushes.Lavender, this.Region);
            Rectangle rect = Rectangle.Round(this.Region.GetBounds(g));
            g.DrawString(this.Value.ToString(), this.Master.ControllerFont, Brushes.Black, this.Region.GetBounds(g), Master.Format);
        }
    }
}
