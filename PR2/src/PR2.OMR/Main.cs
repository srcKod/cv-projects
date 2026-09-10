using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.IO;
using Saraff.Twain;

using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using PR2.OMR;
using PR2.OMR.Properties;


namespace PR2.OMR
{
    public partial class Main : Form
    {
        private bool _isEnable = false;
        private bool _isFolder = false;

        private Image result;
        private Image<Bgr, byte> outputIMG;
        private Mat imgopen, img, imginput, imgshow, imgoutput;
        private Dictionary<string, Mat> images = new Dictionary<string, Mat>();

        private string id;
        private double mark;
        private List<string> answers;
        private List<bool> comp;
        private Dictionary<int, List<bool>> solu;

        public Mat imgRef, imgSol;
        public string path, AnswerKey_Path, Modelimg_Path, PathRef, PathSol;
        public long time;
        public string[] lines = new string[100];
        public float x, y;
        public int idR, QuesR;

        OMR omr = new OMR();
                 
        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dBDataSet.Courses' table. You can move, or remove it, as needed.
            this.coursesTableAdapter.Fill(this.dBDataSet.Courses);
            // TODO: This line of code loads data into the 'dBDataSet.Courses' table. You can move, or remove it, as needed.
            this.coursesTableAdapter.Fill(this.dBDataSet.Courses);
            // TODO: This line of code loads data into the 'dBDataSet.Results' table. You can move, or remove it, as needed.
            //load default settings
            AnswerKey_Path = Settings.Default["AnswerKey_Path"].ToString();
            Modelimg_Path = Settings.Default["Modelimg_Path"].ToString();
            x = Convert.ToSingle(Settings.Default["x"]);
            y = Convert.ToSingle(Settings.Default["y"]);
            idR = Convert.ToInt32(Settings.Default["IdRadius"]);
            QuesR = Convert.ToInt32(Settings.Default["QuesRadius"]);
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string dbpath = (System.IO.Path.GetDirectoryName(executable));
            AppDomain.CurrentDomain.SetData("DataDirectory", dbpath);
        }

        #region Open local Solution scanned image 
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _isFolder = false;
            this.Text = "OMR Grading System";
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "PNG Image|*.png|JPEG Image|*.jpg;*.jpeg;*.jpe;*.jfif|Bitmap Image|*.bmp;*.dib|GIF Image|*.gif";
                op.Title = "Open scaned solution Image File";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    path = op.FileName;
                    //Load and resize Image Asynchronous using new thread
                    BackgroundWorker bw = new BackgroundWorker();
                    //properties
                    bw.WorkerReportsProgress = false;
                    bw.WorkerSupportsCancellation = false;
                    //events
                    bw.DoWork += bw_DoWork;
                    bw.RunWorkerCompleted += bw_RunWorkerCompleted;
                    bw.RunWorkerAsync();
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {           
            imageBox1.Image = imgshow;
            imageBox1.Invalidate();
            imageBox1.Refresh();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            imgopen = CvInvoke.Imread(path, ImreadModes.Color);
            img = imgopen;
            imgshow = new Mat();
            CvInvoke.Resize(img, imgshow, new Size(), 0.5, 0.5, Inter.Linear);
        }

        #endregion

