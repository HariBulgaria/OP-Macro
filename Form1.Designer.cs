using System.Windows.Forms;
using System.Xml.Linq;

namespace OP_Macro
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            clickGroupBox = new GroupBox();
            clickToggleButton = new Button();
            clickHSButton = new Button();
            labelRepTimes = new Label();
            repetitionsNumBox = new NumericUpDown();
            panel2 = new Panel();
            RepetitionCB = new CheckBox();
            CTypeCB = new ComboBox();
            labelClickType = new Label();
            MButtonCB = new ComboBox();
            labelMouseButton = new Label();
            labelMilliseconds = new Label();
            TBMilliseconds = new TextBox();
            labelSeconds = new Label();
            labelMinutes = new Label();
            labelHours = new Label();
            TBSeconds = new TextBox();
            TBMinutes = new TextBox();
            TBHours = new TextBox();
            panel1 = new Panel();
            textGroupBox = new GroupBox();
            panel6 = new Panel();
            labelTextRep = new Label();
            textCheckBox = new CheckBox();
            textNumBox = new NumericUpDown();
            textASButton = new Button();
            panel3 = new Panel();
            textHSButton = new Button();
            textTB = new TextBox();
            RARGroupBox = new GroupBox();
            replayLoopLabel = new Label();
            replayClickCaughtLabel = new Label();
            replayTimePassedLabel = new Label();
            replayStatsCheckBox = new CheckBox();
            viewRecsButton = new Button();
            replayActionLabel = new Label();
            replayToggleButton = new Button();
            replayHSButton = new Button();
            recordTimeLabel = new Label();
            recordDiscardButton = new Button();
            recordSaveButton = new Button();
            recordActionLabel = new Label();
            recordToggleButton = new Button();
            panel4 = new Panel();
            recordHSButton = new Button();
            teleportGroupBox = new GroupBox();
            teleportTPCaughtLabel = new Label();
            teleportLoopsLabel = new Label();
            teleportPositionLabel = new Label();
            teleportCheckBox = new CheckBox();
            teleportASButton = new Button();
            teleportY2 = new Label();
            teleportHSButton = new Button();
            teleportX2 = new Label();
            teleportY1 = new Label();
            teleportX1 = new Label();
            miscGroupBox = new GroupBox();
            panel5 = new Panel();
            creditsLabel = new Label();
            helpButton = new Button();
            autoclicker = new System.Windows.Forms.Timer(components);
            clickGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)repetitionsNumBox).BeginInit();
            textGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)textNumBox).BeginInit();
            RARGroupBox.SuspendLayout();
            teleportGroupBox.SuspendLayout();
            miscGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // clickGroupBox
            // 
            clickGroupBox.Controls.Add(clickToggleButton);
            clickGroupBox.Controls.Add(clickHSButton);
            clickGroupBox.Controls.Add(labelRepTimes);
            clickGroupBox.Controls.Add(repetitionsNumBox);
            clickGroupBox.Controls.Add(panel2);
            clickGroupBox.Controls.Add(RepetitionCB);
            clickGroupBox.Controls.Add(CTypeCB);
            clickGroupBox.Controls.Add(labelClickType);
            clickGroupBox.Controls.Add(MButtonCB);
            clickGroupBox.Controls.Add(labelMouseButton);
            clickGroupBox.Controls.Add(labelMilliseconds);
            clickGroupBox.Controls.Add(TBMilliseconds);
            clickGroupBox.Controls.Add(labelSeconds);
            clickGroupBox.Controls.Add(labelMinutes);
            clickGroupBox.Controls.Add(labelHours);
            clickGroupBox.Controls.Add(TBSeconds);
            clickGroupBox.Controls.Add(TBMinutes);
            clickGroupBox.Controls.Add(TBHours);
            clickGroupBox.Controls.Add(panel1);
            clickGroupBox.Location = new Point(11, 12);
            clickGroupBox.Name = "clickGroupBox";
            clickGroupBox.Size = new Size(894, 125);
            clickGroupBox.TabIndex = 0;
            clickGroupBox.TabStop = false;
            clickGroupBox.Text = "Click";
            // 
            // clickToggleButton
            // 
            clickToggleButton.FlatStyle = FlatStyle.Flat;
            clickToggleButton.Font = new Font("Segoe UI", 9F);
            clickToggleButton.Location = new Point(747, 75);
            clickToggleButton.Name = "clickToggleButton";
            clickToggleButton.Size = new Size(141, 44);
            clickToggleButton.TabIndex = 18;
            clickToggleButton.Text = "Toggle";
            clickToggleButton.UseVisualStyleBackColor = true;
            // 
            // clickHSButton
            // 
            clickHSButton.BackColor = SystemColors.Control;
            clickHSButton.FlatStyle = FlatStyle.Flat;
            clickHSButton.Font = new Font("Segoe UI", 9F);
            clickHSButton.Location = new Point(747, 19);
            clickHSButton.Name = "clickHSButton";
            clickHSButton.Size = new Size(141, 44);
            clickHSButton.TabIndex = 16;
            clickHSButton.Text = "Hotkey Settings";
            clickHSButton.UseVisualStyleBackColor = false;
            // 
            // labelRepTimes
            // 
            labelRepTimes.AutoSize = true;
            labelRepTimes.Location = new Point(587, 83);
            labelRepTimes.Name = "labelRepTimes";
            labelRepTimes.Size = new Size(41, 15);
            labelRepTimes.TabIndex = 1;
            labelRepTimes.Text = "Times:";
            // 
            // repetitionsNumBox
            // 
            repetitionsNumBox.Enabled = false;
            repetitionsNumBox.Location = new Point(646, 81);
            repetitionsNumBox.Name = "repetitionsNumBox";
            repetitionsNumBox.Size = new Size(88, 23);
            repetitionsNumBox.TabIndex = 15;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Location = new Point(741, 19);
            panel2.Name = "panel2";
            panel2.Size = new Size(1, 100);
            panel2.TabIndex = 2;
            panel2.TabStop = true;
            // 
            // RepetitionCB
            // 
            RepetitionCB.AutoSize = true;
            RepetitionCB.Location = new Point(587, 29);
            RepetitionCB.Name = "RepetitionCB";
            RepetitionCB.Size = new Size(115, 19);
            RepetitionCB.TabIndex = 14;
            RepetitionCB.Text = "Toggle repetition";
            RepetitionCB.UseVisualStyleBackColor = true;
            // 
            // CTypeCB
            // 
            CTypeCB.DropDownStyle = ComboBoxStyle.DropDownList;
            CTypeCB.FormattingEnabled = true;
            CTypeCB.Items.AddRange(new object[] { "Single", "Double" });
            CTypeCB.Location = new Point(424, 80);
            CTypeCB.Name = "CTypeCB";
            CTypeCB.Size = new Size(151, 23);
            CTypeCB.TabIndex = 13;
            // 
            // labelClickType
            // 
            labelClickType.AutoSize = true;
            labelClickType.Location = new Point(343, 83);
            labelClickType.Name = "labelClickType";
            labelClickType.Size = new Size(62, 15);
            labelClickType.TabIndex = 12;
            labelClickType.Text = "Click type:";
            // 
            // MButtonCB
            // 
            MButtonCB.DisplayMember = "Left";
            MButtonCB.DropDownStyle = ComboBoxStyle.DropDownList;
            MButtonCB.FormattingEnabled = true;
            MButtonCB.Items.AddRange(new object[] { "Left", "Right", "Middle" });
            MButtonCB.Location = new Point(117, 80);
            MButtonCB.Name = "MButtonCB";
            MButtonCB.Size = new Size(126, 23);
            MButtonCB.TabIndex = 11;
            // 
            // labelMouseButton
            // 
            labelMouseButton.AutoSize = true;
            labelMouseButton.Location = new Point(6, 83);
            labelMouseButton.Name = "labelMouseButton";
            labelMouseButton.Size = new Size(85, 15);
            labelMouseButton.TabIndex = 10;
            labelMouseButton.Text = "Mouse button:";
            // 
            // labelMilliseconds
            // 
            labelMilliseconds.AutoSize = true;
            labelMilliseconds.Location = new Point(485, 29);
            labelMilliseconds.Name = "labelMilliseconds";
            labelMilliseconds.Size = new Size(73, 15);
            labelMilliseconds.TabIndex = 9;
            labelMilliseconds.Text = "milliseconds";
            // 
            // TBMilliseconds
            // 
            TBMilliseconds.Location = new Point(411, 27);
            TBMilliseconds.Name = "TBMilliseconds";
            TBMilliseconds.Size = new Size(67, 23);
            TBMilliseconds.TabIndex = 8;
            TBMilliseconds.Text = "1000";
            TBMilliseconds.TextAlign = HorizontalAlignment.Right;
            // 
            // labelSeconds
            // 
            labelSeconds.AutoSize = true;
            labelSeconds.Location = new Point(343, 29);
            labelSeconds.Name = "labelSeconds";
            labelSeconds.Size = new Size(50, 15);
            labelSeconds.TabIndex = 7;
            labelSeconds.Text = "seconds";
            // 
            // labelMinutes
            // 
            labelMinutes.AutoSize = true;
            labelMinutes.Location = new Point(203, 29);
            labelMinutes.Name = "labelMinutes";
            labelMinutes.Size = new Size(50, 15);
            labelMinutes.TabIndex = 6;
            labelMinutes.Text = "minutes";
            // 
            // labelHours
            // 
            labelHours.AutoSize = true;
            labelHours.Location = new Point(79, 29);
            labelHours.Name = "labelHours";
            labelHours.Size = new Size(37, 15);
            labelHours.TabIndex = 5;
            labelHours.Text = "hours";
            // 
            // TBSeconds
            // 
            TBSeconds.Location = new Point(270, 27);
            TBSeconds.Name = "TBSeconds";
            TBSeconds.Size = new Size(67, 23);
            TBSeconds.TabIndex = 4;
            TBSeconds.Text = "0";
            TBSeconds.TextAlign = HorizontalAlignment.Right;
            // 
            // TBMinutes
            // 
            TBMinutes.Location = new Point(130, 27);
            TBMinutes.Name = "TBMinutes";
            TBMinutes.Size = new Size(67, 23);
            TBMinutes.TabIndex = 3;
            TBMinutes.Text = "0";
            TBMinutes.TextAlign = HorizontalAlignment.Right;
            // 
            // TBHours
            // 
            TBHours.Location = new Point(5, 27);
            TBHours.Name = "TBHours";
            TBHours.Size = new Size(67, 23);
            TBHours.TabIndex = 2;
            TBHours.Text = "0";
            TBHours.TextAlign = HorizontalAlignment.Right;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Location = new Point(581, 19);
            panel1.Name = "panel1";
            panel1.Size = new Size(1, 100);
            panel1.TabIndex = 1;
            panel1.TabStop = true;
            // 
            // textGroupBox
            // 
            textGroupBox.Controls.Add(panel6);
            textGroupBox.Controls.Add(labelTextRep);
            textGroupBox.Controls.Add(textCheckBox);
            textGroupBox.Controls.Add(textNumBox);
            textGroupBox.Controls.Add(textASButton);
            textGroupBox.Controls.Add(panel3);
            textGroupBox.Controls.Add(textHSButton);
            textGroupBox.Controls.Add(textTB);
            textGroupBox.Location = new Point(11, 143);
            textGroupBox.Name = "textGroupBox";
            textGroupBox.Size = new Size(894, 125);
            textGroupBox.TabIndex = 1;
            textGroupBox.TabStop = false;
            textGroupBox.Text = "Text";
            // 
            // panel6
            // 
            panel6.BorderStyle = BorderStyle.FixedSingle;
            panel6.Location = new Point(581, 19);
            panel6.Name = "panel6";
            panel6.Size = new Size(1, 100);
            panel6.TabIndex = 2;
            panel6.TabStop = true;
            // 
            // labelTextRep
            // 
            labelTextRep.AutoSize = true;
            labelTextRep.Location = new Point(587, 87);
            labelTextRep.Name = "labelTextRep";
            labelTextRep.Size = new Size(41, 15);
            labelTextRep.TabIndex = 19;
            labelTextRep.Text = "Times:";
            // 
            // textCheckBox
            // 
            textCheckBox.AutoSize = true;
            textCheckBox.Location = new Point(587, 31);
            textCheckBox.Name = "textCheckBox";
            textCheckBox.Size = new Size(115, 19);
            textCheckBox.TabIndex = 19;
            textCheckBox.Text = "Toggle repetition";
            textCheckBox.UseVisualStyleBackColor = true;
            // 
            // textNumBox
            // 
            textNumBox.Location = new Point(646, 85);
            textNumBox.Name = "textNumBox";
            textNumBox.Size = new Size(88, 23);
            textNumBox.TabIndex = 20;
            // 
            // textASButton
            // 
            textASButton.FlatStyle = FlatStyle.Flat;
            textASButton.Font = new Font("Segoe UI", 8F);
            textASButton.Location = new Point(747, 75);
            textASButton.Name = "textASButton";
            textASButton.Size = new Size(141, 44);
            textASButton.TabIndex = 20;
            textASButton.Text = "Advanced Settings";
            textASButton.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Location = new Point(741, 19);
            panel3.Name = "panel3";
            panel3.Size = new Size(1, 100);
            panel3.TabIndex = 3;
            panel3.TabStop = true;
            // 
            // textHSButton
            // 
            textHSButton.BackColor = SystemColors.Control;
            textHSButton.FlatStyle = FlatStyle.Flat;
            textHSButton.Font = new Font("Segoe UI", 9F);
            textHSButton.Location = new Point(747, 19);
            textHSButton.Name = "textHSButton";
            textHSButton.Size = new Size(141, 44);
            textHSButton.TabIndex = 19;
            textHSButton.Text = "Hotkey Settings";
            textHSButton.UseVisualStyleBackColor = false;
            // 
            // textTB
            // 
            textTB.Location = new Point(6, 27);
            textTB.Name = "textTB";
            textTB.Size = new Size(570, 23);
            textTB.TabIndex = 0;
            // 
            // RARGroupBox
            // 
            RARGroupBox.Controls.Add(replayLoopLabel);
            RARGroupBox.Controls.Add(replayClickCaughtLabel);
            RARGroupBox.Controls.Add(replayTimePassedLabel);
            RARGroupBox.Controls.Add(replayStatsCheckBox);
            RARGroupBox.Controls.Add(viewRecsButton);
            RARGroupBox.Controls.Add(replayActionLabel);
            RARGroupBox.Controls.Add(replayToggleButton);
            RARGroupBox.Controls.Add(replayHSButton);
            RARGroupBox.Controls.Add(recordTimeLabel);
            RARGroupBox.Controls.Add(recordDiscardButton);
            RARGroupBox.Controls.Add(recordSaveButton);
            RARGroupBox.Controls.Add(recordActionLabel);
            RARGroupBox.Controls.Add(recordToggleButton);
            RARGroupBox.Controls.Add(panel4);
            RARGroupBox.Controls.Add(recordHSButton);
            RARGroupBox.Location = new Point(11, 275);
            RARGroupBox.Margin = new Padding(3, 4, 3, 4);
            RARGroupBox.Name = "RARGroupBox";
            RARGroupBox.Padding = new Padding(3, 4, 3, 4);
            RARGroupBox.Size = new Size(894, 125);
            RARGroupBox.TabIndex = 2;
            RARGroupBox.TabStop = false;
            RARGroupBox.Text = "Record and Replay";
            // 
            // replayLoopLabel
            // 
            replayLoopLabel.AutoSize = true;
            replayLoopLabel.BackColor = Color.Transparent;
            replayLoopLabel.Location = new Point(477, 85);
            replayLoopLabel.Name = "replayLoopLabel";
            replayLoopLabel.Size = new Size(39, 15);
            replayLoopLabel.TabIndex = 34;
            replayLoopLabel.Text = "Loops";
            // 
            // replayClickCaughtLabel
            // 
            replayClickCaughtLabel.AutoSize = true;
            replayClickCaughtLabel.BackColor = Color.Transparent;
            replayClickCaughtLabel.Location = new Point(477, 65);
            replayClickCaughtLabel.Name = "replayClickCaughtLabel";
            replayClickCaughtLabel.Size = new Size(72, 15);
            replayClickCaughtLabel.TabIndex = 33;
            replayClickCaughtLabel.Text = "ClickCaught";
            // 
            // replayTimePassedLabel
            // 
            replayTimePassedLabel.AutoSize = true;
            replayTimePassedLabel.BackColor = Color.Transparent;
            replayTimePassedLabel.Location = new Point(477, 45);
            replayTimePassedLabel.Name = "replayTimePassedLabel";
            replayTimePassedLabel.Size = new Size(72, 15);
            replayTimePassedLabel.TabIndex = 32;
            replayTimePassedLabel.Text = "Time passed";
            // 
            // replayStatsCheckBox
            // 
            replayStatsCheckBox.AutoSize = true;
            replayStatsCheckBox.Location = new Point(642, 40);
            replayStatsCheckBox.Margin = new Padding(3, 4, 3, 4);
            replayStatsCheckBox.Name = "replayStatsCheckBox";
            replayStatsCheckBox.Size = new Size(81, 19);
            replayStatsCheckBox.TabIndex = 31;
            replayStatsCheckBox.Text = "More stats";
            replayStatsCheckBox.UseVisualStyleBackColor = true;
            // 
            // viewRecsButton
            // 
            viewRecsButton.FlatStyle = FlatStyle.Flat;
            viewRecsButton.Font = new Font("Segoe UI", 9F);
            viewRecsButton.Location = new Point(593, 73);
            viewRecsButton.Margin = new Padding(0);
            viewRecsButton.Name = "viewRecsButton";
            viewRecsButton.Size = new Size(141, 44);
            viewRecsButton.TabIndex = 30;
            viewRecsButton.Text = "View Captures";
            viewRecsButton.UseVisualStyleBackColor = true;
            // 
            // replayActionLabel
            // 
            replayActionLabel.AutoSize = true;
            replayActionLabel.BackColor = Color.Transparent;
            replayActionLabel.Location = new Point(477, 25);
            replayActionLabel.Name = "replayActionLabel";
            replayActionLabel.Size = new Size(70, 15);
            replayActionLabel.TabIndex = 29;
            replayActionLabel.Text = "ActionLabel";
            // 
            // replayToggleButton
            // 
            replayToggleButton.FlatStyle = FlatStyle.Flat;
            replayToggleButton.Font = new Font("Segoe UI", 9F);
            replayToggleButton.Location = new Point(747, 73);
            replayToggleButton.Margin = new Padding(0);
            replayToggleButton.Name = "replayToggleButton";
            replayToggleButton.Size = new Size(141, 44);
            replayToggleButton.TabIndex = 28;
            replayToggleButton.Text = "Toggle";
            replayToggleButton.UseVisualStyleBackColor = true;
            // 
            // replayHSButton
            // 
            replayHSButton.BackColor = SystemColors.Control;
            replayHSButton.FlatStyle = FlatStyle.Flat;
            replayHSButton.Font = new Font("Segoe UI", 9F);
            replayHSButton.Location = new Point(747, 17);
            replayHSButton.Name = "replayHSButton";
            replayHSButton.Size = new Size(141, 44);
            replayHSButton.TabIndex = 27;
            replayHSButton.Text = "Hotkey Settings";
            replayHSButton.UseVisualStyleBackColor = false;
            // 
            // recordTimeLabel
            // 
            recordTimeLabel.AutoSize = true;
            recordTimeLabel.Location = new Point(230, 41);
            recordTimeLabel.Name = "recordTimeLabel";
            recordTimeLabel.Size = new Size(63, 15);
            recordTimeLabel.TabIndex = 26;
            recordTimeLabel.Text = "<<time>>";
            // 
            // recordDiscardButton
            // 
            recordDiscardButton.FlatStyle = FlatStyle.Flat;
            recordDiscardButton.Font = new Font("Segoe UI", 9F);
            recordDiscardButton.Location = new Point(161, 73);
            recordDiscardButton.Margin = new Padding(0);
            recordDiscardButton.Name = "recordDiscardButton";
            recordDiscardButton.Size = new Size(141, 44);
            recordDiscardButton.TabIndex = 25;
            recordDiscardButton.Text = "Discard";
            recordDiscardButton.UseVisualStyleBackColor = true;
            // 
            // recordSaveButton
            // 
            recordSaveButton.FlatStyle = FlatStyle.Flat;
            recordSaveButton.Font = new Font("Segoe UI", 9F);
            recordSaveButton.Location = new Point(6, 73);
            recordSaveButton.Margin = new Padding(0);
            recordSaveButton.Name = "recordSaveButton";
            recordSaveButton.Size = new Size(141, 44);
            recordSaveButton.TabIndex = 24;
            recordSaveButton.Text = "Save";
            recordSaveButton.UseVisualStyleBackColor = true;
            // 
            // recordActionLabel
            // 
            recordActionLabel.AutoSize = true;
            recordActionLabel.Location = new Point(7, 25);
            recordActionLabel.Name = "recordActionLabel";
            recordActionLabel.Size = new Size(70, 15);
            recordActionLabel.TabIndex = 23;
            recordActionLabel.Text = "ActionLabel";
            // 
            // recordToggleButton
            // 
            recordToggleButton.FlatStyle = FlatStyle.Flat;
            recordToggleButton.Font = new Font("Segoe UI", 9F);
            recordToggleButton.Location = new Point(321, 73);
            recordToggleButton.Margin = new Padding(0);
            recordToggleButton.Name = "recordToggleButton";
            recordToggleButton.Size = new Size(141, 44);
            recordToggleButton.TabIndex = 22;
            recordToggleButton.Text = "Toggle";
            recordToggleButton.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Location = new Point(469, 17);
            panel4.Name = "panel4";
            panel4.Size = new Size(1, 100);
            panel4.TabIndex = 4;
            panel4.TabStop = true;
            // 
            // recordHSButton
            // 
            recordHSButton.BackColor = SystemColors.Control;
            recordHSButton.FlatStyle = FlatStyle.Flat;
            recordHSButton.Font = new Font("Segoe UI", 9F);
            recordHSButton.Location = new Point(321, 17);
            recordHSButton.Name = "recordHSButton";
            recordHSButton.Size = new Size(141, 44);
            recordHSButton.TabIndex = 21;
            recordHSButton.Text = "Hotkey Settings";
            recordHSButton.UseVisualStyleBackColor = false;
            // 
            // teleportGroupBox
            // 
            teleportGroupBox.Controls.Add(teleportTPCaughtLabel);
            teleportGroupBox.Controls.Add(teleportLoopsLabel);
            teleportGroupBox.Controls.Add(teleportPositionLabel);
            teleportGroupBox.Controls.Add(teleportCheckBox);
            teleportGroupBox.Controls.Add(teleportASButton);
            teleportGroupBox.Controls.Add(teleportY2);
            teleportGroupBox.Controls.Add(teleportHSButton);
            teleportGroupBox.Controls.Add(teleportX2);
            teleportGroupBox.Controls.Add(teleportY1);
            teleportGroupBox.Controls.Add(teleportX1);
            teleportGroupBox.Location = new Point(11, 408);
            teleportGroupBox.Margin = new Padding(3, 4, 3, 4);
            teleportGroupBox.Name = "teleportGroupBox";
            teleportGroupBox.Padding = new Padding(3, 4, 3, 4);
            teleportGroupBox.Size = new Size(337, 125);
            teleportGroupBox.TabIndex = 3;
            teleportGroupBox.TabStop = false;
            teleportGroupBox.Text = "Teleport";
            // 
            // teleportTPCaughtLabel
            // 
            teleportTPCaughtLabel.AutoSize = true;
            teleportTPCaughtLabel.Location = new Point(78, 87);
            teleportTPCaughtLabel.Name = "teleportTPCaughtLabel";
            teleportTPCaughtLabel.Size = new Size(62, 15);
            teleportTPCaughtLabel.TabIndex = 38;
            teleportTPCaughtLabel.Text = "TP Caught";
            // 
            // teleportLoopsLabel
            // 
            teleportLoopsLabel.AutoSize = true;
            teleportLoopsLabel.Location = new Point(78, 65);
            teleportLoopsLabel.Name = "teleportLoopsLabel";
            teleportLoopsLabel.Size = new Size(42, 15);
            teleportLoopsLabel.TabIndex = 37;
            teleportLoopsLabel.Text = "Loops:";
            // 
            // teleportPositionLabel
            // 
            teleportPositionLabel.AutoSize = true;
            teleportPositionLabel.Location = new Point(78, 45);
            teleportPositionLabel.Name = "teleportPositionLabel";
            teleportPositionLabel.Size = new Size(39, 15);
            teleportPositionLabel.TabIndex = 36;
            teleportPositionLabel.Text = "Index:";
            // 
            // teleportCheckBox
            // 
            teleportCheckBox.AutoSize = true;
            teleportCheckBox.BackColor = Color.Transparent;
            teleportCheckBox.Location = new Point(79, 24);
            teleportCheckBox.Margin = new Padding(3, 4, 3, 4);
            teleportCheckBox.Name = "teleportCheckBox";
            teleportCheckBox.Size = new Size(81, 19);
            teleportCheckBox.TabIndex = 35;
            teleportCheckBox.Text = "More stats";
            teleportCheckBox.UseVisualStyleBackColor = false;
            // 
            // teleportASButton
            // 
            teleportASButton.FlatStyle = FlatStyle.Flat;
            teleportASButton.Font = new Font("Segoe UI", 8F);
            teleportASButton.Location = new Point(190, 75);
            teleportASButton.Margin = new Padding(0);
            teleportASButton.Name = "teleportASButton";
            teleportASButton.Size = new Size(141, 44);
            teleportASButton.TabIndex = 22;
            teleportASButton.Text = "Advanced Settings";
            teleportASButton.UseVisualStyleBackColor = true;
            // 
            // teleportY2
            // 
            teleportY2.AutoSize = true;
            teleportY2.BackColor = Color.Transparent;
            teleportY2.Location = new Point(7, 99);
            teleportY2.Name = "teleportY2";
            teleportY2.Size = new Size(57, 15);
            teleportY2.TabIndex = 9;
            teleportY2.Text = "Y2: coord";
            // 
            // teleportHSButton
            // 
            teleportHSButton.BackColor = SystemColors.Control;
            teleportHSButton.FlatStyle = FlatStyle.Flat;
            teleportHSButton.Font = new Font("Segoe UI", 9F);
            teleportHSButton.Location = new Point(190, 17);
            teleportHSButton.Name = "teleportHSButton";
            teleportHSButton.Size = new Size(141, 44);
            teleportHSButton.TabIndex = 100;
            teleportHSButton.Text = "Hotkey Settings";
            teleportHSButton.UseVisualStyleBackColor = false;
            // 
            // teleportX2
            // 
            teleportX2.AutoSize = true;
            teleportX2.BackColor = Color.Transparent;
            teleportX2.Location = new Point(7, 79);
            teleportX2.Name = "teleportX2";
            teleportX2.Size = new Size(57, 15);
            teleportX2.TabIndex = 8;
            teleportX2.Text = "X2: coord";
            // 
            // teleportY1
            // 
            teleportY1.AutoSize = true;
            teleportY1.BackColor = Color.Transparent;
            teleportY1.Location = new Point(7, 45);
            teleportY1.Name = "teleportY1";
            teleportY1.Size = new Size(57, 15);
            teleportY1.TabIndex = 7;
            teleportY1.Text = "Y1: coord";
            // 
            // teleportX1
            // 
            teleportX1.AutoSize = true;
            teleportX1.BackColor = Color.Transparent;
            teleportX1.Location = new Point(7, 25);
            teleportX1.Name = "teleportX1";
            teleportX1.Size = new Size(57, 15);
            teleportX1.TabIndex = 6;
            teleportX1.Text = "X1: coord";
            // 
            // miscGroupBox
            // 
            miscGroupBox.Controls.Add(panel5);
            miscGroupBox.Controls.Add(creditsLabel);
            miscGroupBox.Controls.Add(helpButton);
            miscGroupBox.Location = new Point(354, 408);
            miscGroupBox.Margin = new Padding(3, 4, 3, 4);
            miscGroupBox.Name = "miscGroupBox";
            miscGroupBox.Padding = new Padding(3, 4, 3, 4);
            miscGroupBox.Size = new Size(551, 125);
            miscGroupBox.TabIndex = 4;
            miscGroupBox.TabStop = false;
            miscGroupBox.Text = "Miscellaneous";
            // 
            // panel5
            // 
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Location = new Point(398, 17);
            panel5.Name = "panel5";
            panel5.Size = new Size(1, 100);
            panel5.TabIndex = 4;
            panel5.TabStop = true;
            // 
            // creditsLabel
            // 
            creditsLabel.AutoSize = true;
            creditsLabel.Location = new Point(404, 17);
            creditsLabel.Name = "creditsLabel";
            creditsLabel.Size = new Size(90, 30);
            creditsLabel.TabIndex = 36;
            creditsLabel.Text = "OP Macro v1.0\r\nBy H. Bohosyan\r\n";
            // 
            // helpButton
            // 
            helpButton.BackColor = Color.Silver;
            helpButton.FlatStyle = FlatStyle.Flat;
            helpButton.Font = new Font("Segoe UI", 9F);
            helpButton.Location = new Point(404, 75);
            helpButton.Margin = new Padding(0);
            helpButton.Name = "helpButton";
            helpButton.Size = new Size(141, 44);
            helpButton.TabIndex = 35;
            helpButton.Text = "Need Help?";
            helpButton.UseVisualStyleBackColor = false;
            // 
            // autoclicker
            // 
            autoclicker.Interval = int.MaxValue;
            // 
            // Form1
            // 
            ClientSize = new Size(919, 547);
            Controls.Add(miscGroupBox);
            Controls.Add(teleportGroupBox);
            Controls.Add(RARGroupBox);
            Controls.Add(textGroupBox);
            Controls.Add(clickGroupBox);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            Text = "OP Macro";
            clickGroupBox.ResumeLayout(false);
            clickGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)repetitionsNumBox).EndInit();
            textGroupBox.ResumeLayout(false);
            textGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)textNumBox).EndInit();
            RARGroupBox.ResumeLayout(false);
            RARGroupBox.PerformLayout();
            teleportGroupBox.ResumeLayout(false);
            teleportGroupBox.PerformLayout();
            miscGroupBox.ResumeLayout(false);
            miscGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox clickGroupBox;
        private Panel panel1;
        private TextBox TBSeconds;
        private TextBox TBMinutes;
        private TextBox TBHours;
        private Label labelSeconds;
        private Label labelMinutes;
        private Label labelHours;
        private Label labelMouseButton;
        private Label labelMilliseconds;
        private TextBox TBMilliseconds;
        private ComboBox MButtonCB;
        private ComboBox CTypeCB;
        private Label labelClickType;
        private CheckBox RepetitionCB;
        private Button clickToggleButton;
        private Button clickHotkeySettings;
        private Label labelRepTimes;
        private NumericUpDown repetitionsNumBox;
        private Panel panel2;
        private GroupBox textGroupBox;
        private Button textAdvancedSettings;
        private Panel panel3;
        private Button textHotkeySettings;
        private TextBox textTB;
        private Label labelTextRep;
        private CheckBox textCheckBox;
        private NumericUpDown textNumBox;
        private GroupBox RARGroupBox;
        private Label recordActionLabel;
        private Button recordToggle;
        private Panel panel4;
        private Button recordHSButton;
        private Button recordDiscardButton;
        private Button recordSaveButton;
        private Label recordTimeLabel;
        private Button replayToggleButton;
        private Button replayHSButton;
        private CheckBox replayStatsCheckBox;
        private Button viewRecsButton;
        private Label replayActionLabel;
        private Label replayClickCaughtLabel;
        private Label replayTimePassedLabel;
        private Label replayLoopLabel;
        private GroupBox teleportGroupBox;
        private Label teleportY2;
        private Label teleportX2;
        private Label teleportY1;
        private Label teleportX1;
        private Button teleportASButton;
        private Button teleportHSButton;
        private Label teleportPositionLabel;
        private CheckBox teleportCheckBox;
        private Label teleportTPCaughtLabel;
        private Label teleportLoopsLabel;
        private GroupBox miscGroupBox;
        private Button helpButton;
        private Panel panel5;
        private Label creditsLabel;
        private Button clickHSButton;
        private Button textASButton;
        private Button textHSButton;
        private Button recordToggleButton;
        private Panel panel6;
        private System.Windows.Forms.Timer autoclicker;
    }
}