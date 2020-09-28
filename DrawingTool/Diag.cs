using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algebra;
using System.Drawing;

namespace DrawingTool
{
    [Serializable]
    public class Diag: ViewItem
    {
        private List<CharCell> DiagCells;
        private Diagram Diagram;

        public Diag(Controller owner,int row, int col,Diagram d)
            : base(owner,row,col)
        {
            Diagram = d;
            int row0 = row;
            string text = "";
            for (int i = 0; i < d.DiagElemRow.Count; i++)
                text += d.DiagElemRow[i].ToString();
            char[] chars = text.ToCharArray();
            DiagCells = new List<CharCell>();
            Partition ar = d.DiagElemCol;
            int charIndex = 0;
            for (int g = 0; g < ar.Count; g++)
            {
                for (int h = 0; h < ar[g]; h++)
                {
                    DiagCells.Add(new CharCell(Master,row, col, ' '));
                    row++;
                }
                col++;
                row = row0;
            }
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);
            foreach (CharCell n in DiagCells)
                n.Draw(g);
        }
    }
}
