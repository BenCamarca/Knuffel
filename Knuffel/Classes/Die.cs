using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knuffel.Classes
{
    // Here we will create a class for the die

    public class Die
    {
        // Here we create the properties for the die
        public int? Value { get; set; }
        public bool Locked { get; set; } = false;
        public List<Die> DieList { get; set; }
        public Die() { }
        public Die(int value, bool locked)
        {
            Value = value;
            Locked = locked;
        }
        // Here we create the method for rolling the die
        
        public void Roll(Random rnd)
        {            
            if (!Locked)
            {
                Value = rnd.Next(1, 7);
            }
            else if(Locked && Value == null)
            {
                Value = rnd.Next(1,7);
            }
            else
            {
                Value = Value;
            }
        }
       
        // Here we create a method for resetting the Locked property of the die to false
        public void DiceReset()
        {
            foreach (Die die in DieList)
            {
                die.Locked = false;
            }
        }
        // Here we create a method for changing the Locked property of the die
        public void ChangeLockStatus(List<Die> DieList, int index)
        {
            if (DieList[index].Locked)
            {
                DieList[index].Locked = false;
            }
            else
            {
                DieList[index].Locked = true;
            }
        }


    }
}
