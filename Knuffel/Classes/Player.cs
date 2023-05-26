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
        public int? SumUpper { get; set; }
        public int? SumLower { get; set; }
        public bool Bonus { get; set; }
        public int? TotalUpper { get; set; }
        public int? ExtraKnuffels { get; set; }
        public int? FinalScore { get; set; }
        public bool Winner { get; set; }
        public Player() { }
        public Player(string name, int sumUpper, int sumLower, bool bonus, int totalUpper, int extraKnuffels, int finalScore, bool winner)
        {
            Name = name;
            SumUpper = sumUpper;
            SumLower = sumLower;
            Bonus = bonus;
            TotalUpper = totalUpper;
            ExtraKnuffels = extraKnuffels;
            FinalScore = finalScore;
            Winner = winner;
        }
       
      
               
    }
}
