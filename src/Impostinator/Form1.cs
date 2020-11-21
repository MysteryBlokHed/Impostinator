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
        public Form1()
        {
            InitializeComponent();
            GameSettingsComboBox.SelectedIndex = 7;
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
                            EnumValuesComboBox.Items.Add("False");
                            EnumValuesComboBox.Items.Add("True");
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
                byte[] currentValue = new byte[4];
                ProcAPI.ReadProcessMemory(Program.hProc, ProcAPI.FindDMAAddy(Program.hProc, Program.dynamicPtrBaseAddr, Program.offsets[GameSettingsComboBox.SelectedIndex].ToArray()), currentValue, currentValue.Length, out var read);

                if (GameSettingsComboBox.SelectedIndex >= 6 && GameSettingsComboBox.SelectedIndex <= 9)
                    GameSettingsValue.Value = (decimal)BitConverter.ToSingle(currentValue, 0);
                else
                    GameSettingsValue.Value = BitConverter.ToInt32(currentValue, 0);
            }
            catch { }
        }

        private void ChangeGameSettingsButton_Click(object sender, EventArgs e)
        {
            // Float values
            if (GameSettingsComboBox.SelectedIndex >= 6 && GameSettingsComboBox.SelectedIndex <= 9)
            {
                float newValue = (float)GameSettingsValue.Value;
                ProcAPI.WriteProcessMemory(Program.hProc, ProcAPI.FindDMAAddy(Program.hProc, Program.dynamicPtrBaseAddr, Program.offsets[GameSettingsComboBox.SelectedIndex].ToArray()), newValue, 4, out _);
            }
            // Boolean values
            else if (GameSettingsComboBox.SelectedIndex == 0 || GameSettingsComboBox.SelectedIndex == 2 || GameSettingsComboBox.SelectedIndex == 12)
            {
                byte newValue = (byte)EnumValuesComboBox.SelectedIndex;
                ProcAPI.WriteProcessMemory(Program.hProc, ProcAPI.FindDMAAddy(Program.hProc, Program.dynamicPtrBaseAddr, Program.offsets[GameSettingsComboBox.SelectedIndex].ToArray()), newValue, 1, out _);
            }
            // Enum values
            else if (GameSettingsComboBox.SelectedIndex == 10 || GameSettingsComboBox.SelectedIndex == 11)
            {
                int newValue = EnumValuesComboBox.SelectedIndex;
                ProcAPI.WriteProcessMemory(Program.hProc, ProcAPI.FindDMAAddy(Program.hProc, Program.dynamicPtrBaseAddr, Program.offsets[GameSettingsComboBox.SelectedIndex].ToArray()), newValue, 4, out _);
            }
            // Integer values
            else
            {
                int newValue = (int)GameSettingsValue.Value;
                ProcAPI.WriteProcessMemory(Program.hProc, ProcAPI.FindDMAAddy(Program.hProc, Program.dynamicPtrBaseAddr, Program.offsets[GameSettingsComboBox.SelectedIndex].ToArray()), newValue, 4, out _);
            }
        }
    }
}
