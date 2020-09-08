using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace PointAcceleration
{
    public partial class Form1 : Form
    {
        Vector3 Z = new Vector3(0, 0, -1);
        float g = 9.8f; // Gravité
        float dt = 1f; // Delta temps en seconde
        int maxT = 300;
        float windSpeed = -5;
        List<Parachutiste> parList = new List<Parachutiste>();

        Parachutiste par1 = new Parachutiste()
        {
            V = new Vector3(50, 0, 0),
            P = new Vector3(50, 0, 4000),
            A = new Vector3(0, 0, 0),
            M = 80,
            Cd = 0.50f
        };


        Parachutiste par2 = new Parachutiste()
        {
            V = new Vector3(50, 0, 0),
            P = new Vector3(150, 0, 4000),
            A = new Vector3(0, 0, 0),
            M = 80,
            Cd = 0.50f // Coeff de drag
        };




        public Form1()
        {
            InitializeComponent();
            chart1.Series.Add("Pos");
            chart1.Series.Add("Pos2");
            chart1.Series.Add("Pos3");
        }

        #region Fonction Calcules

        public Vector3 CalculAcceleration(float m, float g, float Cd, Vector3 Z, Vector3 V)
        {
            return (m * g * Z - V * Cd * V.Length()) / m;
        }

        private Vector3 Vdt(float dt, float m, float g, float cd, Vector3 Z, Vector3 V)
        {
            return V + CalculAcceleration(m, g, cd, Z, V) * dt;
        }

        private Vector3 Pdt(float dt, Vector3 V, Vector3 P)
        {
            return P + V * dt + new Vector3(windSpeed,0,0);
        }

        #endregion

        private void CalculAndDraw()
        {
            var chart = chart1.ChartAreas[0];
            chart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;

            chart.AxisX.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.IsEndLabelVisible = true;

            chart.AxisX.Minimum = -200;
            chart.AxisX.Maximum = 500;
            chart.AxisY.Minimum = -10;
            chart.AxisY.Maximum = 4500;
            chart.AxisX.Interval = 10;
            chart.AxisY.Interval = 200;

            // Par1 Line

            chart1.Series["Pos"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Pos"].Color = Color.Red;

            // Par2 Line
            chart1.Series["Pos2"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Pos2"].Color = Color.Blue;

            // Line Par1 et 2
            chart1.Series["Pos3"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Pos3"].Color = Color.DarkOrange;

            for (int i = 0; i < maxT; i++)
            {

                par1.A = CalculAcceleration(par1.M, g, par1.Cd, Z, par1.V);
                par1.V = Vdt(dt, par1.M, g, par1.Cd, Z, par1.V);
                par1.P = Pdt(dt, par1.V, par1.P);

                chart1.Series["Pos"].Points.AddXY(par1.P.X, par1.P.Z);


                par2.A = CalculAcceleration(par2.M, g, par2.Cd, Z, par2.V);
                par2.V = Vdt(dt, par2.M, g, par2.Cd, Z, par2.V);
                par2.P = Pdt(dt, par2.V, par2.P);
                chart1.Series["Pos2"].Points.AddXY(par2.P.X, par2.P.Z);
            }

            chart1.Series["Pos3"].Points.AddXY(par1.P.X, par2.P.X);
            label1.Text = "Distance de séparation des parachutiste : " + (par1.P.X - par2.P.X).ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CalculAndDraw();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void trb_Par1_Scroll(object sender, EventArgs e)
        {
            par1.Cd = trb_Par1.Value;
            CalculAndDraw();

        }

    }
}
