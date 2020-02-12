﻿using Autofac;
using Sudoku.Core;
using Sudoku.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using IContainer = Autofac.IContainer;

namespace Sudoku.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Icon = Sudoku.UI.Resource.sudoku;

            this.HintTree.Nodes.Add(new TreeNode("提示列表"));
            this.ShowInTaskbar = true;
            this.sudokuPanel1.ShowCandidates = true;
            var c = new QSudoku("020137568050468192618592734060070809100080406080040203040710385800054921501820647");
            //c.RemoveCells(new List<CellInfo> {new NegativeCell(59, 4), new NegativeCell(77, 4) });
            this.sudokuPanel1.Sudoku = c;
            this.sudokuPanel1.Focus();

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

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QSudoku example = new MinimalPuzzleFactory().Make(new SudokuBuilder().MakeWholeSudoku());
            this.sudokuPanel1.Sudoku = example;


        }

        private void 新建数独ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QSudoku example = new QSudoku();
            this.sudokuPanel1.Sudoku = example;
            this.sudokuPanel1.Focus();



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
            if (this.sudokuPanel1.Tag is QSudoku example)
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
                if (queryString.Length == 81)
                {
                    if (this.sudokuPanel1.Tag is QSudoku formSudoku)
                    {
                        if (formSudoku.CurrentString != queryString)
                        {
                            QSudoku example = new QSudoku(queryString);
                            this.sudokuPanel1.Sudoku = example;

                        }
                    }

                }
            }

        }

        private void BtnGetAllHint_Click(object sender, EventArgs e)
        {
            var sudoku = this.sudokuPanel1.Sudoku;
            var queryString = this.sudokuPanel1.Sudoku.CurrentString;
            if (new DanceLink().isValid(queryString))
            {
                this.HintTree.BeginUpdate();

                this.HintTree.Nodes.Clear();
                this.HintTree.Nodes.Add(new TreeNode("提示列表"));
                var builder = new ContainerBuilder();
                Assembly[] assemblies = new Assembly[] {typeof(SolverHandlerBase).Assembly};
                builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces();
                var initString = sudoku.CurrentString;
                IContainer container = builder.Build();
                var solveHandlers = container.Resolve<IEnumerable<ISudokuSolveHandler>>()
                    .OrderBy(c => (int) c.methodType).ToList();
                TreeNode rules = new TreeNode();
                rules.Text = "数独规则";
                TreeNode techniques = new TreeNode();
                techniques.Text = "数独技巧";
                for (int i = 0; i < solveHandlers.Count; i++)
                {
                    var handler = solveHandlers[i];

                    try
                    {
                        var cellinfos = new List<CellInfo>();

                        cellinfos = handler.Assignment(sudoku);
                        if (cellinfos.Count != 0)
                        {
                            TreeNode subTreeNode = new TreeNode();
                            subTreeNode.Text = G.GetEnumDescription(handler.methodType);
                            subTreeNode.Tag = handler;

                            foreach (var cell in cellinfos)
                            {
                                TreeNode hintNode = new TreeNode();
                                hintNode.Tag = cell;
                                hintNode.Text = "" + cell;
                                subTreeNode.Nodes.Add(hintNode);
                            }

                            if (handler.methodClassify == MethodClassify.SudokuRules)
                            {
                                rules.Nodes.Add(subTreeNode);
                            }
                            else
                            {
                                techniques.Nodes.Add(subTreeNode);
                            }
                        }

                    }
                    catch (Exception ex)
                    {

                    }

                }

                if (rules.Nodes.Count > 0)
                {
                    this.HintTree.Nodes.Add(rules);

                }

                if (techniques.Nodes.Count > 0)
                {
                    this.HintTree.Nodes.Add(techniques);
                }

                this.HintTree.ExpandAll();
                this.HintTree.EndUpdate();
            }
            else
            {
                this.HintTree.BeginUpdate();
                this.HintTree.Nodes.Clear();
                this.HintTree.Nodes.Add(new DanceLink().solution_count(queryString) == 0
                    ? new TreeNode("该数独无解。")
                    : new TreeNode("该数独存在多解。"));
                this.HintTree.ExpandAll();
                this.HintTree.EndUpdate();
            }
        }

        private void HintTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is CellInfo cell)
            {
                this.MessageArea.Text = "" + cell.SolveDesc;
            }
            else if (e.Node.Tag is ISudokuSolveHandler hander)
            {
                this.MessageArea.Text = "" + hander;
            }

        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private Dictionary<int, int> keyCodeNumMap = new Dictionary<int, int>
        {
            {49, 1},
            {50, 2},
            {51, 3},
            {52, 4},
            {53, 5},
            {54, 6},
            {55, 7},
            {56, 8},
            {57, 9},
            {97, 1},
            {98, 2},
            {99, 3},
            {100, 4},
            {101, 5},
            {102, 6},
            {103, 7},
            {104, 8},
            {105, 9},
        };


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var intkey = (int)(keyData);
            Debug.WriteLine("ProcessCmdKey"+ "intkey" + intkey);

            var sudoku = sudokuPanel1.Sudoku;
            var deal = false;
            switch (intkey)
            {
                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                case 97:
                case 98:
                case 99:
                case 100:
                case 101:
                case 102:
                case 103:
                    if (sudoku.CurrentCell.CellType != CellType.Init)
                    {
                        sudoku.ApplyCell(new PositiveCell(sudoku.CurrentCell.Index, keyCodeNumMap[intkey]));
                    }

                    deal = true;
                    break;
                case (int) Keys.Left:
                    sudoku.MoveCurrentCellToLeft();
                    deal = true;
                    break;
                case (int) Keys.Up:
                    sudoku.MoveCurrentCellToUp();
                    deal = true;
                    break;
                case (int) Keys.Right:
                    sudoku.MoveCurrentCellToRight();
                    deal = true;
                    break;
                case (int) Keys.Down:
                    sudoku.MoveCurrentCellToDown();
                    deal = true;
                    break;;

            }

            if (deal)
            {
                sudokuPanel1.RefreshPanel();
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

          
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("Form1_KeyUp");

        }           
    }
}
