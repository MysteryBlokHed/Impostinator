﻿namespace Impostinator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.TitleLabel = new System.Windows.Forms.Label();
            this.AuthorLabel = new System.Windows.Forms.Label();
            this.ChangeSettingsLabel = new System.Windows.Forms.Label();
            this.GameSettingsComboBox = new System.Windows.Forms.ComboBox();
            this.GameSettingsValue = new System.Windows.Forms.NumericUpDown();
            this.ChangeGameSettingsButton = new System.Windows.Forms.Button();
            this.EnumValuesComboBox = new System.Windows.Forms.ComboBox();
            this.SaveSettingsButton = new System.Windows.Forms.Button();
            this.RestoreSettingsButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GameSettingsValue)).BeginInit();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.TitleLabel.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.TitleLabel.Location = new System.Drawing.Point(12, 9);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(599, 55);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "Impostinator External Hack";
            // 
            // AuthorLabel
            // 
            this.AuthorLabel.AutoSize = true;
            this.AuthorLabel.BackColor = System.Drawing.Color.Transparent;
            this.AuthorLabel.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AuthorLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.AuthorLabel.Location = new System.Drawing.Point(66, 64);
            this.AuthorLabel.Name = "AuthorLabel";
            this.AuthorLabel.Size = new System.Drawing.Size(221, 27);
            this.AuthorLabel.TabIndex = 1;
            this.AuthorLabel.Text = "By MysteryBlokHed";
            // 
            // ChangeSettingsLabel
            // 
            this.ChangeSettingsLabel.AutoSize = true;
            this.ChangeSettingsLabel.BackColor = System.Drawing.Color.Transparent;
            this.ChangeSettingsLabel.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangeSettingsLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.ChangeSettingsLabel.Location = new System.Drawing.Point(147, 118);
            this.ChangeSettingsLabel.Name = "ChangeSettingsLabel";
            this.ChangeSettingsLabel.Size = new System.Drawing.Size(343, 36);
            this.ChangeSettingsLabel.TabIndex = 2;
            this.ChangeSettingsLabel.Text = "Change Game Settings";
            // 
            // GameSettingsComboBox
            // 
            this.GameSettingsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GameSettingsComboBox.FormattingEnabled = true;
            this.GameSettingsComboBox.Items.AddRange(new object[] {
            "Confirm Ejects",
            "Emergency Meetings",
            "Anonymous Votes",
            "Emergency Cooldown",
            "Discussion Time",
            "Voting Time",
            "Player Speed",
            "Crewmate Vision",
            "Impostor Vision",
            "Kill Cooldown",
            "Kill Distance",
            "Taskbar Updates",
            "Visual Tasks",
            "Common Tasks",
            "Long Tasks",
            "Short Tasks",
            "Impostors",
            "Map",
            "Ghosts do Tasks"});
            this.GameSettingsComboBox.Location = new System.Drawing.Point(153, 155);
            this.GameSettingsComboBox.Name = "GameSettingsComboBox";
            this.GameSettingsComboBox.Size = new System.Drawing.Size(255, 21);
            this.GameSettingsComboBox.TabIndex = 3;
            this.GameSettingsComboBox.SelectedIndexChanged += new System.EventHandler(this.GameSettingsComboBox_SelectedIndexChanged);
            // 
            // GameSettingsValue
            // 
            this.GameSettingsValue.Location = new System.Drawing.Point(414, 155);
            this.GameSettingsValue.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.GameSettingsValue.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.GameSettingsValue.Name = "GameSettingsValue";
            this.GameSettingsValue.Size = new System.Drawing.Size(65, 20);
            this.GameSettingsValue.TabIndex = 4;
            // 
            // ChangeGameSettingsButton
            // 
            this.ChangeGameSettingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangeGameSettingsButton.Location = new System.Drawing.Point(153, 182);
            this.ChangeGameSettingsButton.Name = "ChangeGameSettingsButton";
            this.ChangeGameSettingsButton.Size = new System.Drawing.Size(326, 36);
            this.ChangeGameSettingsButton.TabIndex = 5;
            this.ChangeGameSettingsButton.Text = "Change Setting";
            this.ChangeGameSettingsButton.UseVisualStyleBackColor = true;
            this.ChangeGameSettingsButton.Click += new System.EventHandler(this.ChangeGameSettingsButton_Click);
            // 
            // EnumValuesComboBox
            // 
            this.EnumValuesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EnumValuesComboBox.Location = new System.Drawing.Point(485, 155);
            this.EnumValuesComboBox.Name = "EnumValuesComboBox";
            this.EnumValuesComboBox.Size = new System.Drawing.Size(123, 21);
            this.EnumValuesComboBox.TabIndex = 6;
            this.EnumValuesComboBox.Visible = false;
            this.EnumValuesComboBox.SelectedIndexChanged += new System.EventHandler(this.EnumValuesComboBox_SelectedIndexChanged);
            // 
            // SaveSettingsButton
            // 
            this.SaveSettingsButton.Location = new System.Drawing.Point(12, 163);
            this.SaveSettingsButton.Name = "SaveSettingsButton";
            this.SaveSettingsButton.Size = new System.Drawing.Size(135, 23);
            this.SaveSettingsButton.TabIndex = 7;
            this.SaveSettingsButton.Text = "Save Settings";
            this.SaveSettingsButton.UseVisualStyleBackColor = true;
            this.SaveSettingsButton.Click += new System.EventHandler(this.SaveSettingsButton_Click);
            // 
            // RestoreSettingsButton
            // 
            this.RestoreSettingsButton.Location = new System.Drawing.Point(12, 192);
            this.RestoreSettingsButton.Name = "RestoreSettingsButton";
            this.RestoreSettingsButton.Size = new System.Drawing.Size(135, 23);
            this.RestoreSettingsButton.TabIndex = 8;
            this.RestoreSettingsButton.Text = "Restore Settings";
            this.RestoreSettingsButton.UseVisualStyleBackColor = true;
            this.RestoreSettingsButton.Click += new System.EventHandler(this.RestoreSettingsButton_Click);
            // 
            // Impostinator
            // 
            this.AcceptButton = this.ChangeGameSettingsButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Impostinator.Properties.Resources.Background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(620, 250);
            this.Controls.Add(this.RestoreSettingsButton);
            this.Controls.Add(this.SaveSettingsButton);
            this.Controls.Add(this.EnumValuesComboBox);
            this.Controls.Add(this.ChangeGameSettingsButton);
            this.Controls.Add(this.GameSettingsValue);
            this.Controls.Add(this.GameSettingsComboBox);
            this.Controls.Add(this.ChangeSettingsLabel);
            this.Controls.Add(this.AuthorLabel);
            this.Controls.Add(this.TitleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Impostinator";
            this.Text = "Impostinator";
            ((System.ComponentModel.ISupportInitialize)(this.GameSettingsValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label AuthorLabel;
        private System.Windows.Forms.Label ChangeSettingsLabel;
        private System.Windows.Forms.ComboBox GameSettingsComboBox;
        private System.Windows.Forms.NumericUpDown GameSettingsValue;
        private System.Windows.Forms.Button ChangeGameSettingsButton;
        private System.Windows.Forms.ComboBox EnumValuesComboBox;
        private System.Windows.Forms.Button SaveSettingsButton;
        private System.Windows.Forms.Button RestoreSettingsButton;
    }
}
