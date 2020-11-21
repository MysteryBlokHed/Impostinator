/*
 * Impostinator - An external hack for Among Us written in C#.
 * Copyright (C) 2020  Adam Thompson-Sharpe
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */
using Proc;
using System;
using System.Windows.Forms;

namespace Impostinator
{
    public partial class Form1 : Form
    {
        SavedConfig current;
        SavedConfig saved;

        public Form1()
        {
            InitializeComponent();
            GameSettingsComboBox.SelectedIndex = 7;

            // Set up saved configs
            current = new SavedConfig();

            // Read all settings values and save to current
            for (int i = 0; i < Program.offsets.Count; i++)
            {
                try
                {
                    // Booleans
                    if (i == 0 || i == 2 || i == 12)
                    {
                        byte[] currentValue = new byte[1];
                        ProcAPI.ReadProcessMemory(Program.hProc, ProcAPI.FindDMAAddy(Program.hProc, Program.dynamicPtrBaseAddr, Program.offsets[i].ToArray()), currentValue, currentValue.Length, out var read);
                        current.settings[i] = currentValue[0];
                    }
                    // Other
                    else
                    {
                        byte[] currentValue = new byte[4];
                        ProcAPI.ReadProcessMemory(Program.hProc, ProcAPI.FindDMAAddy(Program.hProc, Program.dynamicPtrBaseAddr, Program.offsets[i].ToArray()), currentValue, currentValue.Length, out var read);

                        // Floats
                        if (i >= 6 && i <= 9)
                        {
                            float currentF = BitConverter.ToSingle(currentValue, 0);
                            current.settings[i] = currentF;
                        }
                        // Ints
                        else
                        {
                            int currentI = BitConverter.ToInt32(currentValue, 0);
                            current.settings[i] = currentI;
                        }
                    }
                }
                catch { }
            }

            // Set saved to current
            saved = current;
        }

        private void GameSettingsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reset visible/enabled elements
            GameSettingsValue.Enabled = true;
            EnumValuesComboBox.Visible = false;

            // Change the decimal places in the number box if the expected value is a float
            if (GameSettingsComboBox.SelectedIndex >= 6 && GameSettingsComboBox.SelectedIndex <= 9)
                GameSettingsValue.DecimalPlaces = 2;
            else
                GameSettingsValue.DecimalPlaces = 0;

            // Disable the number box and show a new combo box if the expected value is part of an enum or is a boolean (eg. Kill Distance or Visual Tasks)
            switch (GameSettingsComboBox.SelectedIndex)
            {
                case 0:
                case 2:
                case 10:
                case 11:
                case 12:
                    GameSettingsValue.Enabled = false;
                    EnumValuesComboBox.Visible = true;

                    EnumValuesComboBox.Items.Clear();

                    // Change the values in the new combo box depending on the targeted value
                    switch (GameSettingsComboBox.SelectedIndex)
                    {
                        case 0:
                        case 2:
                        case 12:
                            EnumValuesComboBox.Items.Add("Off");
                            EnumValuesComboBox.Items.Add("On");
                            break;
                        case 10:
                            EnumValuesComboBox.Items.Add("Short");
                            EnumValuesComboBox.Items.Add("Medium");
                            EnumValuesComboBox.Items.Add("Long");
                            break;
                        case 11:
                            EnumValuesComboBox.Items.Add("Always");
                            EnumValuesComboBox.Items.Add("Meetings");
                            EnumValuesComboBox.Items.Add("Never");
                            break;
                    }
                    break;
                default:
                    break;
            }

