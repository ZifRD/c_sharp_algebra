using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using DrawingTool;
using Algebra;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DrawingTool
{
    public class Controller
    {
        public ViewItemCollection Cells;
        public ViewItemCollection Lines;
        internal int BaseCellWidth = 15;
        internal int BaseCellHeight = 15;
        internal int BaseCellBorder = 2;
        internal int BaseCellControllerColNumber = 50;
        internal int BaseCellControllerRowNumber = 35;
        internal int BaseCellRealRowNumber = 2;
        internal CellControl WorkField;
        internal StringFormat Format;
        internal Font ControllerFont = new Font("Times New Roman", 8);
        internal Font ControllerLetterFont = new Font("Times New Roman", 10);
        private int CurrentR = 1;
        private int CurrentC = 1;
        private int StartR = 1;
        private int StartC = 1;
        private int CurrentDH = 1;
        private int ParagraphDW = 2;

        public void Init()
        {
            Format = new StringFormat();
            Format.Alignment = StringAlignment.Center;
            Format.LineAlignment = StringAlignment.Center;
            Cells = new ViewItemCollection(this, BaseCellControllerColNumber, BaseCellControllerRowNumber);
            Lines = new ViewItemCollection(this, BaseCellControllerColNumber, BaseCellControllerRowNumber);
        }

        public void EnableWorkField(Form f)
        {
            Format = new StringFormat();
            Format.Alignment = StringAlignment.Center;
            Format.LineAlignment = StringAlignment.Center;
            WorkField = new CellControl(this);
            WorkField.Parent = f;
            WorkField.Location = new Point(5, 5);
            WorkField.Focus();
        }
        public Controller(int xCount, int yCount, int BW, int BH, int BB)
        {
            this.BaseCellWidth = BW;
            this.BaseCellHeight = BH;
            this.BaseCellBorder = BB;
            this.BaseCellControllerColNumber = xCount;
            this.BaseCellControllerRowNumber = yCount;
            Init();
        }
        public Controller()
        {
            Init();
            STTableau Tab = new STTableau(new Permutation("1,3,4,2,5,"), new Partition("3,1,1,"));
            List<YTFClass> list = Common.FindTabInYTF(Common.OpenBasFile(5), Tab);
            foreach (YTFClass ytf in list)
            {
                AddChar('e');
                AddTableau(ytf.Tab);
                AddChar('=');
                for (int i = 0; i < ytf.BasElemList.Count; i++)
                {
                    if (i != 0)
                    {
                        AddChar('+');
                    }
                    AddCoeff(ytf.BasElemList[i].Coeff);
                    AddTableau(ytf.BasElemList[i].Sigma);
                    AddChar('x');
                    AddTableau(ytf.BasElemList[i].Tau);
                }
            }
            CurrentC = 1;
            AddDiag(CurrentR, CurrentC, new Diagram(new Partition("3,2,1,")));
            CurrentR += 5;
            AddChar('e');
            BaseCellRealRowNumber = CurrentR + CurrentDH;
        }
       

        public Controller(STTableau Tab, List<BasElem> Elems)
        {
            Init();
            AddChar('e');
            AddTableau(Tab);
            AddChar('=');
            for (int i = 0; i < Elems.Count; i++)
            {
                if (i != 0)
                {
                    AddChar('+');
                }
                AddCoeff(Elems[i].Coeff);
                AddTableau(Elems[i].Sigma);
                AddChar('x');
                AddTableau(Elems[i].Tau);
            }
            BaseCellRealRowNumber = CurrentR + CurrentDH;
        }
        public Controller(List<YTFClass> list)
        {
            Init();
            foreach (YTFClass ytf in list)
            {
                AddChar('e');
                AddTableau(ytf.Tab);
                AddChar('=');
                for (int i = 0; i < ytf.BasElemList.Count; i++)
                {
                    if (i != 0)
                    {
                        AddChar('+');
                    }
                    AddCoeff(ytf.BasElemList[i].Coeff);
                    AddTableau(ytf.BasElemList[i].Sigma);
                    AddChar('x');
                    AddTableau(ytf.BasElemList[i].Tau);
                }
            }
            BaseCellRealRowNumber = CurrentR + CurrentDH;
        }
        public Controller(int number)
        {
            Init();
            FileStream vv = File.OpenRead(number.ToString() + ".ytf");
            BinaryFormatter bbb = new BinaryFormatter();
            List<YTFClass> ytf= (List<YTFClass>)bbb.Deserialize(vv);
            vv.Close();
            for (int a = 0; a < ytf.Count; a++)
            {
                AddChar('e');
                AddTableau(ytf[a].Tab);
                AddChar('=');
                for (int i = 0; i < ytf[a].BasElemList.Count; i++)
                {
                    if (i != 0)
                    {
                        AddChar('+');
                    }
                    AddCoeff(ytf[a].BasElemList[i].Coeff);
                    AddTableau(ytf[a].BasElemList[i].Sigma);
                    AddChar('x');
                    AddTableau(ytf[a].BasElemList[i].Tau);
                }
            }
            BaseCellRealRowNumber = CurrentR + CurrentDH;
        }


        private void AddCell(int row, int col)
        {
            Cells.SetViewItemAtXY(new Cell(this, row, col));
        }
        private void AddTableau(STTableau tab)
        {
            if (CurrentC + tab.Width < BaseCellControllerColNumber)
            {
                Cells.SetViewItemAtXY(new Tableau(this, CurrentR, CurrentC, tab));
                CurrentC += tab.Width;
                if (tab.Height > CurrentDH) CurrentDH = tab.Height+2;
            }
            else
            {
                CurrentR += CurrentDH;
                CurrentC = StartC + ParagraphDW;
                Cells.SetViewItemAtXY(new Tableau(this, CurrentR, CurrentC, tab));
                CurrentC += tab.Width;
                CurrentDH = tab.Height+2;
            }
        }
        private void AddChar(char c)
        {
            if (c == 'e')
            {
                CurrentC = StartC;
                CurrentR += CurrentDH;
                Cells.SetViewItemAtXY(new CharCell(this, CurrentR, CurrentC, c));
                CurrentC++;
                CurrentDH = 0;
                return;
            }
            if (CurrentC < BaseCellControllerColNumber)
            {
                Cells.SetViewItemAtXY(new CharCell(this, CurrentR, CurrentC, c));
                CurrentC++;
            }
            else
            {
                CurrentR += CurrentDH;
                CurrentC = StartC + ParagraphDW;
                Cells.SetViewItemAtXY(new CharCell(this, CurrentR, CurrentC, c));
                CurrentC++;
                CurrentDH = 0;
            }
        }
        private void AddCoeff(Fraction fr)
        {
            if (CurrentC + 1 < BaseCellControllerColNumber)
            {
                Cells.SetViewItemAtXY(new CoeffCell(this, CurrentR, CurrentC, fr));
                CurrentC += 2;
            }
            else
            {
                CurrentR += CurrentDH;
                CurrentC = StartC + ParagraphDW;
                Cells.SetViewItemAtXY(new CoeffCell(this, CurrentR, CurrentC, fr));
                CurrentC += 2;
                CurrentDH = 0;
            }
        }

        private void AddTableau(int row,int col,STTableau tab)
        {
            Cells.SetViewItemAtXY(new Tableau(this, row, col, tab));
        }
        private void AddChar(int row,int col,char c)
        {
            Cells.SetViewItemAtXY(new CharCell(this,row, col, c));
        }
        private void AddCoeff(int row, int col,Fraction fr)
        {
            Cells.SetViewItemAtXY(new CoeffCell(this, row, col, fr));
        }
        private void AddDiag(int row, int col, Diagram d)
        {
            Cells.SetViewItemAtXY(new Diag(this, CurrentR, CurrentC, d));
        }
        private void AddGrossNumber()
        {
            Cells.SetViewItemAtXY(new GrossNumber(this, CurrentR, CurrentC));
        }
        private void AddIndexNumber(int ind)
        {
            Cells.SetViewItemAtXY(new IndexNumber(this, CurrentR, CurrentC,ind));
        }
        public void MakePictures(int number)
        {
            Basis basis = Common.OpenBasFile(number);
            for (int i = 0; i < basis.SaveTabGroups.Count; i++)
            {
                Cells.Clear();
                AddDiag(1, 1, new Diagram(basis.SaveTabGroups[i].TabGroupPartition));
                Bitmap bb = new Bitmap(120, 120);
                Graphics ggg = Graphics.FromImage(bb);
                for (int ii = 0; ii < 6; ii++)
                {
                    for (int jj = 0; jj < 6; jj++)
                    {
                        Cell tempcell = new Cell(this, ii, jj);
                        tempcell.Draw(ggg);
                    }
                }
                foreach (ViewItem item in Cells)
                    item.Draw(ggg);
                string s = number.ToString() + "_" + "Diag" + i.ToString()+".bmp";
                bb.Save(s);
                for (int j = 0; j < basis.SaveTabGroups[i].TabGroupNumerations.Count; j++)
                {
                    Cells.Clear();
                    AddTableau(1, 1, new STTableau(basis.SaveTabGroups[i].TabGroupNumerations[j],basis.SaveTabGroups[i].TabGroupPartition));
                    Bitmap b = new Bitmap(120, 120);
                    Graphics gg = Graphics.FromImage(b);
                    for (int ii = 0; ii < 6; ii++)
                    {
                        for (int jj = 0; jj < 6; jj++)
                        {
                            Cell tempcell = new Cell(this, ii, jj);
                            tempcell.Draw(gg);
                        }
                    }
                    foreach (ViewItem item in Cells)
                        item.Draw(gg);
                    string ss = number.ToString()+"_"+"Tab"+i.ToString()+"_"+j.ToString()+ ".bmp";
                    b.Save(ss);
                }
            }
        }
    }
}
