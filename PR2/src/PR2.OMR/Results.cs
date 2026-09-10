using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR2.OMR
{
    public partial class Results : Form
    {
        public Results()
        {
            InitializeComponent();

        }

        private void resultsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.resultsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dBDataSet);
        }

        private void Results_Load(object sender, EventArgs e)
        {
            CourseCombo.SelectedValue = -1;
            // TODO: This line of code loads data into the 'dBDataSet.Courses' table. You can move, or remove it, as needed.
            this.coursesTableAdapter.Fill(this.dBDataSet.Courses);
            // TODO: This line of code loads data into the 'dBDataSet.Results' table. You can move, or remove it, as needed.
            this.resultsTableAdapter.Fill(this.dBDataSet.Results);
        }

        private void SIDtxtbox_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            long x;
            bool b = long.TryParse(SIDtxtbox.Text, out x);
            if (!b)
                errorProvider1.SetError(SIDtxtbox, "Input Numbers Only!!");
            else
            {
                errorProvider1.Clear();
            }

            this.resultsTableAdapter.FillBySID(this.dBDataSet.Results, x);

        }

        private void CourseCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.CourseCombo.SelectedValue != null)
                this.resultsTableAdapter.FillByCourseId(this.dBDataSet.Results, (long)CourseCombo.SelectedValue);

        }

        private void export_Click(object sender, EventArgs e)
        {
            ExportToExcel xl = new ExportToExcel();
            xl.CreateSheet();
            label1.Visible = true;
        }
    }
}

