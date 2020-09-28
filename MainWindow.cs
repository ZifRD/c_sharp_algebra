using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Algebra;
using DrawingTool;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;


            
            //TranslateEdTauToDrawnTableaux();
        }
      
        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Document doc = new Document();
            doc.MdiParent = this;
            doc.Show();
        }

        private void однаКлеткаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Document doc = new Document();
            doc.MdiParent = this;
            doc.Show();
        }

        private void pRMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Common.CalculatePrimes(2, 120);
            Common.CalculatePrimes(121, 720);
            Common.CalculatePrimes(721, 5040);
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 2; i < 7; i++) Common.CreateBasisFiles(i);
        }

        private void yTFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 3; i < 6; i++)
            {
                Common.CreateAndSaveYTF(i);
                MessageBox.Show(i.ToString() + " Success");
            }
        }

        private void задачаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputForm inf = new InputForm(this);
            inf.ShowDialog();
            
        }

        private void картинкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Controller ctrl = new Controller();
            for (int i = 2; i < 7; i++)
            {
                ctrl.MakePictures(i);
            }
        }

        public void HandleTask()
        {
            FileInfo f = new FileInfo("task.txt");
            StreamReader reader = f.OpenText();
            int basisNum = int.Parse(reader.ReadLine());
            int diagNum = int.Parse(reader.ReadLine());
            int tabNum = int.Parse(reader.ReadLine());
            reader.Close();
            if (diagNum == -1)
            {
                Document doc = new Document(basisNum);
                doc.MdiParent = this;
                doc.Show();
            }
            else
            {
                Basis bas = Common.OpenBasFile(basisNum);
                if (tabNum == -1)
                {
                    Diagram d = new Diagram(bas.SaveTabGroups[diagNum].TabGroupPartition);
                    Document doc = new Document(Common.FindTabsOnDiagInYTF(bas,d));
                    doc.MdiParent = this;
                    doc.Show();
                }
                else
                {
                    STTableau STab = new STTableau(bas.SaveTabGroups[diagNum].TabGroupNumerations[tabNum],bas.SaveTabGroups[diagNum].TabGroupPartition);
                    Document doc = new Document(Common.FindTabInYTF(bas,STab));
                    doc.MdiParent = this;
                    doc.Show();
                }
            }
        }

        #region Printing 
        private void PrintAsBitmap()
        {

        }
        #endregion
    }
}
