using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace _202020
{
    public partial class MainForm : Form
    {
        private System.Timers.Timer workTimer;
        private int workTime = 20 * 60; // 20 minutes in seconds
        private NotifyIcon trayIcon;
        private bool isResting = false; // Track if the rest screen is active

        public MainForm()
        {
            InitializeComponent();
            SetupSystemTray();
            InitializeWorkTimer();
            HideMainWindow(); // Hide the main window immediately
        }

        // Add application to Windows Startup (optional method)
        private void AddToStartup()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            key.SetValue("202020", Application.ExecutablePath.ToString());
        }

        // Set up the system tray icon
        private void SetupSystemTray()
        {
            trayIcon = new NotifyIcon();
            trayIcon.Icon = Properties.Resources.eye_32px; // Use the embedded icon resource
            trayIcon.Visible = true;
            trayIcon.Text = "202020 - Running";
            trayIcon.ContextMenu = new ContextMenu(new MenuItem[]
            {
                new MenuItem("Exit", OnExit) // Prevent user from exiting the app easily
            });
        }

        // Prevent closing the app from taskbar or Alt+F4
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true; // Cancel any attempts to close the main form
            this.Hide(); // Always hide the main window if an attempt is made to close it
        }

        // Block exiting the app from system tray
        private void OnExit(object sender, EventArgs e)
        {
            MessageBox.Show("You cannot close the 202020 app while it is running.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Initialize the 20-minute work timer
        private void InitializeWorkTimer()
        {
            workTimer = new System.Timers.Timer(1000); // Tick every second
            workTimer.Elapsed += WorkTimer_Tick;
            workTimer.AutoReset = true;
            workTimer.Enabled = true;
        }

        private void WorkTimer_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (workTime > 0)
            {
                workTime--;
            }
            else if (!isResting)
            {
                isResting = true; // Prevent multiple rest screens from opening

                // Since this method is called from a non-UI thread, we need to invoke UI updates
                this.Invoke((MethodInvoker)delegate
                {
                    ShowRestScreen();
                });
            }
        }

        private void ShowRestScreen()
        {
            RestForm restForm = new RestForm();
            restForm.ShowDialog(); // Show the rest screen on top of everything
            isResting = false; // Reset resting state after closing rest screen
            workTime = 20 * 60; // Reset work time to 20 minutes after rest
        }

        // Hide the main window as soon as the application starts
        private void HideMainWindow()
        {
            this.WindowState = FormWindowState.Minimized; // Minimize the form
            this.ShowInTaskbar = false; // Remove from taskbar
            this.Hide(); // Completely hide the form
        }

        // Override the CreateParams to remove the form from the Alt + Tab list
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x80; // WS_EX_TOOLWINDOW: This flag makes the window a tool window, removing it from Alt + Tab.
                return cp;
            }
        }
    }
}
