using System.Runtime.InteropServices;

namespace OP_Macro
{
    public partial class Form1 : Form
    {
        // Timers
        private System.Windows.Forms.Timer autoclickerTimer;
        private System.Windows.Forms.Timer recordTimer;

        // Booleans
        private bool isAutoclickerRunning = false;
        private bool isAutoclickerDoubleClick = false;
        private bool isRecording = false;
        private bool isReplaying = false;

        // Parameters
        private uint mouse_down = 0x02;
        private uint mouse_up = 0x04;
        private DateTime lastRecordTime;
        private List<MouseEventRecord> recordedEvents = new List<MouseEventRecord>();

        // Imports from user32.dll
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        public Form1()
        {
            InitializeComponent();

            CTypeCB.SelectedIndex = 0;
            MButtonCB.SelectedIndex = 0;
            TBHours.ContextMenuStrip = new ContextMenuStrip();
            TBMinutes.ContextMenuStrip = new ContextMenuStrip();
            TBSeconds.ContextMenuStrip = new ContextMenuStrip();
            TBMilliseconds.ContextMenuStrip = new ContextMenuStrip();

            recordTimer = new System.Windows.Forms.Timer();
            recordTimer.Interval = 1;
            recordTimer.Tick += RecordTimer_Tick;

            // Registering Hotkeys
            bool autoclickerRegister = RegisterHotKey(this.Handle, 1, 0, (uint)Keys.F1);
            bool textRegister = RegisterHotKey(this.Handle, 2, 0, (uint)Keys.F4);
            bool recordRegister = RegisterHotKey(this.Handle, 3, 0, (uint)Keys.F5);
            bool replayRegister = RegisterHotKey(this.Handle, 4, 0, (uint)Keys.F6);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                if (m.WParam.ToInt32() == 1)
                {
                    ToggleMacro();
                }
                else if (m.WParam.ToInt32() == 2)
                {
                    SendKeys.Send(textTB.Text);
                }
                else if (m.WParam.ToInt32() == 3)
                {
                    Record();
                }
                else if (m.WParam.ToInt32() == 4)
                {

                }
            }
            base.WndProc(ref m);
        }

        private async void RecordTimer_Tick(object sender, EventArgs e) // tick for records
        {
            int delay = (int)(DateTime.Now - lastRecordTime).TotalMilliseconds;
            lastRecordTime = DateTime.Now;

            // Save the current mouse position and delay
            recordedEvents.Add(new MouseEventRecord
            {
                X = Cursor.Position.X,
                Y = Cursor.Position.Y,
                Delay = delay
            });
        }

        private void Record()
        {
            if (!isRecording)
            {
                isRecording = true;
                recordedEvents.Clear();      // Clear previous recordings
                lastRecordTime = DateTime.Now; // Set start time for recording
                recordTimer.Start();         // Start timer for recording
            }
            else
            {
                isRecording = false;
                recordTimer.Stop();           // Stop timer for recording
            }
        }

        private void ToggleMacro()
        {
            if (!isAutoclickerRunning)
            {
                // Start the macro
                isAutoclickerRunning = true;
                autoclicker.Start();
                clickToggleButton.Text = "Toggle (ON)";
            }
            else
            {
                // Stop the macro
                isAutoclickerRunning = false;
                autoclicker.Stop();
                clickToggleButton.Text = "Toggle";
            }
        }

        private void CalcInterval()
        {
            autoclicker.Interval = 3600000 * int.Parse(TBHours.Text) + 60000 * int.Parse(TBMinutes.Text) + 1000 * int.Parse(TBSeconds.Text) + int.Parse(TBMilliseconds.Text);
        }

        private void TBHours_Leave(object sender, EventArgs e)
        {
            if (!int.TryParse(TBHours.Text, out int i) || int.Parse(TBHours.Text) < 0)
            {
                MessageBox.Show("The hours box can't be less than 0.");
                TBHours.Text = "0";
            }
            foreach (var num in TBHours.Text)
            {
                if (TBHours.Text.Length == 1 || num != '0')
                {
                    break;
                }
                else
                {
                    TBHours.Text = TBHours.Text.Substring(1, TBHours.Text.Length - 1);
                }
            }
            CalcInterval();
        }

