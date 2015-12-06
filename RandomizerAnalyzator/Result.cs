using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomizerAnalyzator
{
    class Result
    {
        Dictionary<double, int> streamedData = new Dictionary<double, int>();

        double Hmax = Convert.ToDouble(0);
        double H = Convert.ToDouble(0);
        int count = 0;

        public Result(List<String> values)
        {
            foreach(var current in values)
            {
                addValue( double.Parse(current, CultureInfo.InvariantCulture));
            }
            calculate();
        }

        public void calculate()
        {
            H = Convert.ToDouble(0);
            foreach (var current in streamedData)
            {
                double xyz = -Math.Log(Convert.ToDouble(current.Value) / count, 2.0);
                H += (Convert.ToDouble(current.Value) / count) * (-xyz);
                //H = -H;
            }
            Hmax = Math.Log(Convert.ToDouble(streamedData.Keys.Count), 2.0);
        }

        public void addValue(double number)
        {
            count++;
            int value;
            if (streamedData.TryGetValue(number, out value))
            {
                value++;
                streamedData[number] = value;
            }
            else
            {
                streamedData.Add(number, 1);
            }
        }

        public double getHmax()
        {
            return Hmax;
        }

        public double getH()
        {
            return -H;
        }

        public int getCount()
        {
            return count;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Count: " + getCount() + "\n");
            sb.Append("H =    " + getH() + "\n");
            sb.Append("Hmax   " + getHmax());

            return sb.ToString();
        }

        public string ToHstring()
        {
            return "H =    " + getH() + "\nHmax   " + getHmax();
        }
    }
}
