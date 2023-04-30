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

namespace Knuffel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Die1Image { get; set; }
        public string Die2Image { get; set; }
        public string Die3Image { get; set; }
        public string Die4Image { get; set; }
        public string Die5Image { get; set; }



        public MainWindow()
        {
            InitializeComponent();

            // declaring the images for the die
            string oneEyedDie = "pack://application:,,,/Images/1side.png";
            string twoEyedDie = "pack://application:,,,/Images/2side.png";
            string threeEyedDie = "pack://application:,,,/Images/3side.png";
            string fourEyedDie = "pack://application:,,,/Images/4side.png";
            string fiveEyedDie = "pack://application:,,,/Images/5side.png";
            string sixEyedDie = "pack://application:,,,/Images/6side.png";

            // setting the default images for the dice
            Die1Image = oneEyedDie; 
            DataContext = this;
            Die2Image = twoEyedDie;
            DataContext = this;
            Die3Image = threeEyedDie;
            DataContext = this;
            Die4Image = fourEyedDie;
            DataContext = this;
            Die5Image = fiveEyedDie;
            DataContext = this;

        }
    }
}
