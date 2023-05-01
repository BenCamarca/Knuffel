using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Knuffel.Classes
{
    public class ViewModel : INotifyPropertyChanged
    {
        
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



        // declaring the event
        public event PropertyChangedEventHandler PropertyChanged;

        // declaring the method for the event
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
