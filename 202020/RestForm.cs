using System;
using System.Drawing;
using System.Windows.Forms;

namespace _202020
{
    public partial class RestForm : Form
    {
        private Timer restTimer;
        private int restTime = 30; // 30 seconds rest
        private bool allowClose = false; // Control to allow closing only when timer ends

        public RestForm()
        {
            InitializeComponent();
            InitializeRestTimer();
            SetupRestScreen();
        }

        private void SetupRestScreen()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            this.BackColor = Color.Gray; // Customize the background color
            this.Opacity = 1; // Full opacity for the rest screen

            // Create a TableLayoutPanel to manage layout
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = Color.Gray // Set background color for the layout
            };

            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F)); // 70% space for countdown label
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F)); // 30% space for eye care text

            // Countdown label
            Label countdownLabel = new Label
            {
                Text = restTime.ToString(),
                Font = new Font("Arial", 72, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Eye care text label
            Label eyeCareTextLabel = new Label
            {
                Text = "Rest your eyes and focus on an object 20 feet (6 meters) away for 20-30 seconds.",
                Font = new Font("Arial", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Add labels to the layout
            layout.Controls.Add(countdownLabel, 0, 0); // Countdown label at the top
            layout.Controls.Add(eyeCareTextLabel, 0, 1); // Eye care text at the bottom

            this.Controls.Add(layout); // Add the layout to the form

            // Timer tick event for countdown
            restTimer.Tick += (sender, e) =>
            {
                restTime--;
                countdownLabel.Text = restTime.ToString();

                if (restTime <= 0)
                {
                    restTimer.Stop(); // Stop the timer after the rest period ends
                    allowClose = true; // Allow the form to close
                    this.Close(); // Close the rest screen
                }
            };
        }

        private void InitializeRestTimer()
        {
            restTimer = new Timer();
            restTimer.Interval = 1000; // Tick every second
            restTimer.Start();
        }

        // Override OnFormClosing to prevent closing the rest form with Alt + F4 or any other method
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!allowClose && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true; // Cancel the close event if the user tries to close it before the rest time ends
            }
        }
    }
}
