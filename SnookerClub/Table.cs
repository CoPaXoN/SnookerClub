using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace SnookerClub
{
    public class Table
    {
        private int hours = 0;
        private int minutes = 0;
        private int secs = 0;

        private int allHours = 0;
        private int allMinutes = 0;
        private int allSecs = 0;

        private bool Free = true;
        private bool inReaper = false;

        private PictureBox table;
        private PictureBox ball;
        private PictureBox Reaper;
        private Timer timer;
        private Label lblTimePlayed;

        static int countRow = 0;
        static int countCol = 0; 
        private int countAll = 0;

        private double money = 0;
        private double allMoney = 0;

        public int tor=0;

        private int x = 1, y = 1;

        private System.ComponentModel.IContainer components = null;

        public Table()
        {
            table = new PictureBox();
            this.table.Image = global::SnookerClub.Properties.Resources.SnookerTable;
            this.table.Location = new System.Drawing.Point(15 + countRow * 135, countCol * 191 + 200);
            this.table.Name = countAll.ToString();
            this.table.Size = new System.Drawing.Size(100, 71);

            Reaper = new PictureBox();
            this.Reaper.Image = global::SnookerClub.Properties.Resources.Reaper;
            this.Reaper.Location = new System.Drawing.Point(this.table.Location.X + 25, this.table.Location.Y + 10);
            this.Reaper.Name = "pbReaper" + countAll.ToString();
            this.Reaper.Size = new System.Drawing.Size(50, 50);

            ball = new PictureBox();
            this.ball.Image = global::SnookerClub.Properties.Resources.SnookerBall2;
            this.ball.Location = new System.Drawing.Point(15 + countRow * 135, countCol * 191 + 200);
            this.ball.Name = "pbBall" + countAll.ToString();
            this.ball.Size = new System.Drawing.Size(16, 16);
            this.ball.BringToFront();

            this.lblTimePlayed = new Label();
            this.lblTimePlayed.AutoSize = true;
            this.lblTimePlayed.Location = new System.Drawing.Point(15 + countRow * 135, countCol * 191 + 130 - 15);
            this.lblTimePlayed.Name = "label" + countAll.ToString();
            this.lblTimePlayed.Size = new System.Drawing.Size(35, 13);
            this.lblTimePlayed.Text = "Current Play: " + hours.ToString() + ":" + minutes.ToString() + ":" + secs.ToString()
                + System.Environment.NewLine + "Last Payed: " + Math.Round(this.GetMoney(), 2).ToString()
                + System.Environment.NewLine + "Sum Time Played: " + allHours.ToString()
                + ":" + allMinutes.ToString() + ":" + allSecs.ToString()
                + System.Environment.NewLine + "Sum Money gained: " + Math.Round(allMoney, 2).ToString()
                + System.Environment.NewLine + "in Queue: "+this.tor.ToString();
            ;
            this.lblTimePlayed.AllowDrop = true;

            countRow++;
            countAll++;
            if (countRow == 8)
            {
                countCol++;
                countRow = 0;
            }

            timer = new Timer();
            this.components = new System.ComponentModel.Container();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timer.Tick += new System.EventHandler(this.timerTick);
            this.timer.Interval = 10;
            this.table.MouseClick += table_MouseClick;            
        }
        bool f = true;
        void table_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Middle)
            {
                
                if (!inReaper)
                {
                    if (!Free)
                    {
                        timer.Stop();
                        f = true; // היה מישהו על השולחן באמצע משחק
                    }
                    else
                    {
                        f = false;
                    }

                    this.Reaper.BringToFront();
                    inReaper = true;
                    Free = false;
                }
                else
                {
                    this.table.BringToFront();
                    this.ball.BringToFront();
                    inReaper = false;
                    if (f == true)
                    {
                        timer.Start();
                    }
                    else
                    {
                        if (GlobalVar.tor.Count > 0)
                        {
                            int flag = 0;
                            for (int i = 0; i < GlobalVar.tor.Count; i++)
                            {
                                if (GlobalVar.tor[i] == -1 && flag != 1) // ללא שולחן ספציפי
                                {
                                    GlobalVar.tor.Remove(GlobalVar.tor[i]);
                                    flag = 1;
                                    Free = false;
                                    timer.Start();
                                }
                                else
                                {
                                    if (GlobalVar.tor[i] == int.Parse(table.Name) && flag != 1 && tor != 0) // שולחן ספציפי
                                    {
                                        GlobalVar.tor.Remove(GlobalVar.tor[i]);
                                        tor--;
                                        flag = 1;
                                        Free = false;
                                        timer.Start();
                                    }
                                }
                            }
                        }
                    }
                }     
            }

            if (e.Button == MouseButtons.Left)
            {
                this.StartPlay();
            }
            if (e.Button == MouseButtons.Right)
            {
                this.EndPlay();
            }

        }
        public void RemoveTable()
        {
            countRow--;
            countAll--;
            if (countRow == 0)
            {
                countCol--;
                countRow = 8;
            }
        }

        public PictureBox GetPbBall()
        {
            return ball;
        }
        public PictureBox GetPbRp()
        {
            return Reaper;
        }
        public PictureBox GetPB()
        {
            return table;
        }
        public bool isFree()
        {
            return Free;
        }
        public Label GetLbl()
        {
            return lblTimePlayed;
        }
        private void timerTick(object sender, EventArgs e)
        {
            this.ball.BringToFront();
            secs++;
            allSecs++;
            if (secs == 60)
            {
                minutes++;
                secs = 0;
            }
            if (minutes == 60)
            {
                hours++;
                minutes = 0;
            }
            if (allSecs == 60)
            {
                allMinutes++;
                allSecs = 0;
            }
            if (allMinutes == 60)
            {
                allHours++;
                allMinutes = 0;
            }
            
            money +=  0.01;
            allMoney +=  0.01;
            lblTimePlayed.Text = "Current Play: " + hours.ToString() + ":" + minutes.ToString() + ":" + secs.ToString()
                + System.Environment.NewLine + "Last Payed: " + Math.Round(this.GetMoney(),2).ToString()
                + System.Environment.NewLine + "Sum Time Played: " + allHours.ToString()
                + ":" + allMinutes.ToString() + ":" + allSecs.ToString()
                + System.Environment.NewLine + "Sum Money gained: " + Math.Round(allMoney,2).ToString()
                + System.Environment.NewLine + "in Queue:" + this.tor.ToString(); 
            
            this.ball.Location = new System.Drawing.Point(this.ball.Location.X + x, this.ball.Location.Y + y);
            if(this.ball.Location.X == this.table.Location.X + this.table.Size.Width - 16)
                x=-1;
            if (this.ball.Location.Y == this.table.Location.Y + this.table.Size.Height - 16)
                y=-1;
            if (this.ball.Location.X == this.table.Location.X)
                x=1;
            if (this.ball.Location.Y == this.table.Location.Y)
                y=1;
        }

        public void StartPlay()
        {

            if (!inReaper)
            {
                if (!Free)
                {
                    GlobalVar.tor.Add(int.Parse(table.Name));
                    this.tor++;
                    
                }
                else
                {
                    Free = false;
                    timer.Start();
                }
            }
            else
            {
                GlobalVar.tor.Add(int.Parse(table.Name));
                this.tor++;
            }
            lblTimePlayed.Text = "Current Play: " + hours.ToString() + ":" + minutes.ToString() + ":" + secs.ToString()
                + System.Environment.NewLine + "Last Payed: " + Math.Round(this.GetMoney(), 2).ToString()
                + System.Environment.NewLine + "Sum Time Played: " + allHours.ToString()
                + ":" + allMinutes.ToString() + ":" + allSecs.ToString()
                + System.Environment.NewLine + "Sum Money gained: " + Math.Round(allMoney, 2).ToString()
                + System.Environment.NewLine + "in Queue:" + this.tor.ToString();
            
        }
        int t = -2;
        public void EndPlay()
        {
            Free = true;
            timer.Stop();

            hours = 0;
            minutes = 0;
            secs = 0;

            if (!inReaper)
            {
                if (GlobalVar.tor.Count > 0)
                {
                    int flag = 0;
                    for (int i = 0; i < GlobalVar.tor.Count; i++)
                    {
                        if (GlobalVar.tor[i] == -1 && flag != 1) // ללא שולחן ספציפי
                        {
                            GlobalVar.tor.Remove(GlobalVar.tor[i]);
                            flag = 1;
                            Free = false;
                            timer.Start();
                        }
                        else
                        {
                            if (GlobalVar.tor[i] == int.Parse(table.Name) && flag != 1 && tor != 0) // שולחן ספציפי
                            {
                                GlobalVar.tor.Remove(GlobalVar.tor[i]);
                                tor--;
                                flag = 1;
                                Free = false;
                                timer.Start();
                            }
                        }
                    }
                }
            }
            else // אם השולחן בתיקון
            {
                if (GlobalVar.tor.Count > 0)
                {
                    int flag = 0;
                    for (int i = 0; i < GlobalVar.tor.Count; i++)
                    {
                        if (GlobalVar.tor[i] == -1 && flag != 1)
                        {
                            GlobalVar.tor.Remove(GlobalVar.tor[i]);
                            flag = 1;
                            this.StartPlay();
                        }
                        else
                        {
                            if (GlobalVar.tor[i] == int.Parse(table.Name) && flag != 1)
                            {
                                tor--;
                                GlobalVar.tor.Remove(GlobalVar.tor[i]);
                                this.StartPlay();
                            }
                        }
                    }
                }
            }
            lblTimePlayed.Text = "Current Play: " + hours.ToString() + ":" + minutes.ToString() + ":" + secs.ToString()
                + System.Environment.NewLine + "Last Payed: " + Math.Round(this.GetMoney(), 2).ToString()
                + System.Environment.NewLine + "Sum Time Played: " + allHours.ToString()
                + ":" + allMinutes.ToString() + ":" + allSecs.ToString()
                + System.Environment.NewLine + "Sum Money gained: " + Math.Round(allMoney, 2).ToString()
                + System.Environment.NewLine + "in Queue:" + this.tor.ToString();
        }

        public double GetMoney()
        {
            return money;
        }

        public double GetAllMoney()
        {
            return allMoney;
        }
    }
}

