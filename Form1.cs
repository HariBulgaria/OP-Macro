using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Xml;
using Newtonsoft.Json;

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
        private bool listeningForKeys = true;

        // Hotkey Memory
        private uint autoclickerKey = 0; //add more for everything else when making hotkey settings
        private uint textKey = 0;
        private uint recordKey = 0;
        private uint replayKey = 0;
        private uint firstCoordKey = 0;
        private uint secondCoordKey = 0;
        private uint teleportKey = 0;
        private uint richText1Key = 0;
        private uint richText2Key = 0;
        private uint richText3Key = 0;

        // Parameters
        private uint mouse_down = 0x02;
        private uint mouse_up = 0x04;
        private int replayIndex = 0;
        private int counter = 0;
        private int autoclickerRepCounter = 0;
        private int loopsCounter = 0;
        private int loopsCounterTeleport = 0;
        private int recordClicksCounter = 0;
        private DateTime lastRecordTime;
        private List<MouseEventRecord> recordedEvents = new List<MouseEventRecord>();
        private List<Coord> coords = new List<Coord>();
        private IntPtr hookID = IntPtr.Zero; // Bottom ones are for recording any mouse input
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
        [DllImport("user32.dll")] // Bottom 4 are for recording any mouse input
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

            LoadSavedRecording();
            CTypeCB.SelectedIndex = 0;
            MButtonCB.SelectedIndex = 0;
            TBHours.ContextMenuStrip = new ContextMenuStrip();
            TBMinutes.ContextMenuStrip = new ContextMenuStrip();
            TBSeconds.ContextMenuStrip = new ContextMenuStrip();
            TBMilliseconds.ContextMenuStrip = new ContextMenuStrip();

            // Extracting keys from settings
            autoclickerKey = Properties.Settings.Default.autoclickerHotkeyProperty;
            textKey = Properties.Settings.Default.textHotkeyProperty;
            recordKey = Properties.Settings.Default.recordHotkeyProperty;
            replayKey = Properties.Settings.Default.replayHotkeyProperty;
            firstCoordKey = Properties.Settings.Default.firstCoordHotkeyProperty;
            secondCoordKey = Properties.Settings.Default.secondCoordHotkeyProperty;
            teleportKey = Properties.Settings.Default.teleportHotkeyProperty;
            richText1Key = Properties.Settings.Default.richText1HotkeyProperty;
            richText2Key = Properties.Settings.Default.richText2HotkeyProperty;
            richText3Key = Properties.Settings.Default.richText3HotkeyProperty;

            // Registering Hotkeys
            bool autoclickerRegister = RegisterHotKey(this.Handle, 1, 0, autoclickerKey);
            bool firstCoordsRegister = RegisterHotKey(this.Handle, 5, 0, firstCoordKey);
            bool secondCoordsRegister = RegisterHotKey(this.Handle, 6, 0, secondCoordKey);
            bool textRegister = RegisterHotKey(this.Handle, 2, 0, textKey);
            bool recordRegister = RegisterHotKey(this.Handle, 3, 0, recordKey);
            bool replayRegister = RegisterHotKey(this.Handle, 4, 0, replayKey);
            bool teleportRegister = RegisterHotKey(this.Handle, 7, 0, teleportKey);
            bool richText1Register = RegisterHotKey(this.Handle, 8, 0, richText1Key);
            bool richText2Register = RegisterHotKey(this.Handle, 9, 0, richText2Key);
            bool richText3Register = RegisterHotKey(this.Handle, 10, 0, richText3Key);
        }

        protected override void WndProc(ref Message m)
        {
            if (!listeningForKeys)
            {
                base.WndProc(ref m);
                return;
            }

            if (m.Msg == 0x0312)
            {
                if (m.WParam.ToInt32() == 1)
                {
                    if (RepetitionCB.Checked) ToggleAutoclickerRepetition();
                    else ToggleAutoclicker();
                }
                else if (m.WParam.ToInt32() == 2)
                {
                    UnregisterEverything();
                    if (textCheckBox.Checked)
                    {
                        for (int i = 0; i < textNumBox.Value; i++)
                        {
                            SendKeys.Send(textTB.Text);
                        }
                    }
                    else SendKeys.Send(textTB.Text);
                    RegisterEverything();
                }
                else if (m.WParam.ToInt32() == 3)
                {
                    recordActionLabel.Text = "Активно";
                    recordClicksCounter = 0;
                    Record();
                }
                else if (m.WParam.ToInt32() == 4)
                {
                    replayActionLabel.Text = "Активно";
                    loopsCounter = 0;
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
                else if (m.WParam.ToInt32() == 8)
                {
                    UnregisterEverything();
                    string filePath = Path.Combine(Application.StartupPath, "text1.txt");

                    if (File.Exists(filePath))
                    {
                        string textToSend = File.ReadAllText(filePath);
                        if (textCheckBox.Checked)
                        {
                            for (int i = 0; i < textNumBox.Value; i++)
                            {
                                SendKeys.Send(textToSend);
                            }
                        }
                        else SendKeys.Send(textToSend);
                    }
                    else
                    {
                        MessageBox.Show("Запазеният текст не е намерен!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    RegisterEverything();
                }
                else if (m.WParam.ToInt32() == 9)
                {
                    UnregisterEverything();
                    string filePath = Path.Combine(Application.StartupPath, "text2.txt");

                    if (File.Exists(filePath))
                    {
                        string textToSend = File.ReadAllText(filePath);
                        if (textCheckBox.Checked)
                        {
                            for (int i = 0; i < textNumBox.Value; i++)
                            {
                                SendKeys.Send(textToSend);
                            }
                        }
                        else SendKeys.Send(textToSend);
                    }
                    else
                    {
                        MessageBox.Show("Запазеният текст не е намерен!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    RegisterEverything();
                }
                else if (m.WParam.ToInt32() == 10)
                {
                    UnregisterEverything();
                    string filePath = Path.Combine(Application.StartupPath, "text3.txt");

                    if (File.Exists(filePath))
                    {
                        string textToSend = File.ReadAllText(filePath);
                        if (textCheckBox.Checked)
                        {
                            for (int i = 0; i < textNumBox.Value; i++)
                            {
                                SendKeys.Send(textToSend);
                            }
                        }
                        else SendKeys.Send(textToSend);
                    }
                    else
                    {
                        MessageBox.Show("Запазеният текст не е намерен!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    RegisterEverything();
                }
            }
            base.WndProc(ref m);
        }

        private void UnregisterEverything()
        {
            UnregisterHotKey(this.Handle, 1);
            UnregisterHotKey(this.Handle, 2);
            UnregisterHotKey(this.Handle, 3);
            UnregisterHotKey(this.Handle, 4);
            UnregisterHotKey(this.Handle, 5);
            UnregisterHotKey(this.Handle, 6);
            UnregisterHotKey(this.Handle, 7);
            UnregisterHotKey(this.Handle, 8);
            UnregisterHotKey(this.Handle, 9);
            UnregisterHotKey(this.Handle, 10);
        }

        private void RegisterEverything()
        {
            RegisterHotKey(this.Handle, 1, 0, autoclickerKey);
            RegisterHotKey(this.Handle, 5, 0, firstCoordKey);
            RegisterHotKey(this.Handle, 6, 0, secondCoordKey);
            RegisterHotKey(this.Handle, 2, 0, textKey);
            RegisterHotKey(this.Handle, 3, 0, recordKey);
            RegisterHotKey(this.Handle, 4, 0, replayKey);
            RegisterHotKey(this.Handle, 7, 0, teleportKey);
            RegisterHotKey(this.Handle, 8, 0, richText1Key);
            RegisterHotKey(this.Handle, 9, 0, richText2Key);
            RegisterHotKey(this.Handle, 10, 0, richText3Key);
        }

        private void TeleportFunc()
        {
            if (coords.Count != 0)
            {
                if (counter >= coords.Count)
                {
                    counter = 0;
                    loopsCounterTeleport++;
                    teleportLoopsLabel.Text = "Повторения: " + loopsCounterTeleport;
                }
                SetCursorPos(coords[counter].X, coords[counter].Y);
                counter++;
                teleportPositionLabel.Text = "Индекс: " + counter;
            }
            else
            {
                MessageBox.Show("Няма поставени координати!");
            }
        }

        private void StartReplay()
        {
            if (!isReplaying)
            {
                if (recordedEvents.Count == 0)
                {
                    MessageBox.Show("Няма нищо заснето.");
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
                replayActionLabel.Text = "Неактивно";
            }
        }

        private void ReplayTimer_Tick(object sender, EventArgs e)
        {
            if (replayIndex >= recordedEvents.Count)
            {
                replayTimer.Stop();
                replayIndex = 0;
                loopsCounter++;
                replayTimer.Interval = Math.Max(recordedEvents[replayIndex].Delay, 1);
                replayTimer.Start();
                replayLoopLabel.Text = "Повторения: " + loopsCounter.ToString();
                return;
            }

            var recordedEvent = recordedEvents[replayIndex];

            SetCursorPos(recordedEvent.X, recordedEvent.Y);

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

            replayIndex++;

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
                if (message == WM_LBUTTONDOWN || message == WM_RBUTTONDOWN || message == WM_MBUTTONDOWN || message == 0x0200) // 0x0200 = mouse move
                {
                    if (message == WM_LBUTTONDOWN)
                    {
                        recordClicksCounter++;
                        recordClicksLabel.Text = "Кликове: " + recordClicksCounter;
                    }
                    int delay = (int)(DateTime.Now - lastRecordTime).TotalMilliseconds;

                    // Skips recording movements that happen too fast
                    if (message == 0x0200 && delay < 10)  // This thing accepts movement only if it has passed in the last 10 ms
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
                recordActionLabel.Text = "Неактивно";
                isRecording = false;
                UnhookWindowsHookEx(hookID);
            }
        }

        private async void ToggleAutoclicker()
        {
            if (!isAutoclickerRunning)
            {
                isAutoclickerRunning = true;
                autoclicker.Start();
                clickToggleButton.Text = "Изключи";
                clickToggleButton.Enabled = false;
                RepetitionCB.Enabled = false;
                repetitionsNumBox.Enabled = false;
                await Task.Delay(1000);
                clickToggleButton.Enabled = true;
            }
            else
            {
                isAutoclickerRunning = false;
                autoclicker.Stop();
                clickToggleButton.Text = "Включи";
                RepetitionCB.Enabled = true;
                if (RepetitionCB.Checked)
                {
                    repetitionsNumBox.Enabled = true;
                }
            }
        }

        private async void ToggleAutoclickerRepetition()
        {
            if (!isAutoclickerRunning)
            {
                isAutoclickerRunning = true;
                autoclickerRepetition.Start();
                clickToggleButton.Text = "Изключи";
                clickToggleButton.Enabled = false;
                RepetitionCB.Enabled = false;
                repetitionsNumBox.Enabled = false;
                await Task.Delay(1000);
                clickToggleButton.Enabled = true;
            }
            else
            {
                isAutoclickerRunning = false;
                autoclickerRepetition.Stop();
                clickToggleButton.Text = "Включи";
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
                MessageBox.Show("Часовете не могат да са под 0.");
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
                MessageBox.Show("Минутите не могат да са под 0.");
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
                MessageBox.Show("Секундите не могат да са 0.");
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
                MessageBox.Show("Милисекундите не могат да са 1.");
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
            if (RepetitionCB.Checked)
            {
                ToggleAutoclickerRepetition();
            }
            else ToggleAutoclicker();
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
            UnregisterEverything();
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
            if (autoclickerRepCounter >= repetitionsNumBox.Value)
            {
                ToggleAutoclickerRepetition();
                autoclickerRepCounter = 0;
            }
            SimulateClick();
            autoclickerRepCounter++;
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
            listeningForKeys = false;

            using (HotkeySettings autoclickerHotkeySettings = new HotkeySettings(autoclickerKey))
            {
                if (autoclickerHotkeySettings.ShowDialog() == DialogResult.OK)
                {
                    UnregisterHotKey(this.Handle, 1);
                    autoclickerKey = (uint)autoclickerHotkeySettings.newKey;
                    Properties.Settings.Default.autoclickerHotkeyProperty = autoclickerKey;
                    Properties.Settings.Default.Save();
                    RegisterHotKey(this.Handle, 1, 0, (uint)autoclickerKey);
                }
            }

            listeningForKeys = true;
        }

        private void textHSButton_Click(object sender, EventArgs e)
        {
            listeningForKeys = false;

            using (HotkeySettings textHotkeySettings = new HotkeySettings(textKey))
            {
                if (textHotkeySettings.ShowDialog() == DialogResult.OK)
                {
                    UnregisterHotKey(this.Handle, 2);
                    textKey = (uint)textHotkeySettings.newKey;
                    Properties.Settings.Default.textHotkeyProperty = textKey;
                    Properties.Settings.Default.Save();
                    RegisterHotKey(this.Handle, 2, 0, (uint)textKey);
                }
            }

            listeningForKeys = true;
        }

        private void recordHSButton_Click(object sender, EventArgs e)
        {
            listeningForKeys = false;

            using (HotkeySettings recordHotkeySettings = new HotkeySettings(recordKey))
            {
                if (recordHotkeySettings.ShowDialog() == DialogResult.OK)
                {
                    UnregisterHotKey(this.Handle, 3);
                    recordKey = (uint)recordHotkeySettings.newKey;
                    Properties.Settings.Default.recordHotkeyProperty = recordKey;
                    Properties.Settings.Default.Save();
                    RegisterHotKey(this.Handle, 3, 0, (uint)recordKey);
                }
            }

            listeningForKeys = true;
        }

        private void replayHSButton_Click(object sender, EventArgs e)
        {
            listeningForKeys = false;

            using (HotkeySettings replayHotkeySettings = new HotkeySettings(replayKey))
            {
                if (replayHotkeySettings.ShowDialog() == DialogResult.OK)
                {
                    UnregisterHotKey(this.Handle, 4);
                    replayKey = (uint)replayHotkeySettings.newKey;
                    Properties.Settings.Default.replayHotkeyProperty = replayKey;
                    Properties.Settings.Default.Save();
                    RegisterHotKey(this.Handle, 4, 0, (uint)replayKey);
                }
            }

            listeningForKeys = true;
        }

        private void teleportHSButton_Click(object sender, EventArgs e)
        {
            listeningForKeys = false;

            using (CoordinatesHotkeySettings coordsHotkeySettings = new CoordinatesHotkeySettings(firstCoordKey, secondCoordKey))
            {
                if (coordsHotkeySettings.ShowDialog() == DialogResult.OK)
                {
                    UnregisterHotKey(this.Handle, 5);
                    UnregisterHotKey(this.Handle, 6);
                    firstCoordKey = (uint)coordsHotkeySettings.newFirstKey;
                    secondCoordKey = (uint)coordsHotkeySettings.newSecondKey;
                    Properties.Settings.Default.firstCoordHotkeyProperty = firstCoordKey;
                    Properties.Settings.Default.secondCoordHotkeyProperty = secondCoordKey;
                    Properties.Settings.Default.Save();
                    RegisterHotKey(this.Handle, 5, 0, (uint)firstCoordKey);
                    RegisterHotKey(this.Handle, 6, 0, (uint)secondCoordKey);
                }
            }

            listeningForKeys = true;
        }

        private void teleport2HSButton_Click(object sender, EventArgs e)
        {
            listeningForKeys = false;

            using (HotkeySettings teleportHotkeySettings = new HotkeySettings(teleportKey))
            {
                if (teleportHotkeySettings.ShowDialog() == DialogResult.OK)
                {
                    UnregisterHotKey(this.Handle, 7);
                    teleportKey = (uint)teleportHotkeySettings.newKey;
                    Properties.Settings.Default.teleportHotkeyProperty = teleportKey;
                    Properties.Settings.Default.Save();
                    RegisterHotKey(this.Handle, 7, 0, (uint)teleportKey);
                }
            }

            listeningForKeys = true;
        }

        private void richText1_Click(object sender, EventArgs e)
        {
            listeningForKeys = false;

            using (AdvancedText richText = new AdvancedText(Path.Combine(Application.StartupPath, "text1.txt")))
            {
                richText.ShowDialog();
            }

            listeningForKeys = true;
        }

        private void richText2_Click(object sender, EventArgs e)
        {
            listeningForKeys = false;

            using (AdvancedText richText = new AdvancedText(Path.Combine(Application.StartupPath, "text2.txt")))
            {
                richText.ShowDialog();
            }

            listeningForKeys = true;
        }

        private void richText3_Click(object sender, EventArgs e)
        {
            listeningForKeys = false;

            using (AdvancedText richText = new AdvancedText(Path.Combine(Application.StartupPath, "text3.txt")))
            {
                richText.ShowDialog();
            }

            listeningForKeys = true;
        }

        private void textASButton_Click(object sender, EventArgs e)
        {
            listeningForKeys = false;

            using (TextAdvancedSettings textAdvancedSettings = new TextAdvancedSettings(richText1Key, richText2Key, richText3Key))
            {
                if (textAdvancedSettings.ShowDialog() == DialogResult.OK)
                {
                    UnregisterHotKey(this.Handle, 8);
                    UnregisterHotKey(this.Handle, 9);
                    UnregisterHotKey(this.Handle, 10);
                    richText1Key = (uint)textAdvancedSettings.newFirstKey;
                    richText2Key = (uint)textAdvancedSettings.newSecondKey;
                    richText3Key = (uint)textAdvancedSettings.newThirdKey;
                    Properties.Settings.Default.richText1HotkeyProperty = richText1Key;
                    Properties.Settings.Default.richText2HotkeyProperty = richText2Key;
                    Properties.Settings.Default.richText3HotkeyProperty = richText3Key;
                    Properties.Settings.Default.Save();
                    RegisterHotKey(this.Handle, 8, 0, (uint)richText1Key);
                    RegisterHotKey(this.Handle, 9, 0, (uint)richText2Key);
                    RegisterHotKey(this.Handle, 10, 0, (uint)richText3Key);
                }
            }

            listeningForKeys = true;
        }

        private void saveCaptureButton_Click(object sender, EventArgs e)
        {
            if (recordedEvents.Count > 0)
            {
                string savePath = Path.Combine(Application.StartupPath, "savedRecording.json");

                string json = JsonConvert.SerializeObject(recordedEvents, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(savePath, json);

                MessageBox.Show("Запис запазен успешно!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Запис липсващ!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadSavedRecording()
        {
            string savePath = Path.Combine(Application.StartupPath, "savedRecording.json");

            if (File.Exists(savePath))
            {
                string json = File.ReadAllText(savePath);
                recordedEvents = JsonConvert.DeserializeObject<List<MouseEventRecord>>(json) ?? new List<MouseEventRecord>();
            }
        }

        private void teleportCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (teleportCheckBox.Checked)
            {
                teleportPositionLabel.Visible = true;
                teleportLoopsLabel.Visible = true;
            }
            else
            {
                teleportPositionLabel.Visible = false;
                teleportLoopsLabel.Visible = false;
            }
        }

        private void replayStatsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (replayStatsCheckBox.Checked)
            {
                replayActionLabel.Visible = true;
                replayLoopLabel.Visible = true;
            }
            else
            {
                replayActionLabel.Visible = false;
                replayLoopLabel.Visible = false;
            }
        }

        private void recordStatsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (recordStatsCheckBox.Checked)
            {
                recordClicksLabel.Visible = true;
                recordActionLabel.Visible = true;
            }
            else
            {
                recordClicksLabel.Visible = false;
                recordActionLabel.Visible = false;
            }
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            UnregisterEverything();
            HelpForm help = new HelpForm();
            help.ShowDialog();
            RegisterEverything();
        }
    }
}