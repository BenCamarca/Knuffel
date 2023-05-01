using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Knuffel.Classes;

namespace Knuffel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // this are the properties that are bound to the image in the xaml
        public string Die1Image { get; set; } 
        public string Die2Image { get; set; }
        public string Die3Image { get; set; }
        public string Die4Image { get; set; }
        public string Die5Image { get; set; }

        // this is the property that is bound to the textblock in the xaml
        public string RollsLeft { get; set; }



        public MainWindow()
        {
            InitializeComponent();

            // declaring the images for the dice
            string oneEyedDie = "pack://application:,,,/Images/1side.png";
            string twoEyedDie = "pack://application:,,,/Images/2side.png";
            string threeEyedDie = "pack://application:,,,/Images/3side.png";
            string fourEyedDie = "pack://application:,,,/Images/4side.png";
            string fiveEyedDie = "pack://application:,,,/Images/5side.png";
            string sixEyedDie = "pack://application:,,,/Images/6side.png";
            string oneEyedDieGreen = "pack://application:,,,/Images/1sideGreen.png";
            string twoEyedDieGreen = "pack://application:,,,/Images/2sideGreen.png";
            string threeEyedDieGreen = "pack://application:,,,/Images/3sideGreen.png";
            string fourEyedDieGreen = "pack://application:,,,/Images/4sideGreen.png";
            string fiveEyedDieGreen = "pack://application:,,,/Images/5sideGreen.png";
            string sixEyedDieGreen = "pack://application:,,,/Images/6sideGreen.png";
            string ravenDie = "pack://application:,,,/Images/RavenGamesLogo2Use.png";

            // declaring the amount of rolls left
            string rollsLeft3 = "´s left: 3";
            string rollsLeft2 = "´s left: 2";
            string rollsLeft1 = "´s left: 1";
            string rollsLeft0 = "´s left: 0";

            // setting the default images for the dice
            Die1Image = oneEyedDie; 
            DataContext = this;
            Die2Image = ravenDie;
            DataContext = this;
            Die3Image = threeEyedDieGreen;
            DataContext = this;
            Die4Image = ravenDie;
            DataContext = this;
            Die5Image = fiveEyedDie;
            DataContext = this;

            // setting the default amount of rolls left
            RollsLeft = rollsLeft3;
            DataContext = this;


            // creating a new player
            Player player1 = new Player();

            // creating a new list of dice
            List<Die> dice = new List<Die>();

            // creating 5 new dice
            Die die1 = new Die();
            Die die2 = new Die();
            Die die3 = new Die();
            Die die4 = new Die();
            Die die5 = new Die();

            // adding the dice to the list
            dice.Add(die1);
            dice.Add(die2);
            dice.Add(die3);
            dice.Add(die4);
            dice.Add(die5);

            // adding the list of dice to the player
            player1.Dice = dice;

           


        }
    }
}
