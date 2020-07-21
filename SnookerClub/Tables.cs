using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SnookerClub
{
    public partial class Tables : Form
    {
        SnookerClub snookerClub;
        double MoneyFromRemovedTables = 0;
        public Tables()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void Tables_Load(object sender, EventArgs e)
        {
            snookerClub = new SnookerClub();
            snookerClub.AddTable();
            this.Controls.Add(snookerClub.GetPB());
            this.Controls.Add(snookerClub.GetLbl());
            this.Controls.Add(snookerClub.GetPbBall());
            this.Controls.Add(snookerClub.GetPbRp());
            Sum.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            snookerClub.AddTable();
            this.Controls.Add(snookerClub.GetPB());
            this.Controls.Add(snookerClub.GetLbl());
            this.Controls.Add(snookerClub.GetPbBall());
            this.Controls.Add(snookerClub.GetPbRp());
            if (GlobalVar.tor.Count > 0)
            {
                int flag = 0;
                for (int i = 0; i < GlobalVar.tor.Count; i++)
                {
                    if (GlobalVar.tor[i] == -1 && flag !=1)
                    {
                        flag = 1;
                        GlobalVar.tor.Remove(GlobalVar.tor[i]);
                        GlobalVar.tables.Last().StartPlay();

                    }
                }
            }
        }

        private void Sum_Tick(object sender, EventArgs e)
        {
            Summery.Text = Math.Round(snookerClub.GetMoney() + MoneyFromRemovedTables, 2).ToString()
                + System.Environment.NewLine + "Club Queue: "+ GlobalVar.tor.Count(p=>p==-1);      
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int flag = 0;
            foreach (Table f in GlobalVar.tables)
            {
                if (f.isFree() && flag == 0)
                {
                    f.StartPlay();
                    flag = 1;
                }
            }
            if (flag == 0)
            {
                snookerClub.AddCustomerToTor();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!(GlobalVar.tables.Last() == GlobalVar.tables.First()))
            {
                MoneyFromRemovedTables += GlobalVar.tables.Last().GetAllMoney();
                try
                {
                    if (!GlobalVar.tables.Last().isFree())
                        GlobalVar.tables.Last().EndPlay();
                    snookerClub.RemoveTable();

                }
                catch { }
            }

        } 
        
    }
}
