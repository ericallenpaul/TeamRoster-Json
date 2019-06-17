using EasyConsole;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeamRoster.App.Menus
{
    public class MainMenu : MenuPage
    {

        public MainMenu(Program program) : base("Team Roster Main Menu", program,
                  new Option("Manage Teams", () => program.NavigateTo<TeamMenu>()),
                  new Option("Manage Players", () => program.NavigateTo<PlayerMenu>())
            )
        {

        }

        public override void Display()
        {
            base.Display();
        }


    }
}
