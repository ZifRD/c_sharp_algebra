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
    public partial class InputForm : Form
    {
        private MainWindow Master;
        public InputForm(MainWindow m)
        {
            InitializeComponent();
            Master = m;
            
        }
        private void InputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Master.HandleTask();
        }
    }
}
