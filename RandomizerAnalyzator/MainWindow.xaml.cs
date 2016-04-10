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
        public const string PARSE_STRING = "yyyy/MM/dd HH:mm:ss.FFF";

        Result acceleration1;
        Result acceleration2;
        Result acceleration3;
        Result magnetometer1;
        Result magnetometer2;
        Result magnetometer3;
        Result light;
        Result proximity;
        Result rotationvector1;
        Result rotationvector2;
        Result rotationvector3;
        Result rotationvector4;
        Result gravity1;
        Result gravity2;
        Result gravity3;
        Result gyroscope1;
        Result gyroscope2;
        Result gyroscope3;
        Result orientation1;
        Result orientation2;
        Result orientation3;
        Result sound;

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

        private void openFile(object sender, RoutedEventArgs e)
        {
            Regex g = new Regex(@"(\d{4}\/\d{1,2}\/\d{1,2} \d{1,2}:\d{1,2}:\d{1,2}.\d{1,3})\|(\w+):([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*),([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*),([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*)\|(\w+):([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*),([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*),([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*)\|(\w+):([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*),([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*),([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*)\|(\w+):([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*),([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*),([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*)\|(\w+):([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*)\|(\w+):([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*)\|(\w+):([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*),([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*),([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*)\|(\w+):([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*),([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*),([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*)\|(\w+):([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*),([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*),([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*),([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*),([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*)\|(\w+):([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*)\|(\w+):([-]{0,1}\d{0,1}.\d+E[-]{0,1}\d+|[-]{0,1}\d*.\d*)\|");
            StringBuilder sb = new StringBuilder();

            List<string> seconds = new List<string>();

            List<string> acceleration1 = new List<string>();
            List<string> acceleration2 = new List<string>();
            List<string> acceleration3 = new List<string>();
            List<string> magnetometer1 = new List<string>();
            List<string> magnetometer2 = new List<string>();
            List<string> magnetometer3 = new List<string>();
            List<string> light = new List<string>();
            List<string> proximity = new List<string>();
            List<string> rotvec1 = new List<string>();
            List<string> rotvec2 = new List<string>();
            List<string> rotvec3 = new List<string>();
            List<string> rotvec4 = new List<string>();
            List<string> orientation1 = new List<string>();
            List<string> orientation2 = new List<string>();
            List<string> orientation3 = new List<string>();
            List<string> gyroscope1 = new List<string>();
            List<string> gyroscope2 = new List<string>();
            List<string> gyroscope3 = new List<string>();
            List<string> gravity1 = new List<string>();
            List<string> gravity2 = new List<string>();
            List<string> gravity3 = new List<string>();
            List<string> sound = new List<string>();

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
                                seconds.Add(m.Groups[1].Value);

                                acceleration1.Add(m.Groups[3].Value);
                                acceleration2.Add(m.Groups[4].Value);
                                acceleration3.Add(m.Groups[5].Value);
                                magnetometer1.Add(m.Groups[7].Value);
                                magnetometer2.Add(m.Groups[8].Value);
                                magnetometer3.Add(m.Groups[9].Value);
                                light.Add(m.Groups[19].Value);
                                proximity.Add(m.Groups[21].Value);
                                rotvec1.Add(m.Groups[31].Value);
                                rotvec2.Add(m.Groups[32].Value);
                                rotvec3.Add(m.Groups[33].Value);
                                rotvec4.Add(m.Groups[34].Value);
                                gyroscope1.Add(m.Groups[15].Value);
                                gyroscope2.Add(m.Groups[16].Value);
                                gyroscope3.Add(m.Groups[17].Value);
                                gravity1.Add(m.Groups[23].Value);
                                gravity2.Add(m.Groups[24].Value);
                                gravity3.Add(m.Groups[25].Value);
                                orientation1.Add(m.Groups[11].Value);
                                orientation2.Add(m.Groups[12].Value);
                                orientation3.Add(m.Groups[13].Value);
                                //sb.Append(m.Groups[1].Value + ": Line is OK\n");
                            }
                        }
                    }

                    DateTime from = DateTime.ParseExact(seconds.FirstOrDefault(), PARSE_STRING, System.Globalization.CultureInfo.InvariantCulture);
                    DateTime to = DateTime.ParseExact(seconds.Last(), PARSE_STRING, System.Globalization.CultureInfo.InvariantCulture);
                    sb.Append("Date:     " + from.ToLongDateString() + "\n");
                    sb.Append("Start on: " + from.ToLongTimeString() + "\n");
                    sb.Append("Stop on:  " + to.ToLongTimeString() + "\n\n");
                    TimeSpan diff = to - from;
                    sb.Append("Duration: " + diff.Hours + "h " + diff.Minutes + "m " + diff.Seconds + "s\n");
                    diff = new TimeSpan(diff.Ticks / seconds.Count);
                    sb.Append("Average:  " + diff.Minutes + "m " + diff.Seconds + "s " + diff.Milliseconds + "ms");

                    this.acceleration1 = new Result(acceleration1);
                    this.acceleration2 = new Result(acceleration2);
                    this.acceleration3 = new Result(acceleration3);
                    this.magnetometer1 = new Result(magnetometer1);
                    this.magnetometer2 = new Result(magnetometer2);
                    this.magnetometer3 = new Result(magnetometer3);
                    this.light = new Result(light);
                    this.proximity = new Result(proximity);
                    this.rotationvector1 = new Result(rotvec1);
                    this.rotationvector2 = new Result(rotvec2);
                    this.rotationvector3 = new Result(rotvec3);
                    this.rotationvector4 = new Result(rotvec4);
                    this.orientation1 = new Result(orientation1);
                    this.orientation2 = new Result(orientation2);
                    this.orientation3 = new Result(orientation3);
                    this.gyroscope1 = new Result(gyroscope1);
                    this.gyroscope2 = new Result(gyroscope2);
                    this.gyroscope3 = new Result(gyroscope3);
                    this.gravity1 = new Result(gravity1);
                    this.gravity2 = new Result(gravity2);
                    this.gravity3 = new Result(gravity3);

                    textBox.Text = sb.ToString();

                    Calculate();
                }
            }
        }

        private void Calculate()
        {
            accelerometerResults.Text = "Count: " + acceleration1.getCount() + "\n" + acceleration1.ToHstring()
                + "\n" + acceleration2.ToHstring() + "\n" + acceleration3.ToHstring();
            magnetometerResults.Text = "Count: " + magnetometer1.getCount() + "\n" + magnetometer1.ToHstring()
                + "\n" + magnetometer2.ToHstring() + "\n" + magnetometer3.ToHstring();
            lightResults.Text = light.ToString();
            proximityResults.Text = proximity.ToString();
            rotVecResults.Text = "Count: " + rotationvector1.getCount() + "\n" + rotationvector1.ToHstring()
                + "\n" + rotationvector2.ToHstring() + "\n" + rotationvector3.ToHstring() + "\n" + rotationvector4.ToHstring();
            gravityResults.Text = "Count: " + gravity1.getCount() + "\n" + gravity1.ToHstring()
                    + "\n" + gravity2.ToHstring() + "\n" + gravity3.ToHstring();
            gyroscopeResults.Text = "Count: " + gyroscope1.getCount() + "\n" + gyroscope1.ToHstring()
                    + "\n" + gyroscope2.ToHstring() + "\n" + gyroscope3.ToHstring();
            orientationResults.Text = "Count: " + orientation1.getCount() + "\n" + orientation1.ToHstring()
                    + "\n" + orientation2.ToHstring() + "\n" + orientation3.ToHstring();
            hMaxLabel.Content = Math.Log(Convert.ToDouble(light.getCount()), 2.0);
        }

        private void CalculateSound()
        {
            accelerometerResults.Text = "SOUND\nCount: " + sound.getCount() + "\n" + sound.ToHstring()
                + "\n" + sound.ToHstring() + "\n" + sound.ToHstring();
            hMaxLabel.Content = Math.Log(Convert.ToDouble(sound.getCount()), 2.0);
        }
    }
}