        private void TBHours_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TBHours_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.KeyCode == Keys.C) || (e.Control && e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void TBMinutes_Leave(object sender, EventArgs e)
        {
            if (!int.TryParse(TBMinutes.Text, out int i) || int.Parse(TBMinutes.Text) < 0)
            {
                MessageBox.Show("The minutes box can't be less than 0.");
                TBMinutes.Text = "0";
            }
            foreach (var num in TBMinutes.Text)
            {
                if (TBMinutes.Text.Length == 1 || num != '0')
                {
                    break;
                }
                else
                {
                    TBMinutes.Text = TBMinutes.Text.Substring(1, TBMinutes.Text.Length - 1);
                }
            }
            CalcInterval();
        }

        private void TBMinutes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TBMinutes_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.KeyCode == Keys.C) || (e.Control && e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void TBSeconds_Leave(object sender, EventArgs e)
        {
            if (!int.TryParse(TBSeconds.Text, out int i) || int.Parse(TBSeconds.Text) < 0)
            {
                MessageBox.Show("The seconds box can't be less than 0.");
                TBSeconds.Text = "0";
            }
            foreach (var num in TBSeconds.Text)
            {
                if (TBSeconds.Text.Length == 1 || num != '0')
                {
                    break;
                }
                else
                {
                    TBSeconds.Text = TBSeconds.Text.Substring(1, TBSeconds.Text.Length - 1);
                }
            }
            CalcInterval();
        }

        private void TBSeconds_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TBSeconds_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.KeyCode == Keys.C) || (e.Control && e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void TBMilliseconds_Leave(object sender, EventArgs e)
        {
            if (!int.TryParse(TBMilliseconds.Text, out int i) || int.Parse(TBMilliseconds.Text) < 1)
            {
                MessageBox.Show("The milliseconds box can't be less than 1.");
                TBMilliseconds.Text = "1000";
            }
            foreach (var num in TBMilliseconds.Text)
            {
                if (TBMilliseconds.Text.Length == 1 || num != '0')
                {
                    break;
                }
                else
                {
                    TBMilliseconds.Text = TBMilliseconds.Text.Substring(1, TBMilliseconds.Text.Length - 1);
                }
            }
            CalcInterval();
        }

        private void TBMilliseconds_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TBMilliseconds_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.KeyCode == Keys.C) || (e.Control && e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void clickToggleButton_Click(object sender, EventArgs e)
        {
            ToggleMacro();
        }

        private void autoclicker_Tick(object sender, EventArgs e)
        {
            SimulateClick();
        }

        private void SimulateClick()
        {
            if (isAutoclickerDoubleClick)
            {
                mouse_event(mouse_down | mouse_up, 0, 0, 0, 0);
                mouse_event(mouse_down | mouse_up, 0, 0, 0, 0);
            }
            else mouse_event(mouse_down | mouse_up, 0, 0, 0, 0);
        }

        protected override void OnFormClosing(FormClosingEventArgs e) // Unregister hotkeys so they won't bother other apps.
        {
            UnregisterHotKey(this.Handle, 1);
            base.OnFormClosing(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void RepetitionCB_CheckedChanged(object sender, EventArgs e)
        {
            if (RepetitionCB.Checked)
            {
                repetitionsNumBox.Enabled = true;
            }
            else
            {
                repetitionsNumBox.Enabled = false;
            }
        }

        private void MButtonCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MButtonCB.SelectedItem == "Left")
            {
                mouse_down = 0x02;
                mouse_up = 0x04;
            }
            else if (MButtonCB.SelectedItem == "Middle")
            {
                mouse_down = 0x002;
                mouse_up = 0x004;
            }
            else if (MButtonCB.SelectedItem == "Right")
            {
                mouse_down = 0x0008;
                mouse_up = 0x0010;
            }
        }

        private void CTypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CTypeCB.SelectedItem == "Double")
            {
                isAutoclickerDoubleClick = true;
            }
            else isAutoclickerDoubleClick = false;
        }
    }
}
 