/*
 * Impostinator - An external hack for Among Us written in C#.
 * Copyright (C) 2020  Adam Thompson-Sharpe
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */
using Proc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace Impostinator
{
    static class Program
    {
        public static Process proc;
        public static IntPtr hProc;
        public static IntPtr modBase;
        public static IntPtr dynamicPtrBaseAddr;
        public static List<List<int>> offsets;

        static void Main()
        {
            // Get Process
            try
            {
                proc = Process.GetProcessesByName("Among Us")[0];
            }
            catch(IndexOutOfRangeException)
            {
                MessageBox.Show("Could not find Among Us process. Are you sure it's running?", "Process Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
            }

            // Get Handle to Process
            hProc = ProcAPI.OpenProcess(ProcAPI.ProcessAccessFlags.All, false, proc.Id);

            // Get Base Address of GameAssembly.dll
            modBase = ProcAPI.GetModuleBaseAddress(proc, "GameAssembly.dll");

            // Offsets
            // Read offsets.json file
            try
            {
                StreamReader stream = new StreamReader("offsets.json");
                string offsetsString = stream.ReadToEnd();
                stream.Close();

                Offsets offsetsJson = JsonConvert.DeserializeObject<Offsets>(offsetsString);

                // Base Address for Dynamic Pointers
                dynamicPtrBaseAddr = modBase + Convert.ToInt32(offsetsJson.BaseAddressOffset, 16);

                offsets = new List<List<int>>();

                foreach (KeyValuePair<string, List<string>> offsetList in offsetsJson.GameSettings)
                {
                    List<int> offsetInts = new List<int>();
                    foreach (string offset in offsetList.Value)
                        offsetInts.Add(Convert.ToInt32(offset, 16));
                    offsets.Add(offsetInts);
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Could not find offsets.json file. Make sure that it's next to the executable.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            }
    }
}
