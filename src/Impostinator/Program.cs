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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

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

            // Base Address for Dynamic Pointers
            dynamicPtrBaseAddr = modBase + 0x1468910;

            // Offsets
            offsets = new List<List<int>>();
            offsets.Add(new List<int>() { 0x5C, 0x4, 0x30 }); // Emergency Meetings
            offsets.Add(new List<int>() { 0x5C, 0x4, 0x34 }); // Emergency Cooldown
            offsets.Add(new List<int>() { 0x5C, 0x4, 0x44 }); // Discussion Time
            offsets.Add(new List<int>() { 0x5C, 0x4, 0x48 }); // Voting Time
            offsets.Add(new List<int>() { 0x5C, 0x4, 0x14 }); // Player Speed
            offsets.Add(new List<int>() { 0x5C, 0x4, 0x18 }); // Crewmate Vision
            offsets.Add(new List<int>() { 0x5C, 0x4, 0x1C }); // Impostor Vision
            offsets.Add(new List<int>() { 0x5C, 0x4, 0x20 }); // Kill Cooldown
            offsets.Add(new List<int>() { 0x5C, 0x4, 0x24 }); // Common Tasks
            offsets.Add(new List<int>() { 0x5C, 0x4, 0x28 }); // Long Tasks
            offsets.Add(new List<int>() { 0x5C, 0x4, 0x2C }); // Short Tasks

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            }
    }
}
