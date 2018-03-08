﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ControlEngineApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            var form = new ControlEngineUI.EditorControls();
            if (args != null && args.Length == 1)
            {
                form.ReadFile(args[0]);
            }
            Application.Run(form);
        }
    }
}
