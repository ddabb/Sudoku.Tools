﻿namespace Sudoku.UI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewSudokuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.QuitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyGirdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteGirdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重新计算候选数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空线索ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SolveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CheckVaildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowCandidatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.解题技巧ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.a1I9ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.r1C1R9C9单元格显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowWelComeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutSoftwareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.MessageArea = new System.Windows.Forms.TextBox();
            this.sudokuPanel1 = new Sudoku.UI.SudokuPanel();
            this.menuStrip1.SuspendLayout();
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
            this.menuStrip1.Size = new System.Drawing.Size(1446, 25);
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
            this.QuitToolStripMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(44, 21);
            this.fileMenuItem.Text = "文件";
            this.fileMenuItem.Click += new System.EventHandler(this.fileMenuItem_Click);
            // 
            // NewSudokuToolStripMenuItem
            // 
            this.NewSudokuToolStripMenuItem.Name = "NewSudokuToolStripMenuItem";
            this.NewSudokuToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.NewSudokuToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.NewSudokuToolStripMenuItem.Text = "新建数独";
            this.NewSudokuToolStripMenuItem.Click += new System.EventHandler(this.新建数独ToolStripMenuItem_Click);
            // 
            // NewToolStripMenuItem
            // 
            this.NewToolStripMenuItem.Name = "NewToolStripMenuItem";
            this.NewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.NewToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.NewToolStripMenuItem.Text = "生成数独";
            this.NewToolStripMenuItem.Click += new System.EventHandler(this.生成数独ToolStripMenuItem_Click);
            // 
            // LoadToolStripMenuItem
            // 
            this.LoadToolStripMenuItem.Name = "LoadToolStripMenuItem";
            this.LoadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.LoadToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.LoadToolStripMenuItem.Text = "加载";
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.SaveToolStripMenuItem.Text = "保存";
            // 
            // QuitToolStripMenuItem
            // 
            this.QuitToolStripMenuItem.Name = "QuitToolStripMenuItem";
            this.QuitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.QuitToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.QuitToolStripMenuItem.Text = "退出";
            this.QuitToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
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
            // 
            // PasteGirdToolStripMenuItem
            // 
            this.PasteGirdToolStripMenuItem.Name = "PasteGirdToolStripMenuItem";
            this.PasteGirdToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.PasteGirdToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.PasteGirdToolStripMenuItem.Text = "粘贴网格";
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
            this.重新计算候选数ToolStripMenuItem,
            this.清空线索ToolStripMenuItem,
            this.SolveToolStripMenuItem,
            this.CheckVaildToolStripMenuItem});
            this.toolMenuItem.Name = "toolMenuItem";
            this.toolMenuItem.Size = new System.Drawing.Size(44, 21);
            this.toolMenuItem.Text = "工具";
            // 
            // 重新计算候选数ToolStripMenuItem
            // 
            this.重新计算候选数ToolStripMenuItem.Name = "重新计算候选数ToolStripMenuItem";
            this.重新计算候选数ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.重新计算候选数ToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.重新计算候选数ToolStripMenuItem.Text = "重新计算候选数";
            // 
            // 清空线索ToolStripMenuItem
            // 
            this.清空线索ToolStripMenuItem.Name = "清空线索ToolStripMenuItem";
            this.清空线索ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.清空线索ToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.清空线索ToolStripMenuItem.Text = "清空线索";
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
            this.解题技巧ToolStripMenuItem,
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
            // 解题技巧ToolStripMenuItem
            // 
            this.解题技巧ToolStripMenuItem.Name = "解题技巧ToolStripMenuItem";
            this.解题技巧ToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.解题技巧ToolStripMenuItem.Text = "解题技巧";
            // 
            // a1I9ToolStripMenuItem
            // 
            this.a1I9ToolStripMenuItem.Name = "a1I9ToolStripMenuItem";
            this.a1I9ToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.a1I9ToolStripMenuItem.Text = "A1-I9 单元格显示";
            // 
            // r1C1R9C9单元格显示ToolStripMenuItem
            // 
            this.r1C1R9C9单元格显示ToolStripMenuItem.Name = "r1C1R9C9单元格显示ToolStripMenuItem";
            this.r1C1R9C9单元格显示ToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.r1C1R9C9单元格显示ToolStripMenuItem.Text = "R1C1-R9C9 单元格显示";
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowWelComeToolStripMenuItem,
            this.aboutSoftwareToolStripMenuItem});
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(44, 21);
            this.helpMenuItem.Text = "帮助";
            // 
            // ShowWelComeToolStripMenuItem
            // 
            this.ShowWelComeToolStripMenuItem.Name = "ShowWelComeToolStripMenuItem";
            this.ShowWelComeToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ShowWelComeToolStripMenuItem.Text = "显示欢迎消息";
            this.ShowWelComeToolStripMenuItem.Click += new System.EventHandler(this.ShowWelComeToolStripMenuItem_Click);
            // 
            // aboutSoftwareToolStripMenuItem
            // 
            this.aboutSoftwareToolStripMenuItem.Name = "aboutSoftwareToolStripMenuItem";
            this.aboutSoftwareToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.aboutSoftwareToolStripMenuItem.Text = "关于软件";
            this.aboutSoftwareToolStripMenuItem.Click += new System.EventHandler(this.aboutSoftwareToolStripMenuItem_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(891, 137);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(138, 34);
            this.button2.TabIndex = 3;
            this.button2.Text = "获取下一个提示";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(891, 177);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(138, 34);
            this.button3.TabIndex = 4;
            this.button3.Text = "应用提示";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // MessageArea
            // 
            this.MessageArea.Location = new System.Drawing.Point(852, 239);
            this.MessageArea.Multiline = true;
            this.MessageArea.Name = "MessageArea";
            this.MessageArea.Size = new System.Drawing.Size(563, 499);
            this.MessageArea.TabIndex = 5;
            this.MessageArea.TextChanged += new System.EventHandler(this.MessageArea_TextChanged);
            // 
            // sudokuPanel1
            // 
            this.sudokuPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(215)))), ((int)(((byte)(215)))));
            this.sudokuPanel1.Location = new System.Drawing.Point(12, 53);
            this.sudokuPanel1.Name = "sudokuPanel1";
            this.sudokuPanel1.ShowCandidates = true;
            this.sudokuPanel1.Size = new System.Drawing.Size(810, 810);
            this.sudokuPanel1.TabIndex = 0;
            this.sudokuPanel1.Tag = "030150209000360050700490603001273800000519000003684700100000008320040000409001060" +
    "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1446, 887);
            this.Controls.Add(this.MessageArea);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.sudokuPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "超级数独";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SudokuPanel sudokuPanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox MessageArea;
        private System.Windows.Forms.ToolStripMenuItem NewSudokuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem QuitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyGirdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PasteGirdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重新计算候选数ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空线索ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CheckVaildToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowCandidatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 解题技巧ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem a1I9ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem r1C1R9C9单元格显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowWelComeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutSoftwareToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SolveToolStripMenuItem;
    }
}