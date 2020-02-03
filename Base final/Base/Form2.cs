using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;


namespace Base
{
    public partial class Form2 : Form
    {
        double maxY = 0;
        double minY = 0;
        public Form2()
        {
            InitializeComponent();
            Begining_Config();
        }

        public void ReceivingData(object objV, object objT)
        {
            string strV = (string)objV;
            string strT = (string)objT;
            if ((strV.Contains("E37")) || (strT.Contains("E37")) ||
                (strV.Contains("E") == false) || (strT.Contains("E") == false))
                return;

            cpuChart.Series["Series1"].Points.AddXY(Double.Parse(strT.Replace(".", ",")), Double.Parse(strV.Replace(".", ",")));

            if (maxY == 0)
            {
                maxY = Double.Parse(strV.Replace(".", ","));
                minY = Double.Parse(strV.Replace(".", ","));
                return;
            }

            if (Double.Parse(strV.Replace(".", ",")) > maxY)
                maxY = Double.Parse(strV.Replace(".", ","));
            if (Double.Parse(strV.Replace(".", ",")) < minY)
                minY = Double.Parse(strV.Replace(".", ","));

            cpuChart.ChartAreas[0].AxisY.Maximum = maxY + ((maxY - minY) / 10);
            cpuChart.ChartAreas[0].AxisY.Minimum = minY - ((maxY - minY) / 10);
        }

        private void Begining_Config()
        {
            MaximizeBox = false;   //desabilitar botão de maximizar
            this.Text = "Graph"; //Nome no titulo do form
            this.Icon = Properties.Resources.Blackvariant_Button_Ui_Requests_9_Wire; //Icone

            cpuChart.Size = new Size(this.Size.Width - 40, this.Size.Height - 60);  //O "-40" e "-60" representam as bordas entre as paredes do Form2
            cpuChart.Legends.Clear();
            cpuChart.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32; //Apenas valores inteiros na legenda do eixo X
            cpuChart.ChartAreas[0].AxisY.LabelStyle.Format = "0.00E00"; //Valores representados em notação exponencial
            cpuChart.ChartAreas[0].AxisY.Title = "Voltage";
            cpuChart.ChartAreas[0].AxisX.Title = "Temperature";

            System.Windows.Forms.DataVisualization.Charting.StripLine stripline = new System.Windows.Forms.DataVisualization.Charting.StripLine();
            stripline.Interval = 0;
            stripline.IntervalOffset = 0;
            stripline.StripWidth = 1E-07; //1E-07 é um tamanho muito inferior à variação de tensão que ocorrerá e ainda deixa visivel a linha
            stripline.BackColor = Color.Blue;
            cpuChart.ChartAreas[0].AxisY.StripLines.Add(stripline);
        }

        private void Form2_ResizeEnd(object sender, EventArgs e)
        {
            cpuChart.Size = new Size(this.Size.Width - 40, this.Size.Height - 60);  //O "-40" e "-60" representam as bordas entre as paredes do Form2
        }
        /*
        private void button1_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            double y = r.Next(-100, 100) / 100000.0;
            double x = r.Next(0, 100) / 10.0;

            cpuChart.Series["Series1"].Points.AddXY(x, y);

            if (maxY == 0)
            {
                maxY = y;
                minY = y;
                return;
            }

            if (y > maxY)
                maxY = y;
            if (y < minY)
                minY = y;

            cpuChart.ChartAreas[0].AxisY.Maximum = maxY + ((maxY - minY) / 10);
            cpuChart.ChartAreas[0].AxisY.Minimum = minY - ((maxY - minY) / 10);

        }*/
    }
}
