using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
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

        private string direction_J1 = "S";
        private string direction_J2 = "N";
        public int numero_J1 = 1;
        public int numero_J2 = 1;

        Rectangle[] mur = new Rectangle[20];
        Rectangle[] murH = new Rectangle[20];
        Rect[] murCollision = new Rect[46];

        

        private List<Rectangle> itemsToRemove = new List<Rectangle>();

        ImageBrush murVertival = new ImageBrush();
        ImageBrush murHorizontal = new ImageBrush();
        ImageBrush tank1 = new ImageBrush();
        ImageBrush tank2 = new ImageBrush();
        ImageBrush sol = new ImageBrush();
        ImageBrush vie_Pleine = new ImageBrush();
        ImageBrush vie_Vide = new ImageBrush();

        
        public MainWindow()
        {
            InitializeComponent();

            BoiteDeDialogue dialogue = new BoiteDeDialogue();
            dialogue.ShowDialog();

            if(DialogResult == false)
            {
                Application.Current.Shutdown();
            }
            numero_J1 = dialogue.nb_TankJ1;
            numero_J2 = dialogue.nb_TankJ2;

            tank1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_bleu_"+ numero_J1 + "_S.png"));
            tank2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_rouge_"+ numero_J2 + "_N.png"));
            sol.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "sol.png"));
            murVertival.ImageSource=new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "mur_vertical.jpg"));
            murHorizontal.ImageSource=new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "mur_hor.jpg"));
            vie_Pleine.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "coeur_Plein.png"));
            vie_Vide.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "coeur_Vide.png"));

            InitialiseMurs();
            
            Console.WriteLine(murCollision);
            fond_Arene.Fill = sol;
            Rect_Tank_J1.Fill = tank1;
            Rect_Tank_J2.Fill = tank2;
            coeur_1_J1.Fill = vie_Pleine;
            coeur_2_J1.Fill = vie_Pleine;
            coeur_3_J1.Fill = vie_Pleine;

            coeur_1_J2.Fill = vie_Pleine;
            coeur_2_J2.Fill = vie_Pleine;
            coeur_3_J2.Fill = vie_Pleine;

            lb_J1.Content = dialogue.tb_J1.Text;
            lb_J2.Content = dialogue.tb_J2.Text;

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
                    MoveAndTestBulletTank(x);
                    
                }
            }
            RemoveItemsRemove();
            CollisionMurTank(Rect_Tank_J1, direction_J1);
            CollisionMurTank(Rect_Tank_J2, direction_J2);
            

            

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
            //test control J2
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
            if(e.Key == Key.Space)
            {
                itemsToRemove.Clear();
                Rectangle newBullet = new Rectangle
                {
                    Tag = "bulletTank1",
                    Height = 10,
                    Width = 10,
                    Fill = Brushes.White,
                    Stroke = Brushes.Red
                    
                };
                Canvas.SetTop(newBullet, Canvas.GetTop(Rect_Tank_J1) - newBullet.Height + Rect_Tank_J1.Height / 2);
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
            if (e.Key == Key.NumPad0)
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
                Canvas.SetTop(newBullet, Canvas.GetTop(Rect_Tank_J2) - newBullet.Height + Rect_Tank_J2.Width/ 2);
                Canvas.SetLeft(newBullet, Canvas.GetLeft(Rect_Tank_J2) + Rect_Tank_J2.Width / 2);
                Le_Canvas.Children.Add(newBullet);
            }
        }
        
        public void MovePlayer()
        {

            //deplacement du joueur 1
            if (goLeft_J1 && Canvas.GetLeft(Rect_Tank_J1) > 300 && CollisionMurJoueur(Rect_Tank_J1) == false)
            {
                direction_J1 = "W";
                tank1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_bleu_" + numero_J1 + "_"+direction_J1+".png"));
                Canvas.SetLeft(Rect_Tank_J1, Canvas.GetLeft(Rect_Tank_J1) - Rect_Tank_J1_Speed);
            }
            else if (goRight_J1 && Canvas.GetLeft(Rect_Tank_J1) + Rect_Tank_J1.Width < 1250 && CollisionMurJoueur(Rect_Tank_J1) == false)
            {
                direction_J1 = "E";
                tank1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_bleu_"+ numero_J1 + "_"+direction_J1+".png"));
                Canvas.SetLeft(Rect_Tank_J1, Canvas.GetLeft(Rect_Tank_J1) + Rect_Tank_J1_Speed);
            }


            else if (goUp_J1 && Canvas.GetTop(Rect_Tank_J1) > 30 && CollisionMurJoueur(Rect_Tank_J1) == false)
            {
                direction_J1 = "N";
                Canvas.SetTop(Rect_Tank_J1, Canvas.GetTop(Rect_Tank_J1) - Rect_Tank_J1_Speed);
                tank1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_bleu_" + numero_J1 + "_" + direction_J1 + ".png"));
            }
            else if (goDown_J1 && Canvas.GetTop(Rect_Tank_J1) + Rect_Tank_J1.Height < 980 && CollisionMurJoueur(Rect_Tank_J1) == false)
            {
                direction_J1 = "S";
                Canvas.SetTop(Rect_Tank_J1, Canvas.GetTop(Rect_Tank_J1) + Rect_Tank_J1_Speed);
                tank1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_bleu_" + numero_J1 + "_" + direction_J1 + ".png"));
            }

            //deplacement du joueur 2
            if (goLeft_J2 && Canvas.GetLeft(Rect_Tank_J2) > 300 && CollisionMurJoueur(Rect_Tank_J2) == false)
            {
                direction_J2 = "W";
                tank2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_rouge_"+ numero_J2 + "_"+direction_J2+".png"));
                Canvas.SetLeft(Rect_Tank_J2, Canvas.GetLeft(Rect_Tank_J2) - Rect_Tank_J2_Speed);
            }
            else if (goRight_J2 && Canvas.GetLeft(Rect_Tank_J2) + Rect_Tank_J2.Width <1250 && CollisionMurJoueur(Rect_Tank_J2) == false)
            {
                direction_J2 = "E";
                tank2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_rouge_"+ numero_J2 + "_"+direction_J2+".png"));
                Canvas.SetLeft(Rect_Tank_J2, Canvas.GetLeft(Rect_Tank_J2) + Rect_Tank_J2_Speed);
            }


            else if (goUp_J2 && Canvas.GetTop(Rect_Tank_J2) > 30 && CollisionMurJoueur(Rect_Tank_J2) == false)
            {
                direction_J2 = "N";
                tank2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_rouge_"+ numero_J2 + "_"+direction_J2+".png"));
                Canvas.SetTop(Rect_Tank_J2, Canvas.GetTop(Rect_Tank_J2) - Rect_Tank_J2_Speed);
            }
            else if (goDown_J2 && Canvas.GetTop(Rect_Tank_J2) + Rect_Tank_J2.Height <980 && CollisionMurJoueur(Rect_Tank_J2) == false)
            {
                direction_J2 = "S";
                tank2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_rouge_"+ numero_J2 + "_"+direction_J2+".png"));
                Canvas.SetTop(Rect_Tank_J2, Canvas.GetTop(Rect_Tank_J2) + Rect_Tank_J2_Speed);
            }


        }
        public void MoveAndTestBulletTank(Rectangle x)
        {
           
            //J1----------------------------------------
            if(direction_J1 =="W")
            {
                
                if (x is Rectangle && (string)x.Tag == "bulletTank1")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) + bulletTank1Speed);
                    Rect bullety = new Rect(Canvas.GetTop(x), Canvas.GetLeft(x), x.Width, x.Height);
                    x.Tag = "bullet_W_1";
                }
               
            }
            else if (direction_J1 == "E")
            {
                if (x is Rectangle && (string)x.Tag == "bulletTank1")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - bulletTank1Speed);
                    Rect bullety = new Rect(Canvas.GetTop(x), Canvas.GetLeft(x), x.Width, x.Height);
                    x.Tag = "bullet_E_1";
                }

            }
            else if(direction_J1 == "N")
            {
                if (x is Rectangle && (string)x.Tag == "bulletTank1")
                {
                    // si c’est un tir joueur on le déplace vers le haut
                    Canvas.SetTop(x, Canvas.GetTop(x) - bulletTank1Speed);
                    // création d’un tir joueur à base d’un rectangle Rect (nécessaire pour la collision)
                    Rect bulletx = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    x.Tag = "bullet_N_1";
                }
            }
            else if (direction_J1 == "S") 
            {
                if (x is Rectangle && (string)x.Tag == "bulletTank1")
                {
                    // si c’est un tir joueur on le déplace vers le haut
                    Canvas.SetTop(x, Canvas.GetTop(x) + bulletTank1Speed);

                    // création d’un tir joueur à base d’un rectangle Rect (nécessaire pour la collision)
                    Rect bulletx = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    x.Tag = "bullet_S_1";
                }
            }
            CollisionBalleTankJ1(Rect_Tank_J1, x);
            if((string)x.Tag == "bullet_E_1")
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) + bulletTank1Speed);
                Rect bullety = new Rect(Canvas.GetTop(x), Canvas.GetLeft(x), x.Width, x.Height);
                if (Canvas.GetLeft(x) < 300 || Canvas.GetLeft(x) > 1250 || CollisionMurBalle(x) == true)
                {
                    itemsToRemove.Add(x);
                }
            }
            else if ((string)x.Tag == "bullet_W_1")
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) - bulletTank1Speed);
                Rect bullety = new Rect(Canvas.GetTop(x), Canvas.GetLeft(x), x.Width, x.Height);
                if (Canvas.GetLeft(x) < 300 || Canvas.GetLeft(x) > 1250 || CollisionMurBalle(x) == true)
                {
                    itemsToRemove.Add(x);
                }
            }
            else if (x is Rectangle && (string)x.Tag == "bullet_N_1")
            {
                Canvas.SetTop(x, Canvas.GetTop(x) - bulletTank1Speed);
                Rect bulletx = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                if (Canvas.GetTop(x) < 30 || Canvas.GetTop(x) > 980 || CollisionMurBalle(x) == true)
                {
                    itemsToRemove.Add(x);
                }
            }
            else if (x is Rectangle && (string)x.Tag == "bullet_S_1")
            {
                Canvas.SetTop(x, Canvas.GetTop(x) + bulletTank1Speed);
                Rect bulletx = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                if (Canvas.GetTop(x) < 30 || Canvas.GetTop(x) > 980 || CollisionMurBalle(x) == true)
                {
                    itemsToRemove.Add(x);
                }
            }


            //J2---------------------------
            if (direction_J2 == "W")
            {
                if (x is Rectangle && (string)x.Tag == "bulletTank2")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) + bulletTank1Speed);
                    Rect bullety = new Rect(Canvas.GetTop(x), Canvas.GetLeft(x), x.Width, x.Height);
                    x.Tag = "bullet_W_2";
                }

            }
            else if (direction_J2 == "E")
            {
                if (x is Rectangle && (string)x.Tag == "bulletTank2")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - bulletTank1Speed);
                    Rect bullety = new Rect(Canvas.GetTop(x), Canvas.GetLeft(x), x.Width, x.Height);
                    x.Tag = "bullet_E_2";
                }

            }
            else if (direction_J2 == "N")
            {
                if (x is Rectangle && (string)x.Tag == "bulletTank2")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - bulletTank1Speed);
                    Rect bulletx = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    x.Tag = "bullet_N_2";
                }
            }
            else if (direction_J2 == "S")
            {
                if (x is Rectangle && (string)x.Tag == "bulletTank2")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + bulletTank1Speed);
                    Rect bulletx = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    x.Tag = "bullet_S_2";
                }
            }
            if ((string)x.Tag == "bullet_E_2")
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) + bulletTank1Speed);
                Rect bullety = new Rect(Canvas.GetTop(x), Canvas.GetLeft(x), x.Width, x.Height);
                if (Canvas.GetLeft(x) < 300 || Canvas.GetLeft(x) > 1250||CollisionMurBalle(x)==true)
                {
                    // si c’est le cas on l’ajoute à la liste des éléments à supprimer
                    itemsToRemove.Add(x);
                }
            }
            else if ((string)x.Tag == "bullet_W_2")
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) - bulletTank1Speed);
                Rect bullety = new Rect(Canvas.GetTop(x), Canvas.GetLeft(x), x.Width, x.Height);
                if (Canvas.GetLeft(x) < 300 || Canvas.GetLeft(x) > 1250||CollisionMurBalle(x) == true)
                {
                    // si c’est le cas on l’ajoute à la liste des éléments à supprimer
                    itemsToRemove.Add(x);
                }
            }
            else if (x is Rectangle && (string)x.Tag == "bullet_N_2")
            {
                Canvas.SetTop(x, Canvas.GetTop(x) - bulletTank1Speed);
                Rect bulletx = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                if (Canvas.GetTop(x) < 30 ||Canvas.GetTop(x) > 980 || CollisionMurBalle(x) == true)
                {
                    // si c’est le cas on l’ajoute à la liste des éléments à supprimer
                    itemsToRemove.Add(x);
                }
            }
            else if (x is Rectangle && (string)x.Tag == "bullet_S_2")
            {
                Canvas.SetTop(x, Canvas.GetTop(x) + bulletTank1Speed);
                Rect bulletx = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                if (Canvas.GetTop(x) < 30 || Canvas.GetTop(x) > 980 || CollisionMurBalle(x) == true)
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

        public bool CollisionMurBalle(Rectangle balleRect)
        {
            Rect balle = new Rect(Canvas.GetLeft(balleRect), Canvas.GetTop(balleRect), balleRect.Width, balleRect.Height);
            for (int i = 0; i < murCollision.Length; i++)
            {
                if (murCollision[i].IntersectsWith(balle))
                {
                    murCollision[i]= Rect.Empty;
                    if (i < 20)
                    {
                        mur[i].Margin = new Thickness(-100, -100, 0, 0);
                    }
                    else
                    {
                        murH[i-20].Margin = new Thickness(-100, -100, 0, 0);
                    }
                    return true;   
                }
            }
            return false;
        }


        public bool CollisionMurJoueur(Rectangle tank)
        {
            Rect tankRect = new Rect(Canvas.GetLeft(tank), Canvas.GetTop(tank), tank.Width, tank.Height);

            for (int i = 0; i < murCollision.Length; i++)
            {
                if (murCollision[i].IntersectsWith(tankRect))
                {
                    return true;
                }
            }
            return false;
        }
        public void CollisionMurTank(Rectangle tank, string direction)
        {
            if(CollisionMurJoueur(tank))
            {

                switch (direction)
                        {
                            case "W":
                                Canvas.SetLeft(tank, Canvas.GetLeft(tank) + (Rect_Tank_J1_Speed));
                                break;
                            case "E":
                                Canvas.SetLeft(tank, Canvas.GetLeft(tank) - (Rect_Tank_J1_Speed));
                                break;
                            case "N":
                                Canvas.SetTop(tank, Canvas.GetTop(tank) + (Rect_Tank_J1_Speed));
                                break;
                            case "S":
                                Canvas.SetTop(tank, Canvas.GetTop(tank) - (Rect_Tank_J1_Speed ));
                                break;
                        }
            }
        }
        public void CollisionBalleTankJ1(Rectangle tank, Rectangle balle) 
        {
            Rect tankRect = new Rect(Canvas.GetTop(tank), Canvas.GetLeft(tank),tank.Width,tank.Height);
            Rect balleRect = new Rect(Canvas.GetLeft(balle), Canvas.GetTop(balle), balle.Width, balle.Height);


            if (balleRect.IntersectsWith(tankRect))
            {
                lb_Test.Content = "ca marche";
                
            }
        }
        public void InitialiseMurs()

        {
            Random random = new Random();
            
            int x = 300 + 180, y = 35, largeur = 20, hauteur = 175,compteur=0;
            for (int i = 0; i < mur.Length; i++)
            {
                mur[i] = new Rectangle();
                compteur++;
                mur[i].Width = largeur;
                mur[i].Height = hauteur;
                mur[i].VerticalAlignment = VerticalAlignment.Top;
                mur[i].HorizontalAlignment = HorizontalAlignment.Left;
                mur[i].Margin = new Thickness(x, y, 0, 0);
                mur[i].Fill = murVertival;
                if (random.Next(3) != 0)
                {
                this.Le_Canvas.Children.Add(mur[i]);
                murCollision[i] = new Rect(x, y, largeur, hauteur);
                }
                
                x = x + 190;
                if(compteur > 3)
                {
                    x = 480;
                    y = y + 195;
                    compteur = 0;
                }
            }
            x = 305;
            y = 210;
            largeur = 175;
            hauteur = 20;
            for (int i = 0; i < murH.Length; i++)
            {
                murH[i] = new Rectangle();
                compteur++;
                murH[i].Width = largeur;
                murH[i].Height = hauteur;
                murH[i].VerticalAlignment = VerticalAlignment.Top;
                murH[i].HorizontalAlignment = HorizontalAlignment.Left;
                murH[i].Margin = new Thickness(x, y, 0, 0);
                murH[i].Fill = murHorizontal;
                if (random.Next(3) != 0) 
                {
                    
                this.Le_Canvas.Children.Add(murH[i]);
                murCollision[i + mur.Length] = new Rect(x, y, largeur, hauteur);
                }
                
                x = x + 190;
                if (compteur > 4)
                {
                    x = 305;
                    y = y + 195;
                    compteur = 0;
                }
            }
            

        }

    }
}
