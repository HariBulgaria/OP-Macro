using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OP_Macro
{
    public partial class CoordinatesHotkeySettings : Form
    {
        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;

        public uint newFirstKey;
        public uint newSecondKey;
        private bool hotkeyDetecting = false;

        public CoordinatesHotkeySettings(uint firstKey, uint secondKey)
        {
            InitializeComponent();
            newFirstKey = firstKey;
            newSecondKey = secondKey;
            displayBox.Text = ((Keys)newFirstKey).ToString();
            displayBoxSecond.Text = ((Keys)newSecondKey).ToString();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (!hotkeyDetecting)
            {
                _proc = HookCallback;
                _hookID = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, GetModuleHandle(null), 0);
                hotkeyDetecting = true;
                displayBox.Text = "Натисни бутон...";
            }
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            StopKeyHook();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            StopKeyHook();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void StopKeyHook()
        {
            if (hotkeyDetecting)
            {
                UnhookWindowsHookEx(_hookID);
                hotkeyDetecting = false;
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                newFirstKey = (uint)vkCode;
                displayBox.Text = ((Keys)vkCode).ToString();
                StopKeyHook();
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private IntPtr HookCallbackSecond(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                newSecondKey = (uint)vkCode;
                displayBoxSecond.Text = ((Keys)vkCode).ToString();
                StopKeyHook();
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private void startButtonSecond_Click(object sender, EventArgs e)
        {
            if (!hotkeyDetecting)
            {
                _proc = HookCallbackSecond;
                _hookID = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, GetModuleHandle(null), 0);
                hotkeyDetecting = true;
                displayBoxSecond.Text = "Натисни бутон...";
            }
        }
    }
}
