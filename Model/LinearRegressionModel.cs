using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegressionApp.csproj.Model
{
    class LinearRegressionModel
    {
        public LinearRegressionModel(double slope, double intercept)
        {
            Slope = slope;
            Intercept = intercept;
        }

        public double Slope { get; private set; }
        public double Intercept { get; private set; }
    }
}
