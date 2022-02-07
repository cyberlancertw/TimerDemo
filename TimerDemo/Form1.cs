using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimerDemo
{
    public partial class Form1 : Form
    {
        Label lblTime, lblSec;
        TextBox txtInput;
        Timer tmrTimer;
        Pen penBg = new Pen(new SolidBrush(Color.LightBlue), 3);
        Pen penRun = new Pen(new SolidBrush(Color.Salmon), 5);
        Rectangle rect = new Rectangle(50, 10, 100, 100);
        int totalTime;
        DateTime startDateTime, nowDateTime;
        Button btnGO;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeUI()
        {
            this.Size = new Size(220, 270);

            tmrTimer = new Timer
            {
                Interval = 100,
                Enabled = false
            };
            tmrTimer.Tick += new System.EventHandler(delegate (object sender, EventArgs e)
            {
                nowDateTime = DateTime.Now;
                int remainSecond = totalTime - Convert.ToInt32((nowDateTime - startDateTime).TotalSeconds);
                if (remainSecond < 0)
                {
                    (sender as Timer).Stop();
                    this.Controls.Add(txtInput);
                    this.Controls.Add(btnGO);
                    this.Size = new Size(220, 270);
                    return;
                }

                int nowHr = remainSecond / 3600;
                remainSecond %= 3600;
                int nowMin = remainSecond / 60;
                remainSecond %= 60;
                int nowSec = remainSecond;
                lblTime.Text = nowHr.ToString("00")+ ":" + nowMin.ToString("00") + ":" + nowSec.ToString("00");


                this.Refresh();
            });

            txtInput = new TextBox
            {
                Text = "300",
                Font = new Font("微軟正黑體", 14),
                Size = new Size(50, 40),
                Location = new Point(30, 180),
                MaxLength = 6
            };
            this.Controls.Add(txtInput);

            lblSec = new Label
            {
                Text = "秒",
                Font = new Font("微軟正黑體", 14),
                AutoSize = true,
                Location = new Point(90, 182)
            };
            this.Controls.Add(lblSec);
            btnGO = new Button
            {
                Text = "GO",
                Size = new Size(50, 35),
                Location = new Point(140, 180)
            };
            btnGO.Click += new System.EventHandler(delegate (object sender, EventArgs e)
            {
                bool success = int.TryParse(txtInput.Text, out totalTime);
                startDateTime = DateTime.Now;
                if (success)
                {
                    this.Controls.Remove(txtInput);
                    this.Controls.Remove(btnGO);
                    this.Size = new Size(220, 210);
                    tmrTimer.Start();
                }
            });
            this.Controls.Add(btnGO);
            lblTime = new Label
            {
                Font = new Font("微軟正黑體", 24),
                Text = "00:00:00",
                AutoSize = true,
                Location = new Point(30, 120)
                
            };
            this.Controls.Add(lblTime);


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeUI();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;


            gfx.DrawArc(penBg, rect, 0, 360);

            if(totalTime != 0)
            {
                double nowDegree = 360 * (nowDateTime - startDateTime).TotalMilliseconds / (1000 * totalTime);
                gfx.DrawArc(penRun, rect, -90, Convert.ToSingle(nowDegree));
            }
            
        }
    }
}
