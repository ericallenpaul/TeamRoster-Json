using System;
using System.Collections.Generic;
using System.Text;

namespace TeamRoster.Models
{
    public class Player
    {
        public int Player_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
