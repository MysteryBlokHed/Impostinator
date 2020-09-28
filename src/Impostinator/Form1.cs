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
            // Change the decimal places in the number box if the expected value is a float
            if (GameSettingsComboBox.SelectedIndex >= 4 && GameSettingsComboBox.SelectedIndex <= 7)
                GameSettingsValue.DecimalPlaces = 2;
            else
                GameSettingsValue.DecimalPlaces = 0;
        }

        private void ChangeGameSettingsButton_Click(object sender, EventArgs e)
        {
            if(GameSettingsComboBox.SelectedIndex >= 4 && GameSettingsComboBox.SelectedIndex <= 7)
            {
                float newValue = (float)GameSettingsValue.Value;
                ProcAPI.WriteProcessMemory(Program.hProc, ProcAPI.FindDMAAddy(Program.hProc, Program.dynamicPtrBaseAddr, Program.offsets[GameSettingsComboBox.SelectedIndex].ToArray()), newValue, 4, out _);
            }
            else
            {
                int newValue = (int)GameSettingsValue.Value;
                ProcAPI.WriteProcessMemory(Program.hProc, ProcAPI.FindDMAAddy(Program.hProc, Program.dynamicPtrBaseAddr, Program.offsets[GameSettingsComboBox.SelectedIndex].ToArray()), newValue, 4, out _);
            }
        }
    }
}
