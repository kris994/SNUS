using CommonLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Trending
{
    public partial class Form1 : Form
    {

        System.Windows.Forms.Timer MyTimer = new System.Windows.Forms.Timer();

        List<System.Windows.Forms.Timer> timers = new List<System.Windows.Forms.Timer>();

        Dictionary<string, Signal> tags;
        ITrending proxy;

        List<Color> colors = new List<Color>();

        Dictionary<string, double> prethodnaVrednosti;
        Dictionary<string, System.Windows.Forms.DataVisualization.Charting.Series> series = new Dictionary<string, Series>();
        Dictionary<string, System.Windows.Forms.DataVisualization.Charting.Legend> legends = new Dictionary<string, Legend>();


        public Form1(ITrending p)
        {
            tags = new Dictionary<string, Signal>();

            prethodnaVrednosti = new Dictionary<string, double>();

            proxy = p;

            InitializeComponent();
            chart1.ChartAreas[0].AxisX.IsStartedFromZero = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
            //chart1.ChartAreas[0].AxisY.Interval = 5;
            //chart1.Series[0].XValueType = ChartValueType.Time;
            chart1.ChartAreas[0].AxisX.ScaleView.SizeType = DateTimeIntervalType.Seconds;
            chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
            chart1.ChartAreas[0].AxisX.Interval = 0;
            chart1.ChartAreas[0].AxisX.Minimum = DateTime.Now.ToOADate();
            chart1.ChartAreas[0].AxisX.Maximum = DateTime.Now.AddSeconds(10).ToOADate();

            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;
            chart1.ChartAreas[0].AxisY.ScrollBar.Enabled = true;
            chart1.ChartAreas[0].AxisY.IsLabelAutoFit = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Size = 105;
            chart1.ChartAreas[0].AxisY.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;

            colors.Add(Color.DeepPink);
            colors.Add(Color.Turquoise);
            colors.Add(Color.Coral);
            colors.Add(Color.DarkViolet);
            colors.Add(Color.MediumBlue);
            colors.Add(Color.Lime);


            tags = proxy.showTrending();

            for (int i = 0; i < tags.Count; i++)
            {
                Signal tag = tags.ElementAt(i).Value;
                series.Add(tag.TagName, new System.Windows.Forms.DataVisualization.Charting.Series()
                {
                    XValueType = ChartValueType.Time,
                    ChartArea = "ChartArea1",
                    ChartType = SeriesChartType.Line,
                    Name = tag.TagName

                });
                this.chart1.Series.Add(series[tag.TagName]);

                chart1.Series[tag.TagName].ChartType = SeriesChartType.Line;
                chart1.Series[tag.TagName].Color = colors[i % 6];
                chart1.Series[tag.TagName].BorderWidth = 2;

                prethodnaVrednosti[tag.TagName] = tag.Driver.Value;

                legends.Add(tag.TagName, new System.Windows.Forms.DataVisualization.Charting.Legend()
                {
                    Name = tag.TagName
                });
                this.chart1.Legends.Add(legends[tag.TagName]);

            }
            System.Windows.Forms.Timer MyTimer = new System.Windows.Forms.Timer();
            MyTimer.Interval = (200);
            MyTimer.Tick += new EventHandler(MyTimer_Tick);

            timers.Add(MyTimer);
            MyTimer.Start();
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            tags = proxy.showTrending();

            for (int i = 0; i < tags.Count; i++)
            {

                Signal tag = tags.ElementAt(i).Value;

                double prethodna;
                if (prethodnaVrednosti.ContainsKey(tag.TagName))
                {

                    prethodna = prethodnaVrednosti[tag.TagName];
                    chart1.ChartAreas[0].AxisX.Maximum = DateTime.Now.ToOADate();
                    if (tag.Driver.Value != prethodna)
                        //chart1.Series[tag.tagName].Points.Add(tags[tag.tagName].driver.value);
                        chart1.Series[tag.TagName].Points.AddXY(DateTime.Now.ToOADate(), tags[tag.TagName].Driver.Value);
                    else if (tag.IoAddress.Equals("rect"))
                    {
                        // chart1.Series[tag.tagName].Points.Add(tags[tag.tagName].driver.value);
                        chart1.Series[tag.TagName].Points.AddXY(DateTime.Now.ToOADate(), tags[tag.TagName].Driver.Value);
                    }
                    else if (tag.IoAddress.Equals("digital"))
                    {
                        //  chart1.Series[tag.tagName].Points.Add(tags[tag.tagName].driver.value);
                        chart1.Series[tag.TagName].Points.AddXY(DateTime.Now.ToOADate(), tags[tag.TagName].Driver.Value);
                    }

                    prethodnaVrednosti[tag.TagName] = tag.Driver.Value;
                }
                else
                {
                    series.Add(tag.TagName, new System.Windows.Forms.DataVisualization.Charting.Series()
                    {
                        XValueType = ChartValueType.Time,
                        ChartArea = "ChartArea1",
                        ChartType = SeriesChartType.Line,
                        Name = tag.TagName

                    });

                    this.chart1.Series.Add(series[tag.TagName]);

                    chart1.Series[tag.TagName].ChartType = SeriesChartType.Line;
                    chart1.Series[tag.TagName].Color = colors[i];
                    chart1.Series[tag.TagName].BorderWidth = 2;

                    prethodnaVrednosti[tag.TagName] = tag.Driver.Value;


                    legends.Add(tag.TagName, new System.Windows.Forms.DataVisualization.Charting.Legend()
                    {
                        Name = tag.TagName
                    });

                    this.chart1.Legends.Add(legends[tag.TagName]);
                }
            }
        }
    }
}