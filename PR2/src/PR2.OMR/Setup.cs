using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PR2.OMR.Properties;

namespace PR2.OMR
{
    public partial class Setup : Form
    {
        Main frm;
        public Setup(Main fm, string p1, string p2, float V0, float V1, int V2, int V3)
        {
            InitializeComponent();
            frm = fm;
            numeric_x.Value = (decimal)V0;
            numeric_y.Value = (decimal)V1;
            numeric_IdRadius.Value = (decimal)V2;
            numeric_QuesRadius.Value = (decimal)V3;
            textBox1.Text = p1;
            textBox2.Text = p2;
        }

        private void Setup_Load(object sender, EventArgs e)
        {
            //load default settings
            this.numeric_x.Value = Convert.ToDecimal(Settings.Default["x"]);
            this.numeric_y.Value = Convert.ToDecimal(Settings.Default["y"]);
            this.numeric_IdRadius.Value = Convert.ToDecimal(Settings.Default["IdRadius"]);
            this.numeric_QuesRadius.Value = Convert.ToDecimal(Settings.Default["QuesRadius"]);
            this.textBox1.Text = Settings.Default["AnswerKey_Path"].ToString();
            this.textBox2.Text = Settings.Default["Modelimg_Path"].ToString();
        }

        private void apply_Click(object sender, EventArgs e)
        {
           
            frm.x = (float) numeric_x.Value;
            frm.y = (float)numeric_y.Value;
            frm.idR = (int)numeric_IdRadius.Value;
            frm.QuesR = (int)numeric_QuesRadius.Value;
            frm.AnswerKey_Path = textBox1.Text;
            frm.Modelimg_Path = textBox2.Text;

            Settings.Default["AnswerKey_Path"] = textBox1.Text;
            Settings.Default["Modelimg_Path"] = textBox2.Text;
            Settings.Default["x"] = (float)this.numeric_x.Value;
            Settings.Default["y"] = (float)this.numeric_y.Value;
            Settings.Default["IdRadius"] = (int) this.numeric_IdRadius.Value;
            Settings.Default["QuesRadius"] = (int)this.numeric_QuesRadius.Value;
            Settings.Default.Save();

            this.Close();
        }

        private void key_upload_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Text File :|*.txt";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = op.FileName;                                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void match_upload_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "PNG Image|*.png|JPEG Image|*.jpg;*.jpeg;*.jpe;*.jfif|Bitmap Image|*.bmp;*.dib|GIF Image|*.gif";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = op.FileName;                                     
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            //load default settings
            this.numeric_x.Value = 0;
            this.numeric_y.Value = 0;
            this.numeric_IdRadius.Value = 7;
            this.numeric_QuesRadius.Value = 9;
            this.textBox1.Text = "";
            this.textBox2.Text = "";
        }
    }
 }

