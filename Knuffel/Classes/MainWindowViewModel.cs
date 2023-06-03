using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Threading;

namespace Knuffel.Classes
{
    internal class MainWindowViewModel : BaseViewModel
    {

        //  creating 4 new players
        Player playerOne = new Player();
        Player playerTwo = new Player();
        Player playerThree = new Player();
        Player playerFour = new Player();

        //declaring the images for the dice
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


        public MainWindowViewModel()
        {
            // declaring the default values for the Die Images
            Die1Image = "pack://application:,,,/Images/RavenGamesLogo2Use.png";
            Die2Image = "pack://application:,,,/Images/RavenGamesLogo2Use.png";
            Die3Image = "pack://application:,,,/Images/RavenGamesLogo2Use.png";
            Die4Image = "pack://application:,,,/Images/RavenGamesLogo2Use.png";
            Die5Image = "pack://application:,,,/Images/RavenGamesLogo2Use.png";


            // creating a new list of dice
            List<Die> dieList = new List<Die>();

            // creating 5 new dice
            Die dieOne = new Die();
            Die dieTwo = new Die();
            Die dieThree = new Die();
            Die dieFour = new Die();
            Die dieFive = new Die();

            // connecting the UpdateProperty of the Lock Status to each die
            Die1Locked = dieOne.Locked;
            Die2Locked = dieTwo.Locked;
            Die3Locked = dieThree.Locked;
            Die4Locked = dieFour.Locked;
            Die5Locked = dieFive.Locked;

            // adding the 5 dice to the list of dice
            dieList.Add(dieOne);
            dieList.Add(dieTwo);
            dieList.Add(dieThree);
            dieList.Add(dieFour);
            dieList.Add(dieFive);


            // declaring the default values for the player names
            Player1Name = "Player 1";
            Player2Name = "Player 2";
            Player3Name = "Player 3";
            Player4Name = "Player 4";

            // declaring the default value for AmountOfPlayers
            AmountOfPlayers = 1;

            // declaring the default value for the ActivePlayer
            ActivePlayer = 1;

            // declaring the default value for the ActivePlayerName
            ActivePlayerName = playerOne.Name;

            // declaring the default value for the RollsLeft
            RollsLeft = 3;

            // declaring the default value for the RoundsLeft
            RoundsLeft = 13;

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

            // declaring the default values for the visibility of the Active Player Label and the Your Turn Label
            IsActivePlayerLabelVisible = false;
            IsYourTurnLabelVisible = false;

            // declaring the default values for the visibility of the Score Buttons
            IsScoreButtonsLabelVisible = false;
            IsScoreButtonsGridVisible = false;

            // declaring the default values for the visibility of WinnerOfRoundLabels
            IsWinnerOfRoundLabelVisible = false;
            IsNameOfWinnerOfRoundLabelVisible = false;

            // declaring the default values for the visibility of the NewGameButtons
            IsNewGameButtonsVisible = false;


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
            EnterPlayerNamesCommand = new DelegateCommand((o) => true,
                (o) =>
                {
                    CreatePlayerNames();
                }
                );

            // initializing the command for the Roll Dice Button
            RollDiceCommand = new DelegateCommand(
                (o) => RollsLeft > 0 && (!Die1Locked || !Die2Locked || !Die3Locked || !Die4Locked || !Die5Locked),
                (o) =>
                {
                    RollingTheDice(dieList);

                    Die1Image = GetDieImage(dieList[0].Value, dieList[0].Locked);
                    Die2Image = GetDieImage(dieList[1].Value, dieList[1].Locked);
                    Die3Image = GetDieImage(dieList[2].Value, dieList[2].Locked);
                    Die4Image = GetDieImage(dieList[3].Value, dieList[3].Locked);
                    Die5Image = GetDieImage(dieList[4].Value, dieList[4].Locked);
                    RollsLeft--;
                    if (RollsLeft < 3)
                    {
                        IsScoreButtonsLabelVisible = true;
                        IsScoreButtonsGridVisible = true;
                    }
                    RollDiceCommand.RaiseCanExecuteChanged();
                    LockDieCommand.RaiseCanExecuteChanged();
                    ScoreButtonExtraKnuffelsCommand.RaiseCanExecuteChanged();

                }
                );

            // initializing the command for the Lock Die Buttons
            LockDieCommand = new DelegateCommand(
                (o) => RollsLeft < 3,
                (value) =>
                {
                    int val = int.Parse((string)value);
                    Die dieInstance = new Die();
                    dieInstance.ChangeLockStatus(dieList, val);

                    // changing the lock status of the die
                    switch (val)
                    {
                        case 0:
                            Die1Locked = dieList[val].Locked;
                            break;
                        case 1:
                            Die2Locked = dieList[val].Locked;
                            break;
                        case 2:
                            Die3Locked = dieList[val].Locked;
                            break;
                        case 3:
                            Die4Locked = dieList[val].Locked;
                            break;
                        case 4:
                            Die5Locked = dieList[val].Locked;
                            break;
                    }



                    // changing the image of the die
                    switch (val)
                    {
                        case 0:
                            Die1Image = GetDieImage(dieList[0].Value, dieList[0].Locked);
                            break;
                        case 1:
                            Die2Image = GetDieImage(dieList[1].Value, dieList[1].Locked);
                            break;
                        case 2:
                            Die3Image = GetDieImage(dieList[2].Value, dieList[2].Locked);
                            break;
                        case 3:
                            Die4Image = GetDieImage(dieList[3].Value, dieList[3].Locked);
                            break;
                        case 4:
                            Die5Image = GetDieImage(dieList[4].Value, dieList[4].Locked);
                            break;
                    }

                    // Checking if the Roll Dice Button should be enabled
                    RollDiceCommand.RaiseCanExecuteChanged();
                    LockDieCommand.RaiseCanExecuteChanged();
                });

            // initializing the command for the New Round Button
            NewRoundCommand = new DelegateCommand(
                (o) => true,
                (o) =>
                {
                    // hiding the WinnerOfRoundLabels
                    IsWinnerOfRoundLabelVisible = false;
                    IsNameOfWinnerOfRoundLabelVisible = false;

                    // hiding the NewGameButtons
                    IsNewGameButtonsVisible = false;

                    // resetting the RollsLeft
                    RollsLeft = 3;

                    // resetting the RoundsLeft
                    RoundsLeft = 13;

                    // resetting the ActivePlayer
                    ActivePlayer = 1;
                    ActivePlayerName = playerOne.Name;
                    OnPropertyChanged(nameof(ActivePlayerName));

                    // resetting the DieLocked values
                    Die1Locked = false;
                    Die2Locked = false;
                    Die3Locked = false;
                    Die4Locked = false;
                    Die5Locked = false;

                    // set the die values to 0
                    NullDieValues(dieList);

                    // calling the methods to reset the scores
                    SetAllScoresToNull();
                    SetSumScoreValues();

                    // resetting the visibility of the active player label
                    IsYourTurnLabelVisible = true;
                    IsActivePlayerLabelVisible = true;

                    // resetting the visibility of the Roll Button
                    IsRollButtonVisible = true;
                    IsRollsLeftLabelVisible = true;

                    // resetting the visibility of the Score Buttons
                    IsScoreButtonsLabelVisible = true;
                    IsScoreButtonsGridVisible = true;
                });

            // initializing the command for the New Game Button
            NewGameCommand = new DelegateCommand(
                (o) => true,
                (o) =>
                                              {
                    // hiding the WinnerOfRoundLabels
                    IsWinnerOfRoundLabelVisible = false;
                    IsNameOfWinnerOfRoundLabelVisible = false;

                    // hiding the NewGameButtons
                    IsNewGameButtonsVisible = false;

                    // resetting the RollsLeft
                    RollsLeft = 3;

                    // resetting the RoundsLeft
                    RoundsLeft = 13;

                    // resetting the ActivePlayer
                    ActivePlayer = 1;
                    ActivePlayerName = playerOne.Name;
                    OnPropertyChanged(nameof(ActivePlayerName));

                    // resetting the DieLocked values
                    Die1Locked = false;
                    Die2Locked = false;
                    Die3Locked = false;
                    Die4Locked = false;
                    Die5Locked = false;

                    // set the die values to 0
                    NullDieValues(dieList);

                    // calling the methods to reset the scores
                    SetAllScoresToNull();
                    SetSumScoreValues();

                    // resetting the visibility of the StartGameButton
                    IsStartGameButtonVisible = true;                   

                });

            // initializing the command for the ExitGameButton
            ExitGameCommand = new DelegateCommand(
                               (o) => true,
                                              (o) =>
                                              {
                    // closing the application
                    Application.Current.Shutdown();
                });

            // initializing the command for the ScoreButtonOnes
            ScoreButtonOnesCommand = new DelegateCommand(
                  (o) => ActivePlayer == 1 && PlayerOneOnes == null ||
                         ActivePlayer == 2 && PlayerTwoOnes == null ||
                         ActivePlayer == 3 && PlayerThreeOnes == null ||
                         ActivePlayer == 4 && PlayerFourOnes == null,
                  (o) =>
                  {
                      // for each die in the dieList, if the value is 1, add 1 to the score
                      int? score = 0;
                      foreach (Die die in dieList)
                      {
                          if (die.Value == 1)
                          {
                              score += 1;
                          }
                      }
                      // adding the score to the correct player
                      switch (ActivePlayer)
                      {
                          case 1:
                              PlayerOneOnes = score;
                              PlayerOneSumUpper += score;
                              break;
                          case 2:
                              PlayerTwoOnes = score;
                              PlayerTwoSumUpper += score;
                              break;
                          case 3:
                              PlayerThreeOnes = score;
                              PlayerThreeSumUpper += score;
                              break;
                          case 4:
                              PlayerFourOnes = score;
                              PlayerFourSumUpper += score;
                              break;
                      }
                      // calling the method SetAutoFillScores();
                      SetAutoFillScores();

                      // nulling the die values
                      NullDieValues(dieList);
                      // calling the method NextPlayer
                      NextPlayer();
                  });

            // initializing the command for the ScoreButtonTwos
            ScoreButtonTwosCommand = new DelegateCommand(
                  (o) => ActivePlayer == 1 && PlayerOneTwos == null ||
                         ActivePlayer == 2 && PlayerTwoTwos == null ||
                         ActivePlayer == 3 && PlayerThreeTwos == null ||
                         ActivePlayer == 4 && PlayerFourTwos == null,
                  (o) =>
                  {
                      // for each die in the dieList, if the value is 2, add 2 to the score
                      int? score = 0;
                      foreach (Die die in dieList)
                      {
                          if (die.Value == 2)
                          {
                              score += 2;
                          }

                      }
                      // adding the score to the correct player
                      switch (ActivePlayer)
                      {
                          case 1:
                              PlayerOneTwos = score;
                              PlayerOneSumUpper += score;
                              break;
                          case 2:
                              PlayerTwoTwos = score;
                              PlayerTwoSumUpper += score;
                              break;
                          case 3:
                              PlayerThreeTwos = score;
                              PlayerThreeSumUpper += score;
                              break;
                          case 4:
                              PlayerFourTwos = score;
                              PlayerFourSumUpper += score;
                              break;
                      }


                      // calling the method SetAutoFillScores();
                      SetAutoFillScores();
                      // nulling the die values
                      NullDieValues(dieList);
                      // calling the method NextPlayer
                      NextPlayer();
                  });

            // initializing the command for the ScoreButtonThrees
            ScoreButtonThreesCommand = new DelegateCommand(
                  (o) => ActivePlayer == 1 && PlayerOneThrees == null ||
                         ActivePlayer == 2 && PlayerTwoThrees == null ||
                         ActivePlayer == 3 && PlayerThreeThrees == null ||
                         ActivePlayer == 4 && PlayerFourThrees == null,
                  (o) =>
                  {
                      // for each die in the dieList, if the value is 3, add 3 to the score
                      int? score = 0;
                      foreach (Die die in dieList)
                      {
                          if (die.Value == 3)
                          {
                              score += 3;
                          }
                      }
                      // adding the score to the correct player
                      switch (ActivePlayer)
                      {
                          case 1:
                              PlayerOneThrees = score;
                              PlayerOneSumUpper += score;
                              break;
                          case 2:
                              PlayerTwoThrees = score;
                              PlayerTwoSumUpper += score;
                              break;
                          case 3:
                              PlayerThreeThrees = score;
                              PlayerThreeSumUpper += score;
                              break;
                          case 4:
                              PlayerFourThrees = score;
                              PlayerFourSumUpper += score;
                              break;
                      }

                      // calling the method SetAutoFillScores();
                      SetAutoFillScores();

                      // nulling the die values
                      NullDieValues(dieList);
                      // calling the method NextPlayer
                      NextPlayer();
                  });

            // initializing the command for the ScoreButtonFours
            ScoreButtonFoursCommand = new DelegateCommand(
                  (o) => ActivePlayer == 1 && PlayerOneFours == null ||
                         ActivePlayer == 2 && PlayerTwoFours == null ||
                         ActivePlayer == 3 && PlayerThreeFours == null ||
                         ActivePlayer == 4 && PlayerFourFours == null,
                  (o) =>
                  {
                      // for each die in the dieList, if the value is 4, add 4 to the score
                      int? score = 0;
                      foreach (Die die in dieList)
                      {
                          if (die.Value == 4)
                          {
                              score += 4;
                          }

                      }
                      // adding the score to the correct player
                      switch (ActivePlayer)
                      {
                          case 1:
                              PlayerOneFours = score;
                              PlayerOneSumUpper += score;
                              break;
                          case 2:
                              PlayerTwoFours = score;
                              PlayerTwoSumUpper += score;
                              break;
                          case 3:
                              PlayerThreeFours = score;
                              PlayerThreeSumUpper += score;
                              break;
                          case 4:
                              PlayerFourFours = score;
                              PlayerFourSumUpper += score;
                              break;
                      }

                      // calling the method SetAutoFillScores();
                      SetAutoFillScores();

                      // nulling the die values
                      NullDieValues(dieList);
                      // calling the method NextPlayer
                      NextPlayer();
                  });

            // initializing the command for the ScoreButtonFives
            ScoreButtonFivesCommand = new DelegateCommand(
                  (o) => ActivePlayer == 1 && PlayerOneFives == null ||
                         ActivePlayer == 2 && PlayerTwoFives == null ||
                         ActivePlayer == 3 && PlayerThreeFives == null ||
                         ActivePlayer == 4 && PlayerFourFives == null,
                  (o) =>
                  {
                      // for each die in the dieList, if the value is 5, add 5 to the score
                      int? score = 0;
                      foreach (Die die in dieList)
                      {
                          if (die.Value == 5)
                          {
                              score += 5;
                          }

                      }
                      // adding the score to the correct player
                      switch (ActivePlayer)
                      {
                          case 1:
                              PlayerOneFives = score;
                              PlayerOneSumUpper += score;
                              break;
                          case 2:
                              PlayerTwoFives = score;
                              PlayerTwoSumUpper += score;
                              break;
                          case 3:
                              PlayerThreeFives = score;
                              PlayerThreeSumUpper += score;
                              break;
                          case 4:
                              PlayerFourFives = score;
                              PlayerFourSumUpper += score;
                              break;
                      }

                      // calling the method SetAutoFillScores();
                      SetAutoFillScores();

                      // nulling the die values
                      NullDieValues(dieList);
                      // calling the method NextPlayer
                      NextPlayer();
                  });

            // initializing the command for the ScoreButtonSixes
            ScoreButtonSixesCommand = new DelegateCommand(
                  (o) => ActivePlayer == 1 && PlayerOneSixes == null ||
                         ActivePlayer == 2 && PlayerTwoSixes == null ||
                         ActivePlayer == 3 && PlayerThreeSixes == null ||
                         ActivePlayer == 4 && PlayerFourSixes == null,
                  (o) =>
                  {
                      // for each die in the dieList, if the value is 6, add 6 to the score
                      int? score = 0;
                      foreach (Die die in dieList)
                      {
                          if (die.Value == 6)
                          {
                              score += 6;
                          }

                      }
                      // adding the score to the correct player
                      switch (ActivePlayer)
                      {
                          case 1:
                              PlayerOneSixes = score;
                              PlayerOneSumUpper += score;
                              break;
                          case 2:
                              PlayerTwoSixes = score;
                              PlayerTwoSumUpper += score;
                              break;
                          case 3:
                              PlayerThreeSixes = score;
                              PlayerThreeSumUpper += score;
                              break;
                          case 4:
                              PlayerFourSixes = score;
                              PlayerFourSumUpper += score;
                              break;
                      }

                      // calling the method SetAutoFillScores();
                      SetAutoFillScores();

                      // nulling the die values
                      NullDieValues(dieList);
                      // calling the method NextPlayer
                      NextPlayer();
                  });

            // initializing the command for the ScoreButtonThreeOfAKind
            ScoreButtonThreeOfAKindCommand = new DelegateCommand(
                  (o) => ActivePlayer == 1 && PlayerOneThreeOfAKind == null ||
                         ActivePlayer == 2 && PlayerTwoThreeOfAKind == null ||
                         ActivePlayer == 3 && PlayerThreeThreeOfAKind == null ||
                         ActivePlayer == 4 && PlayerFourThreeOfAKind == null,
                  (o) =>
                  {
                      // declaring the variable for the score
                      int? score = 0;
                      // checking if the dieList contains at least three die with the same value
                      if (dieList.Any(die => dieList.Count(d => d.Value == die.Value) >= 3))
                      {
                          // adding the value of each die to the score
                          foreach (Die die in dieList)
                          {
                              score += die.Value;
                          }
                      }


                      {
                          switch (ActivePlayer)
                          {
                              case 1:
                                  PlayerOneThreeOfAKind = score;
                                  PlayerOneSumLower += score;
                                  break;
                              case 2:
                                  PlayerTwoThreeOfAKind = score;
                                  PlayerTwoSumLower += score;
                                  break;
                              case 3:
                                  PlayerThreeThreeOfAKind = score;
                                  PlayerThreeSumLower += score;
                                  break;
                              case 4:
                                  PlayerFourThreeOfAKind = score;
                                  PlayerFourSumLower += score;
                                  break;
                          }
                      }

                      // calling the method SetAutoFillScores();
                      SetAutoFillScores();

                      // nulling the die values
                      NullDieValues(dieList);
                      // calling the method NextPlayer
                      NextPlayer();
                  });

            // initializing the command for the ScoreButtonFourOfAKind
            ScoreButtonFourOfAKindCommand = new DelegateCommand(
                  (o) => ActivePlayer == 1 && PlayerOneFourOfAKind == null ||
                         ActivePlayer == 2 && PlayerTwoFourOfAKind == null ||
                         ActivePlayer == 3 && PlayerThreeFourOfAKind == null ||
                         ActivePlayer == 4 && PlayerFourFourOfAKind == null,
                  (o) =>
                  {
                      // declaring the variable for the score
                      int? score = 0;
                      // checking if the dieList contains at least four die with the same value
                      if (dieList.Any(die => dieList.Count(d => d.Value == die.Value) >= 4))
                      {
                          // adding the value of each die to the score
                          foreach (Die die in dieList)
                          {
                              score += die.Value;
                          }
                      }


                      {
                          switch (ActivePlayer)
                          {
                              case 1:
                                  PlayerOneFourOfAKind = score;
                                  PlayerOneSumLower += score;
                                  break;
                              case 2:
                                  PlayerTwoFourOfAKind = score;
                                  PlayerTwoSumLower += score;
                                  break;
                              case 3:
                                  PlayerThreeFourOfAKind = score;
                                  PlayerThreeSumLower += score;
                                  break;
                              case 4:
                                  PlayerFourFourOfAKind = score;
                                  PlayerFourSumLower += score;
                                  break;
                          }
                      }

                      // calling the method SetAutoFillScores();
                      SetAutoFillScores();

                      // nulling the die values
                      NullDieValues(dieList);
                      // calling the method NextPlayer
                      NextPlayer();
                  });

            // initializing the command for the ScoreButtonFullHouse
            ScoreButtonFullHouseCommand = new DelegateCommand(
                  (o) => ActivePlayer == 1 && PlayerOneFullHouse == null ||
                         ActivePlayer == 2 && PlayerTwoFullHouse == null ||
                         ActivePlayer == 3 && PlayerThreeFullHouse == null ||
                         ActivePlayer == 4 && PlayerFourFullHouse == null,
                  (o) =>
                  {
                      // declaring the variable for the score
                      int? score = 0;
                      // checking if the dieList contains three die with the same value and two die with the same value
                      if (dieList.Any(die => dieList.Count(d => d.Value == die.Value) == 3) &&
                          dieList.Any(die => dieList.Count(d => d.Value == die.Value) == 2))
                      {
                          // setting the score to 25
                          score = 25;
                      }

                      {
                          switch (ActivePlayer)
                          {
                              case 1:
                                  PlayerOneFullHouse = score;
                                  PlayerOneSumLower += score;
                                  break;
                              case 2:
                                  PlayerTwoFullHouse = score;
                                  PlayerTwoSumLower += score;
                                  break;
                              case 3:
                                  PlayerThreeFullHouse = score;
                                  PlayerThreeSumLower += score;
                                  break;
                              case 4:
                                  PlayerFourFullHouse = score;
                                  PlayerFourSumLower += score;
                                  break;
                          }
                      }

                      // calling the method SetAutoFillScores();
                      SetAutoFillScores();

                      // nulling the die values
                      NullDieValues(dieList);
                      // calling the method NextPlayer
                      NextPlayer();
                  });

            // initializing the command for the ScoreButtonSmallStraight
            ScoreButtonSmallStraightCommand = new DelegateCommand(
                  (o) => ActivePlayer == 1 && PlayerOneSmallStraight == null ||
                         ActivePlayer == 2 && PlayerTwoSmallStraight == null ||
                         ActivePlayer == 3 && PlayerThreeSmallStraight == null ||
                         ActivePlayer == 4 && PlayerFourSmallStraight == null,
                  (o) =>
                  {
                      // declaring the variable for the score
                      int? score = 0;
                      // checking if the dieList contains at least four die with consecutive values
                      if (dieList.Any(die => dieList.Count(d => d.Value == die.Value) >= 1) &&
                          dieList.Any(die => dieList.Count(d => d.Value == die.Value + 1) >= 1) &&
                          dieList.Any(die => dieList.Count(d => d.Value == die.Value + 2) >= 1) &&
                          dieList.Any(die => dieList.Count(d => d.Value == die.Value + 3) >= 1))
                      {
                          // setting the score to 30
                          score = 30;
                      }

                      {
                          switch (ActivePlayer)
                          {
                              case 1:
                                  PlayerOneSmallStraight = score;
                                  PlayerOneSumLower += score;
                                  break;
                              case 2:
                                  PlayerTwoSmallStraight = score;
                                  PlayerTwoSumLower += score;
                                  break;
                              case 3:
                                  PlayerThreeSmallStraight = score;
                                  PlayerThreeSumLower += score;
                                  break;
                              case 4:
                                  PlayerFourSmallStraight = score;
                                  PlayerFourSumLower += score;
                                  break;
                          }
                      }

                      // calling the method SetAutoFillScores();
                      SetAutoFillScores();

                      // nulling the die values
                      NullDieValues(dieList);
                      // calling the method NextPlayer
                      NextPlayer();
                  });

            // initializing the command for the ScoreButtonLargeStraight
            ScoreButtonLargeStraightCommand = new DelegateCommand(
                 (o) => ActivePlayer == 1 && PlayerOneLargeStraight == null ||
                        ActivePlayer == 2 && PlayerTwoLargeStraight == null ||
                        ActivePlayer == 3 && PlayerThreeLargeStraight == null ||
                        ActivePlayer == 4 && PlayerFourLargeStraight == null,
                 (o) =>
                 {
                     // declaring the variable for the score
                     int? score = 0;
                     // checking if the dieList contains five die with consecutive values
                     if (dieList.Any(die => dieList.Count(d => d.Value == die.Value) >= 1) &&
                         dieList.Any(die => dieList.Count(d => d.Value == die.Value + 1) >= 1) &&
                         dieList.Any(die => dieList.Count(d => d.Value == die.Value + 2) >= 1) &&
                         dieList.Any(die => dieList.Count(d => d.Value == die.Value + 3) >= 1) &&
                                                                                                                                                         dieList.Any(die => dieList.Count(d => d.Value == die.Value + 4) >= 1))
                     {
                         // setting the score to 40
                         score = 40;
                     }

                     {
                         switch (ActivePlayer)
                         {
                             case 1:
                                 PlayerOneLargeStraight = score;
                                 PlayerOneSumLower += score;
                                 break;
                             case 2:
                                 PlayerTwoLargeStraight = score;
                                 PlayerTwoSumLower += score;
                                 break;
                             case 3:
                                 PlayerThreeLargeStraight = score;
                                 PlayerThreeSumLower += score;
                                 break;
                             case 4:
                                 PlayerFourLargeStraight = score;
                                 PlayerFourSumLower += score;
                                 break;
                         }
                     }

                     // calling the method SetAutoFillScores();
                     SetAutoFillScores();

                     // nulling the die values
                     NullDieValues(dieList);
                     // calling the method NextPlayer
                     NextPlayer();
                 });

            // initializing the command for the ScoreButtonKnuffel
            ScoreButtonKnuffelCommand = new DelegateCommand(
                 (o) => ActivePlayer == 1 && PlayerOneKnuffel == null ||
                        ActivePlayer == 2 && PlayerTwoKnuffel == null ||
                        ActivePlayer == 3 && PlayerThreeKnuffel == null ||
                        ActivePlayer == 4 && PlayerFourKnuffel == null,
                 (o) =>
                 {
                     // declaring the variable for the score
                     int? score = 0;
                     // checking if the dieList contains five die with the same value
                     if (dieList.Any(die => dieList.Count(d => d.Value == die.Value) >= 5))
                     {
                         // setting the score to 50
                         score = 50;

                     }

                     {
                         switch (ActivePlayer)
                         {
                             case 1:
                                 PlayerOneKnuffel = score;
                                 PlayerOneSumLower += score;
                                 break;
                             case 2:
                                 PlayerTwoKnuffel = score;
                                 PlayerTwoSumLower += score;
                                 break;
                             case 3:
                                 PlayerThreeKnuffel = score;
                                 PlayerThreeSumLower += score;
                                 break;
                             case 4:
                                 PlayerFourKnuffel = score;
                                 PlayerFourSumLower += score;
                                 break;
                         }
                     }

                     // calling the method SetAutoFillScores();
                     SetAutoFillScores();
                     // nulling the die values
                     NullDieValues(dieList);
                     // calling the method NextPlayer
                     NextPlayer();
                 });

            // initializing the command for the ScoreButtonChance
            ScoreButtonChanceCommand = new DelegateCommand(
                (o) => ActivePlayer == 1 && PlayerOneChance == null ||
                       ActivePlayer == 2 && PlayerTwoChance == null ||
                       ActivePlayer == 3 && PlayerThreeChance == null ||
                       ActivePlayer == 4 && PlayerFourChance == null,
                (o) =>
                {
                    // declaring the variable for the score
                    int? score = 0;
                    // adding the value of each die to the score
                    foreach (Die die in dieList)
                    {
                        score += die.Value;
                    }

                    {
                        switch (ActivePlayer)
                        {
                            case 1:
                                PlayerOneChance = score;
                                PlayerOneSumLower += score;
                                break;
                            case 2:
                                PlayerTwoChance = score;
                                PlayerTwoSumLower += score;
                                break;
                            case 3:
                                PlayerThreeChance = score;
                                PlayerThreeSumLower += score;
                                break;
                            case 4:
                                PlayerFourChance = score;
                                PlayerFourSumLower += score;
                                break;
                        }
                    }

                    // calling the method SetAutoFillScores();
                    SetAutoFillScores();

                    // nulling the die values
                    NullDieValues(dieList);
                    // calling the method NextPlayer
                    NextPlayer();
                });

            // initializing the command for the ScoreButtonExtraKnuffels
            ScoreButtonExtraKnuffelsCommand = new DelegateCommand(
                (o) => ActivePlayer == 1 && PlayerOneKnuffel == 50
                        && RollsLeft < 3
                       //&& !dieList.Any(die => die.Value == null || die.Value == 0) 
                       && (dieList.Any(die => dieList.Count(d => d.Value == die.Value) >= 5)) ||
                       ActivePlayer == 2 && PlayerTwoKnuffel == 50
                       && RollsLeft < 3
                       //&& !dieList.Any(die => die.Value == null || die.Value == 0) 
                       && (dieList.Any(die => dieList.Count(d => d.Value == die.Value) >= 5)) ||
                       ActivePlayer == 3 && PlayerThreeKnuffel == 50
                       && RollsLeft < 3
                       //&& !dieList.Any(die => die.Value == null || die.Value == 0) 
                       && (dieList.Any(die => dieList.Count(d => d.Value == die.Value) >= 5)) ||
                       ActivePlayer == 4 && PlayerFourKnuffel == 50
                       && RollsLeft < 3
                       //&& !dieList.Any(die => die.Value == null || die.Value == 0) 
                       && (dieList.Any(die => dieList.Count(d => d.Value == die.Value) >= 5)),
                (o) =>
                {
                    // declaring the variable for the score
                    int? score = 50;


                    {
                        switch (ActivePlayer)
                        {
                            case 1:
                                PlayerOneExtraKnuffels += score;
                                PlayerOneSumLower += score;
                                break;
                            case 2:
                                PlayerTwoExtraKnuffels += score;
                                PlayerTwoSumLower += score;
                                break;
                            case 3:
                                PlayerThreeExtraKnuffels += score;
                                PlayerThreeSumLower += score;
                                break;
                            case 4:
                                PlayerFourExtraKnuffels += score;
                                PlayerFourSumLower += score;
                                break;
                        }
                    }

                    // calling the method SetAutoFillScores();
                    SetAutoFillScores();

                    // nulling the die values
                    NullDieValues(dieList);

                    // show MessageBox "Congratulations, you have earned an extra 50 points! It´s your turn again!"
                    MessageBox.Show("Congratulations, you have earned an extra 50 points! It´s your turn again!");

                    // the active player gets another turn
                    ActivePlayer--;

                    // calling the method NextPlayer
                    NextPlayer();
                });
        }



