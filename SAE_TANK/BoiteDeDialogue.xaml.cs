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
            ImageBrush tank_bleu_1 = new ImageBrush();
            tank_bleu_1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Tank_bleu_1.png"));
            ImageBrush tank_bleu_2 = new ImageBrush();
            tank_bleu_2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Tank_bleu_2.png"));
            ImageBrush tank_bleu_3 = new ImageBrush();
            tank_bleu_3.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Tank_bleu_3.png"));

            ImageBrush tank_rouge_1 = new ImageBrush();
            tank_rouge_1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Tank_rouge_1.png"));
            ImageBrush tank_rouge_2 = new ImageBrush();
            tank_rouge_2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Tank_rouge_2.png"));
            ImageBrush tank_rouge_3 = new ImageBrush();
            tank_rouge_3.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Tank_rouge_3.png"));

        }
    }
}
