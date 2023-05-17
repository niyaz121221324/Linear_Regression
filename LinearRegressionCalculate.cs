using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using LinearRegressionApp.csproj.Model;

namespace LinearRegressionApp.csproj
{
    class LinearRegressionCalculate
    {
        /// <summary>Значение r^2 в строке. Используется для того, чтобы дать представление о точности заданных входных значений</summary>
        public static double rSquared { get; set; }
        /// <summary>Значение y-перехвата строки (т.е. y = ax + b, yIntercept равно b)</summary>
        public static double yIntercept { get; set; }
        /// <summary>Наклон линии (т.е. y = ax + b, наклон равен a).</summary>
        public static double slope { get; set; }

        public Series GetFunc(double[] x, double[] y)
        {
            if (x.Length != y.Length)
                throw new ArgumentException("Размеры массивов должны совпадать");

            Series series = new Series();
            LinearRegressionModel coef = GetCoef(x, y);

            for (int i = 0; i < x.Length * y.Length * 2; i++)
                series.Points.Add(new DataPoint(i, coef.Slope * i + coef.Intercept));

            return series;
        }

        private LinearRegressionModel GetCoef(double[] xVals, double[] yVals)
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
                sumCodeviates += xVals[i] * yVals[i];
                sumOfX += xVals[i]; 
                sumOfY += yVals[i];
                sumOfXSq += xVals[i] * xVals[i];
                sumOfYSq += yVals[i] * yVals[i];
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

            rSquared = dblR * dblR;
            yIntercept = meanY - ((sCo / ssX) * meanX);
            slope = sCo / ssX;

            return new LinearRegressionModel(slope, yIntercept);
        }

        public double CalculatePrediction(double input)
        {
            return (input * slope) + yIntercept;
        }
    }
}

