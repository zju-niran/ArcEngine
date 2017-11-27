namespace MapControlApplication1
{
    partial class SelectBandsForm
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
            this.cklb_CompareHistogram = new System.Windows.Forms.CheckedListBox();
            this.btn_DrawCompareHistogram = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cklb_CompareHistogram
            // 
            this.cklb_CompareHistogram.FormattingEnabled = true;
            this.cklb_CompareHistogram.Location = new System.Drawing.Point(13, 42);
            this.cklb_CompareHistogram.Name = "cklb_CompareHistogram";
            this.cklb_CompareHistogram.Size = new System.Drawing.Size(257, 164);
            this.cklb_CompareHistogram.TabIndex = 0;
            // 
            // btn_DrawCompareHistogram
            // 
            this.btn_DrawCompareHistogram.Location = new System.Drawing.Point(90, 218);
            this.btn_DrawCompareHistogram.Name = "btn_DrawCompareHistogram";
            this.btn_DrawCompareHistogram.Size = new System.Drawing.Size(65, 25);
            this.btn_DrawCompareHistogram.TabIndex = 1;
            this.btn_DrawCompareHistogram.Text = "绘制";
            this.btn_DrawCompareHistogram.UseVisualStyleBackColor = true;
            this.btn_DrawCompareHistogram.Click += new System.EventHandler(this.btn_DrawCompareHistogram_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "选择波段：";
            // 
            // SelectBandsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_DrawCompareHistogram);
            this.Controls.Add(this.cklb_CompareHistogram);
            this.Name = "SelectBandsForm";
            this.Text = "SelectBandForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox cklb_CompareHistogram;
        private System.Windows.Forms.Button btn_DrawCompareHistogram;
        private System.Windows.Forms.Label label1;
    }
}