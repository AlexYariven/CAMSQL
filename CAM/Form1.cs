using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;

namespace CAM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private FilterInfoCollection CaptureDevices;
        private VideoCaptureDevice videoSource;
        private void Form1_Load(object sender, EventArgs e)
        {
            CaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in CaptureDevices)
            {
                comboBox1.Items.Add(Device.Name);
            }
            comboBox1.SelectedIndex = 0;
            videoSource = new VideoCaptureDevice();

            string connectionString;
            SqlConnection con;
            connectionString = @"";
            con = new SqlConnection(connectionString);
            con.Open();

            SqlCommand command;
            sqlDataReader dataReader;
            string sql, output = "";
            sql = "Select texts from texts";
            command = new SqlCommand(sql, con);
            dataReader = command.ExecuteReader();

            dataReader.Read();
            button1.Text = "tryck inte";
            con.Close();

        }
        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            if (videoSource.IsRunning==true)
            {
                videoSource.Stop();
            }
            Application.Exit(null);
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            videoSource.Stop();
            pictureBox1.Image = null;
            pictureBox1.Invalidate();
            
        }

        private void START_Click(object sender, EventArgs e)
        {
            videoSource = new VideoCaptureDevice(CaptureDevices[comboBox1.SelectedIndex].MonikerString);
            videoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame);
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            videoSource.Stop();
        }

        private void Capture_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = (Bitmap)pictureBox1.Image.Clone();
        }

       

    }
}
