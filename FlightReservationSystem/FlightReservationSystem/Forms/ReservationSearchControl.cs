using System;
using System.Linq;
using System.Windows.Forms;
using FlightReservationSystem.Managers;
using FlightReservationSystem.Models;
using FlightReservationSystem.Exceptions;

namespace FlightReservationSystem.Forms
{
    public class ReservationSearchControl : UserControl
    {
        private TextBox? txtCode, txtAirline, txtName;
        private Button? btnSearch, btnUpdate;
        private ListBox? lstResults;
        private TextBox? txtEditName, txtEditCitizenship;
        private CheckBox? chkActive;
        private Label? lblStatus;

        private ReservationManager reservationManager = new ReservationManager();
        private Reservation? selectedReservation;


        public ReservationSearchControl()
        {
            Dock = DockStyle.Fill;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            Label lblCode = new Label { Text = "Reservation Code:", Left = 10, Top = 10 };
            txtCode = new TextBox { Left = 140, Top = 10, Width = 150 };

            Label lblAirline = new Label { Text = "Airline:", Left = 310, Top = 10 };
            txtAirline = new TextBox { Left = 370, Top = 10, Width = 150 };

            Label lblName = new Label { Text = "Name:", Left = 10, Top = 40 };
            txtName = new TextBox { Left = 140, Top = 40, Width = 380 };

            btnSearch = new Button { Text = "Search", Left = 540, Top = 25, Width = 120 };
            btnSearch.Click += BtnSearch_Click;

            lstResults = new ListBox { Left = 10, Top = 80, Width = 650, Height = 120 };
            lstResults.SelectedIndexChanged += LstResults_SelectedIndexChanged;

            txtEditName = CreateTextBox("Edit Name:", 220);
            txtEditCitizenship = CreateTextBox("Edit Citizenship:", 250);

            chkActive = new CheckBox { Text = "Active", Left = 140, Top = 280 };
            chkActive.Checked = true;

            btnUpdate = new Button { Text = "Update Reservation", Left = 10, Top = 320, Width = 650 };
            btnUpdate.Click += BtnUpdate_Click;

            lblStatus = new Label { Left = 10, Top = 360, Width = 650, Height = 40, ForeColor = System.Drawing.Color.DarkGreen };

            Controls.AddRange(new Control[]
            {
                lblCode, txtCode, lblAirline, txtAirline, lblName, txtName, btnSearch,
                lstResults, txtEditName, txtEditCitizenship, chkActive, btnUpdate, lblStatus
            });
        }

        private TextBox CreateTextBox(string label, int top)
        {
            Controls.Add(new Label { Text = label, Left = 10, Top = top });
            return new TextBox { Left = 140, Top = top, Width = 520 };
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            lstResults.Items.Clear();
            selectedReservation = null;

            var results = reservationManager.FindReservations(txtCode.Text, txtAirline.Text, txtName.Text);

            foreach (var res in results)
            {
                lstResults.Items.Add(res);
            }

            lblStatus.Text = results.Count > 0 ? "Reservations found." : "No matching reservations.";
        }

        private void LstResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstResults.SelectedItem is Reservation res)
            {
                selectedReservation = res;
                txtEditName.Text = res.Name;
                txtEditCitizenship.Text = res.Citizenship;
                chkActive.Checked = res.IsActive;
                lblStatus.Text = "";
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedReservation == null)
            {
                lblStatus.Text = "Select a reservation from the list.";
                return;
            }

            try
            {
                reservationManager.UpdateReservation(
                    selectedReservation,
                    txtEditName.Text,
                    txtEditCitizenship.Text,
                    chkActive.Checked
                );

                lblStatus.Text = "Reservation updated successfully.";
                lblStatus.ForeColor = System.Drawing.Color.Green;
                BtnSearch_Click(sender, e); // refresh list
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error: {ex.Message}";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
