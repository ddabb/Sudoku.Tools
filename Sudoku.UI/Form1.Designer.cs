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
            this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.sudokuPanel1 = new Sudoku.UI.SudokuPanel();
            this.生成数独ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建数独ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.拷贝网格ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴网格ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空网格ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重新计算候选数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空线索ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.校验有效性ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.解题技巧ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示候选数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.a1I9ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.r1C1R9C9单元格显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示欢迎消息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于软件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.新建数独ToolStripMenuItem,
            this.生成数独ToolStripMenuItem,
            this.导入ToolStripMenuItem,
            this.保存ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(44, 21);
            this.fileMenuItem.Text = "文件";
            this.fileMenuItem.Click += new System.EventHandler(this.fileMenuItem_Click);
            // 
            // editMenuItem
            // 
            this.editMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.拷贝网格ToolStripMenuItem,
            this.粘贴网格ToolStripMenuItem,
            this.清空网格ToolStripMenuItem});
            this.editMenuItem.Name = "editMenuItem";
            this.editMenuItem.Size = new System.Drawing.Size(48, 21);
            this.editMenuItem.Text = " 编辑";
            // 
            // toolMenuItem
            // 
            this.toolMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.重新计算候选数ToolStripMenuItem,
            this.清空线索ToolStripMenuItem,
            this.校验有效性ToolStripMenuItem});
            this.toolMenuItem.Name = "toolMenuItem";
            this.toolMenuItem.Size = new System.Drawing.Size(44, 21);
            this.toolMenuItem.Text = "工具";
            // 
            // optionMenuItem
            // 
            this.optionMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示候选数ToolStripMenuItem,
            this.解题技巧ToolStripMenuItem,
            this.a1I9ToolStripMenuItem,
            this.r1C1R9C9单元格显示ToolStripMenuItem});
            this.optionMenuItem.Name = "optionMenuItem";
            this.optionMenuItem.Size = new System.Drawing.Size(44, 21);
            this.optionMenuItem.Text = "选项";
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示欢迎消息ToolStripMenuItem,
            this.关于软件ToolStripMenuItem});
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(44, 21);
            this.helpMenuItem.Text = "帮助";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(891, 89);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 34);
            this.button1.TabIndex = 2;
            this.button1.Text = "生成数独";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(879, 290);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(536, 448);
            this.textBox1.TabIndex = 5;
            // 
            // sudokuPanel1
            // 
            this.sudokuPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(215)))), ((int)(((byte)(215)))));
            this.sudokuPanel1.Location = new System.Drawing.Point(12, 53);
            this.sudokuPanel1.Name = "sudokuPanel1";
            this.sudokuPanel1.Size = new System.Drawing.Size(810, 810);
            this.sudokuPanel1.TabIndex = 0;
            this.sudokuPanel1.Tag = "030150209000360050700490603001273800000519000003684700100000008320040000409001060" +
    "";
            // 
            // 生成数独ToolStripMenuItem
            // 
            this.生成数独ToolStripMenuItem.Name = "生成数独ToolStripMenuItem";
            this.生成数独ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.生成数独ToolStripMenuItem.Text = "生成数独";
            // 
            // 新建数独ToolStripMenuItem
            // 
            this.新建数独ToolStripMenuItem.Name = "新建数独ToolStripMenuItem";
            this.新建数独ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.新建数独ToolStripMenuItem.Text = "新建数独";
            // 
            // 导入ToolStripMenuItem
            // 
            this.导入ToolStripMenuItem.Name = "导入ToolStripMenuItem";
            this.导入ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.导入ToolStripMenuItem.Text = "导入";
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.保存ToolStripMenuItem.Text = "保存";
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 拷贝网格ToolStripMenuItem
            // 
            this.拷贝网格ToolStripMenuItem.Name = "拷贝网格ToolStripMenuItem";
            this.拷贝网格ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.拷贝网格ToolStripMenuItem.Text = "拷贝网格";
            // 
            // 粘贴网格ToolStripMenuItem
            // 
            this.粘贴网格ToolStripMenuItem.Name = "粘贴网格ToolStripMenuItem";
            this.粘贴网格ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.粘贴网格ToolStripMenuItem.Text = "粘贴网格";
            // 
            // 清空网格ToolStripMenuItem
            // 
            this.清空网格ToolStripMenuItem.Name = "清空网格ToolStripMenuItem";
            this.清空网格ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.清空网格ToolStripMenuItem.Text = "清空网格";
            // 
            // 重新计算候选数ToolStripMenuItem
            // 
            this.重新计算候选数ToolStripMenuItem.Name = "重新计算候选数ToolStripMenuItem";
            this.重新计算候选数ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.重新计算候选数ToolStripMenuItem.Text = "重新计算候选数";
            // 
            // 清空线索ToolStripMenuItem
            // 
            this.清空线索ToolStripMenuItem.Name = "清空线索ToolStripMenuItem";
            this.清空线索ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.清空线索ToolStripMenuItem.Text = "清空线索";
            // 
            // 校验有效性ToolStripMenuItem
            // 
            this.校验有效性ToolStripMenuItem.Name = "校验有效性ToolStripMenuItem";
            this.校验有效性ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.校验有效性ToolStripMenuItem.Text = "校验有效性";
            // 
            // 解题技巧ToolStripMenuItem
            // 
            this.解题技巧ToolStripMenuItem.Name = "解题技巧ToolStripMenuItem";
            this.解题技巧ToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.解题技巧ToolStripMenuItem.Text = "解题技巧";
            // 
            // 显示候选数ToolStripMenuItem
            // 
            this.显示候选数ToolStripMenuItem.Name = "显示候选数ToolStripMenuItem";
            this.显示候选数ToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.显示候选数ToolStripMenuItem.Text = "显示候选数";
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
            // 显示欢迎消息ToolStripMenuItem
            // 
            this.显示欢迎消息ToolStripMenuItem.Name = "显示欢迎消息ToolStripMenuItem";
            this.显示欢迎消息ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.显示欢迎消息ToolStripMenuItem.Text = "显示欢迎消息";
            // 
            // 关于软件ToolStripMenuItem
            // 
            this.关于软件ToolStripMenuItem.Name = "关于软件ToolStripMenuItem";
            this.关于软件ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.关于软件ToolStripMenuItem.Text = "关于软件";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1446, 887);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.sudokuPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem 新建数独ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 生成数独ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 拷贝网格ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 粘贴网格ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空网格ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重新计算候选数ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空线索ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 校验有效性ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示候选数ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 解题技巧ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem a1I9ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem r1C1R9C9单元格显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示欢迎消息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于软件ToolStripMenuItem;
    }
}