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
            ImageBrush tankB1 = new ImageBrush();
            ImageBrush tankB2 = new ImageBrush();
            ImageBrush tankB3 = new ImageBrush();
            ImageBrush tankR1 = new ImageBrush();
            ImageBrush tankR2 = new ImageBrush();
            ImageBrush tankR3 = new ImageBrush();

            tankB1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_bleu_1_S.png"));
            tankB2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_bleu_2_S.png"));
            tankB3.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_bleu_3_S.png"));
            tankR1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_rouge_1_S.png"));
            tankR2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_rouge_2_S.png"));
            tankR3.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_rouge_3_S.png"));

            Tank_bleu_1.Background = tankB1;
            Tank_bleu_2.Background = tankB2;
            Tank_bleu_3.Background = tankB3;

            Tank_rouge_1.Background = tankR1;
            Tank_rouge_2.Background = tankR2;
            Tank_rouge_3.Background = tankR3;


        }
    }
}
