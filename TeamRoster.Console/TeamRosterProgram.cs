using System;
using System.Collections.Generic;
using System.Text;
using EasyConsole;
using TeamRoster.App.Menus;

namespace TeamRoster.App
{
    class TeamRosterProgram : Program
    {
        public TeamRosterProgram() : base("Team Roster App", breadcrumbHeader: true)
        {
            AddPage(new MainMenu(this));
            AddPage(new TeamMenu(this));
            AddPage(new PlayerMenu(this));
            AddPage(new PlayerMenuAdd(this));

            SetPage<MainMenu>();
        }
    }

}
