﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Management : Form
    {
        Form1 Form;
        bool started;

        Form1.Decre decr;
        Form1.Incre incr;
        Form1.Shows shows;

        public Management(Form1 _form)
        {
            InitializeComponent();
            this.Form = _form;
            if(IO.ManageImage.GetInstance().pathGif.Count < 1)
            {
                this.trackBar1.Maximum = 0;
                this.label4.Visible = false;
            }
            ChangeInPicture();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Length < 1)
                return;
            IO.ManageImage.GetInstance().CreateImageWithWhiteText("Bonjour", this.textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IO.ManageImage.GetInstance().Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (started)
                StopTimer();
            else
                StartTimer();
        }

        /// <summary>
        /// Demarre le timer
        /// </summary>
        private void StartTimer()
        {
            this.Form.timer1.Start();
            this.started = true;
            this.button4.Text = "Stop";
            this.button5.Visible = false;
            this.button6.Visible = false;
        }

        /// <summary>
        /// Stop le timer
        /// </summary>
        private void StopTimer()
        {
            this.Form.timer1.Stop();
            this.started = false;
            this.button4.Text = "Start";
            this.button5.Visible = true;
            this.button6.Visible = true;
        }

        private void Management_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Form.Close();
            IO.ManageImage.GetInstance().Dispose();
            this.Form.Dispose();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (this.trackBar1.Value == 1)
                ChangeInGif();
            else if (this.trackBar1.Value == 0)
                ChangeInPicture();
            else if (this.trackBar1.Value == 2)
                ChangeInVideo();
        }

        private void ChangeInVideo()
        {
            try
            {
                StopTimer();
                this.groupBox1.Visible = false;
                this.groupBox2.Visible = false;
                this.Form.ShowVideo(Const.IO.PathVids + @"\Hologram Project by Kiste.mp4");
                this.Form.panel1.Visible = false;
            }
            catch { }
        }

        private void ChangeInGif()
        {
            StopTimer();
            this.groupBox1.Visible = false;
            this.groupBox2.Visible = true;
            this.Form.axWindowsMediaPlayer1.Visible = false;
            this.decr = this.Form.DecrementGif;
            this.incr = this.Form.IncrementGif;
            this.shows = this.Form.ShowGif;
            this.incr.Invoke();
            this.Form.panel1.Visible = true;
        }

        private void ChangeInPicture()
        {
            StartTimer();
            this.groupBox1.Visible = true;
            this.groupBox2.Visible = false;
            this.Form.axWindowsMediaPlayer1.Visible = false;
            this.Form.axWindowsMediaPlayer1.Ctlcontrols.pause();
            this.decr = this.Form.DecrementPicture;
            this.incr = this.Form.IncrementPicture;
            this.shows = this.Form.ShowPicture;
            this.Form.panel1.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.incr.Invoke();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.decr.Invoke();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.numericUpDown1.Value >= 1 && this.numericUpDown1.Value <= 100)
                this.Form.timer1.Interval = (int)(this.numericUpDown1.Value * 1000);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.incr.Invoke();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.decr.Invoke();
        }
    }
}