        // declaring the command for the start game button
        public DelegateCommand StartGameCommand { get; set; }

        // declaring the command for the 1-4 Player buttons
        public DelegateCommand PlayersButtonsCommand { get; set; }

        // declaring the command for the Enter Player Names Button
        public DelegateCommand EnterPlayerNamesCommand { get; set; }

        // declaring the command for the Roll Dice Button
        public DelegateCommand RollDiceCommand { get; set; }

        // declaring the command for the Lock Die Buttons
        public DelegateCommand LockDieCommand { get; set; }

        // declaring the command for the New Round Button
        public DelegateCommand NewRoundCommand { get; set; }

        // declaring the command for the New Game Button
        public DelegateCommand NewGameCommand { get; set; }

        // declaring the command for the Exit Game Button
        public DelegateCommand ExitGameCommand { get; set; }

        // declaring the commands for the Score Buttons
        public DelegateCommand ScoreButtonOnesCommand { get; set; }
        public DelegateCommand ScoreButtonTwosCommand { get; set; }
        public DelegateCommand ScoreButtonThreesCommand { get; set; }
        public DelegateCommand ScoreButtonFoursCommand { get; set; }
        public DelegateCommand ScoreButtonFivesCommand { get; set; }
        public DelegateCommand ScoreButtonSixesCommand { get; set; }
        public DelegateCommand ScoreButtonThreeOfAKindCommand { get; set; }
        public DelegateCommand ScoreButtonFourOfAKindCommand { get; set; }
        public DelegateCommand ScoreButtonFullHouseCommand { get; set; }
        public DelegateCommand ScoreButtonSmallStraightCommand { get; set; }
        public DelegateCommand ScoreButtonLargeStraightCommand { get; set; }
        public DelegateCommand ScoreButtonKnuffelCommand { get; set; }
        public DelegateCommand ScoreButtonChanceCommand { get; set; }
        public DelegateCommand ScoreButtonExtraKnuffelsCommand { get; set; }


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
            SetSumScoreValues();
        }



