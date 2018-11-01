using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace plot__thesprite
{
    public partial class Form : System.Windows.Forms.Form
    {
        private double[] a;
        private double[] b;
        private double[] c;
        private double[] d;

        private double[] fucn;
        private double[] x;

        private MtdProg metodProd;

        private int N;
        private double H;


        public Form()
        {
            InitializeComponent();
        }


        private void calcBtn_Click(double yN)
        {
            metodProd = new MtdProg(a, b, c, d);

            metodProd.setYN(yN);

            fucn = metodProd.getCalculatedY(yN);               
        }

        void Clear()
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();

            InitializeArrays(N);
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (textBox1.Text.Length != 0)
            {
                N = Convert.ToInt32(textBox1.Text);

                Clear();

                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        {
                            CalcTask1();
                            calcBtn_Click(-1);

                            DrawFirst();
                            break;
                        }
                    case 1:
                        {
                            CalcTask2();
                            calcBtn_Click(1);

                            DrawSecond();
                            break;
                        }

                    case 2:
                        {
                            CalcTask3();
                            calcBtn_Click(Math.E - 2);

                            DrawThird();
                            break;
                        }
                    case 3:
                        {
                            CalcTask4();
                            calcBtn_Click(0);

                            DrawFour();
                            break;
                        }
                }
            }
            else
                MessageBox.Show("ENTER N");
        }

        private void CalcTask1()
        {
            H = 1 / (Convert.ToDouble(N) - 1);

            for (int i = 1; i < N; i++)
            {
                x[i] = i * H;
                a[i] = -2 - H * H;
                b[i] = 1;
                c[i] = 1;
                d[i] = 2 * H * H * x[i];
            }
            a[0] = 1;
            b[0] = 0;
            c[0] = 0;
            d[0] = 0;
            x[0] = 0;
        }


        private void CalcTask2()
        {
            H = 1 / (Convert.ToDouble(N) - 1);
            for (int i = 1; i < N; i++)
            {
                x[i] = i * H;
                a[i] = -4;
                b[i] = 2 + H;
                c[i] = 2 - H;
                d[i] = 2 * H * H;
            }
            a[0] = 1;
            b[0] = -1;
            c[0] = 0;
            d[0] = 0;
            x[0] = 0;
        }


        private void CalcTask3()
        {
            H = 1 / (Convert.ToDouble(N) - 1);
            for (int i = 1; i < N; i++)
            {
                x[i] = i * H;
                a[i] = -4;
                b[i] = 2 - H;
                c[i] = 2 + H;
                d[i] = 0;
            }
            a[0] = 1;
            b[0] = 0;
            c[0] = 0;
            d[0] = -1;
            x[0] = 0;
        }


        private void CalcTask4()
        {
            H = (Math.PI / 2) / (Convert.ToDouble(N) - 1);
            for (int i = 1; i < N; i++)
            {
                x[i] = i * H;
                a[i] = H * H - 2;
                b[i] = 1;
                c[i] = 1;
                d[i] = H * H;
            }

            a[0] = 1;
            b[0] = 0;
            c[0] = 0;
            d[0] = 0;
            x[0] = 0;

        }

        void DrawFirst()
        {
            for (double i = 0; i <= 1; i += 0.5)
            {
                double Y = Math.Sinh(i) / Math.Sinh(1) - 2 * i;
                chart1.Series[0].Points.AddXY(i, Y);
            }

            for (int i = 0; i < N; i++)
            {
                chart1.Series[1].Points.AddXY(x[i], fucn[i]);
            }
        }

        void DrawSecond()
        {
            for (double i = 0; i <= 1; i += 0.5)
            {
                double Y = i + Math.Pow(Math.E, -i) - Math.Pow(Math.E, -1);
                chart1.Series[0].Points.AddXY(i, Y);
            }

            for (int i = 0; i < N; i++)
            {
                chart1.Series[1].Points.AddXY(x[i], fucn[i]);
            }
        }

        void DrawThird()
        {
            for (double i = 0; i <= 1; i += 0.5)
            {
                double Y = Math.Pow(Math.E, i) - 2;
                chart1.Series[0].Points.AddXY(i, Y);
            }

            for (int i = 0; i < N; i++)
            {
                chart1.Series[1].Points.AddXY(x[i], fucn[i]);
            }
        }

        void DrawFour()
        {
            for (double i = 0; i <= Math.PI / 2; i += 0.5)
            {
                double Y = 1 - Math.Sin(i) - Math.Cos(i);
                chart1.Series[0].Points.AddXY(i, Y);
            }

            for (int i = 0; i < N; i++)
            {
                chart1.Series[1].Points.AddXY(x[i], fucn[i]);
            }
        }

        private void InitializeArrays(int N)
        {
            a = new double[N];
            b = new double[N];
            c = new double[N];
            d = new double[N];

            fucn = new double[N];
            x = new double[N];
        }
    }
}
