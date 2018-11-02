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
    public partial class FormSpline : System.Windows.Forms.Form
    {
        private int N;
        private int GU_a;
        private int GU_b;

        private double h;
        private double h1;

        private double[] X;
        private double[] F;


        private double[] S_x;
        private double[] S_y;

        private double[] a, b, c, d;

        private double[] m;

        private MtdProg methodProg;

        public FormSpline()
        {
            InitializeComponent();

            GU_a = 0;
            GU_b = 1;
        }

        public void SetN(int N)
        {
            this.N = N;
        }

        private void Clear()
        {          
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();

            a = null;
            b = null;
            c = null;
            d = null;

            X = new double[N];
            F = new double[N];

            S_x = null;
            S_y = null;
            m = null;

            h = 0;
            h1 = 0;
        }

        public void StartCalculateSpline()
        {
            if (N != 0)
            {
                Clear();

                AlgorithmSpline();

                Draw(X, F, "Function");
                Draw(S_x, S_y, "Spline");

            }
            else MessageBox.Show("Enter N!");
        }

        private void Draw(double[] x, double[] y, string str)
        {
            for(int i = 0, size = x.Length; i < size; i++)
            {
                chart1.Series[str].Points.AddXY(x[i], y[i]);
            }
        }

        private double[] InitArrayX(int a , int b, double h)
        {
            int N = Convert.ToInt32((b - a) / h) + 1;
            double[] local = new double[N];

            double j = a;

            for(int i = 0; i < N ; i++, j += h )
            {
                local[i] = j;
            }

            return local;
        }


        private double[] InitArrayF(double[] x)
        {
            int N = x.Length;
            double[] local = new double[N];

            for (int i = 0; i < N; i++)
                local[i] = Math.Exp(-x[i]);

            return local;
        }

        private void AlgorithmSpline()
        {
            h = Convert.ToDouble(GU_b - GU_a) / (N - 1);
            h1 = h / 10;

            X = InitArrayX(GU_a, GU_b, h);
            F = InitArrayF(X);

            InitParamMatrix();

            methodProg = new MtdProg(a, b, c, d);

            m = methodProg.getCalculatedY(-Math.Exp(-1));

            S_x = InitArrayX(GU_a, GU_b, h1);
            S_y = InitSpline_Y(S_x, X, m, F, h);


        }

        private void InitParamMatrix()
        {
            int N = X.Length;

            a = new double[N];
            b = new double[N];
            c = new double[N];
            d = new double[N];

            a[0] = 1;
            b[0] = 0;
            c[0] = 0;
            d[0] = -1;

            for(int i = 1; i < N - 1; i++)
            {
                a[i] = 4;
                b[i] = 1;
                c[i] = 1;
                d[i] = 3 * (F[i+1] - F[i - 1]) / h;
            }

            a[N - 1] = 1;
            b[N - 1] = 0;
            c[N - 1] = 0;
            d[N - 1] = -Math.Exp(-1);
        }


        private double[] InitSpline_Y(double[] S_x, double[] x, double[] m, double[] F, double h)
        {
            int N = S_x.Length;

            double[] localSpline_Y = new double[N];

            double t;

            int i , j;
            try
            {
                for ( j = 0; j < N - 1; j++)
                {
                    i = (int)((S_x[j] - x[0]) / h);

                    t = (S_x[j] - x[i]) / h;

                    localSpline_Y[j] = F[i] * F1(t) + F[i + 1] * F2(t) + m[i] * h * F3(t) + m[i + 1] * h * F4(t);                   
                }

                localSpline_Y[N - 1] = F[F.Length - 1];
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show("Индекс за переделами массива");
            }

            return localSpline_Y;
        }

        private double F1(double t)
        {
            return Math.Pow((t - 1), 2) * (2 * t + 1);
        }

        private double F2(double t)
        {            
            return Math.Pow(t, 2) * (3 - 2 * t);
        }

        private double F3(double t)
        {
            return  t * Math.Pow((1 - t), 2);
        }

        private double F4(double t)
        {
            return Math.Pow(t, 2) * (t - 1);
        }
    }
}
