using System;
using System.Collections.Generic;
using System.Drawing;
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
        public int nb_TankJ1=1;
        public int nb_TankJ2=1;

        public string n1="Z",n2="Q",n3="S",n4="D",n5="Space",n6="Up",n7="Left",n8="Down",n9="Right",n10="NumPad1";

        ImageBrush fondVert = new ImageBrush();
        ImageBrush tankB1 = new ImageBrush();
        ImageBrush tankB2 = new ImageBrush();
        ImageBrush tankB3 = new ImageBrush();
        ImageBrush tankR1 = new ImageBrush();
        ImageBrush tankR2 = new ImageBrush();
        ImageBrush tankR3 = new ImageBrush();

        public string couleur = "Green";
        public BoiteDeDialogue()
        {
            InitializeComponent();

            ImageBrush background = new ImageBrush();
            background.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images/Menu.jpg"));
            backgroundMenu.Fill = background;

            ImageBrush tankB1 = new ImageBrush();
            ImageBrush tankB2 = new ImageBrush();
            ImageBrush tankB3 = new ImageBrush();
            ImageBrush tankR1 = new ImageBrush();
            ImageBrush tankR2 = new ImageBrush();
            ImageBrush tankR3 = new ImageBrush();
            
            tankB1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images/image_Tanks/Tank_bleu_1_S.png"));
            tankB2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images/image_Tanks/Tank_bleu_2_S.png"));
            tankB3.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images/image_Tanks/Tank_bleu_3_S.png"));
            tankR1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images/image_Tanks/Tank_rouge_1_S.png"));
            tankR2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images/image_Tanks/Tank_rouge_2_S.png"));
            tankR3.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images/image_Tanks/Tank_rouge_3_S.png"));
            
            TankBleu1.Background = tankB1;
            TankBleu2.Background = tankB2;
            TankBleu3.Background = tankB3;

            TankRouge1.Background = tankR1;
            TankRouge2.Background = tankR2;
            TankRouge3.Background = tankR3;

            rectFond.Visibility = Visibility.Hidden;
            btB.Visibility = Visibility.Hidden;
            btD.Visibility = Visibility.Hidden;
            btDr.Visibility = Visibility.Hidden;
            btES.Visibility = Visibility.Hidden;
            btH.Visibility = Visibility.Hidden;
            btQ.Visibility = Visibility.Hidden;
            btRetour.Visibility = Visibility.Hidden;
            btS.Visibility = Visibility.Hidden;
            btT.Visibility = Visibility.Hidden;
            btZ.Visibility = Visibility.Hidden;
            btGa.Visibility = Visibility.Hidden;
            lb1.Visibility = Visibility.Hidden;
            lb2.Visibility = Visibility.Hidden;

        }

        private void Tank_bleu_1_Click(object sender, RoutedEventArgs e)
        {
            nb_TankJ1 = 1;
            MEthode(TankBleu1, TankBleu2, TankBleu3);
        }

        private void Tank_bleu_2_Click(object sender, RoutedEventArgs e)
        {
            nb_TankJ1 = 2;
            MEthode(TankBleu2, TankBleu3 ,TankBleu1);
        }

        private void Tank_bleu_3_Click(object sender, RoutedEventArgs e)
        {
            nb_TankJ1 = 3;
            MEthode(TankBleu3, TankBleu2, TankBleu1);
        }

        private void Tank_rouge_3_Click(object sender, RoutedEventArgs e)
        {
            nb_TankJ2 = 3;
            MEthode(TankRouge3, TankRouge2, TankRouge1);
        }

        private void Tank_rouge_2_Click(object sender, RoutedEventArgs e)
        {
            nb_TankJ2 = 2;
            MEthode(TankRouge2, TankRouge1, TankRouge3);
        }

       

        private void Tank_rouge_1_Click(object sender, RoutedEventArgs e)
        {
            nb_TankJ2 = 1;
            MEthode(TankRouge1, TankRouge2, TankRouge3);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = false;
        }

        

        private void MEthode(Button bouton1 , Button bouton2, Button bouton3)
        {
            bouton1.BorderBrush = Brushes.Green;    
            bouton2.BorderBrush = Brushes.Red;
            bouton3.BorderBrush = Brushes.Red;
        }

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
           
        }

        

        private void bt_parametre_Click(object sender, RoutedEventArgs e)
        {
            rectFond.Visibility = Visibility.Visible;
            btB.Visibility = Visibility.Visible;
            btD.Visibility = Visibility.Visible;
            btDr.Visibility = Visibility.Visible;
            btES.Visibility = Visibility.Visible;
            btH.Visibility = Visibility.Visible;
            btQ.Visibility = Visibility.Visible;
            btRetour.Visibility = Visibility.Visible;
            btS.Visibility = Visibility.Visible;
            btT.Visibility = Visibility.Visible;
            btZ.Visibility = Visibility.Visible;
            btGa.Visibility = Visibility.Visible;
            lb1.Visibility = Visibility.Visible;
            lb2.Visibility = Visibility.Visible;
        }

        

        private void bt_retour_Click(object sender, RoutedEventArgs e)
        {
            rectFond.Visibility = Visibility.Hidden;
            btB.Visibility = Visibility.Hidden;
            btD.Visibility = Visibility.Hidden;
            btDr.Visibility = Visibility.Hidden;
            btES.Visibility = Visibility.Hidden;
            btH.Visibility = Visibility.Hidden;
            btQ.Visibility = Visibility.Hidden;
            btRetour.Visibility = Visibility.Hidden;
            btS.Visibility = Visibility.Hidden;
            btT.Visibility = Visibility.Hidden;
            btZ.Visibility = Visibility.Hidden;
            btGa.Visibility = Visibility.Hidden;
            lb1.Visibility = Visibility.Hidden;
            lb2.Visibility = Visibility.Hidden;
        }
        public string toucheR1
        {
            get
            {
                return n1;
            }
        }
        public string toucheG1
        {
            get
            {
                return n2;
            }
        }
        public string toucheB1
        {
            get
            {
                return n3;
            }
        }
        public string toucheD1
        {
            get
            {
                return n4;
            }
        }
        public string toucheT1
        {
            get
            {
                return n5;
            }
        }
        public string toucheR2
        {
            get
            {
                return n6;
            }
        }
        public string toucheG2
        {
            get
            {
                return n7;
            }
        }
        public string toucheB2
        {
            get
            {
                return n8;
            }
        }
        public string toucheD2
        {
            get
            {
                return n9;
            }
        }
        public string toucheT2
        {
            get
            {
                return n10;
            }
        }


        private void bt_Z_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            n1 = (string)btZ.Content;
            btZ.Content = e.Key.ToString();
        }

        
        private void bt_Q_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            n2 = (string)btQ.Content;
            btQ.Content = e.Key.ToString();
        }
        private void bt_S_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            n3 = (string)btS.Content;
            btS.Content = e.Key.ToString();
        }
        private void bt_D_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            n4 = (string)btD.Content;
            btD.Content = e.Key.ToString();
        }
        private void bt_ES_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            n5 = (string)btES.Content;
            btES.Content = e.Key.ToString();
        }
        private void bt_H_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            n6 = (string)btH.Content;
            btH.Content = e.Key.ToString();
        }
        private void BT_Ga_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            n7 = (string)btGa.Content;
            btGa.Content = e.Key.ToString();
        }
        private void bt_B_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            n8 = (string)btB.Content;
            btB.Content = e.Key.ToString();
        }
        private void bt_Dr_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            n9 = (string)btDr.Content;
            btDr.Content = e.Key.ToString();
        }
        private void bt_T_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            n10 = (string)btT.Content;
            btT.Content = e.Key.ToString();
        }
    }
}
