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
    public partial class FormB_Spline : System.Windows.Forms.Form
    {
        private double a_x;
        private double b_x;

        private int Size;
        private int N;
        private double h;

        private double[] Bn;

        public FormB_Spline()
        {
            InitializeComponent();

            this.a_x = -5.5;
            this.b_x = 5.5;
            N = 20;

            h = 0.09;

            Size = Convert.ToInt32((b_x - a_x) / h) + 1;
        }

        public void CaclucateSpline()
        {
            for (int j = 1; j <= N; j++)
            {
                Bn = new double[Size];
                double xi = a_x;
                for (int i = 0; i < Size; i++, xi = Math.Round(xi + h, 2))
                {
                    if (xi <= 0)
                        Bn[i] = CalcnBn(xi, j);
                    else
                        Bn[i] = Bn[Size - (i + 1)];
                }

                Draw(Bn,j);
            }
        }
        private void Draw(double[] y, int n)
        {
            string str = "spline " + n;
            chart1.Series.Add(str).ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int i = 0;
            for (double xi = a_x; xi <= b_x; i++, xi += h)
            {
                chart1.Series[str].Points.AddXY(xi, y[i]);
            }
        }

        private double CalcnBn(double xi, int n)
        {
            if (n > 1)
            {
                return (n + 2 * xi) / (2 * n - 2) * CalcnBn(xi + 0.5, n - 1) + (n - 2 * xi) / (2 * n - 2) * CalcnBn(xi - 0.5, n - 1);
            }
            else
            {
                return Xi(xi);
            }           
        }

        private double Xi(double xi)
        {
            if (xi > -0.5 && xi < 0.5) return 1;
            else if (Math.Abs(xi) == 0.5) return 0.5;
            else return 0;
        }

        //private double Xi(double xi)
        //{
        //    if (xi > a_x && xi < b_x) return 1;
        //    else if (Math.Abs(xi) == b_x) return 0.5;
        //    else return 0;
        //}
    }
}
