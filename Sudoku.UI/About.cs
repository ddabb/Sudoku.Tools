using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku.UI
{
    public partial class AboutForm : Form
    {
    

        public AboutForm()
        {
            InitializeComponent();
            this.Icon = Sudoku.UI.Resource.sudoku;
            this.versionText.Text= VersionString;

            this.authorText.Text = authorString;
        }

        public string VersionString
        {
            get { return "1.0.2.2"; }
        }
        public string authorString
        {
            get { return "Stone Liu"; }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (sender is LinkLabel label)
            {
                Process.Start(label.Text);
            }
        
        }
    }
}
