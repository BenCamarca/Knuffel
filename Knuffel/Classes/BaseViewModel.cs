using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Knuffel.Classes
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        // declaring the event
        public event PropertyChangedEventHandler PropertyChanged;

        // declaring the method for the event
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (!string.IsNullOrEmpty(propertyName))
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            
            
        }
    }
}
