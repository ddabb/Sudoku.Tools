using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.sudokuPanel1.Tag = new QSudoku("030150209000360050700490603001273800000519000003684700100000008320040000409001060");
        }
    }
}
