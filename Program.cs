using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalpetrolstation_assignment
{

    class Program
    {
        static void Main(string[] args)
        {
            Simulation simulation = new Simulation();//simulation object created
            simulation.start();//simulation calls method start 
            //Console.ReadKey();
        }
    }
}
