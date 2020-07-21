using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SnookerClub
{
    class SnookerClub
    {
        private int hours = 0;
        private int minutes = 0;
        private int secs = 0;
        
        private double money = 0;
        
        public SnookerClub()
        {
            GlobalVar.tables = new List<Table>(); 
        }
        public void AddCustomerToTor()
        {
            GlobalVar.tor.Add(-1);
        }
        public void AddCustomerToTor(int tableNumber)
        {
            GlobalVar.tor.Add(tableNumber);
        }
        public string SumTime()
        {
            return hours + ":" + minutes + ":" + secs;
        }
        public void AddTable()
        {
            GlobalVar.tables.Add(new Table());

        }
        public void RemoveTable()
        {
            GlobalVar.tables.Last().RemoveTable();
            GlobalVar.tables.Last().GetPB().Visible = false;
            GlobalVar.tables.Last().GetPbBall().Visible = false;
            GlobalVar.tables.Last().GetPbRp().Visible = false;
            GlobalVar.tables.Last().GetLbl().Visible = false;
            GlobalVar.tables.Remove(GlobalVar.tables.Last());
            
        }
        public PictureBox GetPB()
        {
            return GlobalVar.tables.Last().GetPB();
        }
        public Label GetLbl()
        {
            return GlobalVar.tables.Last().GetLbl();            
        }
        public PictureBox GetPbBall()
        {
            return GlobalVar.tables.Last().GetPbBall();
        }
        public PictureBox GetPbRp()
        {
            return GlobalVar.tables.Last().GetPbRp();
        }
        public void Summery()
        {
            
        }
        public double GetMoney()
        {
            money = 0;
            for (int i = 0; i < GlobalVar.tables.Count(); i++)
            {
                money = money + GlobalVar.tables[i].GetMoney();
            }
            return money;
        }
        
    }
}
