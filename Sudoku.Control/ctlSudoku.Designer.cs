using System.ComponentModel;
using System.Windows.Forms;

namespace Sudoku.UI
{
    partial class ctlSudoku
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.sudokuPanel = new System.Windows.Forms.Panel();
            this.columnPanel = new System.Windows.Forms.Panel();
            this.rowPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // sudokuPanel
            // 
            this.sudokuPanel.Location = new System.Drawing.Point(25, 10);
            this.sudokuPanel.Margin = new System.Windows.Forms.Padding(0);
            this.sudokuPanel.Name = "sudokuPanel";
            this.sudokuPanel.Size = new System.Drawing.Size(745, 745);
            this.sudokuPanel.TabIndex = 19;
            this.sudokuPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.sudokuPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            this.sudokuPanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDoubleClick);
            this.sudokuPanel.MouseLeave += new System.EventHandler(this.panel1_MouseLeave);
            this.sudokuPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // columnPanel
            // 
            this.columnPanel.Location = new System.Drawing.Point(25, 755);
            this.columnPanel.Name = "columnPanel";
            this.columnPanel.Size = new System.Drawing.Size(745, 25);
            this.columnPanel.TabIndex = 20;
            this.columnPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.columnPanel_Paint);
            // 
            // rowPanel
            // 
            this.rowPanel.Location = new System.Drawing.Point(0, 10);
            this.rowPanel.Name = "rowPanel";
            this.rowPanel.Size = new System.Drawing.Size(25, 745);
            this.rowPanel.TabIndex = 21;
            this.rowPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.rowPanel_Paint);
            // 
            // ctlSudoku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.rowPanel);
            this.Controls.Add(this.columnPanel);
            this.Controls.Add(this.sudokuPanel);
            this.Name = "ctlSudoku";
            this.Size = new System.Drawing.Size(780, 780);
            this.ResumeLayout(false);

        }

        #endregion
        public Panel sudokuPanel;
        private Panel columnPanel;
        private Panel rowPanel;
   
    }
}
