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
        public int Value { get; set; }
        public bool Locked { get; set; }
        public Die() { }
        public Die(int value, bool locked)
        {
            Value = value;
            Locked = locked;
        }
        // Here we create the method for the die
        public void Roll(Random rnd)
        {
            if (!Locked)
            {
                Value = rnd.Next(1, 7);
            }
            else
            {
                Value = Value;
            }
        }
       

    }
}
