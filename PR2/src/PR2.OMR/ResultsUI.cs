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
    public partial class ResultsUI : Form
    {

        private string id;
        private double mark;
        private List<string> answers;
        private List<bool> comp;
        private Dictionary<int, List<bool>> solu;
        private string[] options = { "A", "B", "C", "D" };
        private string stuAns = "";

        public ResultsUI(OMR omr)
        {           
            InitializeComponent();

            // get the results to show
            omr.getResult(out id, out mark, out answers, out comp, out solu);
           
            foreach (var ans in answers)
            {
                stuAns = (stuAns.Trim() + ans + "|").Trim();
            }

            if (id == "") { id = "0"; } // if id not detected set it 0
            id_textBox.Text = id;
            mark_textBox.Text = mark.ToString();
            firstnametxtbox.Text = "";
            lastnametxtbox.Text = "";

            #region inject List View with answers

            lv.Columns.Add("QuesNum", lv.Width / 5);
            lv.Columns.Add("A", lv.Width / 5);
            lv.Columns.Add("B", lv.Width / 5);
            lv.Columns.Add("C", lv.Width / 5);
            lv.Columns.Add("D", lv.Width / 5);

            lv.Items.Clear();

            ListViewItem lvi;

            bool choice = false;
           for(int i =0; i < comp.Count;i++)
            {
              lvi = new ListViewItem((i+1).ToString());
                lvi.UseItemStyleForSubItems = false;
                if (comp[i] == true) //if student answer is correct
                {                    
                        for (int j = 0; j < solu[i+1].Count; j++)
                        {
                            choice = omr.processdata.get(solu, i+1, j);  
                            if(choice == true)
                            {
                            lvi.SubItems.Add(options[j], Color.Black, Color.LightGreen, DefaultFont); // Add student answer and make it green
                        }
                        else if (choice == false)
                          {
                                lvi.SubItems.Add("");
                            }
                        }                   
                }
                else //if student answer is wrong 
                {                 
                        for (int j = 0; j < solu[i+1].Count; j++)
                        {
                            choice = omr.processdata.get(solu, i+1, j);
                            if (choice == true)
                            {
                            lvi.SubItems.Add(options[j], Color.Black, Color.LightPink, DefaultFont);    // Add student answer and make it red                           
                        }
                        else if (choice == false)
                           {
                                lvi.SubItems.Add("");
                            }
                        }                   
                }
                lv.Items.Add(lvi);
            }
            #endregion
    
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ResultsUI_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dBDataSet.Courses' table. You can move, or remove it, as needed.
            this.coursesTableAdapter.Fill(this.dBDataSet.Courses);
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            long l;
            bool a = long.TryParse(id, out l);          
            DateTime m = DateTime.Now;
            string mydate = m.ToString();

            try 
            {
                if (a == true)
                {
                    var v = this.studentsTableAdapter1.GetStudentById(l);
                    if (v == null)
                    {
                        this.studentsTableAdapter1.InsertNewStudent(l, firstnametxtbox.Text, lastnametxtbox.Text);
                        this.resultsTableAdapter1.InsertNewResult((long?)(mark), stuAns, mydate, l, (long)comboBoxResult.SelectedValue);

                    } else this.resultsTableAdapter1.InsertNewResult((long?)(mark), stuAns, mydate, l, (long)comboBoxResult.SelectedValue);

                    lblSaved.Visible = true;

                }else { MessageBox.Show(l.ToString()); }

            } catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            

        }

       
    }
}
