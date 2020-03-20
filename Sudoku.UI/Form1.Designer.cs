using System.Windows.Forms;

namespace Sudoku.UI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewSudokuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.QuitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyGirdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteGirdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetCandidatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearHintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SolveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CheckVaildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowCandidatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solveTechniquesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.a1I9ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.r1C1R9C9单元格显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowWelComeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutSoftwareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AssignmentExampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MessageArea = new System.Windows.Forms.RichTextBox();
            this.HintTree = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnApplyHint = new System.Windows.Forms.Button();
            this.BtnGetAllHint = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ctlSudoku = new Sudoku.UI.ctlSudoku();
            this.btnSavetoPicture = new System.Windows.Forms.Button();
            this.EliminationExampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.editMenuItem,
            this.toolMenuItem,
            this.optionMenuItem,
            this.helpMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1671, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewSudokuToolStripMenuItem,
            this.NewToolStripMenuItem,
            this.LoadToolStripMenuItem,
            this.SaveToolStripMenuItem,
            this.QuitToolStripMenuItem,
            this.ResetToolStripMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(44, 21);
            this.fileMenuItem.Text = "文件";
            this.fileMenuItem.Click += new System.EventHandler(this.fileMenuItem_Click);
            // 
            // NewSudokuToolStripMenuItem
            // 
            this.NewSudokuToolStripMenuItem.Name = "NewSudokuToolStripMenuItem";
            this.NewSudokuToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.NewSudokuToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.NewSudokuToolStripMenuItem.Text = "新建数独";
            this.NewSudokuToolStripMenuItem.Click += new System.EventHandler(this.新建数独ToolStripMenuItem_Click);
            // 
            // NewToolStripMenuItem
            // 
            this.NewToolStripMenuItem.Name = "NewToolStripMenuItem";
            this.NewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.NewToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.NewToolStripMenuItem.Text = "生成数独";
            this.NewToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // LoadToolStripMenuItem
            // 
            this.LoadToolStripMenuItem.Name = "LoadToolStripMenuItem";
            this.LoadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.LoadToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.LoadToolStripMenuItem.Text = "加载";
            this.LoadToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItem_Click);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.SaveToolStripMenuItem.Text = "保存";
            // 
            // QuitToolStripMenuItem
            // 
            this.QuitToolStripMenuItem.Name = "QuitToolStripMenuItem";
            this.QuitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.QuitToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.QuitToolStripMenuItem.Text = "退出";
            this.QuitToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // ResetToolStripMenuItem
            // 
            this.ResetToolStripMenuItem.Name = "ResetToolStripMenuItem";
            this.ResetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.ResetToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.ResetToolStripMenuItem.Text = "重新开始游戏";
            this.ResetToolStripMenuItem.Click += new System.EventHandler(this.ResetToolStripMenuItem_Click);
            // 
            // editMenuItem
            // 
            this.editMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyGirdToolStripMenuItem,
            this.PasteGirdToolStripMenuItem,
            this.ClearToolStripMenuItem});
            this.editMenuItem.Name = "editMenuItem";
            this.editMenuItem.Size = new System.Drawing.Size(48, 21);
            this.editMenuItem.Text = " 编辑";
            // 
            // CopyGirdToolStripMenuItem
            // 
            this.CopyGirdToolStripMenuItem.Name = "CopyGirdToolStripMenuItem";
            this.CopyGirdToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.CopyGirdToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.CopyGirdToolStripMenuItem.Text = "拷贝网格";
            this.CopyGirdToolStripMenuItem.Click += new System.EventHandler(this.CopyGirdToolStripMenuItem_Click);
            // 
            // PasteGirdToolStripMenuItem
            // 
            this.PasteGirdToolStripMenuItem.Name = "PasteGirdToolStripMenuItem";
            this.PasteGirdToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.PasteGirdToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.PasteGirdToolStripMenuItem.Text = "粘贴网格";
            this.PasteGirdToolStripMenuItem.Click += new System.EventHandler(this.PasteGirdToolStripMenuItem_Click);
            // 
            // ClearToolStripMenuItem
            // 
            this.ClearToolStripMenuItem.Name = "ClearToolStripMenuItem";
            this.ClearToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.ClearToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.ClearToolStripMenuItem.Text = "清空网格";
            // 
            // toolMenuItem
            // 
            this.toolMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ResetCandidatesToolStripMenuItem,
            this.ClearHintToolStripMenuItem,
            this.SolveToolStripMenuItem,
            this.CheckVaildToolStripMenuItem});
            this.toolMenuItem.Name = "toolMenuItem";
            this.toolMenuItem.Size = new System.Drawing.Size(44, 21);
            this.toolMenuItem.Text = "工具";
            // 
            // ResetCandidatesToolStripMenuItem
            // 
            this.ResetCandidatesToolStripMenuItem.Name = "ResetCandidatesToolStripMenuItem";
            this.ResetCandidatesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.ResetCandidatesToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.ResetCandidatesToolStripMenuItem.Text = "重新计算候选数";
            // 
            // ClearHintToolStripMenuItem
            // 
            this.ClearHintToolStripMenuItem.Name = "ClearHintToolStripMenuItem";
            this.ClearHintToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.ClearHintToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.ClearHintToolStripMenuItem.Text = "清空线索";
            this.ClearHintToolStripMenuItem.Click += new System.EventHandler(this.ClearHintToolStripMenuItem_Click);
            // 
            // SolveToolStripMenuItem
            // 
            this.SolveToolStripMenuItem.Name = "SolveToolStripMenuItem";
            this.SolveToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.SolveToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.SolveToolStripMenuItem.Text = "求解";
            // 
            // CheckVaildToolStripMenuItem
            // 
            this.CheckVaildToolStripMenuItem.Name = "CheckVaildToolStripMenuItem";
            this.CheckVaildToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.CheckVaildToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.CheckVaildToolStripMenuItem.Text = "校验有效性";
            // 
            // optionMenuItem
            // 
            this.optionMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowCandidatesToolStripMenuItem,
            this.solveTechniquesToolStripMenuItem,
            this.a1I9ToolStripMenuItem,
            this.r1C1R9C9单元格显示ToolStripMenuItem});
            this.optionMenuItem.Name = "optionMenuItem";
            this.optionMenuItem.Size = new System.Drawing.Size(44, 21);
            this.optionMenuItem.Text = "选项";
            // 
            // ShowCandidatesToolStripMenuItem
            // 
            this.ShowCandidatesToolStripMenuItem.Name = "ShowCandidatesToolStripMenuItem";
            this.ShowCandidatesToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.ShowCandidatesToolStripMenuItem.Text = "显示候选数";
            this.ShowCandidatesToolStripMenuItem.Click += new System.EventHandler(this.ShowCandidatesToolStripMenuItem_Click);
            // 
            // solveTechniquesToolStripMenuItem
            // 
            this.solveTechniquesToolStripMenuItem.Name = "solveTechniquesToolStripMenuItem";
            this.solveTechniquesToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.solveTechniquesToolStripMenuItem.Text = "解题技巧";
            // 
            // a1I9ToolStripMenuItem
            // 
            this.a1I9ToolStripMenuItem.Name = "a1I9ToolStripMenuItem";
            this.a1I9ToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.a1I9ToolStripMenuItem.Text = "A1-I9 单元格显示";
            this.a1I9ToolStripMenuItem.Click += new System.EventHandler(this.a1I9ToolStripMenuItem_Click);
            // 
            // r1C1R9C9单元格显示ToolStripMenuItem
            // 
            this.r1C1R9C9单元格显示ToolStripMenuItem.Name = "r1C1R9C9单元格显示ToolStripMenuItem";
            this.r1C1R9C9单元格显示ToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.r1C1R9C9单元格显示ToolStripMenuItem.Text = "R1C1-R9C9 单元格显示";
            this.r1C1R9C9单元格显示ToolStripMenuItem.Click += new System.EventHandler(this.r1C1R9C9单元格显示ToolStripMenuItem_Click);
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowWelComeToolStripMenuItem,
            this.aboutSoftwareToolStripMenuItem,
            this.AssignmentExampleToolStripMenuItem,
            this.EliminationExampleToolStripMenuItem});
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(44, 21);
            this.helpMenuItem.Text = "帮助";
            // 
            // ShowWelComeToolStripMenuItem
            // 
            this.ShowWelComeToolStripMenuItem.Name = "ShowWelComeToolStripMenuItem";
            this.ShowWelComeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.ShowWelComeToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.ShowWelComeToolStripMenuItem.Text = "显示欢迎消息";
            this.ShowWelComeToolStripMenuItem.Click += new System.EventHandler(this.ShowWelComeToolStripMenuItem_Click);
            // 
            // aboutSoftwareToolStripMenuItem
            // 
            this.aboutSoftwareToolStripMenuItem.Name = "aboutSoftwareToolStripMenuItem";
            this.aboutSoftwareToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.aboutSoftwareToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.aboutSoftwareToolStripMenuItem.Text = "关于软件";
            this.aboutSoftwareToolStripMenuItem.Click += new System.EventHandler(this.aboutSoftwareToolStripMenuItem_Click);
            // 
            // AssignmentExampleToolStripMenuItem
            // 
            this.AssignmentExampleToolStripMenuItem.Name = "AssignmentExampleToolStripMenuItem";
            this.AssignmentExampleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.AssignmentExampleToolStripMenuItem.Click += new System.EventHandler(this.AssignmentExampleToolStripMenuItem_Click);
            this.AssignmentExampleToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.AssignmentExampleToolStripMenuItem.Text = "出数示例";

            // 
            // MessageArea
            // 
            this.MessageArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessageArea.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MessageArea.Location = new System.Drawing.Point(0, 0);
            this.MessageArea.Name = "MessageArea";
            this.MessageArea.ReadOnly = true;
            this.MessageArea.Size = new System.Drawing.Size(838, 231);
            this.MessageArea.TabIndex = 5;
            this.MessageArea.Text = "";
            this.MessageArea.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.MessageArea_LinkClicked);
            this.MessageArea.TextChanged += new System.EventHandler(this.MessageArea_TextChanged);
            // 
            // HintTree
            // 
            this.HintTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HintTree.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HintTree.Location = new System.Drawing.Point(0, 0);
            this.HintTree.Name = "HintTree";
            this.HintTree.Size = new System.Drawing.Size(838, 500);
            this.HintTree.TabIndex = 6;
            this.HintTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.HintTree_AfterSelect);
            this.HintTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.HintTree_NodeMouseClick);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(822, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 30);
            this.button1.TabIndex = 7;
            this.button1.Text = "校验有效性";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1084, 53);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(125, 30);
            this.button3.TabIndex = 9;
            this.button3.Text = "获取下一个线索";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // btnApplyHint
            // 
            this.btnApplyHint.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnApplyHint.Location = new System.Drawing.Point(1365, 53);
            this.btnApplyHint.Name = "btnApplyHint";
            this.btnApplyHint.Size = new System.Drawing.Size(134, 30);
            this.btnApplyHint.TabIndex = 10;
            this.btnApplyHint.Text = "应用线索";
            this.btnApplyHint.UseVisualStyleBackColor = true;
            this.btnApplyHint.Click += new System.EventHandler(this.btnApplyHint_Click);
            // 
            // BtnGetAllHint
            // 
            this.BtnGetAllHint.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnGetAllHint.Location = new System.Drawing.Point(1215, 53);
            this.BtnGetAllHint.Name = "BtnGetAllHint";
            this.BtnGetAllHint.Size = new System.Drawing.Size(144, 30);
            this.BtnGetAllHint.TabIndex = 11;
            this.BtnGetAllHint.Text = "获取所有线索";
            this.BtnGetAllHint.UseVisualStyleBackColor = true;
            this.BtnGetAllHint.Click += new System.EventHandler(this.BtnGetAllHint_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(1505, 53);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(137, 30);
            this.button6.TabIndex = 12;
            this.button6.Text = "撤销动作";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(822, 98);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.HintTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.MessageArea);
            this.splitContainer1.Size = new System.Drawing.Size(838, 735);
            this.splitContainer1.SplitterDistance = 500;
            this.splitContainer1.TabIndex = 13;
            // 
            // ctlSudoku
            // 
            this.ctlSudoku.BackColor = System.Drawing.Color.White;
            this.ctlSudoku.ImeMode = System.Windows.Forms.ImeMode.On;
            this.ctlSudoku.Location = new System.Drawing.Point(12, 53);
            this.ctlSudoku.Name = "ctlSudoku";
            this.ctlSudoku.Size = new System.Drawing.Size(780, 780);
            this.ctlSudoku.TabIndex = 0;
            // 
            // btnSavetoPicture
            // 
            this.btnSavetoPicture.Location = new System.Drawing.Point(953, 53);
            this.btnSavetoPicture.Name = "btnSavetoPicture";
            this.btnSavetoPicture.Size = new System.Drawing.Size(125, 30);
            this.btnSavetoPicture.TabIndex = 14;
            this.btnSavetoPicture.Text = "保存数独（图片）";
            this.btnSavetoPicture.UseVisualStyleBackColor = true;
            this.btnSavetoPicture.Click += new System.EventHandler(this.btnSavetoPicture_Click);
            // 
            // EliminationExampleToolStripMenuItem
            // 
            this.EliminationExampleToolStripMenuItem.Name = "EliminationExampleToolStripMenuItem";
            this.EliminationExampleToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.EliminationExampleToolStripMenuItem.Text = "删数示例";
            this.EliminationExampleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift |System.Windows.Forms.Keys.H)));
            this.EliminationExampleToolStripMenuItem.Click += new System.EventHandler(this.EliminationExampleToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1671, 839);
            this.Controls.Add(this.btnSavetoPicture);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.BtnGetAllHint);
            this.Controls.Add(this.btnApplyHint);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ctlSudoku);
            this.Controls.Add(this.menuStrip1);
            this.Icon = global::Sudoku.UI.Resource.sudoku;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "超级数独";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctlSudoku ctlSudoku;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.RichTextBox MessageArea;
        private System.Windows.Forms.ToolStripMenuItem NewSudokuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem QuitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyGirdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PasteGirdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ResetCandidatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClearHintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CheckVaildToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowCandidatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem solveTechniquesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem a1I9ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem r1C1R9C9单元格显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowWelComeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutSoftwareToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SolveToolStripMenuItem;
        private System.Windows.Forms.TreeView HintTree;
        private Button button1;
        private Button button3;
        private Button btnApplyHint;
        private Button BtnGetAllHint;
        private Button button6;
        private ToolStripMenuItem ResetToolStripMenuItem;
        private SplitContainer splitContainer1;
        private ToolStripMenuItem AssignmentExampleToolStripMenuItem;

        private Button btnSavetoPicture;
        private ToolStripMenuItem EliminationExampleToolStripMenuItem;
    }
}