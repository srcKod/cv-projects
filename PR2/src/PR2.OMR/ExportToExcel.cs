using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Data.SQLite;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Drawing.Chart;

namespace PR2.OMR
{
    public class ExportToExcel
    {
        public FileInfo CreateSheet()
        {
            string sheetName = "Results -" + DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss") + ".xlsx";
            var filepath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        
            // Create the file using the FileInfo object
            var file = new FileInfo(Path.Combine(filepath +"\\"+ sheetName));
            if (file.Exists)
                file.Delete();

            // Create the package and make sure you wrap it in a using statement
            using (var package = new ExcelPackage(file))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Results - " + DateTime.Now.ToShortDateString());

                // Start adding the header the first row
                worksheet.Cells[1, 1].Value = "Result ID";
                worksheet.Cells[1, 2].Value = "mark";
                worksheet.Cells[1, 3].Value = "Answers";
                worksheet.Cells[1, 4].Value = "Grading Date";
                worksheet.Cells[1, 5].Value = "Student ID";
                worksheet.Cells[1, 6].Value = "Course ID";

                // format the first row of the heade.
                using (var range = worksheet.Cells[1, 1, 1, 6])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.Black);
                    range.Style.Font.Color.SetColor(Color.WhiteSmoke);
                    range.Style.ShrinkToFit = false;
                }

                // Add some formatting to the worksheet
                worksheet.TabColor = Color.Green;
                worksheet.DefaultRowHeight = 12;
                worksheet.DefaultColWidth = 25;
                worksheet.HeaderFooter.FirstFooter.LeftAlignedText = string.Format("Generated: {0}", DateTime.Now.ToShortDateString());
       
                worksheet.Row(1).Height = 20;

                int rownumber = 1;
                int colnumber = 0;
                string cs = Properties.Settings.Default.DBConnectionString;
                object data ;
                using (SQLiteConnection con = new SQLiteConnection(cs))
                {
                    con.Open();
                    string stmt = "SELECT * FROM Results";
                    using (SQLiteCommand cmd = new SQLiteCommand(stmt, con))
                    {
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read()) // Reading Rows
                            {
                                for (colnumber = 0; colnumber <= rdr.FieldCount - 1; colnumber++) // Looping throw colums
                                {     
                                    data = rdr.GetValue(colnumber);
                                    worksheet.Cells[rownumber + 1, colnumber + 1].Value = data;
                                    worksheet.Cells[rownumber + 1, colnumber + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells[rownumber + 1, colnumber + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                }
                                rownumber++; //add new row
                            }
                        }
                    }
                    con.Close();
                }

                //Setting AVERAGE Formula
                worksheet.Cells[rownumber + 2, 1].Value = "Mark AVERAGE";
                worksheet.Cells[rownumber + 2, 1].Style.Font.Bold = true;
                worksheet.Cells[rownumber + 2, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[rownumber + 2, 1].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                worksheet.Cells[rownumber + 2, 2].Formula = "AVERAGE(" + worksheet.Cells[2, 2].Address + ":" + worksheet.Cells[rownumber, 2].Address + ")";

                //Setting MAX Formula
                worksheet.Cells[rownumber + 3, 1].Value = "MAX MARK";
                worksheet.Cells[rownumber + 3, 1].Style.Font.Bold = true;
                worksheet.Cells[rownumber + 3, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[rownumber + 3, 1].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
                worksheet.Cells[rownumber + 3, 2].Formula = "MAX(" + worksheet.Cells[2, 2].Address + ":" + worksheet.Cells[rownumber, 2].Address + ")";

                //Setting MIN Formula
                worksheet.Cells[rownumber + 4, 1].Value = "MIN MARK";
                worksheet.Cells[rownumber + 4, 1].Style.Font.Bold = true;
                worksheet.Cells[rownumber + 4, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[rownumber + 4, 1].Style.Fill.BackgroundColor.SetColor(Color.Pink);
                worksheet.Cells[rownumber + 4, 2].Formula = "MIN(" + worksheet.Cells[2, 2].Address + ":" + worksheet.Cells[rownumber, 2].Address + ")";

                //var myChart = worksheet.Drawings.AddChart("chart", eChartType.ColumnClustered);

                //// Define series for the chart
                //var series = myChart.Series.Add(worksheet.Cells[2, 2].Address + ":" + worksheet.Cells[rownumber, 2].Address , worksheet.Cells[2, 5].Address + ":" + worksheet.Cells[rownumber, 5].Address);
                //myChart.Border.Fill.Color = System.Drawing.Color.Green;
                //myChart.Title.Text = "My Chart";
                //myChart.SetSize(400, 400);

                //// Add to 6th row and to the 6th column
                //myChart.SetPosition(6, 0, 6, 0);

                // Set some document properties
                package.Workbook.Properties.Title = "Results";
                package.Workbook.Properties.Author = "OMR Grading System";
                package.Workbook.Properties.Company = "skynet";

                package.Save(); //save the workbook
            }

            return file;
        }
    }
}

