using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace LinearRegressionApp.csproj
{
    class CsvParser
    {
        public DataTable CsvToDataTableParse(string path)
        {
            DataTable dt = new DataTable();
            string[] lines = File.ReadAllLines(path);
            string[] headers = lines[0].Split(',');

            foreach (string header in headers)
                dt.Columns.Add(header);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(',');
                DataRow dr = dt.NewRow();
                for (int j = 0; j < headers.Length; j++)
                    dr[j] = fields[j];
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public double[] GetDoubleArray(DataTable dt, string attributeName, int numberOfRows)
        {
            double[] result = new double[numberOfRows];

            for (int i = 0; i < numberOfRows; i++)
                result[i] = Convert.ToDouble(dt.Rows[i][attributeName]);
            return result;
        }
    }
}
