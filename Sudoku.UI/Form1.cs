using Autofac;
using Sudoku.Core;
using Sudoku.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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

            CenterScreen();

            this.HintTree.Nodes.Add(new TreeNode("提示列表"));
            this.ShowInTaskbar = true;
            this.ctlSudoku.ShowCandidates = true;
            var c = new QSudoku();
#if DEBUG
            c = getQSudoku(typeof(XYWingHandler));


#endif
            //c.RemoveCells(new List<CellInfo> {new NegativeCell(59, 4), new NegativeCell(77, 4) });
            this.ctlSudoku.Sudoku = c;
            this.ctlSudoku.RefreshSudokuPanel();

        }

        private void CenterScreen()
        {

            //指定窗口初始化时的位置（计算机屏幕中间）
            this.StartPosition = FormStartPosition.CenterScreen;


            //指定窗口初始化时的位置,如果为Manual，位置由Location决定(计算机屏幕中间，如果不/2，则计算机右下角)
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
        }

        private static QSudoku getQSudoku(Type type)
        {
            object[] objs = type.GetCustomAttributes(typeof(AssignmentExampleAttribute), true);
            if (objs.Count() != 1) return new QSudoku();
            if (!(objs[0] is AssignmentExampleAttribute a)) return new QSudoku();
            var queryString = a.queryString;
            var value = a.value;
            var positionString = a.positionString;
            var handers = a.SolveHandlers;
            return getQSudoku(type, queryString, value, positionString, handers);
        }

        private static QSudoku getQSudoku(Type type, string queryString, int value, string positionString, SolveMethodEnum[] handlerEnums = null)
        {

            var qsudoku = new QSudoku(queryString);
            if (handlerEnums != null)
            {
                foreach (var handerEnum in handlerEnums)
                {
                    var eliminationHanders = FrmG.SolveHandlers.First(c => handerEnum == (c.methodType));
                    var removeCells = eliminationHanders.Elimination(qsudoku);
                    qsudoku.RemoveCells(removeCells);
                }
            }
            return qsudoku;
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
            this.ctlSudoku.ShowCandidates = !this.ctlSudoku.ShowCandidates;
            this.ctlSudoku.Refresh();

        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitUI();
            this.MessageArea.Text = "数独正在生成中，请稍候……" + DateTime.Now + "\r\n";
            this.ctlSudoku.Sudoku = new QSudoku();
            this.ctlSudoku.RefreshSudokuPanel();

            QSudoku example = new MinimalPuzzleFactory().Make(new SudokuBuilder().MakeWholeSudoku());
            this.ctlSudoku.Sudoku = example;
            this.ctlSudoku.RefreshSudokuPanel();
            this.MessageArea.Text = "数独生成完成" + DateTime.Now + "\r\n";

        }

        private void 新建数独ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QSudoku example = new QSudoku();
            this.ctlSudoku.Sudoku = example;
            this.ctlSudoku.Focus();
            this.ctlSudoku.RefreshSudokuPanel();
            InitUI();


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
            Clipboard.Clear();
            Clipboard.SetDataObject(this.ctlSudoku.Sudoku.CurrentString);


        }

        private void PasteGirdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDataObject data = Clipboard.GetDataObject();

            if (data?.GetData(DataFormats.UnicodeText) is string queryString)
            {
                queryString = queryString.Replace("*", "0").Replace(".", "0").Replace("\r\n", "").Trim();
                if (queryString.Length == 81)
                {
                    if (this.ctlSudoku.Sudoku is QSudoku formSudoku)
                    {
                        if (formSudoku.CurrentString != queryString)
                        {
                            QSudoku example = new QSudoku(queryString);
                            this.ctlSudoku.Sudoku = example;
                            this.ctlSudoku.RefreshSudokuPanel();
                            InitUI();
                        }
                    }

                }
            }

        }

        private void BtnGetAllHint_Click(object sender, EventArgs e)
        {
            this.MessageArea.Text = "线索开始加载,请稍候..." + DateTime.Now + "\r\n";
            var sudoku = this.ctlSudoku.Sudoku;
            var queryString = this.ctlSudoku.Sudoku.CurrentString;
            if (new DanceLink().isValid(queryString))
            {
                this.HintTree.BeginUpdate();

                this.HintTree.Nodes.Clear();
                var listNode = new TreeNode("提示列表");
                this.HintTree.Nodes.Add(listNode);
                var builder = new ContainerBuilder();
                Assembly[] assemblies = new Assembly[] { typeof(SolverHandlerBase).Assembly };
                builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces();
                var initString = sudoku.CurrentString;
                IContainer container = builder.Build();
                var solveHandlers = container.Resolve<IEnumerable<ISudokuSolveHandler>>()
                    .OrderBy(c => (int)c.methodType).ToList();
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
                                hintNode.Text = G.GetEnumDescription(handler.methodType) + ":" + cell.Desc;
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
                    listNode.Nodes.Add(rules);

                }

                if (techniques.Nodes.Count > 0)
                {
                    listNode.Nodes.Add(techniques);
                }

                this.HintTree.ExpandAll();
                this.HintTree.EndUpdate();

            }
            else
            {
                this.HintTree.BeginUpdate();
                this.HintTree.Nodes.Clear();

                this.HintTree.Nodes.Add(new TreeNode("该数独不存在解或者存在多个解。"));
                this.HintTree.ExpandAll();
                this.HintTree.EndUpdate();
            }
            this.MessageArea.Text = "线索加载完成..." + DateTime.Now;
        }

        /// <summary>
        /// 重置提示数和消息区域
        /// </summary>
        private void InitUI()
        {
            this.HintTree.BeginUpdate();
            this.HintTree.Nodes.Clear();
            this.HintTree.Nodes.Add(new TreeNode("提示列表"));
            this.HintTree.EndUpdate();
            this.MessageArea.Text = "";
            this.ctlSudoku.DrawingCell = null;
        }
 

        private void HintTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is CellInfo cell)
            {
                var solveMessage = cell.SolveMessages;
                if (solveMessage.Count != 0)
                {
                    SetRichTextBoxValue(solveMessage);

                }
                else
                {
                    this.MessageArea.Text = "" + cell.SolveDesc;
                }
                this.ctlSudoku.DrawingCell= cell;
                this.ctlSudoku.RefreshSudokuPanel();

            }
            else if (e.Node.Tag is ISudokuSolveHandler hander)
            {
                this.MessageArea.Text = "" + hander.GetDesc();
            }

        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private Dictionary<int, int> keyCodeNumMap = new Dictionary<int, int>
        {
            {48, 0},
            {49, 1},
            {50, 2},
            {51, 3},
            {52, 4},
            {53, 5},
            {54, 6},
            {55, 7},
            {56, 8},
            {57, 9},
            {96, 0},
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

        public void SetRichTextBoxValue(List<SolveMessage> messages)
        {
            this.MessageArea.Text = messages.Select(c => c.content).JoinString("");
            foreach (var message in messages)
            {
                var font = GetMessageFont(message.messageType);
                var color = GetMessageColor(message.messageType);
                changeStrColorFont(this.MessageArea, message.content, color, font);
            }
        }

        public Font GetMessageFont(MessageType type)
        {
            switch (type)
            {
                case MessageType.Normal:
                    return new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                case MessageType.Postive:
                case MessageType.Nagetive:
                    return new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                case MessageType.Result:
                    return new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                case MessageType.Important:
                    return new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                case MessageType.Reason:
                    return new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                case MessageType.Location:
                    return new System.Drawing.Font("宋体", 16.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                default:
                    break;
            }
            return new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        }

        public Color GetMessageColor(MessageType type)
        {
            switch (type)
            {
                case MessageType.Normal:
                    return Color.Black;
                case MessageType.Postive:
                    return Color.Green;
                case MessageType.Nagetive:
                    return Color.Red;
                case MessageType.Result:
                    return Color.Gray;
                case MessageType.Important:
                    return Color.Green;
                case MessageType.Reason:
                    return Color.DarkGray;
                case MessageType.Location:
                    return Color.SaddleBrown;
                default:
                    break;
            }
            return Color.Black;
        }

        public static void changeStrColorFont(RichTextBox rtBox, string str, Color color, Font font)
        {
            int pos = 0;
            while (true)
            {
                pos = rtBox.Find(str, pos, RichTextBoxFinds.None);
                if (pos == -1)
                    break;
                rtBox.SelectionStart = pos;
                rtBox.SelectionLength = str.Length;
                rtBox.SelectionColor = color;
                rtBox.SelectionFont = font;
                pos = pos + 1;
            }
        }




        //public ArrayList getIndexArray(String inputStr, String findStr)
        //{
        //    ArrayList list = new ArrayList();
        //    int start = 0;
        //    while (start < inputStr.Length)
        //    {
        //        int index = inputStr.IndexOf(findStr, start);
        //        if (index >= 0)
        //        {
        //            list.Add(index);
        //            start = index + findStr.Length;
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }
        //    return list;
        //}

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var intkey = (int)(keyData);
            Debug.WriteLine("ProcessCmdKey" + "intkey" + intkey);

            var sudoku = ctlSudoku.Sudoku;
            var deal = false;
            switch (intkey)
            {
                case 48://0
                case 49://1
                case 50://2
                case 51://3
                case 52://4
                case 53://5
                case 54://6
                case 55://7
                case 56://8
                case 57://9
                case 96: //0
                case 97: //1
                case 98: //2
                case 99: //3
                case 100://4
                case 101://5
                case 102://6
                case 103://7
                case 104://8
                case 105://9
                    if (sudoku.CurrentCell != null && (sudoku.CurrentCell.CellType != CellType.Init || sudoku.CurrentCell.Value == 0))
                    {
                        var value = keyCodeNumMap[intkey];
                        sudoku.ApplyCell(value == 0 ? (CellInfo)new InitCell(sudoku.CurrentCell.Index, 0) : new PositiveCell(sudoku.CurrentCell.Index, value));

                    }

                    deal = true;
                    break;
                case (int)Keys.Left:
                    sudoku.MoveCurrentCellToLeft();
                    deal = true;
                    break;
                case (int)Keys.Up:
                    sudoku.MoveCurrentCellToUp();
                    deal = true;
                    break;
                case (int)Keys.Right:
                    sudoku.MoveCurrentCellToRight();
                    deal = true;
                    break;
                case (int)Keys.Down:
                    sudoku.MoveCurrentCellToDown();
                    deal = true;
                    break; ;

            }

            if (deal)
            {
                ctlSudoku.RefreshSudokuPanel();
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            MessageArea.Text = "数独的表达式为：" + "" + ctlSudoku.Sudoku.CurrentString;
            MessageArea.Text += new DanceLink().isValid(ctlSudoku.Sudoku.CurrentString) ? "这是一个有效的数独" : "这是一个无效的数独";
        }

        private void ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sudoku = ctlSudoku.Sudoku;
            ctlSudoku.Sudoku = new QSudoku(sudoku.QueryString);
            ctlSudoku.RefreshSudokuPanel();
            InitUI();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void a1I9ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            G.LocationType = LocationType.A1I9;
            ctlSudoku.RetSetRowAndColumnFormat();
        }

        private void r1C1R9C9单元格显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            G.LocationType = LocationType.R1C1;
            ctlSudoku.RetSetRowAndColumnFormat();
        }

        private void 技巧示例ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmExample().ShowDialog();
        }

        public Stack<CellInfo> stacks;
        private void btnApplyHint_Click(object sender, EventArgs e)
        {
            var currentCell = this.ctlSudoku.DrawingCell;
            if (currentCell!=null)
            {
                if (currentCell.CellType==CellType.Positive)
                {

                    this.ctlSudoku.Sudoku.ApplyCell(currentCell);
                }
                if (currentCell.CellType == CellType.Negative)
                {
                    this.ctlSudoku.Sudoku.RemoveCell(currentCell);
                }
                InitUI();
            }
        
        }
    }
}
