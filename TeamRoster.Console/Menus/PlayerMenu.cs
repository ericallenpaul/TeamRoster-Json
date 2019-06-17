using EasyConsole;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeamRoster.App.Menus
{
    class PlayerMenu : MenuPage
    {
        public PlayerMenu(Program program) : base("Player Menu", program,
                  new Option("Add Player", () => program.NavigateTo<PlayerMenuAdd>()))
        {

        }
    }
}
