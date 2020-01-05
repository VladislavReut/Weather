using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OOP_LIB;

namespace WindowsFormsApp1
{
    public partial class Forma : Form
    {
        WeatherStatus weatherStatus;

        public Forma()
        {
            InitializeComponent();
            weatherStatus = new WeatherStatus();
            List<Weather> list = weatherStatus.GetWeatherList();

            Thread t1 = new Thread(() =>
            {
                foreach (var it in list)
                {
                    comboBox1.AutoCompleteCustomSource.Add(it.name);
                }
                
            });
            t1.Start();
            
           
                foreach (var it in list)
                {
                    comboBox1.Items.Add(it.name);
                }
            
            
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Forecast forecast;
            try
            {
                string city = comboBox1.SelectedItem.ToString();
                forecast = weatherStatus.GetForecast(city);
                label1.Text = forecast.dateTime.ToString();
                label2.Text = forecast.country;
                label3.Text = forecast.name;
                label4.Text = forecast.temperature;
                label5.Text = forecast.description;
                label6.Text = forecast.humidity;
            
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("город не выбран, или имя города не корректно");
            }
           

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }


}
