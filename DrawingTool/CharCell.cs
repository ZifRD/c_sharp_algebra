using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DrawingTool
{
    public class CharCell: Cell
    {
        public readonly char Value;
        public CharCell(Controller owner, int row, int column,char value)
            : base(owner, row, column)
        {
           this.Value = value;
        }
       
        public override void Draw(Graphics g)
        {
            g.FillRegion(Brushes.LightCyan, this.Region);
            Rectangle rect = Rectangle.Round(this.Region.GetBounds(g));
            g.DrawRectangle(Pens.Brown, rect);
            g.DrawString(this.Value.ToString(), this.Master.ControllerLetterFont, Brushes.Black, this.Region.GetBounds(g), Master.Format);
        }
    }
}
