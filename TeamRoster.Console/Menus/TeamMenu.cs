using EasyConsole;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeamRoster.App.Menus
{
    class TeamMenu : MenuPage
    {
        public TeamMenu(Program program) : base("Team Menu", program,
            new Option("Add Team", () => program.NavigateTo<TeamMenu>()),
            new Option("Manage Players", () => program.NavigateTo<PlayerMenu>()))
        {
        }

    }
}
