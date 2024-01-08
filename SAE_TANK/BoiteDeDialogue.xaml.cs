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
using System.Windows.Shapes;

namespace SAE_TANK
{
    /// <summary>
    /// Logique d'interaction pour BoiteDeDialogue.xaml
    /// </summary>
    public partial class BoiteDeDialogue : Window
    {
        public BoiteDeDialogue()
        {
            InitializeComponent();
            ImageBrush background = new ImageBrush();
            background.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Menu.jpg"));
            backgroundMenu.Fill = background;
            Tank_Bleu_1.Background = background;
        }
    }
}
