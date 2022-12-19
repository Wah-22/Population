using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
//using Population.Model;

namespace Population
{
    public partial class Estimate : Form
    {
        public Estimate()
        {
            InitializeComponent();

            string strURL = "http://api.e-stat.go.jp/rest/3.0/app/json/getStatsData?appId=a42c8eefd41c8cd7713f0a22eaa88bb0d7cbc420&lang=E&statsDataId=0003433219&metaGetFlg=Y&cntGetFlg=N&explanationGetFlg=Y&annotationGetFlg=Y&sectionHeaderFlg=1&replaceSpChars=0";
            System.Net.WebRequest reqObjGet = System.Net.WebRequest.Create(strURL);
            reqObjGet.Method = "GET";
            System.Net.HttpWebResponse resObj = null;
            resObj = (System.Net.HttpWebResponse)reqObjGet.GetResponse();

            string strres = null;

            string newstr = null;

            using (Stream stream = resObj.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);

                strres = sr.ReadToEnd();

                newstr = strres.Replace("$", "sign");

                sr.Close();

            }
            //string fileName = @"C:\Jfileread.txt";
            //string text = File.ReadAllText(fileName);
            //string newstr = text.Replace("$", "sign");

            populationEstimate = JsonConvert.DeserializeObject<Statistical.Root>(newstr);
        }

        #region Variables

        Statistical.Root populationEstimate = new Statistical.Root();
        List<Statistical.CLASS_OBJ> lstClassObj = new List<Statistical.CLASS_OBJ>();
        List<Statistical.VALUE> lstValue = new List<Statistical.VALUE>();
        Statistical.TITLE tab_title = new Statistical.TITLE();

        #endregion

        #region Event
        private void Estimate_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;             
             
            lstClassObj = populationEstimate.GET_STATS_DATA.STATISTICAL_DATA.CLASS_INF.CLASS_OBJ;

            lstValue = populationEstimate.GET_STATS_DATA.STATISTICAL_DATA.DATA_INF.VALUE;

            tab_title = populationEstimate.GET_STATS_DATA.STATISTICAL_DATA.TABLE_INF.TITLE;

            string surveydate = populationEstimate.GET_STATS_DATA.STATISTICAL_DATA.TABLE_INF.SURVEY_DATE.ToString();

            cboSurveyDate.Text = surveydate.Substring(0, 4) + "-" + surveydate.Substring(4, 2);


        }
        private void btnLoad_Click(object sender, EventArgs e)
        {

            try
            {
                chart1.Visible = false;
                List<Statistical.CLASS_OBJ_Tab> lstNewClass = new List<Statistical.CLASS_OBJ_Tab>();

                List<List<Statistical.CLASS>> ArrList = new List<List<Statistical.CLASS>>();

                var query = lstClassObj.Select(p => p.CLASS).ToList();

                for (int a = 0; a < query.Count(); a++) // convert system object List<> to custom class List<>
                {

                    if (query[a].ToString().Contains("["))// Array object to List
                    {
                        List<Statistical.CLASS> lst1 = JsonConvert.DeserializeObject<List<Statistical.CLASS>>(query[a].ToString());
                        ArrList.Add(lst1);
                    }
                    else // Object to List
                    {
                        Statistical.CLASS sc = JsonConvert.DeserializeObject<Statistical.CLASS>(query[a].ToString());
                        List<Statistical.CLASS> lst2 = new List<Statistical.CLASS>();
                        lst2.Add(sc);
                        ArrList.Add(lst2);
                    }
                }

                for (int i = 0; i < lstClassObj.Count(); i++) // Convert object CLASS to List<Statistical.CLASS> CLASS
                {
                    Statistical.CLASS_OBJ_Tab newClass = new Statistical.CLASS_OBJ_Tab();
                    newClass.Id = lstClassObj[i].Id;
                    newClass.Name = lstClassObj[i].Name;
                    newClass.CLASS = ArrList[i];
                    lstNewClass.Add(newClass);
                }

                List<Statistical.VALUE> lstValue_Total = new List<Statistical.VALUE>();
                List<Statistical.VALUE> lstValue_Male = new List<Statistical.VALUE>();
                List<Statistical.VALUE> lstValue_Female = new List<Statistical.VALUE>();

                lstValue_Total = lstValue.Where(c => c.Cat01 == "0").ToList();
                lstValue_Male = lstValue.Where(c => c.Cat01 == "1").ToList();
                lstValue_Female = lstValue.Where(c => c.Cat01 == "2").ToList();

                if (rdoMale.Checked == true) { LoadPopulation(lstValue_Male, lstNewClass, "Population Male", 100000000); }
                if (rdoFemale.Checked == true) { LoadPopulation(lstValue_Female, lstNewClass, "Population Female", 100000000); }
                if (rdoTotal.Checked == true) { LoadPopulation(lstValue_Total, lstNewClass, "Population Total", 130000000); }


            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

        }
        #endregion

        #region Function
        private void LoadPopulation(List<Statistical.VALUE> lstData, List<Statistical.CLASS_OBJ_Tab> lstClass,string title,int maxpeople)
        {
            //this.Cursor = Cursors.WaitCursor;
            chart1.Series.Clear();           
            chart1.Titles.Clear();
            chart1.Visible = true;
            chart1.Titles.Add(tab_title.sign).Font = new System.Drawing.Font("Arial", 16, FontStyle.Bold);
          
            chart1.ChartAreas[0].AxisX.Title = title;            
            chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            List<string> lstcity = new List<string>();

            foreach (Statistical.VALUE v in lstData)
            {
                string gender = null;
                string city = null;
                string unit = null;
                foreach (Statistical.CLASS_OBJ_Tab ot in lstClass)
                {
                    for (int i = 0; i < ot.CLASS.Count(); i++)
                    {
                        if (v.Cat01 == ot.CLASS[i].code)
                        {
                            gender = ot.CLASS[i].name;
                        }
                        if (v.Area == ot.CLASS[i].code)
                        {
                            city = ot.CLASS[i].name;
                        }
                    }
                }
                unit = v.sign;

                if (!lstcity.Contains(city + gender))
                {
                    chart1.Series.Add(city + gender);
                    lstcity.Add(city + gender);
                    chart1.Series[city + gender].LegendText = city;
                }

                chart1.ChartAreas[0].AxisY.Minimum = 0;
                chart1.ChartAreas[0].AxisY.Maximum = maxpeople;
                chart1.Series[city + gender]["PointWidth"] = "7";
                chart1.Series[city + gender].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
                //chart1.Series[city + gender].ToolTip= "Area :"+city +"\nGender :"+gender+ "\nPeople :" + Convert.ToInt64(unit).ToString("#,###");
                chart1.Series[city + gender].ToolTip = "Area :" + city + "\nSex :" + gender + "\nPeople :" + unit;
                chart1.Series[city + gender].Points.AddXY(city, unit); 

            }

            //this.Cursor = Cursors.Default;

        }

     
        #endregion



    }
}
