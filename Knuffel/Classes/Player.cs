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
        public List<Die> Dice { get; set; }
        public Player() { }
        public Player(string name, int score, bool turn, bool winner, List<Die> dice)
        {
            Name = name;
            Score = score;
            Turn = turn;
            Winner = winner;
            Dice = dice;
        }
        // Here we create the method for the player
        public void Roll(Random rnd)
        {
            foreach (Die die in Dice)
            {
                if(!die.Locked)
                {
                    die.Roll(rnd);
                }
            }
        }
        public void DiceReset()
        {
            foreach (Die die in Dice)
            {
                die.Locked = false;
            }
        }
        public void ChangeLockStatus(int index)
        {
            if (Dice[index].Locked)
            {
                Dice[index].Locked = false;
            }
            else
            {
                Dice[index].Locked = true;
            }
        }
      
               
    }
}
