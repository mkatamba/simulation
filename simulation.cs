using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace finalpetrolstation_assignment
{
    public class Simulation//creates simulation class
    {
        CancellationTokenSource source;// creates variable called source
        VehicleQueue<Vehicle> vehicle_list;//creates an array called vehicle list
        private object locker;
        public List<Pump> pump_list;//an array of pumps

        private double total_amount;//variable created
        private int serviced_vehicle;//variable created

        private const double FUEl_PRICE = 2; // the price of fuel per liter variable

        public void start()//start method
        {
            Locker = new object();//new object called locker
            vehicle_list = new VehicleQueue<Vehicle>(2000);//new memeory that stores vehicle list array 
            initializePumps();//calls the method initialize pump

            source = new CancellationTokenSource();//c token passed to source
            var token = source.Token;//another variable called token

            //generate vehicles   
            Task.Run(() => generateVehicle(token), token);
            //start simulation
            service();
        }
        //initialize pump
        public void initializePumps()
        {
            pump_list = new List<Pump>();
            //add 9 pumps
            for (int i = 0; i < 9; i++)
            {
                Pump pump = new Pump(i + 1);
                pump_list.Add(pump);
            }
        }
        //simulate vehicle is arriving to forecourt
        private void generateVehicle(CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }
                //new vehicle is arriving every 1.5 seconds
                Vehicle new_vehicle = new Vehicle();
                lock (Locker)
                {
                    //new vehicle is arrived now
                    GetVehicles.Push(new_vehicle);
                }
                Thread.Sleep(1500);
            }
        }

        //simulate service
        private void service()
        {
            while (true)
            {
                //show information
                Console.WriteLine();
                showResult();
                Console.WriteLine();
                //attendant try to press pump
                Console.Write("Enter free pump number(1 ~ 9): ");
                int pump_index;
                if (!Int32.TryParse(Console.ReadLine(), out pump_index))
                {
                    Console.WriteLine("Invalid input. Try again !");//error message if wrong input
                    continue;
                }
                else
                {
                    if (pump_index < 1 || pump_index > pump_list.Count)//if available
                    {
                        Console.WriteLine("Please enter pump between 1 to 9.");//enter free no
                        continue;
                    }
                    Pump pump = pump_list[pump_index - 1];//minus pump no. entered
                    if (!pump.Free)//checks pump free
                    {
                        Console.WriteLine("Pump " + pump_index + " is busy now. Please press other pump.");//
                        continue;
                    }
                    if (GetVehicles.Count == 0)
                    {
                        Console.WriteLine("Vehicle is not exist now. Please wait till vehicle is arrived");
                        continue;
                    }
                    lock (Locker)
                    {
                        //pump start service
                        pump.Free = false;
                        Task.Run(() => serviceToVehicle(GetVehicles.Pop(), pump));
                    }

                }
            }
        }
        //start service to vehicle
        private void serviceToVehicle(Vehicle vehicle, Pump pump)
        {
            //get amount
            double amount = pump.DispensingSecond * pump.ServiceTime;
            //start service
            Thread.Sleep(pump.ServiceTime * 1000);//service@pump

            lock (Locker)
            {
                //update total amount
                total_amount += amount;
                //update serviced vehicle number
                serviced_vehicle++;
                pump.TotalDispensedAmount = amount;
                //pump is free
                pump.Free = true;
            }
        }
        //show result
        private void showResult()
        {
            //show pump state as table
            Console.WriteLine("Pump Status:");
            int index = 0;
            string table_content = "";
            for (index = 0; index < pump_list.Count; index++)
            {
                table_content += (index + 1) + "\t" + pump_list[index].Status + "\t||  ";
                if (index % 3 == 2)//
                {
                    table_content += "\n";
                }
            }
            Console.WriteLine(table_content);

            //show each pump dispensed fuel amount
            //string dispensed_content = "";
            //for (index = 0; index < pump_list.Count; index++)
            //{
            //    dispensed_content += "Pump " + (index + 1) + " has dispensed : " + pump_list[index].TotalDispensedAmount + "L\t";
            //    if (index % 3 == 2)
            //    {
            //        dispensed_content += "\n";
            //    }
            //}
            //Console.WriteLine();
            //Console.WriteLine(dispensed_content);

            double total_money = Math.Round(total_amount * FUEl_PRICE, 1, MidpointRounding.AwayFromZero);
            Console.WriteLine();
            Console.WriteLine("Unleaded dispensed: " + total_amount + "L");
            Console.WriteLine("Amount of money made: £" + total_money);
            Console.WriteLine("Attendant's commission is: £" + Math.Round(total_money / 100, 1, MidpointRounding.AwayFromZero));
            Console.WriteLine("Vechicle Served: " + serviced_vehicle);
        }
        public VehicleQueue<Vehicle> GetVehicles
        {
            get { return vehicle_list; }
            set { }
        }
        public object Locker
        {
            get { return locker; }
            set { locker = value; }
        }
    }
}
