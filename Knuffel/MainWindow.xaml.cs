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

        // this is the property that is bound to the textblock "RollsLeft" in the xaml
        public string RollsLeft { get; set; }

        // this are the property that are bound to the textblock "PlayerNames" in the xaml
        public string Player1Name { get; set; }
        public string Player2Name { get; set; }
        public string Player3Name { get; set; }
        public string Player4Name { get; set; }

        // declaring a variable for the amount of players
        int amountOfPlayers;

        



        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            // declaring the images for the dice
            //string oneEyedDie = "pack://application:,,,/Images/1side.png";
            //string twoEyedDie = "pack://application:,,,/Images/2side.png";
            //string threeEyedDie = "pack://application:,,,/Images/3side.png";
            //string fourEyedDie = "pack://application:,,,/Images/4side.png";
            //string fiveEyedDie = "pack://application:,,,/Images/5side.png";
            //string sixEyedDie = "pack://application:,,,/Images/6side.png";
            //string oneEyedDieGreen = "pack://application:,,,/Images/1sideGreen.png";
            //string twoEyedDieGreen = "pack://application:,,,/Images/2sideGreen.png";
            //string threeEyedDieGreen = "pack://application:,,,/Images/3sideGreen.png";
            //string fourEyedDieGreen = "pack://application:,,,/Images/4sideGreen.png";
            //string fiveEyedDieGreen = "pack://application:,,,/Images/5sideGreen.png";
            //string sixEyedDieGreen = "pack://application:,,,/Images/6sideGreen.png";
            //string ravenDie = "pack://application:,,,/Images/RavenGamesLogo2Use.png";

            // declaring the amount of rolls left
            string rollsLeft3 = "´s left: 3";
            string rollsLeft2 = "´s left: 2";
            string rollsLeft1 = "´s left: 1";
            string rollsLeft0 = "´s left: 0";

            // declaring the names of the players
            string player1Name = "Player 1";
            string player2Name = "Player 2";
            string player3Name = "Player 3";
            string player4Name = "Player 4";

            // setting the default images for the dice
            //Die1Image = oneEyedDie; 
            //DataContext = this;
            //Die2Image = ravenDie;
            //DataContext = this;
            //Die3Image = threeEyedDieGreen;
            //DataContext = this;
            //Die4Image = ravenDie;
            //DataContext = this;
            //Die5Image = fiveEyedDie;
            //DataContext = this;

            // setting the default amount of rolls left
            RollsLeft = rollsLeft3;

            // creating a new player
            Player player1 = new Player();
            player1.Name = "Angelika";
            player1Name = player1.Name;

            // setting the default names of the players
            Player1Name = player1Name;
            Player2Name = player2Name;
            Player3Name = player3Name;
            Player4Name = player4Name;


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

            // foreach die set the property locked to false
            foreach (Die die in dice)
            {
                die.Locked = false;
            }

            // setting the property locked of die 1, die 3 and die 5 to true
            dice[0].Locked = true;
            dice[2].Locked = true;
            dice[4].Locked = true;




            // rolling the dice
            Random rnd = new Random();
            foreach (Die die in dice)
            {
                die.Roll(rnd);
            }

            // setting the values of the dice to the properties for binding to the images
            Die1Image = GetDieImage(dice[0].Value, dice[0].Locked);
            Die2Image = GetDieImage(dice[1].Value, dice[1].Locked);
            Die3Image = GetDieImage(dice[2].Value, dice[2].Locked);
            Die4Image = GetDieImage(dice[3].Value, dice[3].Locked);
            Die5Image = GetDieImage(dice[4].Value, dice[4].Locked);



        }


        // in this method we set the images of the dice to the values of the dice

        private string GetDieImage(int? value, bool locked)
        {
            // return the image string based on the value of the die
            {
                switch (value)
                {
                    case 1:
                        if (locked)
                        {
                            return "pack://application:,,,/Images/1sideGreen.png";
                        }
                        else
                        {
                            return "pack://application:,,,/Images/1side.png";
                        }
                    case 2:
                        if (locked)
                        {
                            return "pack://application:,,,/Images/2sideGreen.png";
                        }
                        else
                        {
                            return "pack://application:,,,/Images/2side.png";
                        }
                    case 3:
                        if (locked)
                        {
                            return "pack://application:,,,/Images/3sideGreen.png";
                        }
                        else
                        {
                            return "pack://application:,,,/Images/3side.png";
                        }
                    case 4:
                        if (locked)
                        {
                            return "pack://application:,,,/Images/4sideGreen.png";
                        }
                        else
                        {
                            return "pack://application:,,,/Images/4side.png";
                        }
                    case 5:
                        if (locked)
                        {
                            return "pack://application:,,,/Images/5sideGreen.png";
                        }
                        else
                        {
                            return "pack://application:,,,/Images/5side.png";
                        }
                    case 6:
                        if (locked)
                        {
                            return "pack://application:,,,/Images/6sideGreen.png";
                        }
                        else
                        {
                            return "pack://application:,,,/Images/6side.png";
                        }

                    default:
                        return "";
                }
            }




        }

        private void StartButton_Clicked(object sender, RoutedEventArgs e)
        {
            StartGameButton.Visibility = Visibility.Collapsed;
            ShowChooseAmountOfPlayerUI();
        }

        private void OnePlayerButton_Clicked(object sender, RoutedEventArgs e)
        {
            amountOfPlayers = 1;
            HideChooseAmountOfPlayerUI();
        }

        private void TwoPlayerButton_Clicked(object sender, RoutedEventArgs e)
        {
            amountOfPlayers = 2;
            HideChooseAmountOfPlayerUI();
        }

        private void ThreePlayerButton_Clicked(object sender, RoutedEventArgs e)
        {
            amountOfPlayers = 3;
            HideChooseAmountOfPlayerUI();
        }

        private void FourPlayerButton_Clicked(object sender, RoutedEventArgs e)
        {
            amountOfPlayers = 4;
            HideChooseAmountOfPlayerUI();
        }

        // in this method we set the HowManyPlayersLabel and the buttons for the amount of players to visible
        private void ShowChooseAmountOfPlayerUI()
        {
            HowManyPlayersLabel.Visibility = Visibility.Visible;
            OnePlayerButton.Visibility = Visibility.Visible;
            TwoPlayerButton.Visibility = Visibility.Visible;
            ThreePlayerButton.Visibility = Visibility.Visible;
            FourPlayerButton.Visibility = Visibility.Visible;
        }

        // in this method we set the HowManyPlayersLabel and the buttons for the amount of players to collapsed
        private void HideChooseAmountOfPlayerUI()
        {
            HowManyPlayersLabel.Visibility = Visibility.Collapsed;
            OnePlayerButton.Visibility = Visibility.Collapsed;
            TwoPlayerButton.Visibility = Visibility.Collapsed;
            ThreePlayerButton.Visibility = Visibility.Collapsed;
            FourPlayerButton.Visibility = Visibility.Collapsed;
        }

        // in this method we set the the Image of all dice to ravenDie
        private void SetDiceToRaven()
        {
            string ravenDie = "pack://application:,,,/Images/ravenDie.png";
            Die1Image = ravenDie;
            Die2Image = ravenDie;
            Die3Image = ravenDie;
            Die4Image = ravenDie;
            Die5Image = ravenDie;
            DataContext = this;            
        }
    }
}
