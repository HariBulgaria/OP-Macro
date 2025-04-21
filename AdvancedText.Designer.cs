namespace OP_Macro
{
    partial class AdvancedText
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
            richTextBox = new RichTextBox();
            saveButton = new Button();
            cancelButton = new Button();
            SuspendLayout();
            // 
            // richTextBox
            // 
            richTextBox.BorderStyle = BorderStyle.FixedSingle;
            richTextBox.Location = new Point(12, 12);
            richTextBox.Name = "richTextBox";
            richTextBox.Size = new Size(460, 403);
            richTextBox.TabIndex = 0;
            richTextBox.Text = "";
            // 
            // saveButton
            // 
            saveButton.FlatStyle = FlatStyle.Flat;
            saveButton.Location = new Point(12, 421);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(91, 32);
            saveButton.TabIndex = 1;
            saveButton.Text = "Запази";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.FlatStyle = FlatStyle.Flat;
            cancelButton.Location = new Point(381, 421);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(91, 32);
            cancelButton.TabIndex = 2;
            cancelButton.Text = "Отказ";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // AdvancedText
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 461);
            Controls.Add(cancelButton);
            Controls.Add(saveButton);
            Controls.Add(richTextBox);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "AdvancedText";
            Text = "Rich Text";
            Load += AdvancedText_Load;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox richTextBox;
        private Button saveButton;
        private Button cancelButton;
    }
}