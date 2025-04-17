using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;

namespace OP_Macro
{
    public partial class Form1 : Form
    {
        // Timers
        private System.Windows.Forms.Timer autoclickerTimer;
        private System.Windows.Forms.Timer replayTimer;

        // Booleans
        private bool isAutoclickerRunning = false;
        private bool isAutoclickerDoubleClick = false;
        private bool isRecording = false;
        private bool isReplaying = false;

        // Hotkey Memory
        private uint autoclickerVK = 0; //add more for everything else when making hotkey settings
        private uint textVK = 0;

        // Parameters
        private uint mouse_down = 0x02;
        private uint mouse_up = 0x04;
        private int replayIndex = 0;
        private int counter = 0;
        private int autoclickerRepCounter = 0;
        private DateTime lastRecordTime;
        private List<MouseEventRecord> recordedEvents = new List<MouseEventRecord>();
        private List<Coord> coords = new List<Coord>();
        private IntPtr hookID = IntPtr.Zero; // Bottom ones are for Record!
        private LowLevelMouseProc proc;
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        private const int WH_MOUSE_LL = 14;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_RBUTTONDOWN = 0x0204;
        private const int WM_MBUTTONDOWN = 0x0207;

        // Imports from user32.dll
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);
        [DllImport("user32.dll")] // Bottom 4 are for Record!
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        public Form1()
        {
            InitializeComponent();

            CTypeCB.SelectedIndex = 0;
            MButtonCB.SelectedIndex = 0;
            TBHours.ContextMenuStrip = new ContextMenuStrip();
            TBMinutes.ContextMenuStrip = new ContextMenuStrip();
            TBSeconds.ContextMenuStrip = new ContextMenuStrip();
            TBMilliseconds.ContextMenuStrip = new ContextMenuStrip();

            // Registering Hotkeys
            bool autoclickerRegister = RegisterHotKey(this.Handle, 1, 0, (uint)Keys.F1);
            autoclickerVK = (uint)Keys.F1;
            bool firstCoordsRegister = RegisterHotKey(this.Handle, 5, 0, (uint)Keys.F2);
            bool secondCoordsRegister = RegisterHotKey(this.Handle, 6, 0, (uint)Keys.F3);
            bool textRegister = RegisterHotKey(this.Handle, 2, 0, (uint)Keys.F4);
            textVK = (uint)Keys.F4;
            bool recordRegister = RegisterHotKey(this.Handle, 3, 0, (uint)Keys.F5);
            bool replayRegister = RegisterHotKey(this.Handle, 4, 0, (uint)Keys.F6);
            bool teleportRegister = RegisterHotKey(this.Handle, 7, 0, (uint)Keys.F7);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                if (m.WParam.ToInt32() == 1)
                {
                    if (RepetitionCB.Checked) ToggleAutoclickerRepetition();
                    else ToggleAutoclicker();
                }
                else if (m.WParam.ToInt32() == 2)
                {
                    if (textCheckBox.Checked)
                    {
                        for (int i = 0; i < textNumBox.Value; i++)
                        {
                            SendKeys.Send(textTB.Text);
                        }
                    }
                    else SendKeys.Send(textTB.Text);
                }
                else if (m.WParam.ToInt32() == 3)
                {
                    Record();
                }
                else if (m.WParam.ToInt32() == 4)
                {
                    StartReplay();
                }
                else if (m.WParam.ToInt32() == 5)
                {
                    bool checker = false;
                    teleportX1.Text = Cursor.Position.X.ToString();
                    teleportY1.Text = Cursor.Position.Y.ToString();
                    foreach (var coord in coords)
                    {
                        if (coord.Id == 1)
                        {
                            coord.X = int.Parse(teleportX1.Text);
                            coord.Y = int.Parse(teleportY1.Text);
                            checker = true;
                        }
                    }
                    if (!checker)
                    {
                        coords.Add(new Coord(1, int.Parse(teleportX1.Text), int.Parse(teleportY1.Text)));
                    }
                }
                else if (m.WParam.ToInt32() == 6)
                {
                    bool checker = false;
                    teleportX2.Text = Cursor.Position.X.ToString();
                    teleportY2.Text = Cursor.Position.Y.ToString();
                    foreach (var coord in coords)
                    {
                        if (coord.Id == 2)
                        {
                            coord.X = int.Parse(teleportX2.Text);
                            coord.Y = int.Parse(teleportY2.Text);
                            checker = true;
                        }
                    }
                    if (!checker)
                    {
                        coords.Add(new Coord(2, int.Parse(teleportX2.Text), int.Parse(teleportY2.Text)));
                    }
                }
                else if (m.WParam.ToInt32() == 7)
                {
                    TeleportFunc();
                }
            }
            base.WndProc(ref m);
        }

        private void TeleportFunc()
        {
            if (coords.Count != 0)
            {
                if (counter >= coords.Count) counter = 0;
                SetCursorPos(coords[counter].X, coords[counter].Y);
                counter++;
            }
            else
            {
                MessageBox.Show("No coordinates preset!");
            }
        }

        private void StartReplay()
        {
            if (!isReplaying)
            {
                if (recordedEvents.Count == 0)
                {
                    MessageBox.Show("No events recorded.");
                    return;
                }

                replayIndex = 0;
                replayTimer = new System.Windows.Forms.Timer();
                replayTimer.Tick += ReplayTimer_Tick;
                replayTimer.Interval = recordedEvents[replayIndex].Delay;
                isReplaying = true;
                replayTimer.Start();
            }
            else
            {
                replayTimer.Stop();
                isReplaying = false;
            }
        }

        private void ReplayTimer_Tick(object sender, EventArgs e)
        {
            if (replayIndex >= recordedEvents.Count)
            {
                // Restart the replay loop
                replayTimer.Stop();
                replayIndex = 0;
                replayTimer.Interval = Math.Max(recordedEvents[replayIndex].Delay, 1);
                replayTimer.Start();
                return;
            }

            // Get the next recorded event
            var recordedEvent = recordedEvents[replayIndex];

            // Move cursor to the recorded position
            SetCursorPos(recordedEvent.X, recordedEvent.Y);

            // Simulate mouse click based on the recorded button
            switch (recordedEvent.Button)
            {
                case WM_LBUTTONDOWN:
                    mouse_event(mouse_down | mouse_up, (uint)recordedEvent.X, (uint)recordedEvent.Y, 0, 0);
                    break;
                case WM_RBUTTONDOWN:
                    mouse_event(0x08 | 0x10, (uint)recordedEvent.X, (uint)recordedEvent.Y, 0, 0); // Right down + up
                    break;
                case WM_MBUTTONDOWN:
                    mouse_event(0x20 | 0x40, (uint)recordedEvent.X, (uint)recordedEvent.Y, 0, 0); // Middle down + up
                    break;
            }

            // Move to the next event
            replayIndex++;

            // If there are more events, update the timer interval for the next event
            if (replayIndex < recordedEvents.Count)
            {
                replayTimer.Interval = Math.Max(recordedEvents[replayIndex].Delay, 1);
            }
        }

        private IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && isRecording)
            {
                int message = wParam.ToInt32();
                if (message == WM_LBUTTONDOWN || message == WM_RBUTTONDOWN || message == WM_MBUTTONDOWN || message == 0x0200) // 0x0200 = WM_MOUSEMOVE
                {
                    int delay = (int)(DateTime.Now - lastRecordTime).TotalMilliseconds;

                    // SKIP recording movements that happen too fast (optional, only for WM_MOUSEMOVE)
                    if (message == 0x0200 && delay < 10)  // Example: only accept movement if at least 10ms passed
                    {
                        return CallNextHookEx(hookID, nCode, wParam, lParam);
                    }

                    lastRecordTime = DateTime.Now;

                    var pos = Cursor.Position;

                    recordedEvents.Add(new MouseEventRecord
                    {
                        X = pos.X,
                        Y = pos.Y,
                        Delay = delay,
                        Button = message
                    });
                }
            }

            return CallNextHookEx(hookID, nCode, wParam, lParam);
        }

        private void Record()
        {
            if (!isRecording)
            {
                isRecording = true;
                recordedEvents.Clear();      // Clear previous recordings
                lastRecordTime = DateTime.Now; // Set start time for recording
                proc = MouseHookCallback;
                hookID = SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(null), 0);
            }
            else
            {
                isRecording = false;
                UnhookWindowsHookEx(hookID);
            }
        }

        private async void ToggleAutoclicker()
        {
            if (!isAutoclickerRunning)
            {
                // Start the autoclicker
                isAutoclickerRunning = true;
                autoclicker.Start();
                clickToggleButton.Text = "Toggle (ON)";
                clickToggleButton.Enabled = false;
                await Task.Delay(1000);
                clickToggleButton.Enabled = true;
                RepetitionCB.Enabled = false;
                repetitionsNumBox.Enabled = false;
            }
            else
            {
                // Stop the autoclicker
                isAutoclickerRunning = false;
                autoclicker.Stop();
                clickToggleButton.Text = "Toggle";
                RepetitionCB.Enabled = true;
                repetitionsNumBox.Enabled = true;
            }
        }

        private async void ToggleAutoclickerRepetition()
        {
            if (!isAutoclickerRunning)
            {
                isAutoclickerRunning = true;
                autoclickerRepetition.Start();
                clickToggleButton.Text = "Toggle (ON)";
                clickToggleButton.Enabled = false;
                await Task.Delay(1000);
                clickToggleButton.Enabled = true;
                RepetitionCB.Enabled = false;
                repetitionsNumBox.Enabled = false;
            }
            else
            {
                isAutoclickerRunning = false;
                autoclickerRepetition.Stop();
                clickToggleButton.Text = "Toggle";
                RepetitionCB.Enabled = true;
                repetitionsNumBox.Enabled = true;
            }
        }

        private void CalcInterval()
        {
            autoclicker.Interval = 3600000 * int.Parse(TBHours.Text) + 60000 * int.Parse(TBMinutes.Text) + 1000 * int.Parse(TBSeconds.Text) + int.Parse(TBMilliseconds.Text);
            autoclickerRepetition.Interval = 3600000 * int.Parse(TBHours.Text) + 60000 * int.Parse(TBMinutes.Text) + 1000 * int.Parse(TBSeconds.Text) + int.Parse(TBMilliseconds.Text);
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
            ToggleAutoclicker();
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

        private void autoclickerRepetition_Tick(object sender, EventArgs e)
        {
            autoclickerRepCounter++;
            if (autoclickerRepCounter >= repetitionsNumBox.Value)
            {
                ToggleAutoclickerRepetition();
                autoclickerRepCounter = 0;
            }
            SimulateClick();
        }

        private void textCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (textCheckBox.Checked)
            {
                textNumBox.Enabled = true;
            }
            else
            {
                textNumBox.Enabled = false;
            }
        }

        private void clickHSButton_Click(object sender, EventArgs e)
        {
            HotkeySettings autoclickerHotkeySettings = new HotkeySettings(autoclickerVK);
            autoclickerHotkeySettings.ShowDialog();
        }

        private void textHSButton_Click(object sender, EventArgs e)
        {
            HotkeySettings autoclickerHotkeySettings = new HotkeySettings(textVK);
            autoclickerHotkeySettings.ShowDialog();
        }
    }
}
 