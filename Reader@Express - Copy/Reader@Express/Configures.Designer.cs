namespace Reader_Express
{
    partial class Configures
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
            this.chbRssi = new System.Windows.Forms.CheckBox();
            this.lb1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chbChecksum = new System.Windows.Forms.CheckBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.chbRealtime = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chbBeep = new System.Windows.Forms.CheckBox();
            this.lb = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chbRssi
            // 
            this.chbRssi.AutoSize = true;
            this.chbRssi.Location = new System.Drawing.Point(91, 19);
            this.chbRssi.Name = "chbRssi";
            this.chbRssi.Size = new System.Drawing.Size(51, 17);
            this.chbRssi.TabIndex = 0;
            this.chbRssi.Text = "RSSI";
            this.chbRssi.UseVisualStyleBackColor = true;
            // 
            // lb1
            // 
            this.lb1.AutoSize = true;
            this.lb1.Location = new System.Drawing.Point(12, 19);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(32, 13);
            this.lb1.TabIndex = 1;
            this.lb1.Text = "RSSI";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "CheckSum";
            // 
            // chbChecksum
            // 
            this.chbChecksum.AutoSize = true;
            this.chbChecksum.Location = new System.Drawing.Point(91, 42);
            this.chbChecksum.Name = "chbChecksum";
            this.chbChecksum.Size = new System.Drawing.Size(78, 17);
            this.chbChecksum.TabIndex = 3;
            this.chbChecksum.Text = "CheckSum";
            this.chbChecksum.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(4, 130);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(107, 130);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // chbRealtime
            // 
            this.chbRealtime.AutoSize = true;
            this.chbRealtime.Location = new System.Drawing.Point(91, 70);
            this.chbRealtime.Name = "chbRealtime";
            this.chbRealtime.Size = new System.Drawing.Size(71, 17);
            this.chbRealtime.TabIndex = 6;
            this.chbRealtime.Text = "RealTime";
            this.chbRealtime.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "RealTime";
            // 
            // chbBeep
            // 
            this.chbBeep.AutoSize = true;
            this.chbBeep.Location = new System.Drawing.Point(91, 95);
            this.chbBeep.Name = "chbBeep";
            this.chbBeep.Size = new System.Drawing.Size(51, 17);
            this.chbBeep.TabIndex = 8;
            this.chbBeep.Text = "Beep";
            this.chbBeep.UseVisualStyleBackColor = true;
            // 
            // lb
            // 
            this.lb.AutoSize = true;
            this.lb.Location = new System.Drawing.Point(12, 95);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(32, 13);
            this.lb.TabIndex = 9;
            this.lb.Text = "Beep";
            // 
            // Configures
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 181);
            this.Controls.Add(this.lb);
            this.Controls.Add(this.chbBeep);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chbRealtime);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.chbChecksum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lb1);
            this.Controls.Add(this.chbRssi);
            this.Name = "Configures";
            this.Text = "Configures";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbRssi;
        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chbChecksum;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox chbRealtime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chbBeep;
        private System.Windows.Forms.Label lb;
    }
}