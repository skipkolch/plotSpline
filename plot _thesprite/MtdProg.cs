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

        private double GU_inA;
        private double GU_inB;

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

            GU_inA = double.NaN;
            GU_inB = double.NaN;
            
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

            GU_inA = double.NaN;
            GU_inB = double.NaN;
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

        public void setGU_inA(double a)
        {
            this.GU_inA = a;
        }

        public void setGU_inB(double b)
        {
            this.GU_inB = b;
        }


        private void CalculateV()
        {
            V[0] = -B[0] / A[0];

            for(int i = 1; i < N; i++)
                V[i] = (-B[i])/(C[i]*V[i-1] + A[i]);

        }

        private void CalculateU()
        {
            U[0] = D[0] / A[0];

            for (int i = 1; i < N; i++)
                U[i] = (D[i] - C[i]*U[i-1]) / (C[i] * V[i - 1] + A[i]);

        }

        private void CalculateY()
        {
            if (GU_inA != double.NaN && GU_inB != double.NaN)
            {

                Y[0] = GU_inA;
                Y[N - 1] = GU_inB;

                for(int i = N - 2; i >= 0; i--)
                    Y[i] = V[i] * Y[i + 1] + U[i];

            }
            else throw new InvalidOperationException("Set boundary condition");
        }


        public double[] getCalculatedY()
        {
            CalculateV();

            CalculateU();

            CalculateY();

            return Y;
        }
    }
}
