using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RandomizerAnalyzator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string PARSE_STRING = "yyyy/MM/dd HH:mm:ss";

        Result first1;
        Result first2;
        Result first3;
        Result first4;
        Result second1;
        Result second2;
        Result second3;
        Result third1;
        Result third2;
        Result third3;
        Result fourth1;
        Result fourth2;
        Result fourth3;
        Result sound;
        Result picture;

        List<string> firstTime;
        List<string> secondTime;
        List<string> thirdTime;
        List<string> fourhTime;

        string firstTitle;
        string secondTitle;
        string thirdTitle;
        string fourthTitle;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void browseButtonClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".png";
            dlg.Filter = "TXT Files (*.txt)|*.txt|ALL Files (*.*)|*.*";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                filePath.Text = filename;
            }
        }

        private void split1(string value, List<string> first)
        {
            var values = value.Split(',');
            first.Add(values[0]);
        }

        private void split3(string value, List<string> first, List<string> second, List<string> third)
        {
            var values = value.Split(',');
            first.Add(values[0]);
            second.Add(values[1]);
            third.Add(values[2]);
        }
        private void split4(string value, List<string> first, List<string> second, List<string> third, List<string> fourth)
        {
            var values = value.Split(',');
            first.Add(values[0]);
            second.Add(values[1]);
            third.Add(values[2]);
            fourth.Add(values[3]);
        }

        private void openFile(object sender, RoutedEventArgs e)
        {
            firstTitle = "";
            secondTitle = "";
            thirdTitle = "";
            fourthTitle = "";

            firstTime = new List<string>();
            secondTime = new List<string>();
            thirdTime = new List<string>();
            fourhTime = new List<string>();

            Regex g = new Regex(@"(\d{4}\/\d{1,2}\/\d{1,2} \d{1,2}:\d{1,2}:\d{1,2})-(\w+):([^\|]*)");
            SortedSet<string> names = new SortedSet<string>();

            List<string> first1 = new List<string>();
            List<string> first2 = new List<string>();
            List<string> first3 = new List<string>();
            List<string> first4 = new List<string>();
            List<string> second1 = new List<string>();
            List<string> second2 = new List<string>();
            List<string> second3 = new List<string>();
            List<string> third1 = new List<string>();
            List<string> third2 = new List<string>();
            List<string> third3 = new List<string>();
            List<string> fourth1 = new List<string>();
            List<string> fourth2 = new List<string>();
            List<string> fourth3 = new List<string>();
            List<string> sound = new List<string>();
            List<string> picture = new List<string>();

            if (filePath.Text != "")
            {
                if (filePath.Text.Contains("record_") && filePath.Text.Contains(".vvv"))
                {
                    using (StreamReader r = new StreamReader(filePath.Text))
                    {
                        string line;
                        for (int i = 0; i < 35; i++)
                        {
                            line = r.ReadLine();
                            if (line.Contains("Valid range for data"))
                            {
                                break;
                            }
                        }
                        while ((line = r.ReadLine()) != null)
                        {
                            sound.Add(line);
                        }
                    }

                    this.sound = new Result(sound);

                    CalculateSound();
                }
                else if (filePath.Text.Contains("a_") && filePath.Text.Contains(".vvp"))
                {
                    using (StreamReader r = new StreamReader(filePath.Text))
                    {
                        string line;
                        while ((line = r.ReadLine()) != null)
                        {
                            picture.Add(line);
                        }
                    }

                    this.picture = new Result(picture);

                    CalculatePicture();
                }
                else
                {
                    using (StreamReader r = new StreamReader(filePath.Text))
                    {
                        string line;
                        while ((line = r.ReadLine()) != null)
                        {
                            Match m = g.Match(line);
                            if (m.Success)
                            {
                                names.Add(m.Groups[2].Value);
                                switch (m.Groups[2].Value)
                                {
                                    case "linacc":
                                        firstTime.Add(m.Groups[1].Value);
                                        split3(m.Groups[3].Value, first1, first2, first3);
                                        firstTitle = "Linear Acceleration";
                                        break;
                                    case "proximity":
                                        firstTime.Add(m.Groups[1].Value);
                                        split1(m.Groups[3].Value, first1);
                                        firstTitle = "Proximity";
                                        break;
                                    case "rotvec":
                                        firstTime.Add(m.Groups[1].Value);
                                        split4(m.Groups[3].Value, first1, first2, first3, first4);
                                        firstTitle = "Rotation Vector";
                                        break;

                                    case "light":
                                        secondTime.Add(m.Groups[1].Value);
                                        split1(m.Groups[3].Value, second1);
                                        secondTitle = "Light";
                                        break;
                                    case "acc":
                                        secondTime.Add(m.Groups[1].Value);
                                        split3(m.Groups[3].Value, second1, second2, second3);
                                        secondTitle = "Acceleration";
                                        break;
                                    case "gravity":
                                        secondTime.Add(m.Groups[1].Value);
                                        split3(m.Groups[3].Value, second1, second2, second3);
                                        secondTitle = "Gravity";
                                        break;

                                    case "gyro":
                                        thirdTime.Add(m.Groups[1].Value);
                                        split3(m.Groups[3].Value, third1, third2, third3);
                                        thirdTitle = "Gyroscope";
                                        break;

                                    case "magn":
                                        fourhTime.Add(m.Groups[1].Value);
                                        split3(m.Groups[3].Value, fourth1, fourth2, fourth3);
                                        fourthTitle = "Magnetometer";
                                        break;
                                }
                            }
                        }
                    }
                    
                    this.first1 = new Result(first1);
                    this.first2 = new Result(first2);
                    this.first3 = new Result(first3);
                    this.first4 = new Result(first4);
                    this.second1 = new Result(second1);
                    this.second2 = new Result(second2);
                    this.second3 = new Result(second3);
                    this.third1 = new Result(third1);
                    this.third2 = new Result(third2);
                    this.third3 = new Result(third3);
                    this.fourth1 = new Result(fourth1);
                    this.fourth2 = new Result(fourth2);
                    this.fourth3 = new Result(fourth3);

                    Calculate();
                }
            }
        }

        private string getTime(List<string> times)
        {
            StringBuilder sb = new StringBuilder();
            DateTime from = DateTime.ParseExact(times.FirstOrDefault(), PARSE_STRING, System.Globalization.CultureInfo.InvariantCulture);
            DateTime to = DateTime.ParseExact(times.Last(), PARSE_STRING, System.Globalization.CultureInfo.InvariantCulture);

            sb.Append("Date:     " + from.ToLongDateString() + "\n");
            sb.Append(from.ToLongTimeString() + "-" + to.ToLongTimeString() + "\n");
            TimeSpan diff = to - from;
            sb.Append("Duration: " + diff.Hours + "h " + diff.Minutes + "m " + diff.Seconds + "s\n");
            diff = new TimeSpan(diff.Ticks / times.Count);
            sb.Append("Average:  " + diff.Minutes + "m " + diff.Seconds + "s " + diff.Milliseconds + "ms\n");

            return sb.ToString();
        }

        private void Calculate()
        {
            if (firstTitle != "")
            {
                firstResults.Text = firstTitle + "\n" + getTime(firstTime) + "Count: " + first1.getCount() + "\n" + first1.ToHstring()
                + "\n" + first2.ToHstring() + "\n" + first3.ToHstring() + "\n" + first4.ToHstring();
            }
            else firstResults.Text = "";
            if(secondTitle != "")
            {
                secondResults.Text = secondTitle + "\n" + getTime(secondTime) + "Count: " + second1.getCount() + "\n" + second1.ToHstring()
                + "\n" + second2.ToHstring() + "\n" + second3.ToHstring();
            }
            else secondResults.Text = "";
            if (thirdTitle != "")
            {
                thirdResults.Text = thirdTitle + "\n" + getTime(thirdTime) + "Count: " + third1.getCount() + "\n" + third1.ToHstring()
                + "\n" + third2.ToHstring() + "\n" + third3.ToHstring();
            }
            else thirdResults.Text = "";
            if (fourthTitle != "")
            {
                fourthResults.Text = fourthTitle + "\n" + getTime(fourhTime) + "Count: " + fourth1.getCount() + "\n" + fourth1.ToHstring()
                + "\n" + fourth2.ToHstring() + "\n" + fourth3.ToHstring();
            }
            else fourthResults.Text = "";
        }

        private void CalculateSound()
        {
            firstResults.Text = "SOUND\nCount: " + sound.getCount() + "\n" + sound.ToHstring();
            secondResults.Text = "";
            thirdResults.Text = "";
            fourthResults.Text = "";
        }
        private void CalculatePicture()
        {
            firstResults.Text = "PICTURE\nCount: " + picture.getCount() + "\n" + picture.ToHstring();
            secondResults.Text = "";
            thirdResults.Text = "";
            fourthResults.Text = "";
        }
    }
}
