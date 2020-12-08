using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalpetrolstation_assignment
{
    public class Pump
    {
        private int id;
        private double dispensing_per_second;
        private int service_time;
        private bool state;
        private double total_dispensed_amount;

        public Pump(int id)
        {
            Id = id;
            dispensing_per_second = 1.5; //set dispensing fuel amount as 1.5L/s
            Free = true; //pump state (it is free or busy)
            ServiceTime = 18; //service time is 18 seconds
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public double DispensingSecond
        {
            get { return dispensing_per_second; }
            set { dispensing_per_second = value; }
        }
        public bool Free
        {
            get { return state; }
            set { state = value; }
        }
        public int ServiceTime
        {
            get { return service_time; }
            set { service_time = value; }
        }
        public double TotalDispensedAmount//dispensed fuel amount
        {
            get { return Math.Round(total_dispensed_amount, 1, MidpointRounding.AwayFromZero); }
            set { total_dispensed_amount += value; }
        }
        public string Status
        {
            get
            {
                if (Free)
                {
                    return "FREE";//status of the pump
                }
                else
                {
                    return "BUSY";//status of the pump
                }
            }
            set { }
        }
    }
}
