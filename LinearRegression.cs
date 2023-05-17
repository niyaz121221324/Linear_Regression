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

        private Tuple<double, double> GetCoef(double[] xVals, double[] yVals)
        {
            if (xVals.Length != yVals.Length)
                throw new ArgumentException("Размеры массивов должны совпадать");

            double sumOfX = 0;
            double sumOfY = 0;
            double sumOfXSq = 0;
            double sumOfYSq = 0;
            double sumCodeviates = 0;

            for (var i = 0; i < xVals.Length; i++)
            {
                var x = xVals[i];
                var y = yVals[i];
                sumCodeviates += x * y;
                sumOfX += x;
                sumOfY += y;
                sumOfXSq += x * x;
                sumOfYSq += y * y;
            }

            var count = xVals.Length;
            var ssX = sumOfXSq - ((sumOfX * sumOfX) / count);
            var ssY = sumOfYSq - ((sumOfY * sumOfY) / count);

            var rNumerator = (count * sumCodeviates) - (sumOfX * sumOfY);
            var rDenom = (count * sumOfXSq - (sumOfX * sumOfX)) * (count * sumOfYSq - (sumOfY * sumOfY));
            var sCo = sumCodeviates - ((sumOfX * sumOfY) / count);

            var meanX = sumOfX / count;
            var meanY = sumOfY / count;
            var dblR = rNumerator / Math.Sqrt(rDenom);

            double rSquared = dblR * dblR;
            double yIntercept = meanY - ((sCo / ssX) * meanX);
            double slope = sCo / ssX;

            return Tuple.Create(slope, yIntercept);
        }
    }
}