            // Set the value of GameSettingsValue to the target setting's current value
            try
            {
                if (GameSettingsComboBox.SelectedIndex == 0 || GameSettingsComboBox.SelectedIndex == 2 || GameSettingsComboBox.SelectedIndex == 12)
                {
                    byte[] currentValue = new byte[1];
                    ProcAPI.ReadProcessMemory(Program.hProc, ProcAPI.FindDMAAddy(Program.hProc, Program.dynamicPtrBaseAddr, Program.offsets[GameSettingsComboBox.SelectedIndex].ToArray()), currentValue, currentValue.Length, out var read);
                    GameSettingsValue.Value = currentValue[0];
                }
                else
                {
                    byte[] currentValue = new byte[4];
                    ProcAPI.ReadProcessMemory(Program.hProc, ProcAPI.FindDMAAddy(Program.hProc, Program.dynamicPtrBaseAddr, Program.offsets[GameSettingsComboBox.SelectedIndex].ToArray()), currentValue, currentValue.Length, out var read);

                    if (GameSettingsComboBox.SelectedIndex >= 6 && GameSettingsComboBox.SelectedIndex <= 9)
                        GameSettingsValue.Value = (decimal)BitConverter.ToSingle(currentValue, 0);
                    else
                        GameSettingsValue.Value = BitConverter.ToInt32(currentValue, 0);
                }
            }
            catch { }

            // Try to update the selected EnumValuesComboBox value if applicable
            try
            {
                EnumValuesComboBox.SelectedIndex = (int)GameSettingsValue.Value;
            }
            catch { }
        }

        private void EnumValuesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GameSettingsValue.Value = EnumValuesComboBox.SelectedIndex;
        }

        private void ChangeGameSettingsButton_Click(object sender, EventArgs e)
        {
            // Float values
            if (GameSettingsComboBox.SelectedIndex >= 6 && GameSettingsComboBox.SelectedIndex <= 9)
            {
                float newValue = (float)GameSettingsValue.Value;
                ProcAPI.WriteProcessMemory(Program.hProc, ProcAPI.FindDMAAddy(Program.hProc, Program.dynamicPtrBaseAddr, Program.offsets[GameSettingsComboBox.SelectedIndex].ToArray()), newValue, 4, out _);
                current.settings[GameSettingsComboBox.SelectedIndex] = newValue;
            }
            // Boolean values
            else if (GameSettingsComboBox.SelectedIndex == 0 || GameSettingsComboBox.SelectedIndex == 2 || GameSettingsComboBox.SelectedIndex == 12)
            {
                byte newValue = (byte)GameSettingsValue.Value;
                ProcAPI.WriteProcessMemory(Program.hProc, ProcAPI.FindDMAAddy(Program.hProc, Program.dynamicPtrBaseAddr, Program.offsets[GameSettingsComboBox.SelectedIndex].ToArray()), newValue, 1, out _);
                current.settings[GameSettingsComboBox.SelectedIndex] = newValue;
            }
            // Integer values
            else
            {
                int newValue = (int)GameSettingsValue.Value;
                ProcAPI.WriteProcessMemory(Program.hProc, ProcAPI.FindDMAAddy(Program.hProc, Program.dynamicPtrBaseAddr, Program.offsets[GameSettingsComboBox.SelectedIndex].ToArray()), newValue, 4, out _);
                current.settings[GameSettingsComboBox.SelectedIndex] = newValue;
            }
        }

        private void SaveSettingsButton_Click(object sender, EventArgs e)
        {
            saved = current;
        }

        private void RestoreSettingsButton_Click(object sender, EventArgs e)
        {
            current = saved;
            for (int i = 0; i < Program.offsets.Count; i++)
            {
                // Booleans
                if (i == 0 || i == 2 || i == 12)
                {
                    byte newValue = 0;
                    newValue = (byte)current.settings[i];
                    ProcAPI.WriteProcessMemory(Program.hProc, ProcAPI.FindDMAAddy(Program.hProc, Program.dynamicPtrBaseAddr, Program.offsets[i].ToArray()), newValue, 1, out _);
                }
                // Floats
                else if(i >= 6 && i <= 9)
                {
                    float newValue = 0;
                    newValue = (float)current.settings[i];
                    ProcAPI.WriteProcessMemory(Program.hProc, ProcAPI.FindDMAAddy(Program.hProc, Program.dynamicPtrBaseAddr, Program.offsets[i].ToArray()), newValue, 4, out _);
                }
                // Integers
                else
                {
                    int newValue = 0;
                    newValue = (int)current.settings[i];
                    ProcAPI.WriteProcessMemory(Program.hProc, ProcAPI.FindDMAAddy(Program.hProc, Program.dynamicPtrBaseAddr, Program.offsets[i].ToArray()), newValue, 4, out _);
                }
            }
        }
    }
}
