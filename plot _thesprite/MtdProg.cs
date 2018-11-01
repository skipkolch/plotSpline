using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plot__thesprite
{
    class MtdProg
    {
        private double[] A;
        private double[] B;
        private double[] C;
        private double[] D;

        private double[] U;
        private double[] V;

        private double[] Y;

        private int N;

        public MtdProg(int N)
        {
            this.N = N;
            A = new double[N];
            B = new double[N];
            C = new double[N];
            D = new double[N];

            U = new double[N];
            V = new double[N];

            Y = new double[N];            
        }


        public void setYN(double y)
        {
            Y[N - 1] = y;
        }

        public MtdProg(double[] a, double[] b, double[] c, double[] d)
        {
            this.A = a;
            this.B = b;
            this.C = c;
            this.D = d;

            N = a.Length;

            U = new double[N];
            V = new double[N];

            Y = new double[N];
        }

       
        public void setA(double[] a)
        {
            this.A = a;
        }

        public void setB(double[] b)
        {
            this.B = b;
        }

        public void setC(double[] c)
        {
            this.C = c;
        }

        public void setD(double[] d)
        {
            this.D = d;
        }



        private void CalculateV()
        {
            V[0] = -B[0] / A[0];

            for(int i = 1; i < N - 1; i++)
                V[i] = (-B[i])/(C[i]*V[i-1] + A[i]);

        }

        private void CalculateU()
        {
            U[0] = D[0] / A[0];

            for (int i = 1; i < N; i++)
                U[i] = (D[i] - C[i]*U[i-1]) / (C[i] * V[i - 1] + A[i]);

        }

        private void CalculateY(double y_N)
        {
            Y[N - 1] = y_N;
            for(int i = N - 2; i >= 0; i--)
                Y[i] = V[i] * Y[i + 1] + U[i];
        }


        public double[] getCalculatedY(double y_N)
        {
            CalculateV();

            CalculateU();

            CalculateY(y_N);

            return Y;
        }
    }
}
