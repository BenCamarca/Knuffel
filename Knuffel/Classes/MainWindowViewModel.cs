using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Knuffel.Classes
{
    internal class MainWindowViewModel : BaseViewModel
    {

        public MainWindowViewModel()
        {
            // declaring the default values for the Die Images
            Die1Image = "pack://application:,,,/Images/RavenGamesLogo2Use.png";
            Die2Image = "pack://application:,,,/Images/RavenGamesLogo2Use.png";
            Die3Image = "pack://application:,,,/Images/RavenGamesLogo2Use.png";
            Die4Image = "pack://application:,,,/Images/RavenGamesLogo2Use.png";
            Die5Image = "pack://application:,,,/Images/RavenGamesLogo2Use.png";

            // declaring the default values for the player names
            Player1Name = "Player 1";
            Player2Name = "Player 2";
            Player3Name = "Player 3";
            Player4Name = "Player 4";

            // declaring the default value for AmountOfPlayers
            AmountOfPlayers = 1;

            // declaring the default value for the visibility of the StartGame Button
            IsStartGameButtonVisible = true;

            // initializing the command for the start game button
            StartGameCommand = new DelegateCommand(
                (o) => true,
                (o) => StartGame()
                );
            // initializing the command for the 1-4 Player buttons
            PlayersButtonsCommand = new DelegateCommand((value) =>
            {
                int val = int.Parse((string)value);
                AmountOfPlayers = val;
                HideChooseAmountOfPlayerUI();                
            });
        }

        // declaring the command for the start game button
        public DelegateCommand StartGameCommand { get; set; }

        // declaring the command for the 1-4 Player buttons
        public DelegateCommand PlayersButtonsCommand { get; set; }

        private void StartGame()
        {
            // in this method we set the HowManyPlayersLabel and the buttons for the amount of players to visible
            ShowChooseAmountOfPlayerUI();
        }

        // in this method we set the HowManyPlayersLabel and the buttons for the amount of players to visible
        private void ShowChooseAmountOfPlayerUI()
        {
            // setting the visibility of the StartGame Button to hidden
            IsStartGameButtonVisible = false;
            // setting the visibility of the HowManyPlayersLabel to visible
            IsHowManyPlayersLabelVisible = true;
            // setting the visibility of the buttons for the amount of players to visible
            IsOnePlayerButtonVisible = true;
            IsTwoPlayersButtonVisible = true;
            IsThreePlayersButtonVisible = true;
            IsFourPlayersButtonVisible = true;
        }

        // in this method we set the HowManyPlayersLabel and the buttons for the amount of players to hidden
        private void HideChooseAmountOfPlayerUI()
        {
            // setting the visibility of the HowManyPlayersLabel to hidden
            IsHowManyPlayersLabelVisible = false;
            // setting the visibility of the buttons for the amount of players to hidden
            IsOnePlayerButtonVisible = false;
            IsTwoPlayersButtonVisible = false;
            IsThreePlayersButtonVisible = false;
            IsFourPlayersButtonVisible = false;
        }



        // declaring the private variables
        private string _die1Image;
        private string _die2Image;
        private string _die3Image;
        private string _die4Image;
        private string _die5Image;
        private string _rollsLeft;

        // declaring the private variables for the player names
        private string _player1Name;
        private string _player2Name;
        private string _player3Name;
        private string _player4Name;

        // declaring the private variables for the visibility of the StartGame Button
        private bool _isStartGameButtonVisible;

        // declaring the private variables for the How Many Players Menu
        private bool _isHowManyPlayersLabelVisible;
        private bool _isOnePlayerButtonVisible;
        private bool _isTwoPlayersButtonVisible;
        private bool _isThreePlayersButtonVisible;
        private bool _isFourPlayersButtonVisible;

        // declaring the private variables for the amount of players
        private int _amountOfPlayers;


        // declaring the public properties
        public string Die1Image
        {
            get { return _die1Image; }
            set
            {
                if (_die1Image != value)
                {
                    _die1Image = value;
                    OnPropertyChanged(nameof(Die1Image));
                }
            }
        }
        public string Die2Image
        {
            get { return _die2Image; }
            set
            {
                if (_die2Image != value)
                {
                    _die2Image = value;
                    OnPropertyChanged(nameof(Die2Image));
                }
            }
        }
        public string Die3Image
        {
            get { return _die3Image; }
            set
            {
                if (_die3Image != value)
                {
                    _die3Image = value;
                    OnPropertyChanged(nameof(Die3Image));
                }
            }
        }
        public string Die4Image
        {
            get { return _die4Image; }
            set
            {
                if (_die4Image != value)
                {
                    _die4Image = value;
                    OnPropertyChanged(nameof(Die4Image));
                }
            }
        }
        public string Die5Image
        {
            get { return _die5Image; }
            set
            {
                if (_die5Image != value)
                {
                    _die5Image = value;
                    OnPropertyChanged(nameof(Die5Image));
                }
            }
        }
        public string RollsLeft
        {
            get { return _rollsLeft; }
            set
            {
                if (_rollsLeft != value)
                {
                    _rollsLeft = value;
                    OnPropertyChanged(nameof(RollsLeft));
                }
            }
        }

        // declaring the public properties for the player names
        public string Player1Name
        {
            get { return _player1Name; }
            set
            {
                if (_player1Name != value)
                {
                    _player1Name = value;
                    OnPropertyChanged(nameof(Player1Name));
                }
            }
        }
        public string Player2Name
        {
            get { return _player2Name; }
            set
            {
                if (_player2Name != value)
                {
                    _player2Name = value;
                    OnPropertyChanged(nameof(Player2Name));
                }
            }
        }
        public string Player3Name
        {
            get { return _player3Name; }
            set
            {
                if (_player3Name != value)
                {
                    _player3Name = value;
                    OnPropertyChanged(nameof(Player3Name));
                }
            }
        }
        public string Player4Name
        {
            get { return _player4Name; }
            set
            {
                if (_player4Name != value)
                {
                    _player4Name = value;
                    OnPropertyChanged(nameof(Player4Name));
                }
            }
        }

        // declaring the public property for the visibility of the StartGame Button
        public bool IsStartGameButtonVisible
        {
            get { return _isStartGameButtonVisible; }
            set
            {
                if (_isStartGameButtonVisible != value)
                {
                    _isStartGameButtonVisible = value;
                    OnPropertyChanged(nameof(IsStartGameButtonVisible));
                }
            }
        }

        // declaring the public property AmountOfPlayers
        public int AmountOfPlayers
        {
            get { return _amountOfPlayers; }
            set
            {
                if (_amountOfPlayers != value)
                {
                    _amountOfPlayers = value;
                    OnPropertyChanged(nameof(AmountOfPlayers));
                }
            }
        }

        // declaring the public properties for the visibility of the HowManyPlayers Menu
        public bool IsHowManyPlayersLabelVisible
        {
            get { return _isHowManyPlayersLabelVisible; }
            set
            {
                if (_isHowManyPlayersLabelVisible != value)
                {
                    _isHowManyPlayersLabelVisible = value;
                    OnPropertyChanged(nameof(IsHowManyPlayersLabelVisible));
                }
            }
        }
        public bool IsOnePlayerButtonVisible
        {
            get { return _isOnePlayerButtonVisible; }
            set
            {
                if (_isOnePlayerButtonVisible != value)
                {
                    _isOnePlayerButtonVisible = value;
                    OnPropertyChanged(nameof(IsOnePlayerButtonVisible));
                }
            }
        }
        public bool IsTwoPlayersButtonVisible
        {
            get { return _isTwoPlayersButtonVisible; }
            set
            {
                if (_isTwoPlayersButtonVisible != value)
                {
                    _isTwoPlayersButtonVisible = value;
                    OnPropertyChanged(nameof(IsTwoPlayersButtonVisible));
                }
            }
        }
        public bool IsThreePlayersButtonVisible
        {
            get { return _isThreePlayersButtonVisible; }
            set
            {
                if (_isThreePlayersButtonVisible != value)
                {
                    _isThreePlayersButtonVisible = value;
                    OnPropertyChanged(nameof(IsThreePlayersButtonVisible));
                }
            }
        }
        public bool IsFourPlayersButtonVisible
        {
            get { return _isFourPlayersButtonVisible; }
            set
            {
                if (_isFourPlayersButtonVisible != value)
                {
                    _isFourPlayersButtonVisible = value;
                    OnPropertyChanged(nameof(IsFourPlayersButtonVisible));
                }
            }
        }
    }

}
