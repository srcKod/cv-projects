namespace PR2.OMR
{
    partial class Setup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Setup));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numeric_x = new System.Windows.Forms.NumericUpDown();
            this.numeric_y = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.apply = new System.Windows.Forms.Button();
            this.numeric_IdRadius = new System.Windows.Forms.NumericUpDown();
            this.numeric_QuesRadius = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.key_upload = new System.Windows.Forms.Button();
            this.match_upload = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnDefault = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_x)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_IdRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_QuesRadius)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(85, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(187, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y";
            // 
            // numeric_x
            // 
            this.numeric_x.Location = new System.Drawing.Point(105, 34);
            this.numeric_x.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numeric_x.Minimum = new decimal(new int[] {
            3000,
            0,
            0,
            -2147483648});
            this.numeric_x.Name = "numeric_x";
            this.numeric_x.Size = new System.Drawing.Size(76, 20);
            this.numeric_x.TabIndex = 2;
            // 
            // numeric_y
            // 
            this.numeric_y.Location = new System.Drawing.Point(207, 34);
            this.numeric_y.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numeric_y.Minimum = new decimal(new int[] {
            5000,
            0,
            0,
            -2147483648});
            this.numeric_y.Name = "numeric_y";
            this.numeric_y.Size = new System.Drawing.Size(76, 20);
            this.numeric_y.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(34, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(318, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Just increase or decrease stored template Coordinates";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(34, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(190, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Input radius value for Id centers";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(34, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(172, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Input radius for Ques centers";
            // 
            // apply
            // 
            this.apply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.apply.Location = new System.Drawing.Point(196, 276);
            this.apply.Name = "apply";
            this.apply.Size = new System.Drawing.Size(75, 23);
            this.apply.TabIndex = 11;
            this.apply.Text = "Apply";
            this.apply.UseVisualStyleBackColor = true;
            this.apply.Click += new System.EventHandler(this.apply_Click);
            // 
            // numeric_IdRadius
            // 
            this.numeric_IdRadius.Location = new System.Drawing.Point(250, 80);
            this.numeric_IdRadius.Name = "numeric_IdRadius";
            this.numeric_IdRadius.Size = new System.Drawing.Size(76, 20);
            this.numeric_IdRadius.TabIndex = 13;
            // 
            // numeric_QuesRadius
            // 
            this.numeric_QuesRadius.Location = new System.Drawing.Point(250, 106);
            this.numeric_QuesRadius.Name = "numeric_QuesRadius";
            this.numeric_QuesRadius.Size = new System.Drawing.Size(76, 20);
            this.numeric_QuesRadius.TabIndex = 14;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 164);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(256, 20);
            this.textBox1.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "AnswerKey File";
            // 
            // key_upload
            // 
            this.key_upload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.key_upload.Location = new System.Drawing.Point(277, 162);
            this.key_upload.Name = "key_upload";
            this.key_upload.Size = new System.Drawing.Size(75, 23);
            this.key_upload.TabIndex = 17;
            this.key_upload.Text = "Browse";
            this.key_upload.UseVisualStyleBackColor = true;
            this.key_upload.Click += new System.EventHandler(this.key_upload_Click);
            // 
            // match_upload
            // 
            this.match_upload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.match_upload.Location = new System.Drawing.Point(277, 213);
            this.match_upload.Name = "match_upload";
            this.match_upload.Size = new System.Drawing.Size(75, 23);
            this.match_upload.TabIndex = 20;
            this.match_upload.Text = "Browse";
            this.match_upload.UseVisualStyleBackColor = true;
            this.match_upload.Click += new System.EventHandler(this.match_upload_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 198);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Match Model File";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(15, 215);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(256, 20);
            this.textBox2.TabIndex = 18;
            // 
            // btnDefault
            // 
            this.btnDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDefault.Location = new System.Drawing.Point(88, 276);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(102, 23);
            this.btnDefault.TabIndex = 21;
            this.btnDefault.Text = "Reset to Default";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // Setup
            // 
            this.AcceptButton = this.apply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(387, 318);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.match_upload);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.key_upload);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.numeric_QuesRadius);
            this.Controls.Add(this.numeric_IdRadius);
            this.Controls.Add(this.apply);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numeric_y);
            this.Controls.Add(this.numeric_x);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(403, 357);
            this.MinimumSize = new System.Drawing.Size(403, 357);
            this.Name = "Setup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setup";
            this.Load += new System.EventHandler(this.Setup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numeric_x)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_IdRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_QuesRadius)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numeric_x;
        private System.Windows.Forms.NumericUpDown numeric_y;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button apply;
        private System.Windows.Forms.NumericUpDown numeric_IdRadius;
        private System.Windows.Forms.NumericUpDown numeric_QuesRadius;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button key_upload;
        private System.Windows.Forms.Button match_upload;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnDefault;
    }
}