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
        private bool goLeft, goRight, goUp, goDown = false;
        private int Rect_Tank_J1_Speed = 10;
        public MainWindow()
        {
            InitializeComponent();

            ImageBrush tank1 = new ImageBrush();
            BitmapImage imgTank1 = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Tank_bleu_1.png"));
            imgTank1.Rotation = Rotation.Rotate180;
            tank1.ImageSource = imgTank1;

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
            Rect tank1 = new Rect(Canvas.GetLeft(Rect_Tank_J1), Canvas.GetTop(Rect_Tank_J1), Rect_Tank_J1.Width, Rect_Tank_J1.Height);
            MovePlayer();

        }
        private void Canvas_KeyIsDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Left)
            {
                goLeft = true;
            }
            if (e.Key == Key.Right)
            {
                goRight = true;
            }
            if(e.Key == Key.Up)
            { 
                goUp = true;
            }
            if(e.Key == Key.Down)
            {
                goDown = true;
            }

        }
        private void Canvas_KeyIsUp(object sender, KeyEventArgs e)
        {
           
            if (e.Key == Key.Left)
            {
                goLeft = false;
            }
            if (e.Key == Key.Right)
            {
                goRight = false;
            }
            if (e.Key == Key.Up)
            {
                goUp = false;
            }
            if (e.Key == Key.Down)
            {
                goDown = false;
            }
        }
        
        public void MovePlayer()
        {
            if (goLeft && Canvas.GetLeft(Rect_Tank_J1) > 0)
            {
                Canvas.SetLeft(Rect_Tank_J1, Canvas.GetLeft(Rect_Tank_J1) - Rect_Tank_J1_Speed);
            }
            else if (goRight && Canvas.GetLeft(Rect_Tank_J1) + Rect_Tank_J1.Width <
            Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(Rect_Tank_J1, Canvas.GetLeft(Rect_Tank_J1) + Rect_Tank_J1_Speed);
            }


            if(goUp && Canvas.GetTop(Rect_Tank_J1) > 0) 
            {
                Canvas.SetTop(Rect_Tank_J1, Canvas.GetTop(Rect_Tank_J1) - Rect_Tank_J1_Speed);
            }
            else if(goDown && Canvas.GetTop(Rect_Tank_J1)+Rect_Tank_J1.Width < 
            Application.Current.MainWindow.Width) 
            {
                Canvas.SetTop(Rect_Tank_J1, Canvas.GetTop(Rect_Tank_J1) + Rect_Tank_J1_Speed);
            }
        }
       
    }
}
