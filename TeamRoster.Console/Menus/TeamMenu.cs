using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamRoster.Models;
using TeamRoster.Services;

namespace TeamRoster.App.Menus
{
    public class TeamMenu
    {
        private static string _dataDir;
        private static TeamService _teamService;

        public static List<Team> _teamList { get; set; }

        public static int DisplayMenu()
        {
            //get the data directory and json file
            _dataDir = $@"{Program.DataDirectory}Data\";
            _teamService = new TeamService(_dataDir);

            //print the menu to screen
            Console.Clear();
            Console.WriteLine("Team Manager");
            Console.WriteLine("--------------------");
            Console.WriteLine();
            Console.WriteLine(" 1. List Teams");
            Console.WriteLine(" 2. Add Team");
            Console.WriteLine(" 3. Delete Team");
            Console.WriteLine(" 4. Return to Main Menu");
            Console.WriteLine();
            Console.Write("Choice: ");

            //capture the result
            var result = Console.ReadLine();
            return Convert.ToInt32(result);
        }

        public static void Run()
        {
            //create a var to hold the user's selection
            int userInput = 0;

            //continue to loop until a valid
            //number is chosen
            do
            {
                //get the selection
                userInput = DisplayMenu();

                //perform an action based on a selection
                switch (userInput)
                {
                    case 1:
                        GetAll();
                        break;
                    case 2:
                        Add();
                        break;
                    case 3:
                        Delete();
                        break;
                    case 4:
                        MainMenu.Run();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine(" Error: Invalid Choice");
                        System.Threading.Thread.Sleep(1000);
                        break;
                }

            } while (userInput != 4);
        }

        private static void GetAll()
        {
            //get the list of teams from the service
            _teamList = _teamService.GetAll();

            //create a ConsoleTable object for displaying
            //output like a table
            ConsoleTable ct = new ConsoleTable(77);

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Teams");
            ct.PrintLine();

            string[] headers = new[] { "Id", "First", "Last", "Team", "Age" };
            ct.PrintRow(headers);
            ct.PrintLine();

            if (_teamList.Any())
            {
                foreach (var team in _teamList)
                {
                    string[] rowData = new[] { team.Team_Id.ToString(), team.TeamName };
                    ct.PrintRow(rowData);
                }
            }
            else
            {
                Console.WriteLine(" There are no teams to list. Try adding a team.");
            }

            ct.PrintLine();

            Console.WriteLine();
            Console.WriteLine(" Press [enter] to return to the menu.");
            Console.ReadLine();
        }

        private static void Add()
        {
            //get the existing list of teams
            _teamList = _teamService.GetAll();

            //instantiate the new team object
            Team team = new Team();

            //prompt the user for the first name
            Console.Write("Team Name: ");
            //read the input from the console as the first name
            team.TeamName = Console.ReadLine();

            //call the team service and add the team
            team = _teamService.Add(team, _teamList);

            //give the user feed back--pause for one second on screen
            Console.WriteLine($"Success: Added a new team ID: {team.Team_Id}");
            System.Threading.Thread.Sleep(1000);
        }

        private static void WriteError(string line1, string line2 = null)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(line1);
            if (!string.IsNullOrEmpty(line2))
            {
                Console.WriteLine(line2);
            }

            Console.WriteLine();
            Console.ResetColor();
        }

        private static void Delete()
        {
            //collect the id of the team to delete
            Console.Write("ID of team to delete: ");
            int.TryParse(Console.ReadLine(), out var teamId);

            //get the list of teams
            _teamList = _teamService.GetAll();
            var teamToRemove = _teamList.SingleOrDefault(s => s.Team_Id == teamId);

            //make sure a team with the specified id exists
            //before attemptin to delete it
            if (teamToRemove != null)
            {
                //delete the team
                _teamService.Delete(teamToRemove, _teamList);

                //give feedback to the user and pause for one second
                Console.Write($"Team ID: {teamId} was deleted.");
                System.Threading.Thread.Sleep(1000);
            }
            else
            {
                //could not find the specified team show and error and pause for a second
                Console.Write($"ERROR: Could not find team with ID: {teamId}.");
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
