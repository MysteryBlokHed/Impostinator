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

                        switch (i)
                        {
                            case 0:
                                current.ConfirmEjects = currentValue[0];
                                break;
                            case 2:
                                current.AnonymousVotes = currentValue[0];
                                break;
                            case 12:
                                current.VisualTasks = currentValue[0];
                                break;
                        }
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

                            switch (i)
                            {
                                case 6:
                                    current.PlayerSpeed = currentF;
                                    break;
                                case 7:
                                    current.CrewmateVision = currentF;
                                    break;
                                case 8:
                                    current.ImpostorVision = currentF;
                                    break;
                                case 9:
                                    current.KillCooldown = currentF;
                                    break;
                            }
                        }
                        // Ints
                        else
                        {
                            int currentI = BitConverter.ToInt32(currentValue, 0);

                            switch (i)
                            {
                                case 1:
                                    current.EmergencyMeetings = currentI;
                                    break;
                                case 3:
                                    current.EmergencyCooldown = currentI;
                                    break;
                                case 4:
                                    current.DiscussionTime = currentI;
                                    break;
                                case 5:
                                    current.VotingTime = currentI;
                                    break;
                                case 10:
                                    current.KillDistance = currentI;
                                    break;
                                case 11:
                                    current.TaskBarUpdates = currentI;
                                    break;
                                case 13:
                                    current.CommonTasks = currentI;
                                    break;
                                case 14:
                                    current.LongTasks = currentI;
                                    break;
                                case 15:
                                    current.ShortTasks = currentI;
                                    break;
                            }
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

                switch(GameSettingsComboBox.SelectedIndex)
                {
                    case 6:
                        current.PlayerSpeed = newValue;
                        break;
                    case 7:
                        current.CrewmateVision = newValue;
                        break;
                    case 8:
                        current.ImpostorVision = newValue;
                        break;
                    case 9:
                        current.KillCooldown = newValue;
                        break;
                }
            }
            // Boolean values
            else if (GameSettingsComboBox.SelectedIndex == 0 || GameSettingsComboBox.SelectedIndex == 2 || GameSettingsComboBox.SelectedIndex == 12)
            {
                byte newValue = (byte)GameSettingsValue.Value;
                ProcAPI.WriteProcessMemory(Program.hProc, ProcAPI.FindDMAAddy(Program.hProc, Program.dynamicPtrBaseAddr, Program.offsets[GameSettingsComboBox.SelectedIndex].ToArray()), newValue, 1, out _);

                switch(GameSettingsComboBox.SelectedIndex)
                {
                    case 0:
                        current.ConfirmEjects = newValue;
                        break;
                    case 2:
                        current.AnonymousVotes = newValue;
                        break;
                    case 12:
                        current.VisualTasks = newValue;
                        break;
                }
            }
            // Integer values
            else
            {
                int newValue = (int)GameSettingsValue.Value;
                ProcAPI.WriteProcessMemory(Program.hProc, ProcAPI.FindDMAAddy(Program.hProc, Program.dynamicPtrBaseAddr, Program.offsets[GameSettingsComboBox.SelectedIndex].ToArray()), newValue, 4, out _);

                switch (GameSettingsComboBox.SelectedIndex)
                {
                    case 1:
                        current.EmergencyMeetings = newValue;
                        break;
                    case 3:
                        current.EmergencyCooldown = newValue;
                        break;
                    case 4:
                        current.DiscussionTime = newValue;
                        break;
                    case 5:
                        current.VotingTime = newValue;
                        break;
                    case 10:
                        current.KillDistance = newValue;
                        break;
                    case 11:
                        current.TaskBarUpdates = newValue;
                        break;
                    case 13:
                        current.CommonTasks = newValue;
                        break;
                    case 14:
                        current.LongTasks = newValue;
                        break;
                    case 15:
                        current.ShortTasks = newValue;
                        break;
                }
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

                    switch (i)
                    {
                        case 0:
                            newValue = current.ConfirmEjects;
                            break;
                        case 2:
                            newValue = current.AnonymousVotes;
                            break;
                        case 12:
                            newValue = current.VisualTasks;
                            break;
                    }

                    ProcAPI.WriteProcessMemory(Program.hProc, ProcAPI.FindDMAAddy(Program.hProc, Program.dynamicPtrBaseAddr, Program.offsets[i].ToArray()), newValue, 1, out _);
                }
                // Floats
                else if(i >= 6 && i <= 9)
                {
                    float newValue = 0;

                    switch(i)
                    {
                        case 6:
                            newValue = current.PlayerSpeed;
                            break;
                        case 7:
                            newValue = current.CrewmateVision;
                            break;
                        case 8:
                            newValue = current.ImpostorVision;
                            break;
                        case 9:
                            newValue = current.KillCooldown;
                            break;
                    }

                    Console.WriteLine(newValue);

                    ProcAPI.WriteProcessMemory(Program.hProc, ProcAPI.FindDMAAddy(Program.hProc, Program.dynamicPtrBaseAddr, Program.offsets[i].ToArray()), newValue, 4, out _);
                }
                // Integers
                else
                {
                    int newValue = 0;

                    switch(i)
                    {
                        case 1:
                            newValue = current.EmergencyMeetings;
                            break;
                        case 3:
                            newValue = current.EmergencyCooldown;
                            break;
                        case 4:
                            newValue = current.DiscussionTime;
                            break;
                        case 5:
                            newValue = current.VotingTime;
                            break;
                        case 10:
                            newValue = current.KillDistance;
                            break;
                        case 11:
                            newValue = current.TaskBarUpdates;
                            break;
                        case 13:
                            newValue = current.CommonTasks;
                            break;
                        case 14:
                            newValue = current.LongTasks;
                            break;
                        case 15:
                            newValue = current.ShortTasks;
                            break;
                    }

                    ProcAPI.WriteProcessMemory(Program.hProc, ProcAPI.FindDMAAddy(Program.hProc, Program.dynamicPtrBaseAddr, Program.offsets[i].ToArray()), newValue, 4, out _);
                }
            }
        }
    }
}
