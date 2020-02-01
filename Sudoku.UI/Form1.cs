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
            this.Icon = Sudoku.UI.Resource.sudoku;
            var space = this.sudokuPanel1.SmallSpace * 27;
            this.sudokuPanel1.Size = new System.Drawing.Size(space, space);
            this.HintTree.Nodes.Add(new TreeNode("提示列表"));
            this.ShowInTaskbar = true;
            var c= new QSudoku("002000030481253796703008400000020903006340070030000004100007300300000500009430010");
            c.ApplyCells(new List<CellInfo> {new PositiveCell(5, 4)});
            this.sudokuPanel1.Tag = c;
        }

        private void fileMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowCandidatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sudokuPanel1.ShowCandidates = !this.sudokuPanel1.ShowCandidates;
            this.sudokuPanel1.Refresh();

        }

        private void 生成数独ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QSudoku example = new MinimalPuzzleFactory().Make(new SudokuBuilder().MakeWholeSudoku());
            this.sudokuPanel1.Tag = example;
            this.sudokuPanel1.Refresh();

        }

        private void 新建数独ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QSudoku example = new QSudoku();
            this.sudokuPanel1.Tag = example;
            this.sudokuPanel1.Refresh();
        }

        private void aboutSoftwareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void ShowWelComeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MessageArea_TextChanged(object sender, EventArgs e)
        {

        }

        private void ClearHintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MessageArea.Text = "";
            this.HintTree.Nodes.Clear();

        }


        private void HintTree_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void CopyGirdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.sudokuPanel1.Tag  is QSudoku example)
            {
                var currentString = example.CurrentString;
                Clipboard.SetDataObject(currentString);
            }

        }

        private void PasteGirdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDataObject data = Clipboard.GetDataObject();

            if (data?.GetData(DataFormats.UnicodeText) is string queryString)
            {
                queryString = queryString.Replace("*", "0").Replace(".", "0").Replace("\r\n", "").Trim();
                if (queryString.Length==81)
                {
                
                   QSudoku example = new QSudoku(queryString);
                   this.sudokuPanel1.Tag = example;
                   this.sudokuPanel1.Refresh();
                }
            }
   
        }
    }
}
