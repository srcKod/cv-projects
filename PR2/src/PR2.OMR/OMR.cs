//------------------------------------------------------------------------------
// We Use two correction Approaches :
// First Approach:  recognition of the sheets images scanned by scanner. 
// Second Approach: recognition of the sheets images captured by mobile phone camera.
// this Class has the necessary methodes which handles above approaches.
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.IO;
using System.Diagnostics;
using Emgu.CV.Util;
using Emgu.CV.XFeatures2D;
using Emgu.CV.Flann;
using Emgu.CV.Features2D;

namespace PR2.OMR
{
    /// <summary>
    /// OMR Recognition Module. 
    /// </summary>
    public class OMR
    {              
        private int k = 2;       
        private double uniquenessThreshold = 0.80;
        private double hessianThresh = 300;
        private Stopwatch watch;
        private Mat result, homography, mask, gray, otsu, binz, output;
        private List<PointF> ID_centersF;
        private List<PointF> Ques_centersF;

        public DataProcess processdata = new DataProcess();

        public OMR() { }

        /// <summary>
        /// Add ID points centers locations.
        /// </summary>
        /// <param name="x">value to increase or decrease the X coordinate for each point center.</param>
        /// <param name="y">value to increase or decrease the Y coordinate for each point center.</param>
        public List<PointF> get_IDCentersLocations(float x, float y)
        {
            #region Add Responses Centers Location For ID

            List<PointF> _list = new List<PointF>();

            float[] id_y_coor = { 215, 234, 254, 274, 293 };

            // Add centers of circles
            for (int i = 0; i < id_y_coor.Length; i++)
            {
                _list.Add(new PointF(252, id_y_coor[i]));
                _list.Add(new PointF(271, id_y_coor[i]));
                _list.Add(new PointF(290, id_y_coor[i]));
                _list.Add(new PointF(310, id_y_coor[i]));
                _list.Add(new PointF(329, id_y_coor[i]));
                _list.Add(new PointF(348, id_y_coor[i]));
                _list.Add(new PointF(368, id_y_coor[i]));
                _list.Add(new PointF(387, id_y_coor[i]));
                _list.Add(new PointF(406, id_y_coor[i]));
                _list.Add(new PointF(426, id_y_coor[i]));
            }
            #endregion

            #region Edit Coordinates by increase or decrease each point center with values of x & y

            if (x == 0 && y == 0)
            {
                return _list;
            }
            else if (x == 0 && y > 0)
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    _list[i] = new PointF(_list[i].X + x, _list[i].Y + y);
                }
            }
            else if (x == 0 && y < 0)
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    _list[i] = new PointF(_list[i].X + x, _list[i].Y + y);

