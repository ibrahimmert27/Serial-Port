using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace Serial_Port
{
    public partial class Form1 : Form
    {
        string[] portlar = SerialPort.GetPortNames();
        public Form1()
        {
            InitializeComponent();
        }
        bool move;
        int mouse_x, mouse_y;
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (move == true)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_y = e.Y;
            mouse_x = e.X;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string port in portlar)
            {
                comboBox1.Items.Add(port);
                comboBox1.SelectedIndex = 0;
            }
            comboBox2.SelectedIndex = 4;
            label2.Text = "Bağlantı Yok...";
            label2.ForeColor = Color.Red;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string sonuc = serialPort1.ReadExisting();
                label1.Text = sonuc;
            }
            catch (Exception ex)
            {
                label2.Text = ex.Message.ToString();
                label1.Text = "";
                if (label2.Text == "The port is closed.")
                {
                    label2.ForeColor = Color.Red;
                }
            }
        }
        int stop = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            if (serialPort1.IsOpen == false)
            { if(comboBox1.Text == "")
                return;
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = Convert.ToInt16(comboBox2.Text);
                try
                {
                    serialPort1.Open();
                    label2.Text = "Bağlantı Açık...";
                    label2.ForeColor = Color.Green;
                }
                catch (Exception Hata)
                {
                    stop = 1;
                    if (stop == 1)
                    {
                        MessageBox.Show("Hata: " + Hata.Message);
                        timer1.Stop();
                    }

                }
              }
              else {label2.Text="Bağlantı Kuruldu...";}
            }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
                label2.Text = "Bağlantı Kapalı...";
                label1.Text = "";
                label2.ForeColor = Color.Red;
            }
          }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
                label2.Text = "Bağlantı Kapalı...";
            }
          }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
       }
      }
     
