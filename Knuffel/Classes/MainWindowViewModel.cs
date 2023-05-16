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

            // declaring the default value for the RollsLeft
            RollsLeft = 3;

            // declaring the default value for the visibility of the StartGame Button
            IsStartGameButtonVisible = true;

            // declaring the default values for the CoverGrids
            CoverGridPlayer2To4 = false;
            CoverGridPlayer3To4 = false;
            CoverGridPlayer4 = false;

            // declaring the default values for the visibility of the Enter Player Names Menu
            IsEnterPlayer1NameLabelVisible = false;
            IsPlayer1NameTextBoxVisible = false;
            IsEnterPlayer2NameLabelVisible = false;
            IsPlayer2NameTextBoxVisible = false;
            IsEnterPlayer3NameLabelVisible = false;
            IsPlayer3NameTextBoxVisible = false;
            IsEnterPlayer4NameLabelVisible = false;
            IsPlayer4NameTextBoxVisible = false;
            IsEnterPlayerNamesButtonVisible = false;


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

            // initializing the command for the Enter Player Names Button
            EnterPlayerNamesCommand = new DelegateCommand((o) => true, (o) => CreatePlayers());
        }

        // declaring the command for the start game button
        public DelegateCommand StartGameCommand { get; set; }

        // declaring the command for the 1-4 Player buttons
        public DelegateCommand PlayersButtonsCommand { get; set; }

        // declaring the command for the Enter Player Names Button
        public DelegateCommand EnterPlayerNamesCommand { get; set; }

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

            // calling the method where unneccessary UI elements of the scoreboard are hidden
            HideUnneccessaryScoreBoardUIElements();
        }

        // in this method we hide the unneccessary UI elements of the scoreboard depending on the amount of players
        private void HideUnneccessaryScoreBoardUIElements()
        {
            switch (AmountOfPlayers)
            {
                case 1:
                    // setting the visibility of the scoreboard for player 2 to 4 to hidden
                    CoverGridPlayer2To4 = true;
                    break;
                case 2:
                    // setting the visibility of the scoreboard for player 3 to 4 to hidden
                    CoverGridPlayer3To4 = true;
                    break;
                case 3:
                    // setting the visibility of the scoreboard for player 4 to hidden
                    CoverGridPlayer4 = true;
                    break;
                case 4:
                    // setting the visibility of the scoreboard for player 2 to 4 to visible
                    CoverGridPlayer2To4 = false;
                    CoverGridPlayer3To4 = false;
                    CoverGridPlayer4 = false;
                    break;

            }

            // calling the method where Menu to enter the player names is shown, depending on the amount of players
            ShowEnterPlayerNamesMenu();
        }


        // in this method we show the Menu to enter the player names, depending on the amount of players
        private void ShowEnterPlayerNamesMenu()
        {
            switch (AmountOfPlayers)
            {

                case 1:
                    // setting the visibility for the NameLabel and the TextBox for player 1 to visible
                    // setting the EnterPlayerNamesButton to visible
                    IsEnterPlayer1NameLabelVisible = true;
                    IsPlayer1NameTextBoxVisible = true;
                    IsEnterPlayerNamesButtonVisible = true;
                    break;
                case 2:
                    // setting the visiility for the NameLabels and the TextBoxes for player 1 and 2 to visible
                    // setting the EnterPlayerNamesButton to visible
                    IsEnterPlayer1NameLabelVisible = true;
                    IsPlayer1NameTextBoxVisible = true;
                    IsEnterPlayer2NameLabelVisible = true;
                    IsPlayer2NameTextBoxVisible = true;
                    IsEnterPlayerNamesButtonVisible = true;
                    break;
                case 3:
                    // setting the visiility for the NameLabels and the TextBoxes for player 1 to 3 to visible
                    // setting the EnterPlayerNamesButton to visible
                    IsEnterPlayer1NameLabelVisible = true;
                    IsPlayer1NameTextBoxVisible = true;
                    IsEnterPlayer2NameLabelVisible = true;
                    IsPlayer2NameTextBoxVisible = true;
                    IsEnterPlayer3NameLabelVisible = true;
                    IsPlayer3NameTextBoxVisible = true;
                    IsEnterPlayerNamesButtonVisible = true;
                    break;
                case 4:
                    // setting the visiility for the NameLabels and the TextBoxes for player 1 to 4 to visible
                    // setting the EnterPlayerNamesButton to visible
                    IsEnterPlayer1NameLabelVisible = true;
                    IsPlayer1NameTextBoxVisible = true;
                    IsEnterPlayer2NameLabelVisible = true;
                    IsPlayer2NameTextBoxVisible = true;
                    IsEnterPlayer3NameLabelVisible = true;
                    IsPlayer3NameTextBoxVisible = true;
                    IsEnterPlayer4NameLabelVisible = true;
                    IsPlayer4NameTextBoxVisible = true;
                    IsEnterPlayerNamesButtonVisible = true;
                    break;
            }
        }

        // in this method we are hiding the Menu to enter the player names
        private void HideEnterPlayerNamesMenu()
        {
            // setting the visibility for the NameLabels and the TextBoxes for player 1 to 4 to hidden
            // setting the EnterPlayerNamesButton to hidden
            IsEnterPlayer1NameLabelVisible = false;
            IsPlayer1NameTextBoxVisible = false;
            IsEnterPlayer2NameLabelVisible = false;
            IsPlayer2NameTextBoxVisible = false;
            IsEnterPlayer3NameLabelVisible = false;
            IsPlayer3NameTextBoxVisible = false;
            IsEnterPlayer4NameLabelVisible = false;
            IsPlayer4NameTextBoxVisible = false;
            IsEnterPlayerNamesButtonVisible = false;

            // calling the method where the UI Elements for the RollButton and the RollsLeftLabel are set to visible
            ShowRollButtonAndRollsLeftLabel();
        }

        private void ShowRollButtonAndRollsLeftLabel()
        {
            // setting the visibility for the RollButton and the RollsLeftLabel to visible
            IsRollButtonVisible = true;
            IsRollsLeftLabelVisible = true;
        }

        // in this method we are creating the players corresponding to the amount of players
        private void CreatePlayers()
        {
            if (AmountOfPlayers == 4)
            {
                Player playerOne = new Player();
                playerOne.Name = Player1Name;
                Player playerTwo = new Player();
                playerTwo.Name = Player2Name;
                Player playerThree = new Player();
                playerThree.Name = Player3Name;
                Player playerFour = new Player();
                playerFour.Name = Player4Name;
            }       
            else if (AmountOfPlayers == 3)
            {
                Player playerOne = new Player();
                playerOne.Name = Player1Name;
                Player playerTwo = new Player();
                playerTwo.Name = Player2Name;
                Player playerThree = new Player();
                playerThree.Name = Player3Name;
            }
            else if (AmountOfPlayers == 2)
            {
                Player playerOne = new Player();
                playerOne.Name = Player1Name;
                Player playerTwo = new Player();
                playerTwo.Name = Player2Name;
            }
            else 
            {
                Player playerOne = new Player();
                playerOne.Name = Player1Name;
            }

            // calling the method where the UI elements for the Enter The Player Names Menu are hidden
            HideEnterPlayerNamesMenu();
        }

       



        // declaring the private variables for the path to the images of the dice
        private string _die1Image;
        private string _die2Image;
        private string _die3Image;
        private string _die4Image;
        private string _die5Image;

        // declaring the private variable for the amount of rolls left
        private int _rollsLeft;

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

        // declaring the private variables for the visibility of the scoreboard
        private bool _coverGridPlayer2To4;
        private bool _coverGridPlayer3To4;
        private bool _coverGridPlayer4;

        // declaring the private variables for the visibility of the Menu to enter the player names
        private bool _isEnterPlayer1NameLabelVisible;
        private bool _isEnterPlayer2NameLabelVisible;
        private bool _isEnterPlayer3NameLabelVisible;
        private bool _isEnterPlayer4NameLabelVisible;
        private bool _isPlayer1NameTextBoxVisible;
        private bool _isPlayer2NameTextBoxVisible;
        private bool _isPlayer3NameTextBoxVisible;
        private bool _isPlayer4NameTextBoxVisible;
        private bool _isEnterPlayerNamesButtonVisible;

        // declaring the private variables for the visibility of the RollButton and the RollsLeftLabel
        private bool _isRollButtonVisible;
        private bool _isRollsLeftLabelVisible;


        // declaring the public properties for the dice images
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
        public int RollsLeft
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

        // declaring the public properties for hidding uneccessary Scoreboard UI elements

        public bool CoverGridPlayer2To4
        {
            get { return _coverGridPlayer2To4; }
            set
            {
                if (_coverGridPlayer2To4 != value)
                {
                    _coverGridPlayer2To4 = value;
                    OnPropertyChanged(nameof(CoverGridPlayer2To4));
                }
            }
        }

        public bool CoverGridPlayer3To4
        {
            get { return _coverGridPlayer3To4; }
            set
            {
                if (_coverGridPlayer3To4 != value)
                {
                    _coverGridPlayer3To4 = value;
                    OnPropertyChanged(nameof(CoverGridPlayer3To4));
                }
            }
        }
        public bool CoverGridPlayer4
        {
            get { return _coverGridPlayer4; }
            set
            {
                if (_coverGridPlayer4 != value)
                {
                    _coverGridPlayer4 = value;
                    OnPropertyChanged(nameof(CoverGridPlayer4));
                }
            }
        }

        // declaring the public properties for the visibility of the Enter Player Names Menu
        public bool IsEnterPlayer1NameLabelVisible
        {
            get { return _isEnterPlayer1NameLabelVisible; }
            set
            {
                if (_isEnterPlayer1NameLabelVisible != value)
                {
                    _isEnterPlayer1NameLabelVisible = value;
                    OnPropertyChanged(nameof(IsEnterPlayer1NameLabelVisible));
                }
            }
        }
        public bool IsEnterPlayer2NameLabelVisible
        {
            get { return _isEnterPlayer2NameLabelVisible; }
            set
            {
                if (_isEnterPlayer2NameLabelVisible != value)
                {
                    _isEnterPlayer2NameLabelVisible = value;
                    OnPropertyChanged(nameof(IsEnterPlayer2NameLabelVisible));
                }
            }
        }
        public bool IsEnterPlayer3NameLabelVisible
        {
            get { return _isEnterPlayer3NameLabelVisible; }
            set
            {
                if (_isEnterPlayer3NameLabelVisible != value)
                {
                    _isEnterPlayer3NameLabelVisible = value;
                    OnPropertyChanged(nameof(IsEnterPlayer3NameLabelVisible));
                }
            }
        }
        public bool IsEnterPlayer4NameLabelVisible
        {
            get { return _isEnterPlayer4NameLabelVisible; }
            set
            {
                if (_isEnterPlayer4NameLabelVisible != value)
                {
                    _isEnterPlayer4NameLabelVisible = value;
                    OnPropertyChanged(nameof(IsEnterPlayer4NameLabelVisible));
                }
            }
        }
        public bool IsPlayer1NameTextBoxVisible
        {
            get { return _isPlayer1NameTextBoxVisible; }
            set
            {
                if (_isPlayer1NameTextBoxVisible != value)
                {
                    _isPlayer1NameTextBoxVisible = value;
                    OnPropertyChanged(nameof(IsPlayer1NameTextBoxVisible));
                }
            }
        }
        public bool IsPlayer2NameTextBoxVisible
        {
            get { return _isPlayer2NameTextBoxVisible; }
            set
            {
                if (_isPlayer2NameTextBoxVisible != value)
                {
                    _isPlayer2NameTextBoxVisible = value;
                    OnPropertyChanged(nameof(IsPlayer2NameTextBoxVisible));
                }
            }
        }
        public bool IsPlayer3NameTextBoxVisible
        {
            get { return _isPlayer3NameTextBoxVisible; }
            set
            {
                if (_isPlayer3NameTextBoxVisible != value)
                {
                    _isPlayer3NameTextBoxVisible = value;
                    OnPropertyChanged(nameof(IsPlayer3NameTextBoxVisible));
                }
            }
        }
        public bool IsPlayer4NameTextBoxVisible
        {
            get { return _isPlayer4NameTextBoxVisible; }
            set
            {
                if (_isPlayer4NameTextBoxVisible != value)
                {
                    _isPlayer4NameTextBoxVisible = value;
                    OnPropertyChanged(nameof(IsPlayer4NameTextBoxVisible));
                }
            }
        }
        public bool IsEnterPlayerNamesButtonVisible
        {
            get { return _isEnterPlayerNamesButtonVisible; }
            set
            {
                if (_isEnterPlayerNamesButtonVisible != value)
                {
                    _isEnterPlayerNamesButtonVisible = value;
                    OnPropertyChanged(nameof(IsEnterPlayerNamesButtonVisible));
                }
            }
        }

        // declaring the public properties for the visibility of the RollButton and the RollsLeftLabel
        public bool IsRollButtonVisible
        {
            get { return _isRollButtonVisible; }
            set
            {
                if (_isRollButtonVisible != value)
                {
                    _isRollButtonVisible = value;
                    OnPropertyChanged(nameof(IsRollButtonVisible));
                }
            }
        }
        public bool IsRollsLeftLabelVisible
        {
            get { return _isRollsLeftLabelVisible; }
            set
            {
                if (_isRollsLeftLabelVisible != value)
                {
                    _isRollsLeftLabelVisible = value;
                    OnPropertyChanged(nameof(IsRollsLeftLabelVisible));
                }
            }
        }
    }

}
