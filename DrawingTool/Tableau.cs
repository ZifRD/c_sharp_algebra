using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algebra;
using System.Drawing;


namespace DrawingTool
{
    public class Tableau: ViewItem
    {
        private List<Number> TableauCells;
        private STTableau Tab;
        
        public Tableau(Controller owner,int row, int col,STTableau t)
            : base(owner,row,col)
        {
            Tab = t;
            int row0 = row;
            string text = "";
            for (int i = 0; i < t.Numeration.Count; i++)
                text += t.Numeration[i].ToString();
            char[] chars = text.ToCharArray();
            TableauCells = new List<Number>();
            Partition ar = Tab.DiagElemCol;
            int charIndex = 0;
            for (int g = 0; g < ar.Count; g++)
            {
                for (int h = 0; h < ar[g]; h++)
                {
                    TableauCells.Add(new Number(Master,row, col, Tab,chars[charIndex].ToString(), charIndex++));
                    row++;
                }
                col++;
                row = row0;
            }
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);
            foreach (Number n in TableauCells)
                n.Draw(g);
        }

    }
}
