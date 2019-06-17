using EasyConsole;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeamRoster.App.Menus
{
    class PlayerMenuAdd : Page
    {
        public PlayerMenuAdd(Program program) : base("Add Player", program)
        {
            //https://stackoverflow.com/questions/20365214/a-simple-menu-in-a-console-application
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine(ConsoleColor.Green, "Add Player");

            Input.ReadString("Press [Enter] to navigate home");
            Program.
            Program.NavigateHome();
        }
    }
}
