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
    public partial class Courses : Form
    {
        public Courses()
        {
            InitializeComponent();
        }

        private void coursesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.coursesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dBDataSet);

        }

        private void Courses_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dBDataSet.Courses' table. You can move, or remove it, as needed.
            this.coursesTableAdapter.Fill(this.dBDataSet.Courses);

        }
    }
}
