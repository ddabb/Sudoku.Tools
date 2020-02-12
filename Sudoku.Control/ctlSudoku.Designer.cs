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
            this.components=new Container();
            this.lblD = new System.Windows.Forms.Label();
            this.lblE = new System.Windows.Forms.Label();
            this.lblF = new System.Windows.Forms.Label();
            this.lblG = new System.Windows.Forms.Label();
            this.lblH = new System.Windows.Forms.Label();
            this.lblI = new System.Windows.Forms.Label();
            this.lb11 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.lbl4 = new System.Windows.Forms.Label();
            this.lbl5 = new System.Windows.Forms.Label();
            this.lbl6 = new System.Windows.Forms.Label();
            this.lbl7 = new System.Windows.Forms.Label();
            this.lbl8 = new System.Windows.Forms.Label();
            this.lbl9 = new System.Windows.Forms.Label();
            this.lblC = new System.Windows.Forms.Label();
            this.lblB = new System.Windows.Forms.Label();
            this.lblA = new System.Windows.Forms.Label();
            this.panel1 = new MyPanel(components);
            this.SuspendLayout();
            // 
            // lblD
            // 
            this.lblD.AutoSize = true;
            this.lblD.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblD.Location = new System.Drawing.Point(14, 277);
            this.lblD.Name = "lblD";
            this.lblD.Size = new System.Drawing.Size(22, 22);
            this.lblD.TabIndex = 1;
            this.lblD.Text = "D";
            // 
            // lblE
            // 
            this.lblE.AutoSize = true;
            this.lblE.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblE.Location = new System.Drawing.Point(14, 356);
            this.lblE.Name = "lblE";
            this.lblE.Size = new System.Drawing.Size(22, 22);
            this.lblE.TabIndex = 2;
            this.lblE.Text = "E";
            // 
            // lblF
            // 
            this.lblF.AutoSize = true;
            this.lblF.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblF.Location = new System.Drawing.Point(14, 435);
            this.lblF.Name = "lblF";
            this.lblF.Size = new System.Drawing.Size(22, 22);
            this.lblF.TabIndex = 4;
            this.lblF.Text = "F";
            // 
            // lblG
            // 
            this.lblG.AutoSize = true;
            this.lblG.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblG.Location = new System.Drawing.Point(14, 514);
            this.lblG.Name = "lblG";
            this.lblG.Size = new System.Drawing.Size(22, 22);
            this.lblG.TabIndex = 3;
            this.lblG.Text = "G";
            // 
            // lblH
            // 
            this.lblH.AutoSize = true;
            this.lblH.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblH.Location = new System.Drawing.Point(14, 593);
            this.lblH.Name = "lblH";
            this.lblH.Size = new System.Drawing.Size(22, 22);
            this.lblH.TabIndex = 8;
            this.lblH.Text = "H";
            // 
            // lblI
            // 
            this.lblI.AutoSize = true;
            this.lblI.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblI.Location = new System.Drawing.Point(14, 672);
            this.lblI.Name = "lblI";
            this.lblI.Size = new System.Drawing.Size(22, 22);
            this.lblI.TabIndex = 7;
            this.lblI.Text = "I";
            // 
            // lb11
            // 
            this.lb11.AutoSize = true;
            this.lb11.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb11.Location = new System.Drawing.Point(67, 747);
            this.lb11.Name = "lb11";
            this.lb11.Size = new System.Drawing.Size(22, 22);
            this.lb11.TabIndex = 6;
            this.lb11.Text = "1";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl2.Location = new System.Drawing.Point(149, 747);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(22, 22);
            this.lbl2.TabIndex = 5;
            this.lbl2.Text = "2";
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl3.Location = new System.Drawing.Point(231, 747);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(22, 22);
            this.lbl3.TabIndex = 9;
            this.lbl3.Text = "3";
            // 
            // lbl4
            // 
            this.lbl4.AutoSize = true;
            this.lbl4.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl4.Location = new System.Drawing.Point(313, 747);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(22, 22);
            this.lbl4.TabIndex = 18;
            this.lbl4.Text = "4";
            // 
            // lbl5
            // 
            this.lbl5.AutoSize = true;
            this.lbl5.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl5.Location = new System.Drawing.Point(395, 747);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(22, 22);
            this.lbl5.TabIndex = 17;
            this.lbl5.Text = "5";
            // 
            // lbl6
            // 
            this.lbl6.AutoSize = true;
            this.lbl6.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl6.Location = new System.Drawing.Point(477, 747);
            this.lbl6.Name = "lbl6";
            this.lbl6.Size = new System.Drawing.Size(22, 22);
            this.lbl6.TabIndex = 16;
            this.lbl6.Text = "6";
            // 
            // lbl7
            // 
            this.lbl7.AutoSize = true;
            this.lbl7.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl7.Location = new System.Drawing.Point(559, 747);
            this.lbl7.Name = "lbl7";
            this.lbl7.Size = new System.Drawing.Size(22, 22);
            this.lbl7.TabIndex = 15;
            this.lbl7.Text = "7";
            // 
            // lbl8
            // 
            this.lbl8.AutoSize = true;
            this.lbl8.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl8.Location = new System.Drawing.Point(641, 747);
            this.lbl8.Name = "lbl8";
            this.lbl8.Size = new System.Drawing.Size(22, 22);
            this.lbl8.TabIndex = 14;
            this.lbl8.Text = "8";
            // 
            // lbl9
            // 
            this.lbl9.AutoSize = true;
            this.lbl9.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl9.Location = new System.Drawing.Point(723, 747);
            this.lbl9.Name = "lbl9";
            this.lbl9.Size = new System.Drawing.Size(22, 22);
            this.lbl9.TabIndex = 13;
            this.lbl9.Text = "9";
            // 
            // lblC
            // 
            this.lblC.AutoSize = true;
            this.lblC.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblC.Location = new System.Drawing.Point(14, 198);
            this.lblC.Name = "lblC";
            this.lblC.Size = new System.Drawing.Size(22, 22);
            this.lblC.TabIndex = 12;
            this.lblC.Text = "C";
            // 
            // lblB
            // 
            this.lblB.AutoSize = true;
            this.lblB.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblB.Location = new System.Drawing.Point(14, 119);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(22, 22);
            this.lblB.TabIndex = 11;
            this.lblB.Text = "B";
            // 
            // lblA
            // 
            this.lblA.AutoSize = true;
            this.lblA.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblA.Location = new System.Drawing.Point(14, 40);
            this.lblA.Name = "lblA";
            this.lblA.Size = new System.Drawing.Size(22, 22);
            this.lblA.TabIndex = 10;
            this.lblA.Text = "A";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(42, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(716, 717);
            this.panel1.TabIndex = 19;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // ctlSudoku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbl4);
            this.Controls.Add(this.lbl5);
            this.Controls.Add(this.lbl6);
            this.Controls.Add(this.lbl7);
            this.Controls.Add(this.lbl8);
            this.Controls.Add(this.lbl9);
            this.Controls.Add(this.lblC);
            this.Controls.Add(this.lblB);
            this.Controls.Add(this.lblA);
            this.Controls.Add(this.lbl3);
            this.Controls.Add(this.lblH);
            this.Controls.Add(this.lblI);
            this.Controls.Add(this.lb11);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lblF);
            this.Controls.Add(this.lblG);
            this.Controls.Add(this.lblE);
            this.Controls.Add(this.lblD);
            this.Name = "ctlSudoku";
            this.Size = new System.Drawing.Size(784, 782);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion



        private System.Windows.Forms.Label lblA;
        private System.Windows.Forms.Label lblB;
        private System.Windows.Forms.Label lblC;
        private System.Windows.Forms.Label lblD;
        private System.Windows.Forms.Label lblE;
        private System.Windows.Forms.Label lblF;
        private System.Windows.Forms.Label lblG;
        private System.Windows.Forms.Label lblH;
        private System.Windows.Forms.Label lblI;
        private System.Windows.Forms.Label lb11;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.Label lbl5;
        private System.Windows.Forms.Label lbl6;
        private System.Windows.Forms.Label lbl7;
        private System.Windows.Forms.Label lbl8;
        private System.Windows.Forms.Label lbl9;
        private MyPanel panel1;
    }
}
