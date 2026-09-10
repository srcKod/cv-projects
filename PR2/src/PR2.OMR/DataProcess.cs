//-----------------------------------------------------------------------------------------------
// this Class has the necessary methodes which handles Data Processing section used by OMR Class.
//-----------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR2.OMR
{
   public class DataProcess
    {    
        
        public Dictionary<int, List<bool>> refer_dic = new Dictionary<int, List<bool>>(80); // Correct Answers
        public Dictionary<int, List<bool>> id_dic = new Dictionary<int, List<bool>>(5);     // Id
        public Dictionary<int, List<bool>> solu_dic = new Dictionary<int, List<bool>>(80);  // Student Answers

        /// <summary>
        /// Get choice from dictionary.
        /// </summary>
        /// <param name="dic">dictionary.</param>
        /// <param name="key">Question key number.</param>
        /// <param name="choicennum">Choice number.</param>
        public bool get(Dictionary<int, List<bool>> dic, int key, int choicennum)
        {
            bool choice = false;
            if (dic.ContainsKey(key))
            {
                if (dic[key].Count == 4)
                {
                    switch (choicennum)
                    {
                        case 0:
                            choice = dic[key][0];
                            break;
                        case 1:
                            choice = dic[key][1];
                            break;
                        case 2:
                            choice = dic[key][2];
                            break;
                        case 3:
                            choice = dic[key][3];
                            break;
                        default:
                            break;
                    }
                }
                else if (dic[key].Count == 10)
                {
                    switch (choicennum)
                    {
                        case 0:
                            choice = dic[key][0];
                            break;
                        case 1:
                            choice = dic[key][1];
                            break;
                        case 2:
                            choice = dic[key][2];
                            break;
                        case 3:
                            choice = dic[key][3];
                            break;
                        case 4:
                            choice = dic[key][4];
                            break;
                        case 5:
                            choice = dic[key][5];
                            break;
                        case 6:
                            choice = dic[key][6];
                            break;
                        case 7:
                            choice = dic[key][7];
                            break;
                        case 8:
                            choice = dic[key][8];
                            break;
                        case 9:
                            choice = dic[key][9];
                            break;
                        default:
                            break;
                    }
                }
            }
            return choice;
        }

        /// <summary>
        /// Set 4 boolean values To dictionary.
        /// </summary>
        /// <param name="dic">dictionary.</param>
        /// <param name="x"> dictionary key.</param>
        /// <param name="a1">first bool value.</param>
        /// <param name="a2">second bool value.</param>
        /// <param name="a3">third bool value.</param>
        /// <param name="a4">fourth bool value Dictionary.</param>
        public Dictionary<int, List<bool>> set (Dictionary<int, List<bool>> dic, int x, bool a1, bool a2, bool a3, bool a4)
        {
            List<bool> _list = new List<bool>(4);
            _list.Add(a1);
            _list.Add(a2);
            _list.Add(a3);
            _list.Add(a4);
            //
            if (!dic.ContainsKey(x))
                dic.Add(x, _list);
            //
            return dic;
        }

        /// <summary>
        /// Set 10 boolean values To dictionary.
        /// </summary>
        /// <param name="dic">dictionary.</param>
        /// <param name="x"> dictionary key.</param>
        /// <param name="b1">first bool value.</param>
        /// <param name="b2">second bool value.</param>
        /// <param name="b3">third bool value.</param>
        /// <param name="b4">fourth bool value.</param>
        /// <param name="b5">fifth bool value.</param>
        /// <param name="b6">sixth bool value.</param>
        /// <param name="b7">seventh bool value.</param>
        /// <param name="b8">eighth bool value.</param>
        /// <param name="b9">ninth bool value Dictionary.</param>
        /// <param name="b10">tenth bool value.</param>
        public Dictionary<int, List<bool>> set (Dictionary<int, List<bool>> dic, int x, bool b1, bool b2, bool b3,
                                                                                        bool b4, bool b5, bool b6,
                                                                                        bool b7, bool b8, bool b9, bool b10)
        {
            List<bool> _list = new List<bool>(10);
            _list.Add(b1);
            _list.Add(b2);
            _list.Add(b3);
            _list.Add(b4);
            _list.Add(b5);
            _list.Add(b6);
            _list.Add(b7);
            _list.Add(b8);
            _list.Add(b9);
            _list.Add(b10);
            //
            if (!dic.ContainsKey(x))
                dic.Add(x, _list);
            //
            return dic;
        }

        /// <summary>
        /// Compare two dictionaries.
        /// returns a list of booleans each value represnts the result of the comparision between each pair of dictionaries values.
        /// </summary>
        /// <param name="ref_dic">The dictionary of Refernce Answers.</param>
        /// <param name="sol_dic">The dictionary of Solution Answers.</param>
        public List<bool> compare(Dictionary<int, List<bool>> ref_dic, Dictionary<int, List<bool>> sol_dic)
        {
            List<bool> resule = new List<bool>();   
            foreach (var q in ref_dic)
            {
                bool test = true;
                List<bool> a1 = q.Value;
                List<bool> b1 = sol_dic[q.Key];
                if (a1.Count == b1.Count)
                {
                    //
                    for (int v = 0; v < a1.Count; v++)
                    {
                        if (a1[v] != b1[v])
                         test = false; 
                    }
                    resule.Add(test);
                }
              }
            
            return resule;
        }

        /// <summary>
        /// Print boolean elements of the dictionary,
        /// // As example it returns "1 : true,false,flase,false" if booleans count 4.
        /// </summary>
        /// <param name="dic">dictionary.</param>
        public List<string> printbool(Dictionary<int, List<bool>> dic)
        {
            List<string> temp = new List<string>(dic.Count);

            foreach (var q in dic)
             {
                if (q.Value.Count == 4)
                {
                        //sample output: "1 : true,false,flase,false"
                        temp.Add(q.Key.ToString() + " : " + q.Value[0].ToString() + " " + q.Value[1].ToString()
                                                  + " "   + q.Value[2].ToString() + " " + q.Value[3].ToString());                   
                }
                else if (q.Value.Count == 10) // condition for id dictionary
                {                    
                        //sample output: "1 : false,false,true,false,false,false,false,flase,false.false"
                        temp.Add(q.Key.ToString() + " : " + q.Value[0].ToString() + " " + q.Value[1].ToString()
                                                  + " "   + q.Value[2].ToString() + " " + q.Value[3].ToString()
                                                  + " "   + q.Value[4].ToString() + " " + q.Value[5].ToString()
                                                  + " "   + q.Value[6].ToString() + " " + q.Value[7].ToString()
                                                  + " "   + q.Value[8].ToString() + " " + q.Value[9].ToString());
                    }
                }
            
            return temp;
        }

        /// <summary>
        /// Print true boolean elements of a Dictionary by a matched strings (A or B or C or D),
        /// // As example it returns "1:AB".
        /// </summary>
        /// <param name="dic">dictionary.</param>
        public List<string> printstring(Dictionary<int, List<bool>> dic)
        {
            List<string> temp = new List<string>(dic.Count);

            foreach (var q in dic)
            {              
                if (q.Value.Count == 4) {

                    string[] c = { "A", "B", "C", "D" };
                    List<string> choices = new List<string>(c);
                    string t = "";

                    for (int i = 0; i < 4; i++)
                     {
                        if ( q.Value[i]== true)
                        {
                            t = t.Trim() + choices[i];  //sample output: "BC"                                                                    
                        }
                        else { t = t.Trim(); }
                    }
                        //sample output: "1 : AD" 
                        temp.Add(t);
                        t = "";                    
                }
                else if (q.Value.Count == 10) {  // condition for id dictionary 

                    string[] s = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                    List<string> choi = new List<string>(s);
                    string tt = "";

                    for (int i = 0; i < 10; i++)
                        {
                            if (q.Value[i] == true)
                            {
                                tt = tt.Trim() + choi[i];  //sample output: "BC"                                                                    
                            }
                            else { tt = tt.Trim(); }
                        }
                        //sample output: "1 : AD" 
                        temp.Add(tt);
                        tt = "";                    

                }
            }

            return temp ;
        }

    }
}