        #region Scan new sheet Image
        private void scanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // open Data Source Manager
            try
            {
                twain.OpenDSM();
                twain.SourceIndex = 0; // First Scanner Device
                twain.OpenDataSource();

                if (!twain.ShowUI) // If Scanner UI wizard not shown
                {
                    // Select Resolution
                    // We choose DPI = 100 so the image resolution will be 850 X 1169                                                    
                    twain.Capabilities.XResolution.Set(100f);
                    twain.Capabilities.YResolution.Set(100f);
                    // Select Pixel Type 
                    // the scanned image will be color image                                   
                    twain.Capabilities.PixelType.Set(TwPixelType.RGB);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please Connect Scanner ! ", "Scanner", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            try
            {
                twain.Acquire();
            }
            catch
            {
                //MessageBox.Show(ex.Message, "Scanner", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #region Twain32 events handlers

        // Occurs when acquire completed.
        private void twain_AcquireCompleted(object sender, EventArgs e)
        {
            try
            {
                if (imageBox1.Image != null)
                {
                    imageBox1.Image.Dispose();
                }
                if (twain.ImageCount > 0)
                {
                    result = twain.GetImage(0);                 
                    outputIMG = new Image<Bgr, byte>(new Bitmap(result));
                    img = outputIMG.Mat;
                    imageBox1.Image = img;

                    // Open Save Dialog prompt the user to save the image
                    try
                    {
                        SaveFileDialog sv = new SaveFileDialog();
                        sv.Filter = "Png Image|*.png|Jpeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
                        sv.Title = "Save an Image File";
                        sv.ShowDialog();

                        // If the file name is not an empty string open it for saving.  
                        if (sv.FileName != "")
                        {
                            // Saves the Image via a FileStream created by the OpenFile method.  
                            FileStream fs =(FileStream)sv.OpenFile();
                            // Saves the Image in the appropriate ImageFormat based upon the  
                            // File type selected in the dialog box.  
                            // FilterIndex property is one-based so we can swich Filters one by one.  
                            switch (sv.FilterIndex)
                            {
                                case 1:
                                    result.Save(fs, ImageFormat.Png);
                                    break;
                                case 2:
                                    result.Save(fs, ImageFormat.Jpeg);
                                    break;
                                case 3:
                                    result.Save(fs, ImageFormat.Bmp);
                                    break;
                                case 4:
                                    result.Save(fs, ImageFormat.Gif);
                                    break;
                            }
                            fs.Close();
                        }
                    }
                    catch { } 

                }
                twain.CloseDSM();
            }
            catch (Exception ex)
            {
                MessageBox.Show("hello", "Scanner " + ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void twain_TwainStateChanged(object sender, Twain32.TwainStateEventArgs e)
        {
            try
            {
                if ((e.TwainState & Twain32.TwainStateFlag.DSEnabled) == 0 && this._isEnable)
                {
                    this._isEnable = false;
                    // <<< scaning finished (or closed)
                }
                this._isEnable = (e.TwainState & Twain32.TwainStateFlag.DSEnabled) != 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SAMPLE1", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #endregion
            
        private void setupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setup setup = new Setup(this, AnswerKey_Path, Modelimg_Path, x, y, idR, QuesR);
            setup.StartPosition = FormStartPosition.CenterParent;
            setup.ShowDialog();
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _isFolder = true;
            this.Text = "OMR Grading System";
            try  
            {              
                string[] extensions = new string[] { ".PNG", ".JPG", ".BMP", ".GIF" };
                var fd = new System.Windows.Forms.FolderBrowserDialog();
                if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (images.Count > 0) { return; }
                    foreach (var file in Directory.GetFiles(fd.SelectedPath).Where(f => extensions.Contains(Path.GetExtension(f).ToUpper())))
                    {
                        images.Add(Path.GetFileNameWithoutExtension(file), new Mat(file,ImreadModes.Color) ); //new Bitmap(file)
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close ?", "System Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Students s = new Students();
            s.StartPosition = FormStartPosition.CenterParent;
            s.ShowDialog();
        }

        private void resultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Results r = new Results();
            r.StartPosition = FormStartPosition.CenterParent;
            r.ShowDialog();
        }       

        private void coursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Courses c = new Courses();
            c.StartPosition = FormStartPosition.CenterParent;
            c.ShowDialog();
        }

        #region Process Scanned Sheet
        private void detectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (img == null) {
                    if(images.Count == 0)
                        return;
                }

                if (File.Exists(AnswerKey_Path))
                {
                    //Load AnswerKey text file and inject refernce dictionary with ture answers to compare with solution dictionary
                    using (StreamReader sr = File.OpenText(AnswerKey_Path)) // Read Answers From txt File
                    {
                        int a = 0;
                        while (!sr.EndOfStream)
                        {
                            lines[a] = sr.ReadLine();
                            a++;
                        }
                    }//Finished. Close the file
                }
                else
                {
                    MessageBox.Show("File :" + AnswerKey_Path + "\r\n is not found please choose Answer key file", "File Not Found",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }
          
                //Load and process Image Asynchronous using new thread
                BackgroundWorker bw = new BackgroundWorker();
                //properties
                bw.WorkerReportsProgress = true;
                bw.WorkerSupportsCancellation = false;
                //events
                bw.DoWork += bw_DoWorkdetect;
                bw.RunWorkerCompleted += bw_RunWorkerCompleteddetect;
                bw.ProgressChanged += bw_ProgressChangeddetect;
                progressBar.Maximum = 100;
                progressBar.Step = 1;
                progressBar.Value = 0;
                bw.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void userGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string GuidePath = (System.IO.Path.GetDirectoryName(executable));
            ProcessStartInfo startInfo = new ProcessStartInfo(GuidePath+ "\\userguide.htm");
            Process.Start(startInfo);
        }

        private void bw_RunWorkerCompleteddetect(object sender, RunWorkerCompletedEventArgs e)
        {
            imgshow = new Mat();
            CvInvoke.Resize(imgoutput, imgshow, new Size(), 0.5, 0.5, Inter.Linear);
            imageBox2.Image = imgshow;
            progressBar.Value = 100;
            this.Text = time.ToString() + " Millisecond" +" " + " Completed !!" ;
        }

        private void bw_ProgressChangeddetect(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void bw_DoWorkdetect(object sender, DoWorkEventArgs e)
        {

            if (img != null) { 
            imgoutput = new Mat();
            imgoutput = omr.ScanDetect(img,_isFolder, x, y, lines, idR, QuesR, out time);           
            }

            if (images.Count != 0) {

                var backgroundWorker = sender as BackgroundWorker;
                int index = 0;
                foreach (var q in images)
                {                   
                    imgoutput = omr.ScanDetect(q.Value, _isFolder, x, y, lines, idR, QuesR, out time);
                    // get the results 
                    omr.getResult(out id, out mark, out answers, out comp, out solu);

                    string stuAns = "";
                    foreach (var ans in answers)
                    {
                        stuAns = (stuAns.Trim() + ans + "|").Trim();
                    }
                    if (id == "") { id = "0"; } // if id not detected set it 0

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
                                this.studentsTableAdapter1.InsertNewStudent(l, "", "");
                                this.resultsTableAdapter1.InsertNewResult((long?)mark, stuAns, mydate, l, (long)comboBoxMain.SelectedValue);
                            }                         
                            else this.resultsTableAdapter1.InsertNewResult((long?)mark, stuAns, mydate, l, (long)comboBoxMain.SelectedValue);                            
                        }

                        else { MessageBox.Show(l.ToString()); }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.ToString()); }

                    backgroundWorker.ReportProgress((index * 100) / images.Count);
                    index++;
                }
            }
        }
    
        #endregion
      
        #region Matching Reference Sheet
        private void openReferenceImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = "OMR Grading System";
            try
            {                
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "JPEG Image|*.jpg;*.jpeg;*.jpe;*.jfif|PNG Image|*.png|Bitmap Image|*.bmp;*.dib|GIF Image|*.gif";
                op.Title = "Open Reference Image File";
                if (op.ShowDialog() == DialogResult.OK)
                {                  
                    PathRef = op.FileName;

                    if (File.Exists(Modelimg_Path))
                    {
                        imginput = CvInvoke.Imread(Modelimg_Path, ImreadModes.Color);
                    }
                    else
                    {
                        MessageBox.Show("File :" + Modelimg_Path + "\r\n is not found please choose Model Image file", "File Not Found",
                                       MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    //Load and process Image Asynchronous using new thread
                    BackgroundWorker bw = new BackgroundWorker();
                    //properties
                    bw.WorkerReportsProgress = false;
                    bw.WorkerSupportsCancellation = false;
                    //events
                    bw.DoWork += bw_DoWorkref;
                    bw.RunWorkerCompleted += bw_RunWorkerCompletedref;
                    progressBar.Maximum = 100;
                    progressBar.Step = 1;
                    progressBar.Value = 0;
                    bw.RunWorkerAsync();                               
                }           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void bw_RunWorkerCompletedref(object sender, RunWorkerCompletedEventArgs e)
        {
            imageBox1.Image = imgoutput;
            ImageViewer viewer = new ImageViewer(new Image<Bgr,byte>(imgshow.Size));
            viewer.Image = imgshow;
            viewer.Text = (time.ToString() + " Millisecond" + " " + "Completed !!");
            this.Text = (time.ToString() + " Millisecond" + " " + "Completed !!");
            viewer.StartPosition = FormStartPosition.CenterParent;
            viewer.ShowDialog();
            
        }
     
        private void bw_DoWorkref(object sender, DoWorkEventArgs e)
        {              
                imgRef = CvInvoke.Imread(PathRef, ImreadModes.Color);
                if (imginput == null || imgRef == null) { return; }
                Mat result = omr.MatchDetect(imginput, imgRef, x, y, idR, QuesR, out time, true);
                imgshow = new Mat();
                imgoutput = new Mat();
                CvInvoke.Resize(result, imgshow, new Size(), 0.5, 0.5, Inter.Linear);
                CvInvoke.Resize(imgRef, imgoutput, new Size(), 0.5, 0.5, Inter.Linear);              
        }

        #endregion

        #region Matching Solution Sheet
        private void openSolutionImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = "OMR Grading System";
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "JPEG Image|*.jpg;*.jpeg;*.jpe;*.jfif|PNG Image|*.png|Bitmap Image|*.bmp;*.dib|GIF Image|*.gif";
                op.Title = "Open Solution Image File";
                if (op.ShowDialog() == DialogResult.OK)
                {

                    PathSol = op.FileName;

                    if (File.Exists(Modelimg_Path))
                    {
                        imginput = CvInvoke.Imread(Modelimg_Path, ImreadModes.Color);
                    }
                    else
                    {
                        MessageBox.Show("File :" + Modelimg_Path + "\r\n is not found please choose Model Image file", "File Not Found",
                                       MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    //Load and process Image Asynchronous using new thread
                    BackgroundWorker bw = new BackgroundWorker();
                    //properties
                    bw.WorkerReportsProgress = false;
                    bw.WorkerSupportsCancellation = false;
                    //events
                    bw.DoWork += bw_DoWorkrsol;
                    bw.RunWorkerCompleted += bw_RunWorkerCompletedsol;
                    bw.RunWorkerAsync();                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void bw_RunWorkerCompletedsol(object sender, RunWorkerCompletedEventArgs e)
        {
            imageBox2.Image = imgoutput;
            ImageViewer viewer = new ImageViewer(new Image<Bgr, byte>(imgshow.Size));
            viewer.Image = imgshow;
            viewer.Text = (time.ToString() + " Millisecond" + " " + "Completed !!");
            this.Text = (time.ToString() + " Millisecond" + " " + "Completed !!");
            viewer.StartPosition = FormStartPosition.CenterParent;
            viewer.ShowDialog();
        }

        private void bw_DoWorkrsol(object sender, DoWorkEventArgs e)
        {                      
            imgSol = CvInvoke.Imread(PathSol, ImreadModes.Color);
            if (imginput == null || imgSol == null) { return; }           
            Mat result = omr.MatchDetect(imginput, imgSol, x, y, idR, QuesR, out time, false);
            imgshow = new Mat();
            imgoutput = new Mat();
            CvInvoke.Resize(result, imgshow, new Size(), 0.5, 0.5, Inter.Linear);
            CvInvoke.Resize(imgSol, imgoutput, new Size(), 0.5, 0.5, Inter.Linear);                      
        }

        #endregion

        private void showScanedResultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResultsUI r = new ResultsUI(omr);
            r.StartPosition = FormStartPosition.CenterParent;
            r.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(this," Syrian Virtual University.\r\n PR2 Project (OMR Grading System)\r\n Programmed by alaa_20583@svuonline.org", "info",
            //                MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            Help n = new Help();
            n.StartPosition = FormStartPosition.CenterParent;
            n.ShowDialog();
        }

    }
}


