using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using CsvHelper;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;

namespace LinearRegressionApp.csproj
{
    public partial class Form1 : Form
    {
        private LinearRegressionCalculate regression = new LinearRegressionCalculate();
        private CsvParser parser = new CsvParser();

        public Form1()
        {
            InitializeComponent();
        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void CreateFunction(Series series, double[] x, double[] y)
        {
            Chart chart = new Chart();

            chart.ChartAreas.Add(new ChartArea());

            series.ChartType = SeriesChartType.Line;
            series.Color = Color.Red;
            series.BorderWidth = 4;

            Series series1 = new Series();
            for (int i = 0; i < x.Length; i++)
                series1.Points.Add(new DataPoint(x[i], y[i]));
            series1.ChartType = SeriesChartType.Point;
            series1.Color = Color.Blue;
            series1.BorderWidth = 3;

            chart.Series.Add(series);
            chart.Series.Add(series1);
            chart.Dock = DockStyle.Fill;
            Form chartForm = new Form();
            chartForm.Controls.Add(chart);
            chartForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[] xValues = parser.GetDoubleArray(parser.CsvToDataTableParse("avengers.csv"), "Appearances", Convert.ToInt32(countTb.Text));
            double[] yValues = parser.GetDoubleArray(parser.CsvToDataTableParse("avengers.csv"), "Year", Convert.ToInt32(countTb.Text));

            DataTable list = parser.CsvToDataTableParse("avengers.csv");

            CreateFunction(regression.GetFunc(xValues, yValues), xValues, yValues);
        }

        private double[] GetArray(int lentgth)
        {
            Random random = new Random();
            double[] array = new double[lentgth];
            for (int i = 0; i < array.Length; i++)
                array[i] = random.Next(0, 101);
            return array;
        }
    }
}
