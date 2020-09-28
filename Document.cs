using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DrawingTool;
using Algebra;

namespace WindowsFormsApplication1
{
    public partial class Document : Form
    {
        private Controller Worker;

        public Document()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            Worker = new Controller();
            Worker.EnableWorkField(this);
        }
        public Document(STTableau tab,List<BasElem> list)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            Worker = new Controller(tab,list);
            Worker.EnableWorkField(this);
        }
        public Document(List<YTFClass> list)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            Worker = new Controller(list);
            Worker.EnableWorkField(this);
        }
        public Document(int n)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            Worker = new Controller(n);
            Worker.EnableWorkField(this);
        }
    }
}
