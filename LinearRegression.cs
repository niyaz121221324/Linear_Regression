using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace LinearRegressionApp.csproj
{
    class LinearRegression
    {
        public Series GetFunc(double[] x, double[] y)
        {
            if (x.Length != y.Length)
                throw new ArgumentException("Размеры массивов должны совпадать");

            Series series = new Series();
            Tuple<double, double> coef = GetCoef(x, y);

            for(int i = 0; i < x.Length; i++)
                series.Points.Add(new DataPoint(i, coef.Item1 + coef.Item2 * i));

            return series;
        }

        private Tuple<double, double> GetCoef(double[] x, double[] y)
        {
            if (x.Length != y.Length)
                throw new ArgumentException("Размеры массивов должны совпадать");

            double sumX = 0;
            double sumY = 0;
            double sumXY = 0;
            double sumXX = 0;

            for (int i = 0; i < x.Length; i++)
            {
                sumX += x[i];
                sumY += y[i];
                sumXX += x[i] * x[i];
                sumXY += x[i] * y[i];
            }

            double slope = (x.Length * sumXY - sumX * sumY) / (x.Length * sumXX - sumX * sumX);
            double intercept = (sumY - slope * sumX) / x.Length;

            return Tuple.Create(slope, intercept);
        }
    }
}
