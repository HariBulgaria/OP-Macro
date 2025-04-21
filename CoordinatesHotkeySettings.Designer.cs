namespace OP_Macro
{
    partial class CoordinatesHotkeySettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            startButton = new Button();
            doneButton = new Button();
            displayBox = new RichTextBox();
            cancelButton = new Button();
            startButtonSecond = new Button();
            displayBoxSecond = new RichTextBox();
            SuspendLayout();
            // 
            // startButton
            // 
            startButton.FlatStyle = FlatStyle.Flat;
            startButton.Location = new Point(12, 12);
            startButton.Name = "startButton";
            startButton.Size = new Size(100, 40);
            startButton.TabIndex = 0;
            startButton.Text = "Смени клавиш за координат 1";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // doneButton
            // 
            doneButton.FlatStyle = FlatStyle.Flat;
            doneButton.Location = new Point(253, 13);
            doneButton.Name = "doneButton";
            doneButton.Size = new Size(100, 39);
            doneButton.TabIndex = 4;
            doneButton.Text = "Готово";
            doneButton.UseVisualStyleBackColor = true;
            doneButton.Click += doneButton_Click;
            // 
            // displayBox
            // 
            displayBox.BorderStyle = BorderStyle.FixedSingle;
            displayBox.Enabled = false;
            displayBox.Location = new Point(147, 12);
            displayBox.Name = "displayBox";
            displayBox.Size = new Size(100, 38);
            displayBox.TabIndex = 3;
            displayBox.Text = "";
            // 
            // cancelButton
            // 
            cancelButton.FlatStyle = FlatStyle.Flat;
            cancelButton.Location = new Point(253, 58);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(100, 39);
            cancelButton.TabIndex = 5;
            cancelButton.Text = "Отказ";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // startButtonSecond
            // 
            startButtonSecond.FlatStyle = FlatStyle.Flat;
            startButtonSecond.Location = new Point(12, 58);
            startButtonSecond.Name = "startButtonSecond";
            startButtonSecond.Size = new Size(100, 40);
            startButtonSecond.TabIndex = 6;
            startButtonSecond.Text = "Смени клавиш за координат 2";
            startButtonSecond.UseVisualStyleBackColor = true;
            startButtonSecond.Click += startButtonSecond_Click;
            // 
            // displayBoxSecond
            // 
            displayBoxSecond.BorderStyle = BorderStyle.FixedSingle;
            displayBoxSecond.Enabled = false;
            displayBoxSecond.Location = new Point(147, 60);
            displayBoxSecond.Name = "displayBoxSecond";
            displayBoxSecond.Size = new Size(100, 38);
            displayBoxSecond.TabIndex = 7;
            displayBoxSecond.Text = "";
            // 
            // CoordinatesHotkeySettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(364, 109);
            Controls.Add(displayBoxSecond);
            Controls.Add(startButtonSecond);
            Controls.Add(cancelButton);
            Controls.Add(displayBox);
            Controls.Add(doneButton);
            Controls.Add(startButton);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "CoordinatesHotkeySettings";
            Text = "Coordinates Hotkey Settings";
            ResumeLayout(false);
        }

        #endregion

        private Button startButton;
        private Button doneButton;
        private RichTextBox displayBox;
        private Button cancelButton;
        private Button startButtonSecond;
        private RichTextBox displayBoxSecond;
    }
}