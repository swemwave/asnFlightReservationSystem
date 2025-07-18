using System;
using System.Windows.Forms;

namespace FlightReservationSystem.Forms
{
    public class DashboardForm : Form
    {
        private Panel sideMenu;
        private Panel mainPanel;
        private Button btnHome, btnFlights, btnReservations;

        public DashboardForm()
        {
            Text = "Traveless Dashboard";
            Width = 900;
            Height = 600;
            StartPosition = FormStartPosition.CenterScreen;

            sideMenu = new Panel { Width = 200, Dock = DockStyle.Left, BackColor = System.Drawing.Color.LightSteelBlue };
            mainPanel = new Panel { Dock = DockStyle.Fill };

            btnHome = new Button { Text = "Home", Dock = DockStyle.Top, Height = 50 };
            btnFlights = new Button { Text = "Find Flights", Dock = DockStyle.Top, Height = 50 };
            btnReservations = new Button { Text = "Find Reservations", Dock = DockStyle.Top, Height = 50 };

            btnHome.Click += (s, e) => LoadControl(new HomeControl());
            btnFlights.Click += (s, e) => LoadControl(new FlightSearchControl());
            btnReservations.Click += (s, e) => LoadControl(new ReservationSearchControl());

            sideMenu.Controls.AddRange(new Control[] { btnReservations, btnFlights, btnHome });
            Controls.Add(mainPanel);
            Controls.Add(sideMenu);

            LoadControl(new HomeControl()); // Load default view
        }

        private void LoadControl(UserControl control)
        {
            mainPanel.Controls.Clear();
            control.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(control);
        }
    }
}