                    if (_list[i].Y < 0)
                    {
                        _list[i] = new PointF(_list[i].X, 0);
                    }
                }
            }
            else if (x > 0 && y == 0)
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    _list[i] = new PointF(_list[i].X + x, _list[i].Y + y);
                }
            }
            else if (x < 0 && y == 0)
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    _list[i] = new PointF(_list[i].X + x, _list[i].Y + y);
                    if (_list[i].X < 0)
                    {
                        _list[i] = new PointF(0, _list[i].Y);
                    }
                }
            }
            else if (x > 0 && y > 0)
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    _list[i] = new PointF(_list[i].X + x, _list[i].Y + y);
                }
            }
            else if (x < 0 && y > 0)
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    _list[i] = new PointF(_list[i].X + x, _list[i].Y + y);

                    if (_list[i].X < 0)
                    {
                        _list[i] = new PointF(0, _list[i].Y);
                    }
                }
            }
            else if (x > 0 && y < 0)
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    _list[i] = new PointF(_list[i].X + x, _list[i].Y + y);

                    if (_list[i].Y < 0)
                    {
                        _list[i] = new PointF(_list[i].X, 0);
                    }
                }
            }
            else if (x < 0 && y < 0)
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    _list[i] = new PointF(_list[i].X + x, _list[i].Y + y);

                    if (_list[i].X < 0 && _list[i].Y < 0)
                    {
                        _list[i] = new PointF(0, 0);
                    }
                    else if (_list[i].X < 0 && _list[i].Y > 0)
                    {
                        _list[i] = new PointF(0, _list[i].Y);
                    }
                    else if (_list[i].X > 0 && _list[i].Y < 0)
                    {
                        _list[i] = new PointF(_list[i].X, 0);
                    }
                }
            }

            #endregion

            return _list;
        }

        /// <summary>
        /// Add questions points centers locations.
        /// </summary>
        /// <param name="x">value to increase or decrease the X coordinate for each point center</param>
        /// <param name="y">value to increase or decrease the Y coordinate for each point center</param>
        public List<PointF> get_QuesCentersLocations(float x, float y)
        {
            #region Add Responses Centers Location For Questions

            List<PointF> _list = new List<PointF>();

            int[] Ques_y_coor = { 400, 429, 457, 487, 516, 545, 574, 602, 632, 661, 690, 719, 748, 776, 805, 834, 863, 892, 921, 950 };

            // Add centers of circles in the first column
            for (int i = 0; i < Ques_y_coor.Length; i++)
            {
                _list.Add(new PointF(171, Ques_y_coor[i]));
                _list.Add(new PointF(200, Ques_y_coor[i]));
                _list.Add(new PointF(229, Ques_y_coor[i]));
                _list.Add(new PointF(259, Ques_y_coor[i]));
            }

            // Add centers of circles in the Second column
            for (int i = 0; i < Ques_y_coor.Length; i++)
            {
                _list.Add(new PointF(318, Ques_y_coor[i]));
                _list.Add(new PointF(348, Ques_y_coor[i]));
                _list.Add(new PointF(377, Ques_y_coor[i]));
                _list.Add(new PointF(407, Ques_y_coor[i]));
            }

            // Add centers of circles in the third column
            for (int i = 0; i < Ques_y_coor.Length; i++)
            {
                _list.Add(new PointF(469, Ques_y_coor[i]));
                _list.Add(new PointF(499, Ques_y_coor[i]));
                _list.Add(new PointF(529, Ques_y_coor[i]));
                _list.Add(new PointF(557, Ques_y_coor[i]));
            }

            // Add centers of circles in the Forth column
            for (int i = 0; i < Ques_y_coor.Length; i++)
            {
                _list.Add(new PointF(617, Ques_y_coor[i]));
                _list.Add(new PointF(646, Ques_y_coor[i]));
                _list.Add(new PointF(676, Ques_y_coor[i]));
                _list.Add(new PointF(706, Ques_y_coor[i]));
            }
            #endregion

            #region Edit Coordinates by increase or decrease each point center with values of x & y

            if (x == 0 && y == 0)
            {
                return _list;
            }
            else if (x == 0 && y > 0)
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    _list[i] = new PointF(_list[i].X + x, _list[i].Y + y);
                }
            }
            else if (x == 0 && y < 0)
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    _list[i] = new PointF(_list[i].X + x, _list[i].Y + y);

                    if (_list[i].Y < 0)
                    {
                        _list[i] = new PointF(_list[i].X, 0);
                    }
                }
            }
            else if (x > 0 && y == 0)
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    _list[i] = new PointF(_list[i].X + x, _list[i].Y + y);
                }
            }
            else if (x < 0 && y == 0)
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    _list[i] = new PointF(_list[i].X + x, _list[i].Y + y);
                    if (_list[i].X < 0)
                    {
                        _list[i] = new PointF(0, _list[i].Y);
                    }
                }
            }
            else if (x > 0 && y > 0)
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    _list[i] = new PointF(_list[i].X + x, _list[i].Y + y);
                }
            }
            else if (x < 0 && y > 0)
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    _list[i] = new PointF(_list[i].X + x, _list[i].Y + y);

                    if (_list[i].X < 0)
                    {
                        _list[i] = new PointF(0, _list[i].Y);
                    }
                }
            }
            else if (x > 0 && y < 0)
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    _list[i] = new PointF(_list[i].X + x, _list[i].Y + y);

                    if (_list[i].Y < 0)
                    {
                        _list[i] = new PointF(_list[i].X, 0);
                    }
                }
            }
            else if (x < 0 && y < 0)
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    _list[i] = new PointF(_list[i].X + x, _list[i].Y + y);

                    if (_list[i].X < 0 && _list[i].Y < 0)
                    {
                        _list[i] = new PointF(0, 0);
                    }
                    else if (_list[i].X < 0 && _list[i].Y > 0)
                    {
                        _list[i] = new PointF(0, _list[i].Y);
                    }
                    else if (_list[i].X > 0 && _list[i].Y < 0)
                    {
                        _list[i] = new PointF(_list[i].X, 0);
                    }
                }
            }

            #endregion

            return _list;
        }

        /// <summary>
        /// Get answers in the image with list of centers points,
        /// radius to calc the count of white pixels within it
        /// and blob counts for each question and return
        /// a Dictionary each key linked to a list of booleans.
        /// </summary>
        /// <param name="imgToProcess">image to process.</param>
        /// <param name="ptList">list of points to decide which circle is marked.</param>
        /// <param name="radius">radius value for circles arround each center in the list.</param> 
        /// <param name="blobcnt">the number of centers in each row.</param> 
        public Dictionary<int, List<bool>> getAnswers(Mat imgToProcess, List<PointF> ptList, int radius, int blobcnt)
        {
            List<Point> _list = Array.ConvertAll(ptList.ToArray(), Point.Round).ToList();

            // Segmentaion
            gray = new Mat();
            otsu = new Mat();
            binz = new Mat();
            // Convert image to gray scale
            CvInvoke.CvtColor(imgToProcess, gray, ColorConversion.Bgr2Gray);
            // compute optimal Otsu's threshold
            double threshold = CvInvoke.Threshold(gray, otsu, 0, 255, ThresholdType.Binary | ThresholdType.Otsu);
            // apply threshold
            CvInvoke.Threshold(otsu, binz, threshold, 255, ThresholdType.BinaryInv);

            Dictionary<int, List<bool>> tempAnswerData = new Dictionary<int, List<bool>>();
            //int threshold = 150;
            if (blobcnt == 4)
            {
                for (int i = 0; i < (ptList.Count / 4); i++)
                {
                    int[] pixelCnt = new int[4];
                    for (int j = 0; j < 4; j++)
                    {
                        int position = i * 4 + j;
                        Point pt = _list[position];
                        pixelCnt[j] = getWhitePixelsInBlob(binz, pt, radius);
                        if (pixelCnt[j] > threshold)
                        {
                            CvInvoke.Circle(imgToProcess, pt, radius, new Bgr(Color.DarkOrange).MCvScalar, 2);
                        }

                    }
                    // Question number starts with 1 but index starts with 0 so add one while inject new item.
                    DataProcess res = new DataProcess();
                    res.set(tempAnswerData, i + 1, pixelCnt[0] > threshold, pixelCnt[1] > threshold,
                                                   pixelCnt[2] > threshold, pixelCnt[3] > threshold);
                }
            }
            else if (blobcnt == 10)
            {
                int thresh = 100;
                for (int i = 0; i < (ptList.Count / 10); i++)
                {
                    int[] pixelCnt = new int[10];
                    for (int j = 0; j < 10; j++)
                    {
                        int position = i * 10 + j;
                        Point pt = _list[position];
                        pixelCnt[j] = getWhitePixelsInBlob(binz, pt, radius);
                        if (pixelCnt[j] > thresh)
                        {
                            CvInvoke.Circle(imgToProcess, pt, radius, new Bgr(Color.DarkOrange).MCvScalar, 2);
                        }

                    }
                    // Question number starts with 1 but index starts with 0 so add one while inject new item.
                    DataProcess res = new DataProcess();
                    res.set(tempAnswerData, i + 1, pixelCnt[0] > thresh, pixelCnt[1] > thresh, pixelCnt[2] > thresh,
                                                   pixelCnt[3] > thresh, pixelCnt[4] > thresh, pixelCnt[5] > thresh,
                                                   pixelCnt[6] > thresh, pixelCnt[7] > thresh, pixelCnt[8] > thresh,
                                                   pixelCnt[9] > thresh);
                }
            }

            return tempAnswerData;
        }

        /// <summary>
        /// Checks the white value pixels by looping all the pixels values in the square with center at given point.
        /// </summary>
        /// <param name="imgToProcess">Image to process.</param>
        /// <param name="pt">point.</param>
        /// <param name="radius">radius value for circles arround each center in the list.</param> 
        private int getWhitePixelsInBlob(Mat imgToProcess, Point pt, int radius)
        {

            Image<Gray, Byte> imgdata = imgToProcess.ToImage<Gray, Byte>();

            int centerX = (int)pt.X;
            int centerY = (int)pt.Y;
            int cntOfWhite = 0;

            Gray pixel = new Gray();
            double value = pixel.MCvScalar.V0;

            for (int i = (centerX - radius); i < (centerX + radius); i++)
            {
                for (int j = (centerY - radius); j < (centerY + radius); j++)
                {
                    if (j < imgdata.Height && i < imgdata.Width)
                    {
                        pixel = imgdata[j, i];
                        value = pixel.MCvScalar.V0;
                        if (value == 255)
                        {
                            cntOfWhite++;
                        }
                    }
                }
            }
            return cntOfWhite;
        }

        /// <summary>
        /// Detect and draw all blobs in the sheet image, the student marked answers and the correct answers. 
        /// </summary>
        /// <param name="m">scaned student solution sheet image.</param>
        /// <param name="isfolder">inform if there is folder of images.</param>
        /// <param name="x">value to increase or decrease the X coordinate for each point center.</param>
        /// <param name="y">value to increase or decrease the Y coordinate for each point center.</param>
        /// <param name="keyanswers">correct answers.</param> 
        /// <param name="idR">radius value for circles arround id center in the list.</param> 
        /// <param name="QuesR">radius value for circles arround id center in the list.</param>
        /// <param name="matchTime">output total time for computing the homography matrix.</param>
        public Mat ScanDetect(Mat m, bool isfolder, float x, float y, string[] keyanswers, int idR, int QuesR, out long time)
        {

            output = m;
            time = 0;

            Stopwatch watch = Stopwatch.StartNew();

            #region Add Answers to Dictionary

            int z = 1;
            for (int i = 0; i < keyanswers.Length; i++)
            {
                if (keyanswers[i] == "A")
                {
                    processdata.set(processdata.refer_dic, z, true, false, false, false);
                    z++;
                }
                else if (keyanswers[i] == "B")
                {
                    processdata.set(processdata.refer_dic, z, false, true, false, false);
                    z++;
                }
                else if (keyanswers[i] == "C")
                {
                    processdata.set(processdata.refer_dic, z, false, false, true, false);
                    z++;
                }
                else if (keyanswers[i] == "D")
                {
                    processdata.set(processdata.refer_dic, z, false, false, false, true);
                    z++;
                }
            }
            #endregion

            // get Orginal stored Template centers from OMR Class and set shift value for x and y,
            // shift value are (0,0) by default they can be set from setup dialog too.
            List<PointF> IDLocationsF = get_IDCentersLocations(x, y);
            List<PointF> QuesLocationsF = get_QuesCentersLocations(x, y);               

            processdata.id_dic = getAnswers(m, IDLocationsF, idR, 10); // get id number
            processdata.solu_dic = getAnswers(m, QuesLocationsF, QuesR, 4);   // get answers  

                watch.Stop();
                time = watch.ElapsedMilliseconds;

            // convert PointF Lists to Point Lists For drawing 
            List<Point> ID_centers = Array.ConvertAll(IDLocationsF.ToArray(), Point.Round).ToList();
            List<Point> Ques_centers = Array.ConvertAll(QuesLocationsF.ToArray(), Point.Round).ToList();

            if(isfolder == false)
            {
                #region Draw Reference Answers Circles

                foreach (var q in processdata.refer_dic)
                {

                    int position = 0;
                    if (q.Value[0] == true)
                    {
                        position = (q.Key - 1) * 4 + 0;
                        CvInvoke.Circle(output, Ques_centers[position], 3, new Bgr(Color.Green).MCvScalar, 2);
                    }

                    else if (q.Value[1] == true)
                    {
                        position = (q.Key - 1) * 4 + 1;
                        CvInvoke.Circle(output, Ques_centers[position], 3, new Bgr(Color.Green).MCvScalar, 2);
                    }
                    else if (q.Value[2] == true)
                    {
                        position = (q.Key - 1) * 4 + 2;
                        CvInvoke.Circle(output, Ques_centers[position], 3, new Bgr(Color.Green).MCvScalar, 2);
                    }
                    else if (q.Value[3] == true)
                    {
                        position = (q.Key - 1) * 4 + 3;
                        CvInvoke.Circle(output, Ques_centers[position], 3, new Bgr(Color.Green).MCvScalar, 2);
                    }
                }
                #endregion

                #region Draw Circle arround each response

                foreach (Point p in ID_centers)
                {
                    CvInvoke.Circle(output, p, idR, new Bgr(Color.LightBlue).MCvScalar, 1);
                }

                foreach (Point p in Ques_centers)
                {
                    CvInvoke.Circle(output, p, QuesR, new Bgr(Color.LightBlue).MCvScalar, 1);
                }

                #endregion
            }

            return output;
        }

        /// <summary>
        /// Draw the reference image and solution image, the matched features and homography matrix.
        /// </summary>
        /// <param name="modelImage">reference image.</param>
        /// <param name="observedImage">solution image.</param>
        /// <param name="x">value to increase or decrease the X coordinate for each point center.</param>
        /// <param name="y">value to increase or decrease the Y coordinate for each point center.</param>
        /// <param name="matchTime">output total time for computing the homography matrix.</param>
        /// <param name="idR">radius value for circles arround id center in the list.</param> 
        /// <param name="QuesR">radius value for circles arround id center in the list.</param>
        /// <param name="matchTime">output total time for computing the homography matrix.</param>
        /// <param name="_isReference">set true if the image is the reference image and set false the image is the solution image.</param>
        /// <returns>the reference image and Solution image, the matched features and homography matrix.</returns>
        public Mat MatchDetect(Mat modelImage, Mat observedImage, float x, float y, int idR, int QuesR, out long matchTime, bool _isReference)
        {

            using (VectorOfVectorOfDMatch matches = new VectorOfVectorOfDMatch())
            {
                result = new Mat();
                homography = new Mat();
                mask = new Mat();
                gray = new Mat();
                otsu = new Mat();
                binz = new Mat();
                homography = null;

                using (UMat uImage_model = modelImage.GetUMat(AccessType.Read))
                using (UMat uImage_observed = observedImage.GetUMat(AccessType.Read))
                {
                    // We use SURF (Speeded-Up Robust Features) for keypoint detection and description 
                    // http://docs.opencv.org/3.0-beta/doc/py_tutorials/py_feature2d/py_surf_intro/py_surf_intro.html

                    SURF surf = new SURF(hessianThresh);

                    watch = Stopwatch.StartNew();

                    //Detect and extract features from the reference image
                    VectorOfKeyPoint keyPoints_model = new VectorOfKeyPoint();
                    Mat descriptors_model = new Mat();
                    surf.DetectAndCompute(modelImage, null, keyPoints_model, descriptors_model, false);

                    //Detect and extract features from the solution image
                    VectorOfKeyPoint keyPoints_observed = new VectorOfKeyPoint();
                    Mat descriptors_observed = new Mat();
                    surf.DetectAndCompute(observedImage, null, keyPoints_observed, descriptors_observed, false);

                    // Bruteforce, slower but more accurate
                    // FlannBasedMatcher for faster matching with slight loss in accuracy                
                    using (LinearIndexParams ip = new LinearIndexParams())
                    using (SearchParams sp = new SearchParams())
                    using (DescriptorMatcher matcher = new FlannBasedMatcher(ip, sp))
                    //using (DescriptorMatcher matcher = new BFMatcher(DistanceType.L2))
                    {
                        matcher.Add(descriptors_model);
                        matcher.KnnMatch(descriptors_observed, matches, k, null);
                        mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);
                        mask.SetTo(new MCvScalar(255));
                        Features2DToolbox.VoteForUniqueness(matches, uniquenessThreshold, mask);

                        int nonZeroCount = CvInvoke.CountNonZero(mask);
                        if (nonZeroCount >= 4)
                        {
                            nonZeroCount = Features2DToolbox.VoteForSizeAndOrientation(keyPoints_model, keyPoints_observed,
                                matches, mask, 1.5, 20);
                            if (nonZeroCount >= 4)
                                homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(keyPoints_model,
                                    keyPoints_observed, matches, mask, 2);
                        }

                        watch.Stop();
                        matchTime = watch.ElapsedMilliseconds;

                        // get Original stored Template centers from OMR Class and set shift value for x and y,
                        // shift value are (0,0) by default they can be set from setup dialog too.
                        List<PointF> IDLocationsF = get_IDCentersLocations(x, y);
                        List<PointF> QuesLocationsF = get_QuesCentersLocations(x, y);

                        #region transform Orginal Template centers by homography matrix
                        VectorOfPointF IDLocV = new VectorOfPointF(IDLocationsF.ToArray());
                        VectorOfPointF IDLoc_transV = new VectorOfPointF();
                        VectorOfPointF QuesLocV = new VectorOfPointF(QuesLocationsF.ToArray());
                        VectorOfPointF QuesLoc_transV = new VectorOfPointF();
                      
                            CvInvoke.PerspectiveTransform(IDLocV, IDLoc_transV, homography);
                            CvInvoke.PerspectiveTransform(QuesLocV, QuesLoc_transV, homography);                                             

                        #endregion

                        // Convert Transformation vectors to PointF Lists (New Transformed Centers)
                        ID_centersF = new List<PointF>(IDLoc_transV.ToArray());
                        Ques_centersF = new List<PointF>(QuesLoc_transV.ToArray());

                        // we convert PointF Lists to Point Lists For drawing 
                        List<Point> ID_centers = Array.ConvertAll(ID_centersF.ToArray(), Point.Round).ToList();
                        List<Point> Ques_centers = Array.ConvertAll(Ques_centersF.ToArray(), Point.Round).ToList();
                        //Draw the matched keypoints
                        Features2DToolbox.DrawMatches(modelImage, keyPoints_model,
                                                          observedImage, keyPoints_observed,
                                                          matches, result,
                                                          new MCvScalar(255, 0, 255),
                                                          new MCvScalar(255, 0, 255), mask,
                                                          Features2DToolbox.KeypointDrawType.NotDrawSinglePoints
                                                         );
                     

                        if (_isReference == true) // check if this is refernce sheet if true then get its answers
                        {
                            processdata.refer_dic = getAnswers(result, Ques_centersF, QuesR, 4);
                        }
                        else if (_isReference == false) // if false the it's solution sheet then get id and student answers
                        {
                            processdata.id_dic = getAnswers(result, ID_centersF, idR, 10);
                            processdata.solu_dic = getAnswers(result, Ques_centersF, QuesR, 4);
                        }

                        #region draw the projected region on the image

                        if (homography != null)
                        {
                            //draw a rectangle along the projected model
                            Rectangle rect = new Rectangle(Point.Empty, modelImage.Size);
                            PointF[] pts = new PointF[]
                            {
                                 new PointF(rect.Left, rect.Bottom),
                                 new PointF(rect.Right, rect.Bottom),
                                 new PointF(rect.Right, rect.Top),
                                 new PointF(rect.Left, rect.Top)
                            };

                            pts = CvInvoke.PerspectiveTransform(pts, homography);

#if NETFX_CORE
                            Point[] points = Extensions.ConvertAll<PointF, Point>(pts, Point.Round);
#else
                            Point[] points = Array.ConvertAll<PointF, Point>(pts, Point.Round);
#endif
                            using (VectorOfPoint vp = new VectorOfPoint(points))
                            {
                                CvInvoke.Polylines(result, vp, true, new MCvScalar(0, 255, 0, 255), 5);
                            }
                        }
                        #endregion

                        #region Draw Reference Answers Circles

                        foreach (var q in processdata.refer_dic)
                        {

                            int position = 0;
                            if (q.Value[0] == true)
                            {
                                position = (q.Key - 1) * 4 + 0;
                                CvInvoke.Circle(result, Ques_centers[position], 3, new Bgr(Color.Green).MCvScalar, 2);
                            }

                            else if (q.Value[1] == true)
                            {
                                position = (q.Key - 1) * 4 + 1;
                                CvInvoke.Circle(result, Ques_centers[position], 3, new Bgr(Color.Green).MCvScalar, 2);
                            }
                            else if (q.Value[2] == true)
                            {
                                position = (q.Key - 1) * 4 + 2;
                                CvInvoke.Circle(result, Ques_centers[position], 3, new Bgr(Color.Green).MCvScalar, 2);
                            }
                            else if (q.Value[3] == true)
                            {
                                position = (q.Key - 1) * 4 + 3;
                                CvInvoke.Circle(result, Ques_centers[position], 3, new Bgr(Color.Green).MCvScalar, 2);
                            }
                        }
                        #endregion

                        #region Draw Circle arround each response
                        foreach (PointF p in ID_centersF)
                        {
                            CvInvoke.Circle(result, Point.Round(p), idR, new Bgr(Color.LightBlue).MCvScalar, 1);
                        }

                        foreach (PointF p in Ques_centersF)
                        {
                            CvInvoke.Circle(result, Point.Round(p), QuesR, new Bgr(Color.LightBlue).MCvScalar, 1);
                        }

                        #endregion


                        return result;

                    }
                }
            }
        }

        /// <summary>
        /// Show grade results. 
        /// </summary>
        /// <param name="answers">sist of strings represents student answers.</param>
        /// <param name="mark">student mark.</param>
        /// <param name="id">student ID.</param>
        public void getResult(out string id, out double mark, out List<string> answers, out List<bool> comp, out Dictionary<int,List<bool>> solu )
        {
         
            mark = 0;
            id = "";

            solu = processdata.solu_dic; //output student answers

            //List holds comparison result between Student Answers and Correct refernce answers for each question,
            comp = processdata.compare(processdata.refer_dic, processdata.solu_dic);  //it returns true if Student answer is correct and false 
                                                                                      //if it's not correct
                      
            answers = new List<string>(processdata.solu_dic.Count);  //list holds result string to show as // Example "1 : True Student answer is: A"
            List<string> temp = processdata.printstring(processdata.solu_dic); //list holds marked choices as strings (A or B or C or D)

            for (int i = 0; i < comp.Count; i++)
            {
               answers.Add((i + 1).ToString() + ":" + comp[i].ToString() + ":" + temp[i]);
            }

            foreach (var c in comp) // claculate total Mark by adding true answers
            {
                if (c == true)
                {
                    mark += 1;
                }
            }

            // get and assign student id to string // sample output: "20583"
            List<string> _idlist = processdata.printstring(processdata.id_dic);
            foreach (var n in _idlist) 
            {
                id = id.Trim() + n;
            }
        }     
    
    }
}
