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



        // declaring the event
        public event PropertyChangedEventHandler PropertyChanged;

        // declaring the method for the event
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
