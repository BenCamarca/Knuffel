using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knuffel.Classes
{
    // Here we will create a class for the player
    public class Player
    {
        // Here we create the properties for the player
        public string Name { get; set; }
        public int Score { get; set; }
        public bool Turn { get; set; }
        public bool Winner { get; set; }
        public Player() { }
        public Player(string name, int score, bool turn, bool winner)
        {
            Name = name;
            Score = score;
            Turn = turn;
            Winner = winner;
        }
       
      
               
    }
}