        // in this method we show the Menu to enter the player names, depending on the amount of players
        private void ShowEnterPlayerNamesMenu()
        {
            switch (AmountOfPlayers)
            {

                case 1:
                    IsEnterPlayer1NameLabelVisible = true;
                    IsPlayer1NameTextBoxVisible = true;
                    IsEnterPlayerNamesButtonVisible = true;
                    break;
                case 2:
                    IsEnterPlayer1NameLabelVisible = true;
                    IsPlayer1NameTextBoxVisible = true;
                    IsEnterPlayer2NameLabelVisible = true;
                    IsPlayer2NameTextBoxVisible = true;
                    IsEnterPlayerNamesButtonVisible = true;
                    break;
                case 3:
                    IsEnterPlayer1NameLabelVisible = true;
                    IsPlayer1NameTextBoxVisible = true;
                    IsEnterPlayer2NameLabelVisible = true;
                    IsPlayer2NameTextBoxVisible = true;
                    IsEnterPlayer3NameLabelVisible = true;
                    IsPlayer3NameTextBoxVisible = true;
                    IsEnterPlayerNamesButtonVisible = true;
                    break;
                case 4:
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

            // calling the method where the UI Elements for the YourTurnLabel and the ActivePlayerLabel are set to visible
            ShowYourTurnLabelAndActivePlayerLabel();
        }

        // in this method we are setting the Sum-Score values for the players depending on the amount of players
        private void SetSumScoreValues()
        {
            switch (AmountOfPlayers)
            {
                case 1:
                    // setting all the Sum-Score values for player 1 to 0
                    PlayerOneSumUpper = 0;
                    PlayerOneSumLower = 0;
                    PlayerOneBonus = 0;
                    PlayerOneTotalUpper = 0;
                    PlayerOneExtraKnuffels = 0;
                    PlayerOneFinalScore = 0;
                    PlayerTwoSumUpper = null;
                    PlayerTwoSumLower = null;
                    PlayerTwoBonus = null;
                    PlayerTwoTotalUpper = null;
                    PlayerTwoExtraKnuffels = null;
                    PlayerTwoFinalScore = null;
                    PlayerThreeSumUpper = null;
                    PlayerThreeSumLower = null;
                    PlayerThreeBonus = null;
                    PlayerThreeTotalUpper = null;
                    PlayerThreeExtraKnuffels = null;
                    PlayerThreeFinalScore = null;
                    PlayerFourSumUpper = null;
                    PlayerFourSumLower = null;
                    PlayerFourBonus = null;
                    PlayerFourTotalUpper = null;
                    PlayerFourExtraKnuffels = null;
                    PlayerFourFinalScore = null;
                    break;

                case 2:
                    // setting all the Sum-Score values for player 1 and 2 to 0
                    PlayerOneSumUpper = 0;
                    PlayerOneSumLower = 0;
                    PlayerOneBonus = 0;
                    PlayerOneTotalUpper = 0;
                    PlayerOneExtraKnuffels = 0;
                    PlayerOneFinalScore = 0;
                    PlayerTwoSumUpper = 0;
                    PlayerTwoSumLower = 0;
                    PlayerTwoBonus = 0;
                    PlayerTwoTotalUpper = 0;
                    PlayerTwoExtraKnuffels = 0;
                    PlayerTwoFinalScore = 0;
                    PlayerThreeSumUpper = null;
                    PlayerThreeSumLower = null;
                    PlayerThreeBonus = null;
                    PlayerThreeTotalUpper = null;
                    PlayerThreeExtraKnuffels = null;
                    PlayerThreeFinalScore = null;
                    PlayerFourSumUpper = null;
                    PlayerFourSumLower = null;
                    PlayerFourBonus = null;
                    PlayerFourTotalUpper = null;
                    PlayerFourExtraKnuffels = null;
                    PlayerFourFinalScore = null;
                    break;

                case 3:
                    // setting all the Sum-Score values for player 1 to 3 to 0
                    PlayerOneSumUpper = 0;
                    PlayerOneSumLower = 0;
                    PlayerOneBonus = 0;
                    PlayerOneTotalUpper = 0;
                    PlayerOneExtraKnuffels = 0;
                    PlayerOneFinalScore = 0;
                    PlayerTwoSumUpper = 0;
                    PlayerTwoSumLower = 0;
                    PlayerTwoBonus = 0;
                    PlayerTwoTotalUpper = 0;
                    PlayerTwoExtraKnuffels = 0;
                    PlayerTwoFinalScore = 0;
                    PlayerThreeSumUpper = 0;
                    PlayerThreeSumLower = 0;
                    PlayerThreeBonus = 0;
                    PlayerThreeTotalUpper = 0;
                    PlayerThreeExtraKnuffels = 0;
                    PlayerThreeFinalScore = 0;
                    PlayerFourSumUpper = null;
                    PlayerFourSumLower = null;
                    PlayerFourBonus = null;
                    PlayerFourTotalUpper = null;
                    PlayerFourExtraKnuffels = null;
                    PlayerFourFinalScore = null;
                    break;

                case 4:
                    // setting all the Sum-Score values for player 1 to 4 to 0
                    PlayerOneSumUpper = 0;
                    PlayerOneSumLower = 0;
                    PlayerOneBonus = 0;
                    PlayerOneTotalUpper = 0;
                    PlayerOneExtraKnuffels = 0;
                    PlayerOneFinalScore = 0;
                    PlayerTwoSumUpper = 0;
                    PlayerTwoSumLower = 0;
                    PlayerTwoBonus = 0;
                    PlayerTwoTotalUpper = 0;
                    PlayerTwoExtraKnuffels = 0;
                    PlayerTwoFinalScore = 0;
                    PlayerThreeSumUpper = 0;
                    PlayerThreeSumLower = 0;
                    PlayerThreeBonus = 0;
                    PlayerThreeTotalUpper = 0;
                    PlayerThreeExtraKnuffels = 0;
                    PlayerThreeFinalScore = 0;
                    PlayerFourSumUpper = 0;
                    PlayerFourSumLower = 0;
                    PlayerFourBonus = 0;
                    PlayerFourTotalUpper = 0;
                    PlayerFourExtraKnuffels = 0;
                    PlayerFourFinalScore = 0;
                    break;
            }
        }

        // in this method we are setting all the Score values for the players to null
        private void SetAllScoresToNull()
        {
            // setting the Score values for player 1 to null
            PlayerOneOnes = null;
            PlayerOneTwos = null;
            PlayerOneThrees = null;
            PlayerOneFours = null;
            PlayerOneFives = null;
            PlayerOneSixes = null;
            PlayerOneThreeOfAKind = null;
            PlayerOneFourOfAKind = null;
            PlayerOneFullHouse = null;
            PlayerOneSmallStraight = null;
            PlayerOneLargeStraight = null;
            PlayerOneKnuffel = null;
            PlayerOneChance = null;

            // setting the Score values for player 2 to null
            PlayerTwoOnes = null;
            PlayerTwoTwos = null;
            PlayerTwoThrees = null;
            PlayerTwoFours = null;
            PlayerTwoFives = null;
            PlayerTwoSixes = null;
            PlayerTwoThreeOfAKind = null;
            PlayerTwoFourOfAKind = null;
            PlayerTwoFullHouse = null;
            PlayerTwoSmallStraight = null;
            PlayerTwoLargeStraight = null;
            PlayerTwoKnuffel = null;
            PlayerTwoChance = null;

            // setting the Score values for player 3 to null
            PlayerThreeOnes = null;
            PlayerThreeTwos = null;
            PlayerThreeThrees = null;
            PlayerThreeFours = null;
            PlayerThreeFives = null;
            PlayerThreeSixes = null;
            PlayerThreeThreeOfAKind = null;
            PlayerThreeFourOfAKind = null;
            PlayerThreeFullHouse = null;
            PlayerThreeSmallStraight = null;
            PlayerThreeLargeStraight = null;
            PlayerThreeKnuffel = null;
            PlayerThreeChance = null;

            // setting the Score values for player 4 to null
            PlayerFourOnes = null;
            PlayerFourTwos = null;
            PlayerFourThrees = null;
            PlayerFourFours = null;
            PlayerFourFives = null;
            PlayerFourSixes = null;
            PlayerFourThreeOfAKind = null;
            PlayerFourFourOfAKind = null;
            PlayerFourFullHouse = null;
            PlayerFourSmallStraight = null;
            PlayerFourLargeStraight = null;
            PlayerFourKnuffel = null;
            PlayerFourChance = null;

        }

        // in this method we are setting the visibility for the RollButton and the RollsLeftLabel to visible
        private void ShowRollButtonAndRollsLeftLabel()
        {
            IsRollButtonVisible = true;
            IsRollsLeftLabelVisible = true;
        }

        // in this method we are setting the visibility for the RollButton and the RollsLeftLabel to hidden
        private void HideRollButtonAndRollsLeftLabel()
        {
            IsRollButtonVisible = false;
            IsRollsLeftLabelVisible = false;
        }

        // in this method we are setting the visibility for the YourTurnLabel and the ActivePlayerLabel to visible
        private void ShowYourTurnLabelAndActivePlayerLabel()
        {
            IsYourTurnLabelVisible = true;
            IsActivePlayerLabelVisible = true;
        }

        // in this method we are setting the visibility for the YourTurnLabel and the ActivePlayerLabel to hidden
        private void HideYourTurnLabelAndActivePlayerLabel()
        {
            IsYourTurnLabelVisible = false;
            IsActivePlayerLabelVisible = false;
        }

        // in this method we are creating the players corresponding to the amount of players
        private void CreatePlayerNames()
        {
            if (AmountOfPlayers == 4)
            {
                playerOne.Name = Player1Name;
                playerTwo.Name = Player2Name;
                playerThree.Name = Player3Name;
                playerFour.Name = Player4Name;
            }
            else if (AmountOfPlayers == 3)
            {
                playerOne.Name = Player1Name;
                playerTwo.Name = Player2Name;
                playerThree.Name = Player3Name;
            }
            else if (AmountOfPlayers == 2)
            {
                playerOne.Name = Player1Name;
                playerTwo.Name = Player2Name;
            }
            else
            {
                playerOne.Name = Player1Name;
            }

            // calling the method where the UI elements for the Enter The Player Names Menu are hidden
            HideEnterPlayerNamesMenu();

            // calling the method where we are setting the ActivePlayerName to the actual active player
            SetActivePlayerName();
        }

        // in this method we are setting the ActivePlayerName to the actual active player
        private void SetActivePlayerName()
        {
            // setting the ActivePlayerName to the name of the player who is active
            switch (ActivePlayer)
            {
                case 1:
                    ActivePlayerName = playerOne.Name;
                    break;
                case 2:
                    ActivePlayerName = playerTwo.Name;
                    break;
                case 3:
                    ActivePlayerName = playerThree.Name;
                    break;
                case 4:
                    ActivePlayerName = playerFour.Name;
                    break;

            }
        }

        // in this method we roll all the dice wich are in the list

        private void RollingTheDice(List<Die> listOfDie)
        {
            Random random = new Random();
            foreach (Die die in listOfDie)
            {
                die.Roll(random);
            }
        }

        // in this method we set the images for the dice in relation to the value of the dice
        private string GetDieImage(int? value, bool locked)
        {
            switch (value)
            {

                case null:
                    if (locked)
                    {
                        return ravenDie;
                    }
                    else
                    {
                        return ravenDie;
                    }
                case 0:
                    if (locked)
                    {
                        return ravenDie;
                    }
                    else
                    {
                        return ravenDie;
                    }
                case 1:
                    if (locked)
                    {
                        return oneEyedDieGreen;
                    }
                    else
                    {
                        return oneEyedDie;
                    }
                case 2:
                    if (locked)
                    {
                        return twoEyedDieGreen;
                    }
                    else
                    {
                        return twoEyedDie;
                    }
                case 3:
                    if (locked)
                    {
                        return threeEyedDieGreen;
                    }
                    else
                    {
                        return threeEyedDie;
                    }
                case 4:
                    if (locked)
                    {
                        return fourEyedDieGreen;
                    }
                    else
                    {
                        return fourEyedDie;
                    }
                case 5:
                    if (locked)
                    {
                        return fiveEyedDieGreen;
                    }
                    else
                    {
                        return fiveEyedDie;
                    }
                case 6:
                    if (locked)
                    {
                        return sixEyedDieGreen;
                    }
                    else
                    {
                        return sixEyedDie;
                    }

                default:
                    return "";
            }

        }

        // in this method we are setting a switch case depending on the actualPlayer
        // where we declare the conditions for the SumUpper, SumLower Bonus, TotalUpper and FinalScore for each player
        private void SetAutoFillScores()
        {
            switch (ActivePlayer)
            {
                case 1:
                    PlayerOneBonus = PlayerOneSumUpper >= 63 ? 35 : 0;
                    PlayerOneTotalUpper = PlayerOneSumUpper + PlayerOneBonus;
                    PlayerOneFinalScore = PlayerOneTotalUpper + PlayerOneSumLower;
                    break;
                case 2:
                    PlayerTwoBonus = PlayerTwoSumUpper >= 63 ? 35 : 0;
                    PlayerTwoTotalUpper = PlayerTwoSumUpper + PlayerTwoBonus;
                    PlayerTwoFinalScore = PlayerTwoTotalUpper + PlayerTwoSumLower;
                    break;
                case 3:
                    PlayerThreeBonus = PlayerThreeSumUpper >= 63 ? 35 : 0;
                    PlayerThreeTotalUpper = PlayerThreeSumUpper + PlayerThreeBonus;
                    PlayerThreeFinalScore = PlayerThreeTotalUpper + PlayerThreeSumLower;
                    break;
                case 4:
                    PlayerFourBonus = PlayerFourSumUpper >= 63 ? 35 : 0;
                    PlayerFourTotalUpper = PlayerFourSumUpper + PlayerFourBonus;
                    PlayerFourFinalScore = PlayerFourTotalUpper + PlayerFourSumLower;
                    break;
            }
        }


        // in this method we are setting the value of each die to null and the locked value to false
        private void NullDieValues(List<Die> listOfDie)
        {
            foreach (Die die in listOfDie)
            {
                die.Value = null;
                die.Locked = false;
            }
            Die1Locked = false;
            Die2Locked = false;
            Die3Locked = false;
            Die4Locked = false;
            Die5Locked = false;
            Die1Image = GetDieImage(listOfDie[0].Value, listOfDie[0].Locked);
            Die2Image = GetDieImage(listOfDie[1].Value, listOfDie[1].Locked);
            Die3Image = GetDieImage(listOfDie[2].Value, listOfDie[2].Locked);
            Die4Image = GetDieImage(listOfDie[3].Value, listOfDie[3].Locked);
            Die5Image = GetDieImage(listOfDie[4].Value, listOfDie[4].Locked);

        }

        // in this method we are checking if the game advances to the next player
        private void NextPlayer()
        {
            // calling the method SetAutoFillScores
            SetAutoFillScores();
            // here we are checking if the active player is equal to the amount of players, and if there are still rounds to play
            if (ActivePlayer == AmountOfPlayers && RoundsLeft > 0)
            {
                // if the above is true, we are setting the active player to 1
                ActivePlayer = 1;
                // and we are decreasing the amount of rounds left by 1
                RoundsLeft--;
                // here we are setting the amount of rolls left to 3
                RollsLeft = 3;
            }
            // here we are checking if the active player is not equal to the amount of players, and if there are still rounds to play
            else if (ActivePlayer != AmountOfPlayers && RoundsLeft > 0)
            {
                // if the above is true, we are increasing the active player by 1
                ActivePlayer++;
                // here we are setting the amount of rolls left to 3
                RollsLeft = 3;
            };

            // here we check if there are no rounds left
            if (RoundsLeft == 0)
            {
                // if the above is true, we are calling the method GameFinished
                GameFinished();

            }
            else
            {
                // if the above is not true, we are setting the ActivePlayerName to the actual active player
                SetActivePlayerName();
                IsScoreButtonCommandAvailable();
                RollDiceCommand.RaiseCanExecuteChanged();
                LockDieCommand.RaiseCanExecuteChanged();

            }


        }

        // in this method we are checking if the ScoreButtonCommands can be executed
        private void IsScoreButtonCommandAvailable()
        {
            ScoreButtonOnesCommand.RaiseCanExecuteChanged();
            ScoreButtonTwosCommand.RaiseCanExecuteChanged();
            ScoreButtonThreesCommand.RaiseCanExecuteChanged();
            ScoreButtonFoursCommand.RaiseCanExecuteChanged();
            ScoreButtonFivesCommand.RaiseCanExecuteChanged();
            ScoreButtonSixesCommand.RaiseCanExecuteChanged();
            ScoreButtonThreeOfAKindCommand.RaiseCanExecuteChanged();
            ScoreButtonFourOfAKindCommand.RaiseCanExecuteChanged();
            ScoreButtonFullHouseCommand.RaiseCanExecuteChanged();
            ScoreButtonSmallStraightCommand.RaiseCanExecuteChanged();
            ScoreButtonLargeStraightCommand.RaiseCanExecuteChanged();
            ScoreButtonKnuffelCommand.RaiseCanExecuteChanged();
            ScoreButtonChanceCommand.RaiseCanExecuteChanged();
            ScoreButtonExtraKnuffelsCommand.RaiseCanExecuteChanged();
        }

        private void GameFinished()
        {


            // here we are calling the method SetFinalScore
            SetFinalScore();


            // here we are setting a varity of UI elements to hidden
            HideRollButtonAndRollsLeftLabel();
            HideYourTurnLabelAndActivePlayerLabel();
            IsScoreButtonsLabelVisible = false;
            IsScoreButtonsGridVisible = false;

            // here we are calling the method CheckForWinner
            CheckForWinner();
        }



        // in this method we are setting the FinalScore of each player depending on the amount of players
        private void SetFinalScore()
        {
            switch (AmountOfPlayers)
            {
                case 1:
                    playerOne.FinalScore = PlayerOneFinalScore;
                    break;
                case 2:
                    playerOne.FinalScore = PlayerOneFinalScore;
                    playerTwo.FinalScore = PlayerTwoFinalScore;
                    break;
                case 3:
                    playerOne.FinalScore = PlayerOneFinalScore;
                    playerTwo.FinalScore = PlayerTwoFinalScore;
                    playerThree.FinalScore = PlayerThreeFinalScore;
                    break;
                case 4:
                    playerOne.FinalScore = PlayerOneFinalScore;
                    playerTwo.FinalScore = PlayerTwoFinalScore;
                    playerThree.FinalScore = PlayerThreeFinalScore;
                    playerFour.FinalScore = PlayerFourFinalScore;
                    break;
            }
        }

        // in this method we are checking the name of the player whose FinalScore is the highest
        private void CheckForWinner()
        {
            // here we are creating a list of players
            List<Player> listOfPlayers = new List<Player>();

            // here we are adding the players to the list depending on the amount of players
            switch (AmountOfPlayers)
            {
                case 1:
                    listOfPlayers.Add(playerOne);
                    break;
                case 2:
                    listOfPlayers.Add(playerOne);
                    listOfPlayers.Add(playerTwo);
                    break;
                case 3:
                    listOfPlayers.Add(playerOne);
                    listOfPlayers.Add(playerTwo);
                    listOfPlayers.Add(playerThree);
                    break;
                case 4:
                    listOfPlayers.Add(playerOne);
                    listOfPlayers.Add(playerTwo);
                    listOfPlayers.Add(playerThree);
                    listOfPlayers.Add(playerFour);
                    break;
            }

            // here we are creating a variable for the highest score
            int? highestScore = 0;
            // here we are creating a variable for the winner
            string winnerOfRound = "";
            WinnerOfRound = winnerOfRound;

            // here we are looping through the list of players
            foreach (Player player in listOfPlayers)
            {
                // here we are checking if the FinalScore of the player is equal or higher than the highestScore
                if (player.FinalScore == highestScore)
                {
                    // if the FinalScore of the player is equal to the highestScore, we are setting the winner to "Draw"
                    WinnerOfRound = "It´s a Draw!";

                }
                else if (player.FinalScore > highestScore)
                {
                    // if the above is true, we are setting the highestScore to the FinalScore of the player
                    highestScore = player.FinalScore;
                    // and we are setting the winner to the name of the player
                    WinnerOfRound = player.Name.ToUpper();
                }
            }

            // here we are showing the winner of the round
            IsWinnerOfRoundLabelVisible = true;
            IsNameOfWinnerOfRoundLabelVisible = true;

            // here we are showing the NewGameButtonsMenu
            IsNewGameButtonsVisible = true;
        }


        // declaring the private variables for the path to the images of the dice
        private string _die1Image;
        private string _die2Image;
        private string _die3Image;
        private string _die4Image;
        private string _die5Image;

        // declaring the private variables for the Lock Status of each die
        private bool _die1Locked;
        private bool _die2Locked;
        private bool _die3Locked;
        private bool _die4Locked;
        private bool _die5Locked;

        // declaring the private variable for the amount of rolls left
        private int _rollsLeft;

        // declaring the private variable for the amount of rounds left
        private int _roundsLeft;

        // declaring the private variables for the winner of the round
        private string _winnerOfRound;

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

        // declaring the private variable for the amount of players
        private int _amountOfPlayers;

        // declaring the private variable for the active player
        private int _activePlayer;

        // declaring the private variable for the Name of the active player
        private string _activePlayerName;

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

        // declaring the private variables for the visibility of the ActivePlayerLabel and the YourTurnLabel
        private bool _isYourTurnLabelVisible;
        private bool _isActivePlayerLabelVisible;

        // declaring the private variables for the visibility of the ScoreButtons
        private bool _isScoreButtonsLabelVisible;
        private bool _isScoreButtonsGridVisible;

        // declaring the private variables for the visibility of the WinnerOfRoundLabels
        private bool _isWinnerOfRoundLabelVisible;
        private bool _isNameOfWinnerOfRoundLabelVisible;

        // declaring the private variable for the visibility of the NewGameButtonsMenu
        private bool _isNewGameButtonsVisible;

        // declaring the private nullable int variables for the Scoreboard Entries of Player 1
        private int? _playerOneOnes = null;
        private int? _playerOneTwos = null;
        private int? _playerOneThrees = null;
        private int? _playerOneFours = null;
        private int? _playerOneFives = null;
        private int? _playerOneSixes = null;
        private int? _playerOneSumUpper = null;
        private int? _playerOneBonus = null;
        private int? _playerOneTotalUpper = null;
        private int? _playerOneThreeOfAKind = null;
        private int? _playerOneFourOfAKind = null;
        private int? _playerOneFullHouse = null;
        private int? _playerOneSmallStraight = null;
        private int? _playerOneLargeStraight = null;
        private int? _playerOneKnuffel = null;
        private int? _playerOneChance = null;
        private int? _playerOneSumLower = null;
        private int? _playerOneExtraKnuffels = null;
        private int? _playerOneFinalScore = null;

        // declaring the private nullable int variables for the Scoreboard Entries of Player 2
        private int? _playerTwoOnes = null;
        private int? _playerTwoTwos = null;
        private int? _playerTwoThrees = null;
        private int? _playerTwoFours = null;
        private int? _playerTwoFives = null;
        private int? _playerTwoSixes = null;
        private int? _playerTwoSumUpper = null;
        private int? _playerTwoBonus = null;
        private int? _playerTwoTotalUpper = null;
        private int? _playerTwoThreeOfAKind = null;
        private int? _playerTwoFourOfAKind = null;
        private int? _playerTwoFullHouse = null;
        private int? _playerTwoSmallStraight = null;
        private int? _playerTwoLargeStraight = null;
        private int? _playerTwoKnuffel = null;
        private int? _playerTwoChance = null;
        private int? _playerTwoSumLower = null;
        private int? _playerTwoExtraKnuffels = null;
        private int? _playerTwoFinalScore = null;

        // declaring the private nullable int variables for the Scoreboard Entries of Player 3
        private int? _playerThreeOnes = null;
        private int? _playerThreeTwos = null;
        private int? _playerThreeThrees = null;
        private int? _playerThreeFours = null;
        private int? _playerThreeFives = null;
        private int? _playerThreeSixes = null;
        private int? _playerThreeSumUpper = null;
        private int? _playerThreeBonus = null;
        private int? _playerThreeTotalUpper = null;
        private int? _playerThreeThreeOfAKind = null;
        private int? _playerThreeFourOfAKind = null;
        private int? _playerThreeFullHouse = null;
        private int? _playerThreeSmallStraight = null;
        private int? _playerThreeLargeStraight = null;
        private int? _playerThreeKnuffel = null;
        private int? _playerThreeChance = null;
        private int? _playerThreeSumLower = null;
        private int? _playerThreeExtraKnuffels = null;
        private int? _playerThreeFinalScore = null;

        // declaring the private nullable int variables for the Scoreboard Entries of Player 4
        private int? _playerFourOnes = null;
        private int? _playerFourTwos = null;
        private int? _playerFourThrees = null;
        private int? _playerFourFours = null;
        private int? _playerFourFives = null;
        private int? _playerFourSixes = null;
        private int? _playerFourSumUpper = null;
        private int? _playerFourBonus = null;
        private int? _playerFourTotalUpper = null;
        private int? _playerFourThreeOfAKind = null;
        private int? _playerFourFourOfAKind = null;
        private int? _playerFourFullHouse = null;
        private int? _playerFourSmallStraight = null;
        private int? _playerFourLargeStraight = null;
        private int? _playerFourKnuffel = null;
        private int? _playerFourChance = null;
        private int? _playerFourSumLower = null;
        private int? _playerFourExtraKnuffels = null;
        private int? _playerFourFinalScore = null;
       




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


        // declaring the public properties for the LockStatus of each die
        public bool Die1Locked
        {
            get { return _die1Locked; }
            set
            {
                if (_die1Locked != value)
                {
                    _die1Locked = value;
                    OnPropertyChanged(nameof(Die1Locked));
                    OnPropertyChanged(nameof(Die1Image));
                }
            }
        }
        public bool Die2Locked
        {
            get { return _die2Locked; }
            set
            {
                if (_die2Locked != value)
                {
                    _die2Locked = value;
                    OnPropertyChanged(nameof(Die2Locked));
                    OnPropertyChanged(nameof(Die2Image));
                }
            }
        }
        public bool Die3Locked
        {
            get { return _die3Locked; }
            set
            {
                if (_die3Locked != value)
                {
                    _die3Locked = value;
                    OnPropertyChanged(nameof(Die3Locked));
                    OnPropertyChanged(nameof(Die3Image));
                }
            }
        }
        public bool Die4Locked
        {
            get { return _die4Locked; }
            set
            {
                if (_die4Locked != value)
                {
                    _die4Locked = value;
                    OnPropertyChanged(nameof(Die4Locked));
                    OnPropertyChanged(nameof(Die4Image));
                }
            }
        }
        public bool Die5Locked
        {
            get { return _die5Locked; }
            set
            {
                if (_die5Locked != value)
                {
                    _die5Locked = value;
                    OnPropertyChanged(nameof(Die5Locked));
                    OnPropertyChanged(nameof(Die5Image));
                }
            }
        }

        // declaring the public property for the amount of rolls left
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

        // declaring the public properties for the rounds left
        public int RoundsLeft
        {
            get { return _roundsLeft; }
            set
            {
                if (_roundsLeft != value)
                {
                    _roundsLeft = value;
                    OnPropertyChanged(nameof(RoundsLeft));
                }
            }
        }

        // declaring the public property for the winnerOfRound
        public string WinnerOfRound
        {
            get { return _winnerOfRound; }
            set
            {
                if (_winnerOfRound != value)
                {
                    _winnerOfRound = value;
                    OnPropertyChanged(nameof(WinnerOfRound));
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

        // declaring the public property ActivePlayer
        public int ActivePlayer
        {
            get { return _activePlayer; }
            set
            {
                if (_activePlayer != value)
                {
                    _activePlayer = value;
                    OnPropertyChanged(nameof(ActivePlayer));
                }
            }
        }

        // declaring the public property ActivePlayerName
        public string ActivePlayerName
        {
            get { return _activePlayerName; }
            set
            {
                if (_activePlayerName != value)
                {
                    _activePlayerName = value;
                    OnPropertyChanged(nameof(ActivePlayerName));
                }
            }
        }

        // declaring the public nullable int properties for the PlayerOne Scoreboard Section
        public int? PlayerOneOnes
        {
            get { return _playerOneOnes; }
            set
            {
                if (_playerOneOnes != value)
                {
                    _playerOneOnes = value;
                    OnPropertyChanged(nameof(PlayerOneOnes));
                    OnPropertyChanged(nameof(PlayerOneSumUpper));
                    OnPropertyChanged(nameof(PlayerOneBonus));
                    OnPropertyChanged(nameof(PlayerOneTotalUpper));
                    OnPropertyChanged(nameof(PlayerOneFinalScore));

                }
            }
        }
        public int? PlayerOneTwos
        {
            get { return _playerOneTwos; }
            set
            {
                if (_playerOneTwos != value)
                {
                    _playerOneTwos = value;
                    OnPropertyChanged(nameof(PlayerOneTwos));
                    OnPropertyChanged(nameof(PlayerOneSumUpper));
                    OnPropertyChanged(nameof(PlayerOneBonus));
                    OnPropertyChanged(nameof(PlayerOneTotalUpper));
                    OnPropertyChanged(nameof(PlayerOneFinalScore));
                }
            }
        }
        public int? PlayerOneThrees
        {
            get { return _playerOneThrees; }
            set
            {
                if (_playerOneThrees != value)
                {
                    _playerOneThrees = value;
                    OnPropertyChanged(nameof(PlayerOneThrees));
                    OnPropertyChanged(nameof(PlayerOneSumUpper));
                    OnPropertyChanged(nameof(PlayerOneBonus));
                    OnPropertyChanged(nameof(PlayerOneTotalUpper));
                    OnPropertyChanged(nameof(PlayerOneFinalScore));
                }
            }
        }
        public int? PlayerOneFours
        {
            get { return _playerOneFours; }
            set
            {
                if (_playerOneFours != value)
                {
                    _playerOneFours = value;
                    OnPropertyChanged(nameof(PlayerOneFours));
                    OnPropertyChanged(nameof(PlayerOneSumUpper));
                    OnPropertyChanged(nameof(PlayerOneBonus));
                    OnPropertyChanged(nameof(PlayerOneTotalUpper));
                    OnPropertyChanged(nameof(PlayerOneFinalScore));
                }
            }
        }
        public int? PlayerOneFives
        {
            get { return _playerOneFives; }
            set
            {
                if (_playerOneFives != value)
                {
                    _playerOneFives = value;
                    OnPropertyChanged(nameof(PlayerOneFives));
                    OnPropertyChanged(nameof(PlayerOneSumUpper));
                    OnPropertyChanged(nameof(PlayerOneBonus));
                    OnPropertyChanged(nameof(PlayerOneTotalUpper));
                    OnPropertyChanged(nameof(PlayerOneFinalScore));
                }
            }
        }
        public int? PlayerOneSixes
        {
            get { return _playerOneSixes; }
            set
            {
                if (_playerOneSixes != value)
                {
                    _playerOneSixes = value;
                    OnPropertyChanged(nameof(PlayerOneSixes));
                    OnPropertyChanged(nameof(PlayerOneSumUpper));
                    OnPropertyChanged(nameof(PlayerOneBonus));
                    OnPropertyChanged(nameof(PlayerOneTotalUpper));
                    OnPropertyChanged(nameof(PlayerOneFinalScore));
                }
            }
        }
        public int? PlayerOneSumUpper
        {
            get { return _playerOneSumUpper; }
            set
            {
                if (_playerOneSumUpper != value)
                {
                    _playerOneSumUpper = value;
                    OnPropertyChanged(nameof(PlayerOneSumUpper));
                    OnPropertyChanged(nameof(PlayerOneBonus));
                    OnPropertyChanged(nameof(PlayerOneTotalUpper));
                    OnPropertyChanged(nameof(PlayerOneFinalScore));
                }
            }
        }
        public int? PlayerOneBonus
        {
            get { return _playerOneBonus; }
            set
            {
                if (_playerOneBonus != value)
                {
                    _playerOneBonus = value;
                    OnPropertyChanged(nameof(PlayerOneBonus));
                    OnPropertyChanged(nameof(PlayerOneTotalUpper));
                    OnPropertyChanged(nameof(PlayerOneFinalScore));
                }
            }
        }
        public int? PlayerOneTotalUpper
        {
            get { return _playerOneTotalUpper; }
            set
            {
                if (_playerOneTotalUpper != value)
                {
                    _playerOneTotalUpper = value;
                    OnPropertyChanged(nameof(PlayerOneTotalUpper));
                    OnPropertyChanged(nameof(PlayerOneFinalScore));
                }
            }
        }
        public int? PlayerOneThreeOfAKind
        {
            get { return _playerOneThreeOfAKind; }
            set
            {
                if (_playerOneThreeOfAKind != value)
                {
                    _playerOneThreeOfAKind = value;
                    OnPropertyChanged(nameof(PlayerOneThreeOfAKind));
                    OnPropertyChanged(nameof(PlayerOneSumLower));
                    OnPropertyChanged(nameof(PlayerOneFinalScore));
                }
            }
        }
        public int? PlayerOneFourOfAKind
        {
            get { return _playerOneFourOfAKind; }
            set
            {
                if (_playerOneFourOfAKind != value)
                {
                    _playerOneFourOfAKind = value;
                    OnPropertyChanged(nameof(PlayerOneFourOfAKind));
                    OnPropertyChanged(nameof(PlayerOneSumLower));
                    OnPropertyChanged(nameof(PlayerOneFinalScore));
                }
            }
        }
        public int? PlayerOneFullHouse
        {
            get { return _playerOneFullHouse; }
            set
            {
                if (_playerOneFullHouse != value)
                {
                    _playerOneFullHouse = value;
                    OnPropertyChanged(nameof(PlayerOneFullHouse));
                    OnPropertyChanged(nameof(PlayerOneSumLower));
                    OnPropertyChanged(nameof(PlayerOneFinalScore));
                }
            }
        }
        public int? PlayerOneSmallStraight
        {
            get { return _playerOneSmallStraight; }
            set
            {
                if (_playerOneSmallStraight != value)
                {
                    _playerOneSmallStraight = value;
                    OnPropertyChanged(nameof(PlayerOneSmallStraight));
                    OnPropertyChanged(nameof(PlayerOneSumLower));
                    OnPropertyChanged(nameof(PlayerOneFinalScore));
                }
            }
        }
        public int? PlayerOneLargeStraight
        {
            get { return _playerOneLargeStraight; }
            set
            {
                if (_playerOneLargeStraight != value)
                {
                    _playerOneLargeStraight = value;
                    OnPropertyChanged(nameof(PlayerOneLargeStraight));
                    OnPropertyChanged(nameof(PlayerOneSumLower));
                    OnPropertyChanged(nameof(PlayerOneFinalScore));
                }
            }
        }
        public int? PlayerOneKnuffel
        {
            get { return _playerOneKnuffel; }
            set
            {
                if (_playerOneKnuffel != value)
                {
                    _playerOneKnuffel = value;
                    OnPropertyChanged(nameof(PlayerOneKnuffel));
                    OnPropertyChanged(nameof(PlayerOneSumLower));
                    OnPropertyChanged(nameof(PlayerOneFinalScore));
                }
            }
        }
        public int? PlayerOneChance
        {
            get { return _playerOneChance; }
            set
            {
                if (_playerOneChance != value)
                {
                    _playerOneChance = value;
                    OnPropertyChanged(nameof(PlayerOneChance));
                    OnPropertyChanged(nameof(PlayerOneSumLower));
                    OnPropertyChanged(nameof(PlayerOneFinalScore));
                }
            }
        }
        public int? PlayerOneSumLower
        {
            get { return _playerOneSumLower; }
            set
            {
                if (_playerOneSumLower != value)
                {
                    _playerOneSumLower = value;
                    OnPropertyChanged(nameof(PlayerOneSumLower));
                    OnPropertyChanged(nameof(PlayerOneFinalScore));
                }
            }
        }
        public int? PlayerOneExtraKnuffels
        {
            get { return _playerOneExtraKnuffels; }
            set
            {
                if (_playerOneExtraKnuffels != value)
                {
                    _playerOneExtraKnuffels = value;
                    OnPropertyChanged(nameof(PlayerOneExtraKnuffels));
                    OnPropertyChanged(nameof(PlayerOneSumLower));
                    OnPropertyChanged(nameof(PlayerOneFinalScore));
                }
            }
        }
        public int? PlayerOneFinalScore
        {
            get { return _playerOneFinalScore; }
            set
            {
                if (_playerOneFinalScore != value)
                {
                    _playerOneFinalScore = value;
                    OnPropertyChanged(nameof(PlayerOneFinalScore));
                }
            }
        }

        // declaring the public properties for the PlayerTwo Scoreboard Section
        public int? PlayerTwoOnes
        {
            get { return _playerTwoOnes; }
            set
            {
                if (_playerTwoOnes != value)
                {
                    _playerTwoOnes = value;
                    OnPropertyChanged(nameof(PlayerTwoOnes));
                    OnPropertyChanged(nameof(PlayerTwoSumUpper));
                    OnPropertyChanged(nameof(PlayerTwoBonus));
                    OnPropertyChanged(nameof(PlayerTwoTotalUpper));
                    OnPropertyChanged(nameof(PlayerTwoFinalScore));
                }
            }
        }
        public int? PlayerTwoTwos
        {
            get { return _playerTwoTwos; }
            set
            {
                if (_playerTwoTwos != value)
                {
                    _playerTwoTwos = value;
                    OnPropertyChanged(nameof(PlayerTwoTwos));
                    OnPropertyChanged(nameof(PlayerTwoSumUpper));
                    OnPropertyChanged(nameof(PlayerTwoBonus));
                    OnPropertyChanged(nameof(PlayerTwoTotalUpper));
                    OnPropertyChanged(nameof(PlayerTwoFinalScore));
                }
            }
        }
        public int? PlayerTwoThrees
        {
            get { return _playerTwoThrees; }
            set
            {
                if (_playerTwoThrees != value)
                {
                    _playerTwoThrees = value;
                    OnPropertyChanged(nameof(PlayerTwoThrees));
                    OnPropertyChanged(nameof(PlayerTwoSumUpper));
                    OnPropertyChanged(nameof(PlayerTwoBonus));
                    OnPropertyChanged(nameof(PlayerTwoTotalUpper));
                    OnPropertyChanged(nameof(PlayerTwoFinalScore));
                }
            }
        }
        public int? PlayerTwoFours
        {
            get { return _playerTwoFours; }
            set
            {
                if (_playerTwoFours != value)
                {
                    _playerTwoFours = value;
                    OnPropertyChanged(nameof(PlayerTwoFours));
                    OnPropertyChanged(nameof(PlayerTwoSumUpper));
                    OnPropertyChanged(nameof(PlayerTwoBonus));
                    OnPropertyChanged(nameof(PlayerTwoTotalUpper));
                    OnPropertyChanged(nameof(PlayerTwoFinalScore));
                }
            }
        }
        public int? PlayerTwoFives
        {
            get { return _playerTwoFives; }
            set
            {
                if (_playerTwoFives != value)
                {
                    _playerTwoFives = value;
                    OnPropertyChanged(nameof(PlayerTwoFives));
                    OnPropertyChanged(nameof(PlayerTwoSumUpper));
                    OnPropertyChanged(nameof(PlayerTwoBonus));
                    OnPropertyChanged(nameof(PlayerTwoTotalUpper));
                    OnPropertyChanged(nameof(PlayerTwoFinalScore));
                }
            }
        }
        public int? PlayerTwoSixes
        {
            get { return _playerTwoSixes; }
            set
            {
                if (_playerTwoSixes != value)
                {
                    _playerTwoSixes = value;
                    OnPropertyChanged(nameof(PlayerTwoSixes));
                    OnPropertyChanged(nameof(PlayerTwoSumUpper));
                    OnPropertyChanged(nameof(PlayerTwoBonus));
                    OnPropertyChanged(nameof(PlayerTwoTotalUpper));
                    OnPropertyChanged(nameof(PlayerTwoFinalScore));
                }
            }
        }
        public int? PlayerTwoSumUpper
        {
            get { return _playerTwoSumUpper; }
            set
            {
                if (_playerTwoSumUpper != value)
                {
                    _playerTwoSumUpper = value;
                    OnPropertyChanged(nameof(PlayerTwoSumUpper));
                    OnPropertyChanged(nameof(PlayerTwoBonus));
                    OnPropertyChanged(nameof(PlayerTwoTotalUpper));
                    OnPropertyChanged(nameof(PlayerTwoFinalScore));
                }
            }
        }
        public int? PlayerTwoBonus
        {
            get { return _playerTwoBonus; }
            set
            {
                if (_playerTwoBonus != value)
                {
                    _playerTwoBonus = value;
                    OnPropertyChanged(nameof(PlayerTwoBonus));
                    OnPropertyChanged(nameof(PlayerTwoTotalUpper));
                    OnPropertyChanged(nameof(PlayerTwoFinalScore));
                }
            }
        }
        public int? PlayerTwoTotalUpper
        {
            get { return _playerTwoTotalUpper; }
            set
            {
                if (_playerTwoTotalUpper != value)
                {
                    _playerTwoTotalUpper = value;
                    OnPropertyChanged(nameof(PlayerTwoTotalUpper));
                    OnPropertyChanged(nameof(PlayerTwoFinalScore));
                }
            }
        }
        public int? PlayerTwoThreeOfAKind
        {
            get { return _playerTwoThreeOfAKind; }
            set
            {
                if (_playerTwoThreeOfAKind != value)
                {
                    _playerTwoThreeOfAKind = value;
                    OnPropertyChanged(nameof(PlayerTwoThreeOfAKind));
                    OnPropertyChanged(nameof(PlayerTwoSumLower));
                    OnPropertyChanged(nameof(PlayerTwoFinalScore));
                }
            }
        }
        public int? PlayerTwoFourOfAKind
        {
            get { return _playerTwoFourOfAKind; }
            set
            {
                if (_playerTwoFourOfAKind != value)
                {
                    _playerTwoFourOfAKind = value;
                    OnPropertyChanged(nameof(PlayerTwoFourOfAKind));
                    OnPropertyChanged(nameof(PlayerTwoSumLower));
                    OnPropertyChanged(nameof(PlayerTwoFinalScore));
                }
            }
        }
        public int? PlayerTwoFullHouse
        {
            get { return _playerTwoFullHouse; }
            set
            {
                if (_playerTwoFullHouse != value)
                {
                    _playerTwoFullHouse = value;
                    OnPropertyChanged(nameof(PlayerTwoFullHouse));
                    OnPropertyChanged(nameof(PlayerTwoSumLower));
                    OnPropertyChanged(nameof(PlayerTwoFinalScore));
                }
            }
        }
        public int? PlayerTwoSmallStraight
        {
            get { return _playerTwoSmallStraight; }
            set
            {
                if (_playerTwoSmallStraight != value)
                {
                    _playerTwoSmallStraight = value;
                    OnPropertyChanged(nameof(PlayerTwoSmallStraight));
                    OnPropertyChanged(nameof(PlayerTwoSumLower));
                    OnPropertyChanged(nameof(PlayerTwoFinalScore));
                }
            }
        }
        public int? PlayerTwoLargeStraight
        {
            get { return _playerTwoLargeStraight; }
            set
            {
                if (_playerTwoLargeStraight != value)
                {
                    _playerTwoLargeStraight = value;
                    OnPropertyChanged(nameof(PlayerTwoLargeStraight));
                    OnPropertyChanged(nameof(PlayerTwoSumLower));
                    OnPropertyChanged(nameof(PlayerTwoFinalScore));
                }
            }
        }
        public int? PlayerTwoKnuffel
        {
            get { return _playerTwoKnuffel; }
            set
            {
                if (_playerTwoKnuffel != value)
                {
                    _playerTwoKnuffel = value;
                    OnPropertyChanged(nameof(PlayerTwoKnuffel));
                    OnPropertyChanged(nameof(PlayerTwoSumLower));
                    OnPropertyChanged(nameof(PlayerTwoFinalScore));
                }
            }
        }
        public int? PlayerTwoChance
        {
            get { return _playerTwoChance; }
            set
            {
                if (_playerTwoChance != value)
                {
                    _playerTwoChance = value;
                    OnPropertyChanged(nameof(PlayerTwoChance));
                    OnPropertyChanged(nameof(PlayerTwoSumLower));
                    OnPropertyChanged(nameof(PlayerTwoFinalScore));
                }
            }
        }
        public int? PlayerTwoSumLower
        {
            get { return _playerTwoSumLower; }
            set
            {
                if (_playerTwoSumLower != value)
                {
                    _playerTwoSumLower = value;
                    OnPropertyChanged(nameof(PlayerTwoSumLower));
                    OnPropertyChanged(nameof(PlayerTwoFinalScore));
                }
            }
        }
        public int? PlayerTwoExtraKnuffels
        {
            get { return _playerTwoExtraKnuffels; }
            set
            {
                if (_playerTwoExtraKnuffels != value)
                {
                    _playerTwoExtraKnuffels = value;
                    OnPropertyChanged(nameof(PlayerTwoExtraKnuffels));
                    OnPropertyChanged(nameof(PlayerTwoSumLower));
                    OnPropertyChanged(nameof(PlayerTwoFinalScore));
                }
            }
        }
        public int? PlayerTwoFinalScore
        {
            get { return _playerTwoFinalScore; }
            set
            {
                if (_playerTwoFinalScore != value)
                {
                    _playerTwoFinalScore = value;
                    OnPropertyChanged(nameof(PlayerTwoFinalScore));
                }
            }
        }

        // declaring the public properties for the PlayerThree Scoreboard Section
        public int? PlayerThreeOnes
        {
            get { return _playerThreeOnes; }
            set
            {
                if (_playerThreeOnes != value)
                {
                    _playerThreeOnes = value;
                    OnPropertyChanged(nameof(PlayerThreeOnes));
                    OnPropertyChanged(nameof(PlayerThreeSumUpper));
                    OnPropertyChanged(nameof(PlayerThreeBonus));
                    OnPropertyChanged(nameof(PlayerThreeTotalUpper));
                    OnPropertyChanged(nameof(PlayerThreeFinalScore));
                }
            }
        }
        public int? PlayerThreeTwos
        {
            get { return _playerThreeTwos; }
            set
            {
                if (_playerThreeTwos != value)
                {
                    _playerThreeTwos = value;
                    OnPropertyChanged(nameof(PlayerThreeTwos));
                    OnPropertyChanged(nameof(PlayerThreeSumUpper));
                    OnPropertyChanged(nameof(PlayerThreeBonus));
                    OnPropertyChanged(nameof(PlayerThreeTotalUpper));
                    OnPropertyChanged(nameof(PlayerThreeFinalScore));
                }
            }
        }
        public int? PlayerThreeThrees
        {
            get { return _playerThreeThrees; }
            set
            {
                if (_playerThreeThrees != value)
                {
                    _playerThreeThrees = value;
                    OnPropertyChanged(nameof(PlayerThreeThrees));
                    OnPropertyChanged(nameof(PlayerThreeSumUpper));
                    OnPropertyChanged(nameof(PlayerThreeBonus));
                    OnPropertyChanged(nameof(PlayerThreeTotalUpper));
                    OnPropertyChanged(nameof(PlayerThreeFinalScore));
                }
            }
        }
        public int? PlayerThreeFours
        {
            get { return _playerThreeFours; }
            set
            {
                if (_playerThreeFours != value)
                {
                    _playerThreeFours = value;
                    OnPropertyChanged(nameof(PlayerThreeFours));
                    OnPropertyChanged(nameof(PlayerThreeSumUpper));
                    OnPropertyChanged(nameof(PlayerThreeBonus));
                    OnPropertyChanged(nameof(PlayerThreeTotalUpper));
                    OnPropertyChanged(nameof(PlayerThreeFinalScore));
                }
            }
        }
        public int? PlayerThreeFives
        {
            get { return _playerThreeFives; }
            set
            {
                if (_playerThreeFives != value)
                {
                    _playerThreeFives = value;
                    OnPropertyChanged(nameof(PlayerThreeFives));
                    OnPropertyChanged(nameof(PlayerThreeSumUpper));
                    OnPropertyChanged(nameof(PlayerThreeBonus));
                    OnPropertyChanged(nameof(PlayerThreeTotalUpper));
                    OnPropertyChanged(nameof(PlayerThreeFinalScore));
                }
            }
        }
        public int? PlayerThreeSixes
        {
            get { return _playerThreeSixes; }
            set
            {
                if (_playerThreeSixes != value)
                {
                    _playerThreeSixes = value;
                    OnPropertyChanged(nameof(PlayerThreeSixes));
                    OnPropertyChanged(nameof(PlayerThreeSumUpper));
                    OnPropertyChanged(nameof(PlayerThreeBonus));
                    OnPropertyChanged(nameof(PlayerThreeTotalUpper));
                    OnPropertyChanged(nameof(PlayerThreeFinalScore));
                }
            }
        }
        public int? PlayerThreeSumUpper
        {
            get { return _playerThreeSumUpper; }
            set
            {
                if (_playerThreeSumUpper != value)
                {
                    _playerThreeSumUpper = value;
                    OnPropertyChanged(nameof(PlayerThreeSumUpper));
                    OnPropertyChanged(nameof(PlayerThreeBonus));
                    OnPropertyChanged(nameof(PlayerThreeTotalUpper));
                    OnPropertyChanged(nameof(PlayerThreeFinalScore));
                }
            }
        }
        public int? PlayerThreeBonus
        {
            get { return _playerThreeBonus; }
            set
            {
                if (_playerThreeBonus != value)
                {
                    _playerThreeBonus = value;
                    OnPropertyChanged(nameof(PlayerThreeBonus));
                    OnPropertyChanged(nameof(PlayerThreeTotalUpper));
                    OnPropertyChanged(nameof(PlayerThreeFinalScore));
                }
            }
        }
        public int? PlayerThreeTotalUpper
        {
            get { return _playerThreeTotalUpper; }
            set
            {
                if (_playerThreeTotalUpper != value)
                {
                    _playerThreeTotalUpper = value;
                    OnPropertyChanged(nameof(PlayerThreeTotalUpper));
                    OnPropertyChanged(nameof(PlayerThreeFinalScore));
                }
            }
        }
        public int? PlayerThreeThreeOfAKind
        {
            get { return _playerThreeThreeOfAKind; }
            set
            {
                if (_playerThreeThreeOfAKind != value)
                {
                    _playerThreeThreeOfAKind = value;
                    OnPropertyChanged(nameof(PlayerThreeThreeOfAKind));
                    OnPropertyChanged(nameof(PlayerThreeSumLower));
                    OnPropertyChanged(nameof(PlayerThreeFinalScore));
                }
            }
        }
        public int? PlayerThreeFourOfAKind
        {
            get { return _playerThreeFourOfAKind; }
            set
            {
                if (_playerThreeFourOfAKind != value)
                {
                    _playerThreeFourOfAKind = value;
                    OnPropertyChanged(nameof(PlayerThreeFourOfAKind));
                    OnPropertyChanged(nameof(PlayerThreeSumLower));
                    OnPropertyChanged(nameof(PlayerThreeFinalScore));
                }
            }
        }
        public int? PlayerThreeFullHouse
        {
            get { return _playerThreeFullHouse; }
            set
            {
                if (_playerThreeFullHouse != value)
                {
                    _playerThreeFullHouse = value;
                    OnPropertyChanged(nameof(PlayerThreeFullHouse));
                    OnPropertyChanged(nameof(PlayerThreeSumLower));
                    OnPropertyChanged(nameof(PlayerThreeFinalScore));
                }
            }
        }
        public int? PlayerThreeSmallStraight
        {
            get { return _playerThreeSmallStraight; }
            set
            {
                if (_playerThreeSmallStraight != value)
                {
                    _playerThreeSmallStraight = value;
                    OnPropertyChanged(nameof(PlayerThreeSmallStraight));
                    OnPropertyChanged(nameof(PlayerThreeSumLower));
                    OnPropertyChanged(nameof(PlayerThreeFinalScore));
                }
            }
        }
        public int? PlayerThreeLargeStraight
        {
            get { return _playerThreeLargeStraight; }
            set
            {
                if (_playerThreeLargeStraight != value)
                {
                    _playerThreeLargeStraight = value;
                    OnPropertyChanged(nameof(PlayerThreeLargeStraight));
                    OnPropertyChanged(nameof(PlayerThreeSumLower));
                    OnPropertyChanged(nameof(PlayerThreeFinalScore));
                }
            }
        }
        public int? PlayerThreeKnuffel
        {
            get { return _playerThreeKnuffel; }
            set
            {
                if (_playerThreeKnuffel != value)
                {
                    _playerThreeKnuffel = value;
                    OnPropertyChanged(nameof(PlayerThreeKnuffel));
                    OnPropertyChanged(nameof(PlayerThreeSumLower));
                    OnPropertyChanged(nameof(PlayerThreeFinalScore));
                }
            }
        }
        public int? PlayerThreeChance
        {
            get { return _playerThreeChance; }
            set
            {
                if (_playerThreeChance != value)
                {
                    _playerThreeChance = value;
                    OnPropertyChanged(nameof(PlayerThreeChance));
                    OnPropertyChanged(nameof(PlayerThreeSumLower));
                    OnPropertyChanged(nameof(PlayerThreeFinalScore));
                }
            }
        }
        public int? PlayerThreeSumLower
        {
            get { return _playerThreeSumLower; }
            set
            {
                if (_playerThreeSumLower != value)
                {
                    _playerThreeSumLower = value;
                    OnPropertyChanged(nameof(PlayerThreeSumLower));
                    OnPropertyChanged(nameof(PlayerThreeFinalScore));
                }
            }
        }
        public int? PlayerThreeExtraKnuffels
        {
            get { return _playerThreeExtraKnuffels; }
            set
            {
                if (_playerThreeExtraKnuffels != value)
                {
                    _playerThreeExtraKnuffels = value;
                    OnPropertyChanged(nameof(PlayerThreeExtraKnuffels));
                    OnPropertyChanged(nameof(PlayerThreeSumLower));
                    OnPropertyChanged(nameof(PlayerThreeFinalScore));
                }
            }
        }
        public int? PlayerThreeFinalScore
        {
            get { return _playerThreeFinalScore; }
            set
            {
                if (_playerThreeFinalScore != value)
                {
                    _playerThreeFinalScore = value;
                    OnPropertyChanged(nameof(PlayerThreeFinalScore));
                }
            }
        }

        // declaring the public properties for the PlayerFour Scoreboard Section
        public int? PlayerFourOnes
        {
            get { return _playerFourOnes; }
            set
            {
                if (_playerFourOnes != value)
                {
                    _playerFourOnes = value;
                    OnPropertyChanged(nameof(PlayerFourOnes));
                    OnPropertyChanged(nameof(PlayerFourSumUpper));
                    OnPropertyChanged(nameof(PlayerFourBonus));
                    OnPropertyChanged(nameof(PlayerFourTotalUpper));
                    OnPropertyChanged(nameof(PlayerFourFinalScore));
                }
            }
        }
        public int? PlayerFourTwos
        {
            get { return _playerFourTwos; }
            set
            {
                if (_playerFourTwos != value)
                {
                    _playerFourTwos = value;
                    OnPropertyChanged(nameof(PlayerFourTwos));
                    OnPropertyChanged(nameof(PlayerFourSumUpper));
                    OnPropertyChanged(nameof(PlayerFourBonus));
                    OnPropertyChanged(nameof(PlayerFourTotalUpper));
                    OnPropertyChanged(nameof(PlayerFourFinalScore));
                }
            }
        }
        public int? PlayerFourThrees
        {
            get { return _playerFourThrees; }
            set
            {
                if (_playerFourThrees != value)
                {
                    _playerFourThrees = value;
                    OnPropertyChanged(nameof(PlayerFourThrees));
                    OnPropertyChanged(nameof(PlayerFourSumUpper));
                    OnPropertyChanged(nameof(PlayerFourBonus));
                    OnPropertyChanged(nameof(PlayerFourTotalUpper));
                    OnPropertyChanged(nameof(PlayerFourFinalScore));
                }
            }
        }
        public int? PlayerFourFours
        {
            get { return _playerFourFours; }
            set
            {
                if (_playerFourFours != value)
                {
                    _playerFourFours = value;
                    OnPropertyChanged(nameof(PlayerFourFours));
                    OnPropertyChanged(nameof(PlayerFourSumUpper));
                    OnPropertyChanged(nameof(PlayerFourBonus));
                    OnPropertyChanged(nameof(PlayerFourTotalUpper));
                    OnPropertyChanged(nameof(PlayerFourFinalScore));
                }
            }
        }
        public int? PlayerFourFives
        {
            get { return _playerFourFives; }
            set
            {
                if (_playerFourFives != value)
                {
                    _playerFourFives = value;
                    OnPropertyChanged(nameof(PlayerFourFives));
                    OnPropertyChanged(nameof(PlayerFourSumUpper));
                    OnPropertyChanged(nameof(PlayerFourBonus));
                    OnPropertyChanged(nameof(PlayerFourTotalUpper));
                    OnPropertyChanged(nameof(PlayerFourFinalScore));
                }
            }
        }
        public int? PlayerFourSixes
        {
            get { return _playerFourSixes; }
            set
            {
                if (_playerFourSixes != value)
                {
                    _playerFourSixes = value;
                    OnPropertyChanged(nameof(PlayerFourSixes));
                    OnPropertyChanged(nameof(PlayerFourSumUpper));
                    OnPropertyChanged(nameof(PlayerFourBonus));
                    OnPropertyChanged(nameof(PlayerFourTotalUpper));
                    OnPropertyChanged(nameof(PlayerFourFinalScore));
                }
            }
        }
        public int? PlayerFourSumUpper
        {
            get { return _playerFourSumUpper; }
            set
            {
                if (_playerFourSumUpper != value)
                {
                    _playerFourSumUpper = value;
                    OnPropertyChanged(nameof(PlayerFourSumUpper));
                    OnPropertyChanged(nameof(PlayerFourBonus));
                    OnPropertyChanged(nameof(PlayerFourTotalUpper));
                    OnPropertyChanged(nameof(PlayerFourFinalScore));
                }
            }
        }
        public int? PlayerFourBonus
        {
            get { return _playerFourBonus; }
            set
            {
                if (_playerFourBonus != value)
                {
                    _playerFourBonus = value;
                    OnPropertyChanged(nameof(PlayerFourBonus));
                    OnPropertyChanged(nameof(PlayerFourTotalUpper));
                    OnPropertyChanged(nameof(PlayerFourFinalScore));
                }
            }
        }
        public int? PlayerFourTotalUpper
        {
            get { return _playerFourTotalUpper; }
            set
            {
                if (_playerFourTotalUpper != value)
                {
                    _playerFourTotalUpper = value;
                    OnPropertyChanged(nameof(PlayerFourTotalUpper));
                    OnPropertyChanged(nameof(PlayerFourFinalScore));
                }
            }
        }
        public int? PlayerFourThreeOfAKind
        {
            get { return _playerFourThreeOfAKind; }
            set
            {
                if (_playerFourThreeOfAKind != value)
                {
                    _playerFourThreeOfAKind = value;
                    OnPropertyChanged(nameof(PlayerFourThreeOfAKind));
                    OnPropertyChanged(nameof(PlayerFourSumLower));
                    OnPropertyChanged(nameof(PlayerFourFinalScore));
                }
            }
        }
        public int? PlayerFourFourOfAKind
        {
            get { return _playerFourFourOfAKind; }
            set
            {
                if (_playerFourFourOfAKind != value)
                {
                    _playerFourFourOfAKind = value;
                    OnPropertyChanged(nameof(PlayerFourFourOfAKind));
                    OnPropertyChanged(nameof(PlayerFourSumLower));
                    OnPropertyChanged(nameof(PlayerFourFinalScore));
                }
            }
        }
        public int? PlayerFourFullHouse
        {
            get { return _playerFourFullHouse; }
            set
            {
                if (_playerFourFullHouse != value)
                {
                    _playerFourFullHouse = value;
                    OnPropertyChanged(nameof(PlayerFourFullHouse));
                    OnPropertyChanged(nameof(PlayerFourSumLower));
                    OnPropertyChanged(nameof(PlayerFourFinalScore));
                }
            }
        }
        public int? PlayerFourSmallStraight
        {
            get { return _playerFourSmallStraight; }
            set
            {
                if (_playerFourSmallStraight != value)
                {
                    _playerFourSmallStraight = value;
                    OnPropertyChanged(nameof(PlayerFourSmallStraight));
                    OnPropertyChanged(nameof(PlayerFourSumLower));
                    OnPropertyChanged(nameof(PlayerFourFinalScore));
                }
            }
        }
        public int? PlayerFourLargeStraight
        {
            get { return _playerFourLargeStraight; }
            set
            {
                if (_playerFourLargeStraight != value)
                {
                    _playerFourLargeStraight = value;
                    OnPropertyChanged(nameof(PlayerFourLargeStraight));
                    OnPropertyChanged(nameof(PlayerFourSumLower));
                    OnPropertyChanged(nameof(PlayerFourFinalScore));
                }
            }
        }
        public int? PlayerFourKnuffel
        {
            get { return _playerFourKnuffel; }
            set
            {
                if (_playerFourKnuffel != value)
                {
                    _playerFourKnuffel = value;
                    OnPropertyChanged(nameof(PlayerFourKnuffel));
                    OnPropertyChanged(nameof(PlayerFourSumLower));
                    OnPropertyChanged(nameof(PlayerFourFinalScore));
                }
            }
        }
        public int? PlayerFourChance
        {
            get { return _playerFourChance; }
            set
            {
                if (_playerFourChance != value)
                {
                    _playerFourChance = value;
                    OnPropertyChanged(nameof(PlayerFourChance));
                    OnPropertyChanged(nameof(PlayerFourSumLower));
                    OnPropertyChanged(nameof(PlayerFourFinalScore));
                }
            }
        }
        public int? PlayerFourSumLower
        {
            get { return _playerFourSumLower; }
            set
            {
                if (_playerFourSumLower != value)
                {
                    _playerFourSumLower = value;
                    OnPropertyChanged(nameof(PlayerFourSumLower));
                    OnPropertyChanged(nameof(PlayerFourFinalScore));
                }
            }
        }
        public int? PlayerFourExtraKnuffels
        {
            get { return _playerFourExtraKnuffels; }
            set
            {
                if (_playerFourExtraKnuffels != value)
                {
                    _playerFourExtraKnuffels = value;
                    OnPropertyChanged(nameof(PlayerFourExtraKnuffels));
                    OnPropertyChanged(nameof(PlayerFourSumLower));
                    OnPropertyChanged(nameof(PlayerFourFinalScore));
                }
            }
        }
        public int? PlayerFourFinalScore
        {
            get { return _playerFourFinalScore; }
            set
            {
                if (_playerFourFinalScore != value)
                {
                    _playerFourFinalScore = value;
                    OnPropertyChanged(nameof(PlayerFourFinalScore));
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

        // declaring the public properties for the visibility of the Labels for the active Player
        public bool IsYourTurnLabelVisible
        {
            get { return _isYourTurnLabelVisible; }
            set
            {
                if (_isYourTurnLabelVisible != value)
                {
                    _isYourTurnLabelVisible = value;
                    OnPropertyChanged(nameof(IsYourTurnLabelVisible));
                }
            }
        }
        public bool IsActivePlayerLabelVisible
        {
            get { return _isActivePlayerLabelVisible; }
            set
            {
                if (_isActivePlayerLabelVisible != value)
                {
                    _isActivePlayerLabelVisible = value;
                    OnPropertyChanged(nameof(IsActivePlayerLabelVisible));
                }
            }
        }
        // declaring the public properties for the visibility of the Score-Buttons
        public bool IsScoreButtonsLabelVisible
        {
            get { return _isScoreButtonsLabelVisible; }
            set
            {
                if (_isScoreButtonsLabelVisible != value)
                {
                    _isScoreButtonsLabelVisible = value;
                    OnPropertyChanged(nameof(IsScoreButtonsLabelVisible));
                }
            }
        }
        public bool IsScoreButtonsGridVisible
        {
            get { return _isScoreButtonsGridVisible; }
            set
            {
                if (_isScoreButtonsGridVisible != value)
                {
                    _isScoreButtonsGridVisible = value;
                    OnPropertyChanged(nameof(IsScoreButtonsGridVisible));
                }
            }
        }

        // declaring the public properties for the visibility of WinnerOfRoundLabels
        public bool IsWinnerOfRoundLabelVisible
        {
            get { return _isWinnerOfRoundLabelVisible; }
            set
            {
                if (_isWinnerOfRoundLabelVisible != value)
                {
                    _isWinnerOfRoundLabelVisible = value;
                    OnPropertyChanged(nameof(IsWinnerOfRoundLabelVisible));
                }
            }
        }
        public bool IsNameOfWinnerOfRoundLabelVisible
        {
            get { return _isNameOfWinnerOfRoundLabelVisible; }
            set
            {
                if (_isNameOfWinnerOfRoundLabelVisible != value)
                {
                    _isNameOfWinnerOfRoundLabelVisible = value;
                    OnPropertyChanged(nameof(IsNameOfWinnerOfRoundLabelVisible));
                }
            }
        }

        // declaring the public properties for the visibility of the NewGameButtonsMenu
        public bool IsNewGameButtonsVisible
        {
            get { return _isNewGameButtonsVisible; }
            set
            {
                if (_isNewGameButtonsVisible != value)
                {
                    _isNewGameButtonsVisible = value;
                    OnPropertyChanged(nameof(IsNewGameButtonsVisible));
                }
            }
        }

    }

}
