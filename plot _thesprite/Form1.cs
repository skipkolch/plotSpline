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
        private double[] trueFucn;


        private MtdProg metodProd;

        private int size = 50;
        private int H;


        public Form()
        {
            InitializeComponent();

            a = new double[size];
            b = new double[size];
            c = new double[size];
            d = new double[size];

            fucn = new double[size];
            trueFucn = new double[size];
        }

        private void dataToArrays()
        {
           try
           {
                int size = dataGridView1.ColumnCount;

                a = new double[size];
                b = new double[size];
                c = new double[size];
                d = new double[size];

                for (int i = 0; i < size; i++)
                {
                    a[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells["A"].Value);
                    b[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells["B"].Value);
                    c[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells["C"].Value);
                    d[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells["D"].Value);

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

        }

        private void ArrayToData(double[] arr, string Cell)
        {
            for(int i = 0, N = arr.Length; i < N; i++)
                dataGridView1.Rows[i].Cells[Cell].Value = arr[i];
        }

        private void calcBtn_Click(object sender, EventArgs e)
        {
            metodProd = new MtdProg(a, b, c, d);
            fucn = metodProd.getCalculatedY();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBox1.SelectedIndex)
            {
                case 0: { CalcTask1(); break; }
                case 1: { CalcTask2(); break; }
            }
        }

        private void CalcTask1()
        {
            H = 1 / size;

            a[0] = 1;
            b[0] = 0;
            c[0] = 0;
            d[0] = 0;

            for(int i = 1; i < size - 1; i++)
            {
                a[i] = -2 - H*H;
                b[i] = 1;
                c[i] = 1;
                d[i] = 2 * H * H * (i - 1) * H;
            }

            a[size - 1] = 1;
            b[size - 1] = 0;
            c[size - 1] = 0;
            d[size - 1] = -1;

            AllArrayToGrid();
        }

        private void CalcTask2()
        {
            H = 1 / size;

            a[0] = 1;
            b[0] = -1;
            c[0] = 0;
            d[0] = 0;

            for (int i = 1; i < size - 1; i++)
            {
                a[i] = -4;
                b[i] = 2 + H;
                c[i] = 2 - H;
                d[i] = 2 * H * H;
            }

            a[size - 1] = 1;
            b[size - 1] = 0;
            c[size - 1] = 0;
            d[size - 1] = 1;

            AllArrayToGrid();
        }
        private void CalcTask3()
        {
            H = 1 / size;

            a[0] = 1;
            b[0] = -1;
            c[0] = 0;
            d[0] = 0;

            for (int i = 1; i < size - 1; i++)
            {
                a[i] = -4;
                b[i] = 2 + H;
                c[i] = 2 - H;
                d[i] = 2 * H * H;
            }

            a[size - 1] = 1;
            b[size - 1] = 0;
            c[size - 1] = 0;
            d[size - 1] = 1;

            AllArrayToGrid();
        }

        private void AllArrayToGrid()
        {
            ArrayToData(a, "A");
            ArrayToData(b, "B");
            ArrayToData(c, "C");
            ArrayToData(d, "D");
        }

        private void CalculateTrueFucn()
        {

        }
    }
}
