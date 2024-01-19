﻿using System;
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

            rect_fond.Visibility = Visibility.Hidden;
            bt_B.Visibility = Visibility.Hidden;
            bt_D.Visibility = Visibility.Hidden;
            bt_Dr.Visibility = Visibility.Hidden;
            bt_ES.Visibility = Visibility.Hidden;
            bt_H.Visibility = Visibility.Hidden;
            bt_Q.Visibility = Visibility.Hidden;
            bt_retour.Visibility = Visibility.Hidden;
            bt_S.Visibility = Visibility.Hidden;
            bt_T.Visibility = Visibility.Hidden;
            bt_Z.Visibility = Visibility.Hidden;
            BT_Ga.Visibility = Visibility.Hidden;
            lb_1.Visibility = Visibility.Hidden;
            lb_2.Visibility = Visibility.Hidden;

        }

        private void Tank_bleu_1_Click(object sender, RoutedEventArgs e)
        {
            nb_TankJ1 = 1;
            MEthode(Tank_bleu_1, Tank_bleu_2, Tank_bleu_3);
        }

        private void Tank_bleu_2_Click(object sender, RoutedEventArgs e)
        {
            nb_TankJ1 = 2;
            MEthode(Tank_bleu_2, Tank_bleu_3 ,Tank_bleu_1);
        }

        private void Tank_bleu_3_Click(object sender, RoutedEventArgs e)
        {
            nb_TankJ1 = 3;
            MEthode(Tank_bleu_3, Tank_bleu_2, Tank_bleu_1);
        }

        private void Tank_rouge_3_Click(object sender, RoutedEventArgs e)
        {
            nb_TankJ2 = 3;
            MEthode(Tank_rouge_3, Tank_rouge_2, Tank_rouge_1);
        }

        private void Tank_rouge_2_Click(object sender, RoutedEventArgs e)
        {
            nb_TankJ2 = 2;
            MEthode(Tank_rouge_2, Tank_rouge_1, Tank_rouge_3);
        }

       

        private void Tank_rouge_1_Click(object sender, RoutedEventArgs e)
        {
            nb_TankJ2 = 1;
            MEthode(Tank_rouge_1, Tank_rouge_2, Tank_rouge_3);
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
            rect_fond.Visibility = Visibility.Visible;
            bt_B.Visibility = Visibility.Visible;
            bt_D.Visibility = Visibility.Visible;
            bt_Dr.Visibility = Visibility.Visible;
            bt_ES.Visibility = Visibility.Visible;
            bt_H.Visibility = Visibility.Visible;
            bt_Q.Visibility = Visibility.Visible;
            bt_retour.Visibility = Visibility.Visible;
            bt_S.Visibility = Visibility.Visible;
            bt_T.Visibility = Visibility.Visible;
            bt_Z.Visibility = Visibility.Visible;
            BT_Ga.Visibility = Visibility.Visible;
            lb_1.Visibility = Visibility.Visible;
            lb_2.Visibility = Visibility.Visible;
        }

        

        private void bt_retour_Click(object sender, RoutedEventArgs e)
        {
            rect_fond.Visibility = Visibility.Hidden;
            bt_B.Visibility = Visibility.Hidden;
            bt_D.Visibility = Visibility.Hidden;
            bt_Dr.Visibility = Visibility.Hidden;
            bt_ES.Visibility = Visibility.Hidden;
            bt_H.Visibility = Visibility.Hidden;
            bt_Q.Visibility = Visibility.Hidden;
            bt_retour.Visibility = Visibility.Hidden;
            bt_S.Visibility = Visibility.Hidden;
            bt_T.Visibility = Visibility.Hidden;
            bt_Z.Visibility = Visibility.Hidden;
            BT_Ga.Visibility = Visibility.Hidden;
            lb_1.Visibility = Visibility.Hidden;
            lb_2.Visibility = Visibility.Hidden;
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
            n1 = (string)bt_Z.Content;
            bt_Z.Content = e.Key.ToString();
        }

        
        private void bt_Q_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            n2 = (string)bt_Q.Content;
            bt_Q.Content = e.Key.ToString();
        }
        private void bt_S_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            n3 = (string)bt_S.Content;
            bt_S.Content = e.Key.ToString();
        }
        private void bt_D_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            n4 = (string)bt_D.Content;
            bt_D.Content = e.Key.ToString();
        }
        private void bt_ES_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            n5 = (string)bt_ES.Content;
            bt_ES.Content = e.Key.ToString();
        }
        private void bt_H_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            n6 = (string)bt_H.Content;
            bt_H.Content = e.Key.ToString();
        }
        private void BT_Ga_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            n7 = (string)BT_Ga.Content;
            BT_Ga.Content = e.Key.ToString();
        }
        private void bt_B_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            n8 = (string)bt_B.Content;
            bt_B.Content = e.Key.ToString();
        }
        private void bt_Dr_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            n9 = (string)bt_Dr.Content;
            bt_Dr.Content = e.Key.ToString();
        }
        private void bt_T_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            n10 = (string)bt_T.Content;
            bt_T.Content = e.Key.ToString();
        }
    }
}
