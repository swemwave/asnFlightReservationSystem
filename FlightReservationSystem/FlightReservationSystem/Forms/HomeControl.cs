using System;
using System.Windows.Forms;

namespace FlightReservationSystem.Forms
{
    public class HomeControl : UserControl
    {
        private Label lblTitle, lblMessage;

        public HomeControl()
        {
            Dock = DockStyle.Fill;

            lblTitle = new Label
            {
                Text = "Traveless Flight Reservation System",
                Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold),
                AutoSize = true,
                Top = 60,
                Left = 60
            };

            lblMessage = new Label
            {
                Text = "Welcome to Traveless! A smarter way to manage flights and reservations.",
                Font = new System.Drawing.Font("Segoe UI", 10F),
                AutoSize = true,
                Top = 120,
                Left = 60,
                Width = 600
            };

            Controls.Add(lblTitle);
            Controls.Add(lblMessage);
        }
    }
}
