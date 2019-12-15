using System;
using System.Collections.Generic;
using System.Text;

namespace SociFilarmonicaDesktop.ViewModels
{
    public class MainWindowVm
    {
        public MainWindowVm()
        {
            MenuItems = new[]
            {
                "Uno",
                "Due"
            };
        }

        public string[] MenuItems { get; }
    }
}
