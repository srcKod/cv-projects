namespace PR2.OMR
{
    partial class ResultsUI
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultsUI));
            this.mark_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.id_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cancel = new System.Windows.Forms.Button();
            this.lv = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxResult = new System.Windows.Forms.ComboBox();
            this.coursesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dBDataSet = new PR2.OMR.DBDataSet();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.studentsTableAdapter1 = new PR2.OMR.DBDataSetTableAdapters.StudentsTableAdapter();
            this.label3 = new System.Windows.Forms.Label();
            this.coursesTableAdapter = new PR2.OMR.DBDataSetTableAdapters.CoursesTableAdapter();
            this.resultsTableAdapter1 = new PR2.OMR.DBDataSetTableAdapters.ResultsTableAdapter();
            this.lblSaved = new System.Windows.Forms.Label();
            this.firstnametxtbox = new System.Windows.Forms.TextBox();
            this.lastnametxtbox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.coursesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // mark_textBox
            // 
            this.mark_textBox.BackColor = System.Drawing.SystemColors.Window;
            this.mark_textBox.Location = new System.Drawing.Point(105, 382);
            this.mark_textBox.Name = "mark_textBox";
            this.mark_textBox.ReadOnly = true;
            this.mark_textBox.Size = new System.Drawing.Size(121, 20);
            this.mark_textBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 385);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mark";
            // 
            // id_textBox
            // 
            this.id_textBox.BackColor = System.Drawing.SystemColors.Window;
            this.id_textBox.Location = new System.Drawing.Point(105, 408);
            this.id_textBox.Name = "id_textBox";
            this.id_textBox.ReadOnly = true;
            this.id_textBox.Size = new System.Drawing.Size(121, 20);
            this.id_textBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 411);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Student ID";
            // 
            // cancel
            // 
            this.cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancel.Location = new System.Drawing.Point(523, 457);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 5;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // lv
            // 
            this.lv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv.FullRowSelect = true;
            this.lv.GridLines = true;
            this.lv.Location = new System.Drawing.Point(0, 0);
            this.lv.Name = "lv";
            this.lv.Size = new System.Drawing.Size(669, 348);
            this.lv.TabIndex = 6;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = System.Windows.Forms.View.Details;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lv);
            this.panel1.Location = new System.Drawing.Point(10, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(669, 348);
            this.panel1.TabIndex = 7;
            // 
            // comboBoxResult
            // 
            this.comboBoxResult.DataSource = this.coursesBindingSource;
            this.comboBoxResult.DisplayMember = "Title";
            this.comboBoxResult.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxResult.FormattingEnabled = true;
            this.comboBoxResult.Location = new System.Drawing.Point(373, 382);
            this.comboBoxResult.Name = "comboBoxResult";
            this.comboBoxResult.Size = new System.Drawing.Size(121, 21);
            this.comboBoxResult.TabIndex = 8;
            this.comboBoxResult.ValueMember = "CourseId";
            // 
            // coursesBindingSource
            // 
            this.coursesBindingSource.DataMember = "Courses";
            this.coursesBindingSource.DataSource = this.dBDataSet;
            // 
            // dBDataSet
            // 
            this.dBDataSet.DataSetName = "DBDataSet";
            this.dBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // SaveBtn
            // 
            this.SaveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveBtn.Location = new System.Drawing.Point(604, 457);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveBtn.TabIndex = 9;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // studentsTableAdapter1
            // 
            this.studentsTableAdapter1.ClearBeforeFill = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(287, 385);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Choose Course";
            // 
            // coursesTableAdapter
            // 
            this.coursesTableAdapter.ClearBeforeFill = true;
            // 
            // resultsTableAdapter1
            // 
            this.resultsTableAdapter1.ClearBeforeFill = true;
            // 
            // lblSaved
            // 
            this.lblSaved.AutoSize = true;
            this.lblSaved.Font = new System.Drawing.Font("Tahoma", 14F);
            this.lblSaved.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblSaved.Location = new System.Drawing.Point(606, 375);
            this.lblSaved.Name = "lblSaved";
            this.lblSaved.Size = new System.Drawing.Size(73, 23);
            this.lblSaved.TabIndex = 11;
            this.lblSaved.Text = "Saved !";
            this.lblSaved.Visible = false;
            // 
            // firstnametxtbox
            // 
            this.firstnametxtbox.Location = new System.Drawing.Point(105, 434);
            this.firstnametxtbox.Name = "firstnametxtbox";
            this.firstnametxtbox.Size = new System.Drawing.Size(121, 20);
            this.firstnametxtbox.TabIndex = 12;
            // 
            // lastnametxtbox
            // 
            this.lastnametxtbox.Location = new System.Drawing.Point(105, 460);
            this.lastnametxtbox.Name = "lastnametxtbox";
            this.lastnametxtbox.Size = new System.Drawing.Size(121, 20);
            this.lastnametxtbox.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 437);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Enter First Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 463);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Enter Last Name";
            // 
            // ResultsUI
            // 
            this.AcceptButton = this.cancel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(691, 499);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lastnametxtbox);
            this.Controls.Add(this.firstnametxtbox);
            this.Controls.Add(this.lblSaved);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.comboBoxResult);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.id_textBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mark_textBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(707, 538);
            this.MinimumSize = new System.Drawing.Size(707, 538);
            this.Name = "ResultsUI";
            this.Text = "Results";
            this.Load += new System.EventHandler(this.ResultsUI_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.coursesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox mark_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox id_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.Panel panel1;
        private DBDataSetTableAdapters.StudentsTableAdapter studentsTableAdapter1;
        private System.Windows.Forms.ComboBox comboBoxResult;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Label label3;
        private DBDataSet dBDataSet;
        private System.Windows.Forms.BindingSource coursesBindingSource;
        private DBDataSetTableAdapters.CoursesTableAdapter coursesTableAdapter;
        private DBDataSetTableAdapters.ResultsTableAdapter resultsTableAdapter1;
        private System.Windows.Forms.Label lblSaved;
        private System.Windows.Forms.TextBox firstnametxtbox;
        private System.Windows.Forms.TextBox lastnametxtbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}