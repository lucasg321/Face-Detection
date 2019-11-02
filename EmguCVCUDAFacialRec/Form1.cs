using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.Cuda;
using System.Timers;
using System.Windows;
using System.Runtime.InteropServices;



namespace EmguCVCUDAFacialRec
{
    public partial class Form1 : Form
    {
        Mat m1 = new Mat();
        System.Timers.Timer aTimer = new System.Timers.Timer();
        System.Timers.Timer hTimer = new System.Timers.Timer();

        VideoCapture capture;
        bool pause = false;
        bool haarDetect = false;
        bool lbpDetect = false;

        int X = 0, Y = 0;

        Image<Bgr, Byte> m_cam;

        public Form1()
        {
            InitializeComponent();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try { 
            pause = false;
            capture = new VideoCapture();
            Mat m = new Mat();
            capture.Read(m);
            imageBox1.Image = m;
            aTimer.Elapsed += new ElapsedEventHandler(imageCapture);
            aTimer.Interval = 500;
            aTimer.Enabled = true;
        }
                     catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        private void imageCapture(object source, ElapsedEventArgs e)
        { 
           
            try
            { 
                while (!pause)
                {
                    capture.Read(m1);
                    
                    if (!m1.IsEmpty)
                    {
                        imageBox1.Image = m1;
                        double fps = capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);

                    }
                    else
                    {
                        break;
                    }
                    if (haarDetect == true)
                    {
                        DetectFaceHaar();
                    }
                    if (lbpDetect == true)
                    {
                        DetectFaceLBP();
                    }
                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);

            }
            return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            haarDetect = true;
        }
        public void DetectFaceHaar()
        { 
           
            try 
            {
                string facePath = Path.GetFullPath(@"../../data/haarcascade_frontalface_default.xml");
                string eyePath = Path.GetFullPath(@"../../data/haarcascade_eye.xml");

                CascadeClassifier classifierFace = new CascadeClassifier(facePath);
                CascadeClassifier classifierEye = new CascadeClassifier(eyePath);

                var imgGray = m1.ToImage<Bgr, Byte>().Convert<Gray, byte>().Clone();
                Rectangle[] faces =  classifierFace.DetectMultiScale(imgGray, 1.1,5);
                m_cam = m1.ToImage<Bgr, Byte>();

                foreach (var face in faces)
                {
                    m_cam.Draw(face, new Bgr(0, 0, 255), 2);

                    imgGray.ROI = face;

                Rectangle[] eyes = classifierEye.DetectMultiScale(imgGray, 1.1, 5); 
                    
                    foreach(var eye in eyes)
                    {
                        var ee = eye;
                        ee.X += face.X;
                        ee.Y += face.Y;
                        m_cam.Draw(ee, new Bgr(0, 255, 0), 2);

                        X = ee.X;
                        Y = ee.Y;
                    }
                } 
                imageBox2.Image = m_cam;
               
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
                lbpDetect = true;
      
        }
        public void DetectFaceLBP()
        {
            try
            {
                string facePath = Path.GetFullPath(@"../../data/lbpcascade_frontalface.xml");

                CascadeClassifier classifierFace = new CascadeClassifier(facePath);

                var imgGray = m1.ToImage<Bgr, Byte>().Convert<Gray, byte>().Clone();
                Rectangle[] faces = classifierFace.DetectMultiScale(imgGray, 1.1, 4);
                m_cam = m1.ToImage<Bgr, Byte>();
                foreach (var face in faces)
                {
                    m_cam.Draw(face, new Bgr(255, 0, 0), 2);

                }
                imageBox2.Image = m_cam;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pause = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "X Value: " + X + "                      "; //enough to make new line: "                      "
            textBox1.Text += "Y Value: " + Y;

            String xstring = "X" + X.ToString();
            String ystring = "Y" + Y.ToString();
            String xystring = xstring + ystring;

            //serialPort1.Write(xystring);//send x and y values to serial port

        }

        private void button6_Click(object sender, EventArgs e)
        {
            String fire = "fire";
          //  serialPort1.Write(fire);//send trigger word "fire" to serial port
        }

    }
}
