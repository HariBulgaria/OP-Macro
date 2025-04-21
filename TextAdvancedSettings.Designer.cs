namespace OP_Macro
{
    partial class TextAdvancedSettings
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
            startButtonSecond = new Button();
            displayBox = new RichTextBox();
            displayBoxSecond = new RichTextBox();
            doneButton = new Button();
            cancelButton = new Button();
            displayBoxThird = new RichTextBox();
            startButtonThird = new Button();
            SuspendLayout();
            // 
            // startButton
            // 
            startButton.FlatStyle = FlatStyle.Flat;
            startButton.Location = new Point(12, 12);
            startButton.Name = "startButton";
            startButton.Size = new Size(100, 40);
            startButton.TabIndex = 0;
            startButton.Text = "Смени клавиш за текст 1";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // startButtonSecond
            // 
            startButtonSecond.FlatStyle = FlatStyle.Flat;
            startButtonSecond.Location = new Point(12, 58);
            startButtonSecond.Name = "startButtonSecond";
            startButtonSecond.Size = new Size(100, 40);
            startButtonSecond.TabIndex = 6;
            startButtonSecond.Text = "Смени клавиш за текст 2";
            startButtonSecond.UseVisualStyleBackColor = true;
            startButtonSecond.Click += startButtonSecond_Click;
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
            // cancelButton
            // 
            cancelButton.FlatStyle = FlatStyle.Flat;
            cancelButton.Location = new Point(253, 108);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(100, 39);
            cancelButton.TabIndex = 5;
            cancelButton.Text = "Отказ";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // displayBoxThird
            // 
            displayBoxThird.BorderStyle = BorderStyle.FixedSingle;
            displayBoxThird.Enabled = false;
            displayBoxThird.Location = new Point(147, 108);
            displayBoxThird.Name = "displayBoxThird";
            displayBoxThird.Size = new Size(100, 38);
            displayBoxThird.TabIndex = 9;
            displayBoxThird.Text = "";
            // 
            // startButtonThird
            // 
            startButtonThird.FlatStyle = FlatStyle.Flat;
            startButtonThird.Location = new Point(12, 104);
            startButtonThird.Name = "startButtonThird";
            startButtonThird.Size = new Size(100, 40);
            startButtonThird.TabIndex = 8;
            startButtonThird.Text = "Смени клавиш за текст 3";
            startButtonThird.UseVisualStyleBackColor = true;
            startButtonThird.Click += startButtonThird_Click;
            // 
            // TextAdvancedSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(364, 158);
            Controls.Add(displayBoxThird);
            Controls.Add(startButtonThird);
            Controls.Add(cancelButton);
            Controls.Add(doneButton);
            Controls.Add(displayBoxSecond);
            Controls.Add(displayBox);
            Controls.Add(startButtonSecond);
            Controls.Add(startButton);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "TextAdvancedSettings";
            Text = "Rich Text Hotkey Settings";
            ResumeLayout(false);
        }

        #endregion

        private Button startButton;
        private Button startButtonSecond;
        private RichTextBox displayBox;
        private RichTextBox displayBoxSecond;
        private Button doneButton;
        private Button cancelButton;
        private RichTextBox displayBoxThird;
        private Button startButtonThird;
    }
}