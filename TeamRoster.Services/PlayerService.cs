using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamRoster.Models;

namespace TeamRoster.Services
{
    public class PlayerService
    {
        private string _DataDirectory;

        public PlayerService(string DataDirectory)
        {
            _DataDirectory = DataDirectory;
        }

        public IList<Player> GetAllPlayers()
        {
            IList<Player> returnValue = new List<Player>();


            return returnValue;
        }

        public Player Add(Player player)
        {
            return player;
        }

        public IList<Player> Delete(Player player, List<Player> players)
        {
            try
            {
                players.RemoveAll(x => x.Player_Id == player.Player_Id);
            }
            catch (Exception ex)
            {
                ex.Data.Add("DeletePlayerError", "An error occurred while trying to delete a player.");
                throw;
            }

            return players;
        }

        private int GetNextId(List<Player> players)
        {
            int returnValue = 1;

            if (players.Any())
            {
                //get the player with the highest ID
                var player = players.OrderByDescending(u => u.Player_Id).FirstOrDefault();

                //get that players ID and add 1
                returnValue = player.Player_Id++;
            }

            return returnValue;
        }

    }
}
