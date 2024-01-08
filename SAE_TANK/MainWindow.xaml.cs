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

        private int Rect_Tank_J1_Speed = 10;
        private int Rect_Tank_J2_Speed = 10;

        public MainWindow()
        {
            InitializeComponent();

            ImageBrush tank1 = new ImageBrush();
            tank1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Tank_bleu_1.png"));





            ImageBrush tank2 = new ImageBrush();
            tank2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Tank_rouge_3.png"));

            ImageBrush sol = new ImageBrush();
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
        }
        
        public void MovePlayer()
        {
            //deplacement du joueur 1
            if (goLeft_J1 && Canvas.GetLeft(Rect_Tank_J1) > 0)
            {
                Canvas.SetLeft(Rect_Tank_J1, Canvas.GetLeft(Rect_Tank_J1) - Rect_Tank_J1_Speed);
            }
            else if (goRight_J1 && Canvas.GetLeft(Rect_Tank_J1) + Rect_Tank_J1.Width <
            Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(Rect_Tank_J1, Canvas.GetLeft(Rect_Tank_J1) + Rect_Tank_J1_Speed);
            }


            if (goUp_J1 && Canvas.GetTop(Rect_Tank_J1) > 0)
            {
                Canvas.SetTop(Rect_Tank_J1, Canvas.GetTop(Rect_Tank_J1) - Rect_Tank_J1_Speed);
            }
            else if (goDown_J1 && Canvas.GetTop(Rect_Tank_J1) + Rect_Tank_J1.Width <
            Application.Current.MainWindow.Width)
            {
                Canvas.SetTop(Rect_Tank_J1, Canvas.GetTop(Rect_Tank_J1) + Rect_Tank_J1_Speed);
            }

            //deplacement du joueur 2
            if (goLeft_J2 && Canvas.GetLeft(Rect_Tank_J2) > 0)
            {
                Canvas.SetLeft(Rect_Tank_J2, Canvas.GetLeft(Rect_Tank_J2) - Rect_Tank_J2_Speed);
            }
            else if (goRight_J2 && Canvas.GetLeft(Rect_Tank_J2) + Rect_Tank_J2.Width <
            Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(Rect_Tank_J2, Canvas.GetLeft(Rect_Tank_J2) + Rect_Tank_J2_Speed);
            }


            if (goUp_J2 && Canvas.GetTop(Rect_Tank_J2) > 0)
            {
                Canvas.SetTop(Rect_Tank_J2, Canvas.GetTop(Rect_Tank_J2) - Rect_Tank_J2_Speed);
            }
            else if (goDown_J2 && Canvas.GetTop(Rect_Tank_J1) + Rect_Tank_J2.Width <
            Application.Current.MainWindow.Width)
            {
                Canvas.SetTop(Rect_Tank_J2, Canvas.GetTop(Rect_Tank_J2) + Rect_Tank_J2_Speed);
            }


        }
       
    }
}
