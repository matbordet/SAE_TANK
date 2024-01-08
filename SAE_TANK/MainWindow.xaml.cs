using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
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
using System.Windows.Threading;

namespace SAE_TANK
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private bool goLeft_J1, goRight_J1, goUp_J1, goDown_J1 = false;
        private bool goLeft_J2, goRight_J2, goUp_J2, goDown_J2 = false;

        private int Rect_Tank_J1_Speed = 5;
        private int bulletTank1Speed = 15;
        private int Rect_Tank_J2_Speed = 5;
        private int bulletTank2Speed = 15;

        private List<Rectangle> itemsToRemove = new List<Rectangle>();

        ImageBrush tank1 = new ImageBrush();
        ImageBrush tank2 = new ImageBrush();
        ImageBrush sol = new ImageBrush();

        public MainWindow()
        {
            InitializeComponent();

            tank1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_bleu_1_S.png"));

            tank2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_rouge_3_N.png"));

            sol.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "sol.png"));

            fond_Arene.Fill = sol;
            Rect_Tank_J1.Fill = tank1;
            Rect_Tank_J2.Fill = tank2;

            dispatcherTimer.Tick += GameEngine;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(16);
            dispatcherTimer.Start();

        }
        private void GameEngine(object sender, EventArgs e)
        {
            MovePlayer();
            {
                foreach (Rectangle x in Le_Canvas.Children.OfType<Rectangle>())
                {
                    MoveAndTestBulletTankx(x);
                }
                
            }
        }

        private void Canvas_KeyUp(object sender, KeyEventArgs e)
        {
            //test control J1
            if (e.Key == Key.Q)
            {
                goLeft_J1 = false;
               
            }
            if (e.Key == Key.D)
            {
                goRight_J1 = false;
            }
            if (e.Key == Key.Z)
            {
                goUp_J1 = false;
            }
            if (e.Key == Key.S)
            {
                goDown_J1 = false;
            }
            //test control J1
            if (e.Key == Key.Left)
            {
                goLeft_J2 = false;
            }
            if (e.Key == Key.Right)
            {
                goRight_J2 = false;
            }
            if (e.Key == Key.Up)
            {
                goUp_J2 = false;
            }
            if (e.Key == Key.Down)
            {
                goDown_J2 = false;
            }
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            //test controle J1
            if (e.Key == Key.Q)
            {
                goLeft_J1 = true;
            }
            if (e.Key == Key.D)
            {
                goRight_J1 = true;
            }
            if (e.Key == Key.Z)
            {
                goUp_J1 = true;
            }
            if (e.Key == Key.S)
            {
                goDown_J1 = true;
            }
            if(e.Key == Key.E)
            {
                itemsToRemove.Clear();
                Rectangle newBullet = new Rectangle
                {
                    Tag = "bulletTank1"
                ,
                    Height = 10,
                    Width = 10,
                    Fill = Brushes.White,
                    Stroke = Brushes.Red
                };
                Canvas.SetTop(newBullet, Canvas.GetTop(Rect_Tank_J1) + newBullet.Height + 50);
                Canvas.SetLeft(newBullet, Canvas.GetLeft(Rect_Tank_J1) + Rect_Tank_J1.Width / 2);
                Le_Canvas.Children.Add(newBullet);
            }
            //test controle J2
            if (e.Key == Key.Left)
            {
                goLeft_J2 = true;
            }
            if (e.Key == Key.Right)
            {
                goRight_J2 = true;
            }
            if (e.Key == Key.Up)
            {
                goUp_J2 = true;
            }
            if (e.Key == Key.Down)
            {
                goDown_J2 = true;
            }
            if (e.Key == Key.Space)
            {
                itemsToRemove.Clear();
                Rectangle newBullet = new Rectangle
                {
                    Tag = "bulletTank2"
                ,
                    Height = 10,
                    Width = 10,
                    Fill = Brushes.White,
                    Stroke = Brushes.Red
                };
                Canvas.SetTop(newBullet, Canvas.GetTop(Rect_Tank_J2) - newBullet.Height);
                Canvas.SetLeft(newBullet, Canvas.GetLeft(Rect_Tank_J2) + Rect_Tank_J2.Width / 2);
                Le_Canvas.Children.Add(newBullet);
            }
        }
        
        public void MovePlayer()
        {
            //deplacement du joueur 1
            if (goLeft_J1 && Canvas.GetLeft(Rect_Tank_J1) > 0)
            {
                tank1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_bleu_1_W.png"));
                Canvas.SetLeft(Rect_Tank_J1, Canvas.GetLeft(Rect_Tank_J1) - Rect_Tank_J1_Speed);
            }
            else if (goRight_J1 && Canvas.GetLeft(Rect_Tank_J1) + Rect_Tank_J1.Width <
            Application.Current.MainWindow.Width)
            {
                tank1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_bleu_1_E.png"));
                Canvas.SetLeft(Rect_Tank_J1, Canvas.GetLeft(Rect_Tank_J1) + Rect_Tank_J1_Speed);
            }


            if (goUp_J1 && Canvas.GetTop(Rect_Tank_J1) > 0)
            {
                Canvas.SetTop(Rect_Tank_J1, Canvas.GetTop(Rect_Tank_J1) - Rect_Tank_J1_Speed);
                tank1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_bleu_1_N.png"));
            }
            else if (goDown_J1 && Canvas.GetTop(Rect_Tank_J1) + Rect_Tank_J1.Width <
            Application.Current.MainWindow.Width)
            {
                Canvas.SetTop(Rect_Tank_J1, Canvas.GetTop(Rect_Tank_J1) + Rect_Tank_J1_Speed);
                tank1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_bleu_1_S.png"));
            }

            //deplacement du joueur 2
            if (goLeft_J2 && Canvas.GetLeft(Rect_Tank_J2) > 0)
            {
                tank2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_rouge_3_W.png"));
                Canvas.SetLeft(Rect_Tank_J2, Canvas.GetLeft(Rect_Tank_J2) - Rect_Tank_J2_Speed);
            }
            else if (goRight_J2 && Canvas.GetLeft(Rect_Tank_J2) + Rect_Tank_J2.Width <
            Application.Current.MainWindow.Width)
            {
                tank2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_rouge_3_E.png"));
                Canvas.SetLeft(Rect_Tank_J2, Canvas.GetLeft(Rect_Tank_J2) + Rect_Tank_J2_Speed);
            }


            if (goUp_J2 && Canvas.GetTop(Rect_Tank_J2) > 0)
            {
                tank2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_rouge_3_N.png"));
                Canvas.SetTop(Rect_Tank_J2, Canvas.GetTop(Rect_Tank_J2) - Rect_Tank_J2_Speed);
            }
            else if (goDown_J2 && Canvas.GetTop(Rect_Tank_J1) + Rect_Tank_J2.Width <
            Application.Current.MainWindow.Width)
            {
                tank2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_rouge_3_S.png"));
                Canvas.SetTop(Rect_Tank_J2, Canvas.GetTop(Rect_Tank_J2) + Rect_Tank_J2_Speed);
            }


        }
        public void MoveAndTestBulletTankx(Rectangle x)
        {
            if (x is Rectangle && (string)x.Tag == "bulletTank1")
            {
                // si c’est un tir joueur on le déplace vers le haut
                Canvas.SetTop(x, Canvas.GetTop(x) + bulletTank1Speed);
                // création d’un tir joueur à base d’un rectangle Rect (nécessaire pour la collision)
                Rect bullet = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                // on vérifie que le tir a quitté le le haut du canvas (pas de collision avec un ennemie)
                if (Canvas.GetTop(x) < 10)
                {
                    // si c’est le cas on l’ajoute à la liste des éléments à supprimer
                    itemsToRemove.Add(x);
                }
            }
            if (x is Rectangle && (string)x.Tag == "bulletTank2")
            {
                // si c’est un tir joueur on le déplace vers le haut
                Canvas.SetTop(x, Canvas.GetTop(x) - bulletTank2Speed);
                // création d’un tir joueur à base d’un rectangle Rect (nécessaire pour la collision)
                Rect bullet = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                // on vérifie que le tir a quitté le le haut du canvas (pas de collision avec un ennemie)
                if (Canvas.GetTop(x) < 10)
                {
                    // si c’est le cas on l’ajoute à la liste des éléments à supprimer
                    itemsToRemove.Add(x);
                }
            }
        }

        public void RemoveItemsRemove()
        {
            foreach (Rectangle y in itemsToRemove)
            {
                // on les enlève du canvas
                Le_Canvas.Children.Remove(y);
            }
        }

    }
}
