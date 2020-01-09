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
            this.sudokuPanel1 = new Sudoku.UI.SudokuPanel();
            this.SuspendLayout();
            // 
            // sudokuPanel1
            // 
            this.sudokuPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(215)))), ((int)(((byte)(215)))));
            this.sudokuPanel1.Location = new System.Drawing.Point(12, 12);
            this.sudokuPanel1.Name = "sudokuPanel1";
            this.sudokuPanel1.Size = new System.Drawing.Size(810, 810);
            this.sudokuPanel1.TabIndex = 0;
            this.sudokuPanel1.Tag = "030150209000360050700490603001273800000519000003684700100000008320040000409001060" +
    "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 853);
            this.Controls.Add(this.sudokuPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private SudokuPanel sudokuPanel1;
    }
}