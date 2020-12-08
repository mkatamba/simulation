using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace finalpetrolstation_assignment
{
    public class Vehicle
    {
        private bool leave;
        private bool in_service;

        public Vehicle()//creates vehicle constructor to assign field created above
        {
            Leave = false;//attributes of the field
            InService = false;
        }
        public bool Leave//
        {
            get { return leave; }
            set { leave = value; }
        }
        public bool InService
        {
            get { return in_service; }
            set { in_service = value; }
        }

    }
}
