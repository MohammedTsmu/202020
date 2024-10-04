using System;
using System.Drawing;
using System.Windows.Forms;

namespace _202020
{
    public partial class RestForm : Form
    {
        private Timer restTimer;
        private int restTime = 30; // 20 seconds rest
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
            this.Opacity = 0.9; // Slight opacity to make it more advanced

            Label countdownLabel = new Label
            {
                Text = restTime.ToString(),
                Font = new Font("Arial", 72, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(countdownLabel);

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
