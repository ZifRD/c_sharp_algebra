using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrawingTool
{
    public partial class CellControl : UserControl
    {
        internal Controller Master;

        public CellControl(Controller c)
        {
            InitializeComponent();
            Master = c;
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.Selectable, true);
            int BitmapHeight = Master.BaseCellHeight * Master.BaseCellRealRowNumber + Master.BaseCellBorder * (Master.BaseCellRealRowNumber-1);
            int BitmapWidth = Master.BaseCellWidth * Master.BaseCellControllerColNumber + Master.BaseCellBorder * (Master.BaseCellControllerColNumber);
            if (Master.BaseCellControllerRowNumber < Master.BaseCellRealRowNumber)
            {
                this.Height = 40 + Master.BaseCellHeight * Master.BaseCellControllerRowNumber + Master.BaseCellBorder * (Master.BaseCellControllerRowNumber); ;
            }
            else this.Height = BitmapHeight + 40;
            this.Width = BitmapWidth+40;
            Bitmap b = new Bitmap(BitmapWidth,BitmapHeight);
            pictureBox1.BackColor = Color.White;
            Graphics gg = Graphics.FromImage(b);
            for (int i = 0; i < Master.BaseCellRealRowNumber; i++)
            {
                for (int j = 0; j < Master.BaseCellControllerColNumber; j++)
                {
                    Cell tempcell = new Cell(Master, i, j);
                    tempcell.Draw(gg);
                }
            }
            foreach (ViewItem item in this.Master.Cells)
                item.Draw(gg);
            foreach (ViewItem item in this.Master.Lines)
                item.Draw(gg);
            pictureBox1.Image = (Image)b;
        }
    }
}
