using System;
using System.Linq;
using System.Windows.Forms;
using FlightReservationSystem.Managers;
using FlightReservationSystem.Models;
using FlightReservationSystem.Exceptions;

namespace FlightReservationSystem.Forms
{
    public class FlightSearchControl : UserControl
    {
        private ComboBox? cbOrigin, cbDestination, cbDay;
        private Button? btnSearch, btnReserve;
        private ListBox? lstFlights;
        private TextBox? txtCode, txtAirline, txtTime, txtCost, txtName, txtCitizenship;
        private Label? lblStatus;

        private FlightManager flightManager = new FlightManager();
        private ReservationManager reservationManager = new ReservationManager();
        private Flight? selectedFlight;

        public FlightSearchControl()
        {
            Dock = DockStyle.Fill;
            InitializeComponents();
            LoadFlights();
        }

        private void InitializeComponents()
        {
            int labelWidth = 90;
            int inputLeft = 100;
            int top = 10;

            Label lblOrigin = new Label { Text = "Origin:", Left = 10, Top = top, Width = labelWidth };
            cbOrigin = new ComboBox { Left = inputLeft, Top = top, Width = 150 };

            Label lblDest = new Label { Text = "Destination:", Left = 270, Top = top, Width = labelWidth };
            cbDestination = new ComboBox { Left = 360, Top = top, Width = 150 };

            Label lblDay = new Label { Text = "Day:", Left = 520, Top = 10, Width = 40 };
            cbDay = new ComboBox { Left = 570, Top = 10, Width = 100 };
            cbDay.Items.AddRange(new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" });

            top += 35;

            btnSearch = new Button { Text = "Search Flights", Left = 10, Top = top, Width = 660 };
            btnSearch.Click += BtnSearch_Click;

            top += 40;

            lstFlights = new ListBox { Left = 10, Top = top, Width = 660, Height = 100 };
            lstFlights.SelectedIndexChanged += LstFlights_SelectedIndexChanged;

            top += 120;

            txtCode = CreateReadOnlyTextBox("Flight Code:", top); top += 30;
            txtAirline = CreateReadOnlyTextBox("Airline:", top); top += 30;
            txtTime = CreateReadOnlyTextBox("Time:", top); top += 30;
            txtCost = CreateReadOnlyTextBox("Cost:", top); top += 30;
            txtName = CreateEditableTextBox("Name:", top); top += 30;
            txtCitizenship = CreateEditableTextBox("Citizenship:", top); top += 30;

            btnReserve = new Button { Text = "Make Reservation", Left = 10, Top = top, Width = 660 };
            btnReserve.Click += BtnReserve_Click;

            top += 40;

            lblStatus = new Label { Left = 10, Top = top, Width = 660, Height = 30, ForeColor = System.Drawing.Color.DarkGreen };

            Controls.AddRange(new Control[] {
        lblOrigin, cbOrigin, lblDest, cbDestination, lblDay, cbDay,
        btnSearch, lstFlights, txtCode, txtAirline, txtTime, txtCost,
        txtName, txtCitizenship, btnReserve, lblStatus
            });
        }

        private TextBox CreateReadOnlyTextBox(string label, int top)
        {
            var lbl = new Label { Text = label, Left = 10, Top = top, Width = 100 };
            var tb = new TextBox { Left = 120, Top = top, Width = 500, ReadOnly = true };
            Controls.Add(lbl);
            Controls.Add(tb);
            return tb;
        }

        private TextBox CreateEditableTextBox(string label, int top)
        {
            var lbl = new Label { Text = label, Left = 10, Top = top, Width = 100 };
            var tb = new TextBox { Left = 120, Top = top, Width = 500 };
            Controls.Add(lbl);
            Controls.Add(tb);
            return tb;
        }

        private void LoadFlights()
        {
            flightManager.LoadFlights("flights.csv");
            var origins = flightManager.Flights.Select(f => f.Origin).Distinct().ToArray();
            var destinations = flightManager.Flights.Select(f => f.Destination).Distinct().ToArray();

            cbOrigin.Items.AddRange(origins);
            cbDestination.Items.AddRange(destinations);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            lstFlights.Items.Clear();
            selectedFlight = null;

            if (cbOrigin.SelectedItem == null || cbDestination.SelectedItem == null || cbDay.SelectedItem == null)
            {
                lblStatus.Text = "Please select origin, destination, and day.";
                return;
            }

            var results = flightManager.FindFlights(cbOrigin.Text, cbDestination.Text, cbDay.Text);
            foreach (var flight in results)
            {
                lstFlights.Items.Add(flight);
            }

            lblStatus.Text = results.Count > 0 ? "Flights loaded." : "No flights found.";
        }

        private void LstFlights_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstFlights.SelectedItem is Flight flight)
            {
                selectedFlight = flight;
                txtCode.Text = flight.FlightCode;
                txtAirline.Text = flight.Airline;
                txtTime.Text = flight.Time;
                txtCost.Text = flight.Cost.ToString("C");
            }
        }

        private void BtnReserve_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedFlight == null)
                {
                    lblStatus.Text = "Please select a flight.";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                var name = txtName.Text;
                var citizenship = txtCitizenship.Text;

                var reservation = reservationManager.MakeReservation(selectedFlight, name, citizenship);
                lblStatus.Text = $"Reservation created: {reservation.Code}";
                lblStatus.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error: {ex.Message}";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
