// Made by Asad Arif and Talha Arif //
// 7/17/2025 //
// OOP 2 //

using System;
using System.Windows.Forms;
using FlightReservationSystem.Forms;

namespace FlightReservationSystem
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DashboardForm());
        }
    }
}
