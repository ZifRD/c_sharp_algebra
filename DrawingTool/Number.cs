using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DrawingTool;
using Algebra;
using System.Drawing;

namespace DrawingTool
{
    internal class Number : Cell
    {
        public readonly STTableau Tab;
        public readonly int Index;
        public readonly string Value;

        public Number(Controller owner, int row, int column, STTableau T, string value, int index)
            : base(owner, row, column)
        {
            this.Index = index;
            this.Tab = T;
            this.Value = value;
        }

        public override void Draw(Graphics g)
        {
            g.FillRegion(Brushes.WhiteSmoke, this.Region);
            Rectangle rect = Rectangle.Round(this.Region.GetBounds(g));
            g.DrawRectangle(Pens.LightSlateGray, rect);
            g.DrawString(this.Value, this.Master.ControllerFont, Brushes.Black, this.Region.GetBounds(g), Master.Format);

        }
    }
}
