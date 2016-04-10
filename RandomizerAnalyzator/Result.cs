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

        private double Hmin = Convert.ToDouble(0);
        private double H = Convert.ToDouble(0);
        private int maxCount = -1;
        private int count = 0;

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
                if(current.Value > maxCount)
                {
                    maxCount = current.Value;
                }

                double xyz = -Math.Log(Convert.ToDouble(current.Value) / count, 2.0);
                H += (Convert.ToDouble(current.Value) / count) * (-xyz);
            }

            Hmin = -Math.Log(Convert.ToDouble(maxCount) / count, 2.0);
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

        public double getHmin()
        {
            return Hmin;
        }

        public double getH()
        {
            return -H;
        }

        public int getCount()
        {
            return count;
        }

        public int getMaxCount()
        {
            return maxCount;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Count: " + getCount() + "\n");
            sb.Append("H =    " + getH() + "\n");
            sb.Append("Hmin   " + getHmin() + " [" + getMaxCount() + "]");

            return sb.ToString();
        }

        public string ToHstring()
        {
            return "H =    " + getH() + "\nHmin   " + getHmin() + " [" + getMaxCount() + "]";
        }
    }
}
