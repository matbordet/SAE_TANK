using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
using System.Media;

namespace SAE_TANK
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        private const string JOUEUR_UN = "1";
        private const string JOUEUR_DEUX = "2";
        private const int DELAI_ENTRE_TIR = 10;
        private const int DELAI_ENTRE_TP = 200;
        private const int VIE_MUR_RENFORCE = 25;
        private const int DELAI_MIN_COEUR = 500;
        private const int DELAI_MAX_COEUR = 800;

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private bool goLeft_J1, goRight_J1, goUp_J1, goDown_J1, usePowerUp_J1 = false;
        private bool goLeft_J2, goRight_J2, goUp_J2, goDown_J2, usePowerUp_J2 = false;

        private string toucheAvancer1;
        private string toucheAvancer2;
        private string toucheReculer1;
        private string toucheReculer2;
        private string toucheGauche1;
        private string toucheGauche2;
        private string toucheDroite1;
        private string toucheDroite2;
        private string toucheTir1;
        private string toucheTir2;
       


        private BoiteDeDialogue accessBoiteDeDialogue;


        private int Rect_Tank_J1_Speed = 5;
        private int bulletTank1Speed = 15;
        private int delaiTirJ1 = 0;

        private int Rect_Tank_J2_Speed = 5;
        private int bulletTank2Speed = 15;
        private int delaiTirJ2 = 0;
        

        private int vie_J1 = 3;
        private int vie_J2 = 3;
        private int[] vie_mur = new int[40];
        
        private Random random = new Random();
        private int compteur_pouvoir = 0;
        private int duree_entre_pouvoir = 0;
        private bool[] est_apparu = new bool[4];
        private Rectangle[] coeur = new Rectangle[4];

        private string direction_J1 = "S";
        private string direction_J2 = "N";
        public int numero_J1 = 1;
        public int numero_J2 = 1;

        private double tempsdejeu;
        private int compteur = 0;

        Rectangle[] mur = new Rectangle[20];
        Rectangle[] murH = new Rectangle[20];
        Rect[] murCollision = new Rect[46];

        EcrandFin fin = new EcrandFin();

        private List<Rectangle> itemsToRemove = new List<Rectangle>();
        private List<Rectangle> listeTirJ1 = new List<Rectangle>();
        private List<Rectangle> listeTirJ2 = new List<Rectangle>();


        ImageBrush murVertical_R = new ImageBrush();
        ImageBrush murVertical4 = new ImageBrush();
        ImageBrush murVertical3 = new ImageBrush();
        ImageBrush murVertical2 = new ImageBrush();
        ImageBrush murVertical1 = new ImageBrush();
        ImageBrush murHorizontal_R = new ImageBrush();
        ImageBrush murHorizontal4 = new ImageBrush();
        ImageBrush murHorizontal3 = new ImageBrush();
        ImageBrush murHorizontal2 = new ImageBrush();
        ImageBrush murHorizontal1 = new ImageBrush();

        ImageBrush tank1 = new ImageBrush();
        ImageBrush tank2 = new ImageBrush();

        ImageBrush sol = new ImageBrush();
        ImageBrush sprite_vie_J1 = new ImageBrush();
        ImageBrush sprite_vie_J2 = new ImageBrush();
        ImageBrush sprite_coeur = new ImageBrush();
        ImageBrush sprite_interface = new ImageBrush();

        ImageBrush sprite_apartition = new ImageBrush();
        ImageBrush sprite_teleporter = new ImageBrush();

        System.Media.SoundPlayer tir_Piou = new SoundPlayer(AppDomain.CurrentDomain.BaseDirectory + "son/tir_son.wav");
        System.Media.SoundPlayer murSon = new SoundPlayer(AppDomain.CurrentDomain.BaseDirectory + "son/mur_son.wav");
        System.Media.SoundPlayer mortSon = new SoundPlayer(AppDomain.CurrentDomain.BaseDirectory + "son/mort_son.wav");
        System.Media.SoundPlayer touche_Son_1 = new SoundPlayer(AppDomain.CurrentDomain.BaseDirectory + "son/touche_son_1.wav");
        System.Media.SoundPlayer touche_Son_2 = new SoundPlayer(AppDomain.CurrentDomain.BaseDirectory + "son/touche_son_2.wav");
        
     

        
        public MainWindow()
        {
            InitializeComponent();
            InitialiseImage();
            InitialiseMurs();

            







            lb_pause.Visibility = Visibility.Hidden;

            BoiteDeDialogue dialogue = new BoiteDeDialogue();
            dialogue.ShowDialog();
            if(DialogResult == false)
            {
                Application.Current.Shutdown();
            }

            numero_J1 = dialogue.nb_TankJ1;
            numero_J2 = dialogue.nb_TankJ2;

            duree_entre_pouvoir = random.Next(DELAI_MIN_COEUR, DELAI_MAX_COEUR);
            

            Console.WriteLine(murCollision);
            

            lb_J1.Content = dialogue.tb_J1.Text;
            lb_J2.Content = dialogue.tb_J2.Text;

            dispatcherTimer.Tick += GameEngine;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(16);
            dispatcherTimer.Start();
           

        }
        private void GameEngine(object sender, EventArgs e)
        {
            delaiTirJ1++;
            delaiTirJ2++;

            direction_J1 = MoveTank(Rect_Tank_J1, goLeft_J1, goRight_J1, goUp_J1, goDown_J1, Rect_Tank_J1_Speed, numero_J1, direction_J1);
            direction_J2 = MoveTank(Rect_Tank_J2, goLeft_J2, goRight_J2, goUp_J2, goDown_J2, Rect_Tank_J2_Speed, numero_J2, direction_J2);


            CollisionPouvoir(Rect_Tank_J1, "J1");
            CollisionPouvoir(Rect_Tank_J2, "J2");


            foreach (Rectangle x in Le_Canvas.Children.OfType<Rectangle>())
            {
                MoveAndTestBulletTank(x);
                for (int i = 0; i < listeTirJ1.Count; i++)
                {
                    if (CollisionBalleTank(Rect_Tank_J2, listeTirJ1[i], "R") == true || CollisionMurBalle(listeTirJ1[i]) == true)
                    {
                        listeTirJ1.Remove(listeTirJ1[i]);
                    }

                }
                for (int i = 0; i < listeTirJ2.Count; i++)
                {
                    if (CollisionBalleTank(Rect_Tank_J1, listeTirJ2[i], "B") == true || CollisionMurBalle(listeTirJ2[i]) == true)
                    {
                        listeTirJ2.Remove(listeTirJ2[i]);
                    }

                }




            }
            InitialisePouvoir();
            RemoveItemsRemove();
            CollisionMurTank(Rect_Tank_J1, direction_J1);
            CollisionMurTank(Rect_Tank_J2, direction_J2);
            CollisionTankTp(Rect_Tank_J1);
            CollisionTankTp(Rect_Tank_J2);

            TestVieJoueur();
            TestWin();
            Compteur();

        }
        private void Compteur()
        {
            tempsdejeu = Math.Round(tempsdejeu + 0.016,2);
            lb_Compteur.Content = compteur_pouvoir;
            
        }
        
        private void CollisionTankTp(Rectangle x)
        {
            Rect tp1 = new Rect(Canvas.GetLeft(teleporter1), Canvas.GetTop(teleporter1), teleporter1.Width, teleporter1.Height);
            Rect tp2 = new Rect(Canvas.GetLeft(teleporter2), Canvas.GetTop(teleporter2), teleporter2.Width, teleporter2.Height);

            Rect tankRect = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

            compteur++;

            if (tankRect.IntersectsWith(tp1) && compteur > DELAI_ENTRE_TP)
            {
                Canvas.SetLeft(x,Canvas.GetLeft(teleporter2));
                Canvas.SetTop(x, Canvas.GetTop(teleporter2));
                compteur = 0;
            }
            else if (tankRect.IntersectsWith(tp2) && compteur > DELAI_ENTRE_TP)
            {
                Canvas.SetLeft(x, Canvas.GetLeft(teleporter1));
                Canvas.SetTop(x, Canvas.GetTop(teleporter1));
                compteur = 0;
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
            if (e.Key == Key.E)
            {
                usePowerUp_J1 = false;
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
            if (e.Key == Key.NumPad1)
            {
                usePowerUp_J2 = false;
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
                if(delaiTirJ1>DELAI_ENTRE_TIR)
                {
                    delaiTirJ1 = 0;                    
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

                    
                    tir_Piou.Play();

                }
            }
            if(e.Key == Key.P)
            {
                dispatcherTimer.Stop();
                lb_pause.Visibility = Visibility.Visible;

            }
            if(e.Key == Key.Escape)
            {
                dispatcherTimer.Start();
                lb_pause.Visibility = Visibility.Hidden;
            }
            if(e.Key == Key.E)
            {
                usePowerUp_J1 = true;
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
            if (e.Key == Key.NumPad1)
            {
                usePowerUp_J2 = true;
            }
            if (e.Key == Key.NumPad0)
            {
                if(delaiTirJ2>DELAI_ENTRE_TIR)
                {
                    delaiTirJ2 = 0;
                    Rectangle newBullet = new Rectangle
                    {
                        Tag = "bulletTank2",
                        Height = 10,
                        Width = 10,
                        Fill = Brushes.White,
                        Stroke = Brushes.Red
                    };
                    Canvas.SetTop(newBullet, Canvas.GetTop(Rect_Tank_J2) - newBullet.Height + Rect_Tank_J2.Width/ 2);
                    Canvas.SetLeft(newBullet, Canvas.GetLeft(Rect_Tank_J2) + Rect_Tank_J2.Width / 2);
                    Le_Canvas.Children.Add(newBullet);

                   
                    
                    tir_Piou.Play();
                   
                }
            }
        }
        
        public string MoveTank(Rectangle tank,bool goLeft,bool goRight,bool goUp, bool goDown,int speed, int numero, string direction)
        {
            
            if (goLeft && Canvas.GetLeft(tank) > 300 && CollisionMurJoueur(tank) == false)
            {
                direction = "W";
                Canvas.SetLeft(tank, Canvas.GetLeft(tank) - speed);
            }
            else if (goRight && Canvas.GetLeft(tank) + tank.Width < 1250 && CollisionMurJoueur(tank) == false)
            {
                direction = "E";
                Canvas.SetLeft(tank, Canvas.GetLeft(tank) + speed);
            }


            else if (goUp && Canvas.GetTop(tank) > 30 && CollisionMurJoueur(tank) == false)
            {
                direction = "N";
                Canvas.SetTop(tank, Canvas.GetTop(tank) - speed);
            }
            else if (goDown && Canvas.GetTop(tank) + tank.Height < 980 && CollisionMurJoueur(tank) == false)
            {
                direction = "S";
                Canvas.SetTop(tank, Canvas.GetTop(tank) + speed);
            }
            if((string)tank.Tag == JOUEUR_UN)
                tank1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_bleu_" + numero + "_" + direction + ".png"));
           else
                tank2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_rouge_" + numero + "_" + direction + ".png"));
            return direction;

        }



        //public void MovePlayer()
        //{

        //    //deplacement du joueur 1
        //    if (goLeft_J1 && Canvas.GetLeft(Rect_Tank_J1) > 300 && CollisionMurJoueur(Rect_Tank_J1) == false)
        //    {
        //        direction_J1 = "W";
        //        tank1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_bleu_" + numero_J1 + "_"+direction_J1+".png"));
        //        Canvas.SetLeft(Rect_Tank_J1, Canvas.GetLeft(Rect_Tank_J1) - Rect_Tank_J1_Speed);
        //    }
        //    else if (goRight_J1 && Canvas.GetLeft(Rect_Tank_J1) + Rect_Tank_J1.Width < 1250 && CollisionMurJoueur(Rect_Tank_J1) == false)
        //    {
        //        direction_J1 = "E";
        //        tank1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_bleu_"+ numero_J1 + "_"+direction_J1+".png"));
        //        Canvas.SetLeft(Rect_Tank_J1, Canvas.GetLeft(Rect_Tank_J1) + Rect_Tank_J1_Speed);
        //    }


        //    else if (goUp_J1 && Canvas.GetTop(Rect_Tank_J1) > 30 && CollisionMurJoueur(Rect_Tank_J1) == false)
        //    {
        //        direction_J1 = "N";
        //        Canvas.SetTop(Rect_Tank_J1, Canvas.GetTop(Rect_Tank_J1) - Rect_Tank_J1_Speed);
        //        tank1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_bleu_" + numero_J1 + "_" + direction_J1 + ".png"));
        //    }
        //    else if (goDown_J1 && Canvas.GetTop(Rect_Tank_J1) + Rect_Tank_J1.Height < 980 && CollisionMurJoueur(Rect_Tank_J1) == false)
        //    {
        //        direction_J1 = "S";
        //        Canvas.SetTop(Rect_Tank_J1, Canvas.GetTop(Rect_Tank_J1) + Rect_Tank_J1_Speed);
        //        tank1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_bleu_" + numero_J1 + "_" + direction_J1 + ".png"));
        //    }

        //    //deplacement du joueur 2
        //    if (goLeft_J2 && Canvas.GetLeft(Rect_Tank_J2) > 300 && CollisionMurJoueur(Rect_Tank_J2) == false)
        //    {
        //        direction_J2 = "W";
        //        tank2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_rouge_"+ numero_J2 + "_"+direction_J2+".png"));
        //        Canvas.SetLeft(Rect_Tank_J2, Canvas.GetLeft(Rect_Tank_J2) - Rect_Tank_J2_Speed);
        //    }
        //    else if (goRight_J2 && Canvas.GetLeft(Rect_Tank_J2) + Rect_Tank_J2.Width <1250 && CollisionMurJoueur(Rect_Tank_J2) == false)
        //    {
        //        direction_J2 = "E";
        //        tank2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_rouge_"+ numero_J2 + "_"+direction_J2+".png"));
        //        Canvas.SetLeft(Rect_Tank_J2, Canvas.GetLeft(Rect_Tank_J2) + Rect_Tank_J2_Speed);
        //    }


        //    else if (goUp_J2 && Canvas.GetTop(Rect_Tank_J2) > 30 && CollisionMurJoueur(Rect_Tank_J2) == false)
        //    {
        //        direction_J2 = "N";
        //        tank2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_rouge_"+ numero_J2 + "_"+direction_J2+".png"));
        //        Canvas.SetTop(Rect_Tank_J2, Canvas.GetTop(Rect_Tank_J2) - Rect_Tank_J2_Speed);
        //    }
        //    else if (goDown_J2 && Canvas.GetTop(Rect_Tank_J2) + Rect_Tank_J2.Height <980 && CollisionMurJoueur(Rect_Tank_J2) == false)
        //    {
        //        direction_J2 = "S";
        //        tank2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_rouge_"+ numero_J2 + "_"+direction_J2+".png"));
        //        Canvas.SetTop(Rect_Tank_J2, Canvas.GetTop(Rect_Tank_J2) + Rect_Tank_J2_Speed);
        //    }


        //}
        public void MoveAndTestBulletTank(Rectangle x)
        {
            
            //J1----------------------------------------
            if (direction_J1 =="W")
            {
                
                if (x is Rectangle && (string)x.Tag == "bulletTank1")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) + bulletTank1Speed);                    
                    x.Tag = "bullet_W_1";
                    listeTirJ1.Add(x);

                }

            }
            else if (direction_J1 == "E")
            {
                if (x is Rectangle && (string)x.Tag == "bulletTank1")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - bulletTank1Speed);                   
                    x.Tag = "bullet_E_1";
                    listeTirJ1.Add(x);
                }

            }
            else if(direction_J1 == "N")
            {
                if (x is Rectangle && (string)x.Tag == "bulletTank1")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - bulletTank1Speed);                    
                    x.Tag = "bullet_N_1";
                    listeTirJ1.Add(x);
                }
            }
            else if (direction_J1 == "S") 
            {
                if (x is Rectangle && (string)x.Tag == "bulletTank1")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + bulletTank1Speed);   
                    x.Tag = "bullet_S_1";
                    listeTirJ1.Add(x);
                }
            }
           

            if ((string)x.Tag == "bullet_E_1")
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) + bulletTank1Speed);
                Rect bullety = new Rect(Canvas.GetTop(x), Canvas.GetLeft(x), x.Width, x.Height);
                if (Canvas.GetLeft(x) < 300 || Canvas.GetLeft(x) > 1250 )
                {
                    itemsToRemove.Add(x);
                }
            }
            else if ((string)x.Tag == "bullet_W_1")
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) - bulletTank1Speed);
                Rect bullety = new Rect(Canvas.GetTop(x), Canvas.GetLeft(x), x.Width, x.Height);
                if (Canvas.GetLeft(x) < 300 || Canvas.GetLeft(x) > 1250 )
                {
                    itemsToRemove.Add(x);
                }
            }
            else if (x is Rectangle && (string)x.Tag == "bullet_N_1")
            {
                Canvas.SetTop(x, Canvas.GetTop(x) - bulletTank1Speed);
                Rect bulletx = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                if (Canvas.GetTop(x) < 30 || Canvas.GetTop(x) > 980 )
                {
                    itemsToRemove.Add(x);
                }
            }
            else if (x is Rectangle && (string)x.Tag == "bullet_S_1")
            {
                Canvas.SetTop(x, Canvas.GetTop(x) + bulletTank1Speed);
                Rect bulletx = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                if (Canvas.GetTop(x) < 30 || Canvas.GetTop(x) > 980 )
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
                    x.Tag = "bullet_W_2";
                    listeTirJ2.Add(x);

                }

            }
            else if (direction_J2 == "E")
            {
                if (x is Rectangle && (string)x.Tag == "bulletTank2")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - bulletTank1Speed);
                    x.Tag = "bullet_E_2";
                    listeTirJ2.Add(x);
                }

            }
            else if (direction_J2 == "N")
            {
                if (x is Rectangle && (string)x.Tag == "bulletTank2")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - bulletTank1Speed);
                    x.Tag = "bullet_N_2";
                    listeTirJ2.Add(x);
                }
            }
            else if (direction_J2 == "S")
            {
                if (x is Rectangle && (string)x.Tag == "bulletTank2")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + bulletTank1Speed);
                    x.Tag = "bullet_S_2";
                    listeTirJ2.Add(x);
                }
            }
            if ((string)x.Tag == "bullet_E_2")
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) + bulletTank1Speed);
                Rect bullety = new Rect(Canvas.GetTop(x), Canvas.GetLeft(x), x.Width, x.Height);
                if (Canvas.GetLeft(x) < 300 || Canvas.GetLeft(x) > 1250 )
                {
                    // si c’est le cas on l’ajoute à la liste des éléments à supprimer
                    itemsToRemove.Add(x);
                }
            }
            else if ((string)x.Tag == "bullet_W_2")
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) - bulletTank1Speed);
                Rect bullety = new Rect(Canvas.GetTop(x), Canvas.GetLeft(x), x.Width, x.Height);
                if (Canvas.GetLeft(x) < 300 || Canvas.GetLeft(x) > 1250)
                {
                    // si c’est le cas on l’ajoute à la liste des éléments à supprimer
                    itemsToRemove.Add(x);
                }
            }
            else if (x is Rectangle && (string)x.Tag == "bullet_N_2")
            {
                Canvas.SetTop(x, Canvas.GetTop(x) - bulletTank1Speed);
                Rect bulletx = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                if (Canvas.GetTop(x) < 30 || Canvas.GetTop(x) > 980)
                {
                    // si c’est le cas on l’ajoute à la liste des éléments à supprimer
                    itemsToRemove.Add(x);
                }
            }
            else if (x is Rectangle && (string)x.Tag == "bullet_S_2")
            {
                Canvas.SetTop(x, Canvas.GetTop(x) + bulletTank1Speed);
                Rect bulletx = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                if (Canvas.GetTop(x) < 30 || Canvas.GetTop(x) > 980)
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
                    itemsToRemove.Add(balleRect);
                    
                    if (i < 20)
                    {
                        vie_mur[i]--;
                        switch (vie_mur[i])
                        {
                            case 3:                              
                                mur[i].Fill = murVertical3;
                                break;
                            case 2:                                
                                mur[i].Fill = murVertical2;
                                break;
                            case 1:
                                mur[i].Fill = murVertical1;
                                break;
                            case 0:
                                murSon.Play();
                                mur[i].Margin = new Thickness(-100, -100, 0, 0);
                                murCollision[i] = Rect.Empty;
                                break;
                        }                     
                    }
                    else
                    {
                        vie_mur[i]--;
                        switch (vie_mur[i])
                        {
                            case 3:
                                murH[i - mur.Length].Fill = murHorizontal3;
                                break;
                            case 2:   
                                murH[i - mur.Length].Fill = murHorizontal2;
                                break;
                            case 1:
                                murH[i - mur.Length].Fill = murHorizontal1;
                                break;
                            case 0:
                                murSon.Play();

                                murH[i - mur.Length].Margin = new Thickness(-100, -100, 0, 0);
                                murCollision[i] = Rect.Empty;
                                break;

                        }
                        
                        
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
        public void TestVieJoueur()
        {
            if (vie_J1 >=0 && vie_J2 >= 0)
            {
                sprite_vie_J1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + vie_J1 +"_coeur.png"));
                sprite_vie_J2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + vie_J2 +"_coeur.png"));
            }
        }

        public bool CollisionBalleTank(Rectangle tank, Rectangle balleRect,string couleur) 
        {
            Rect tankRect = new Rect(Canvas.GetLeft(tank), Canvas.GetTop(tank), tank.Width, tank.Height);
            Rect balle = new Rect(Canvas.GetLeft(balleRect), Canvas.GetTop(balleRect), balleRect.Width, balleRect.Height);

            if (balle.IntersectsWith(tankRect))
            {
                itemsToRemove.Add(balleRect);
                touche_Son_2.Play();
                if (couleur == "B") { vie_J1--; }
                else if (couleur == "R") { vie_J2--; }
                return true;
                
            }
            return false;
        }
        public void InitialiseMurs()

        {
            Random random = new Random();
            
            int x = 480, y = 40, largeur = 20, hauteur = 170,compteur=0;
            for (int i = 0; i < mur.Length; i++)
            {
                mur[i] = new Rectangle();
                compteur++;
                mur[i].Width = largeur;
                mur[i].Height = hauteur;
                mur[i].VerticalAlignment = VerticalAlignment.Top;
                mur[i].HorizontalAlignment = HorizontalAlignment.Left;
                mur[i].Margin = new Thickness(x, y, 0, 0);
                mur[i].Fill = murVertical4;

                vie_mur[i] = 4;
                if (random.Next(3) != 0)
                {
                    this.Le_Canvas.Children.Add(mur[i]);
                    murCollision[i] = new Rect(x, y, largeur, hauteur);
                    if (random.Next(10) == 1)
                    {
                        vie_mur[i] = VIE_MUR_RENFORCE;
                        mur[i].Fill = murVertical_R;
                    }
                }
                
                x = x + 190;
                if(compteur > 3)
                {
                    x = 480;
                    y = y + 190;
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
                murH[i].Fill = murHorizontal4;

                vie_mur[i+mur.Length] = 4;
                if (random.Next(3) != 0) 
                {
                    this.Le_Canvas.Children.Add(murH[i]);
                    murCollision[i + mur.Length] = new Rect(x, y, largeur, hauteur);
                    if(random.Next(10) == 1)
                    {
                        vie_mur[i + mur.Length] = VIE_MUR_RENFORCE;
                        murH[i].Fill = murHorizontal_R;
                    }
                }
                
                x = x + 190;
                if (compteur > 4)
                {
                    x = 305;
                    y = y + 190;
                    compteur = 0;
                }
            }
            

        }
        public void CollisionPouvoir(Rectangle tank,string joueur)
        {

           Rect tankRect = new Rect(Canvas.GetLeft(tank), Canvas.GetTop(tank), tank.Width, tank.Height);
            for (int i = 0; i < 4; i++)
            {
                if (est_apparu[i] == true)
                {
                    Rect coeurRect = new Rect(Canvas.GetLeft(coeur[i]), Canvas.GetTop(coeur[i]), coeur[i].Width, coeur[i].Height);
                    if (coeurRect.IntersectsWith(tankRect))
                    {
                        if (joueur == "J1" && vie_J1 < 3) { vie_J1++; itemsToRemove.Add(coeur[i]); est_apparu[i] = false; }
                        else if (joueur == "J2" && vie_J2 < 3) { vie_J2++; itemsToRemove.Add(coeur[i]); est_apparu[i] = false; }
                        
                    }
                }
            }
            
        }
        
        public void InitialisePouvoir()
        {
            compteur_pouvoir++;
            int position = random.Next(1,5);
            if (compteur_pouvoir > duree_entre_pouvoir && est_apparu[position-1]==false)
            {
                
                Rectangle pouvoir = new Rectangle();
                pouvoir.Width = 75;
                pouvoir.Height = 75;
                pouvoir.VerticalAlignment = VerticalAlignment.Top;
                pouvoir.HorizontalAlignment = HorizontalAlignment.Left;
                switch (position)
                {
                    case 1:
                        Canvas.SetLeft(pouvoir, Canvas.GetLeft(Apparition1));
                        Canvas.SetTop(pouvoir, Canvas.GetTop(Apparition1));                       
                        break;
                    case 2:
                        Canvas.SetLeft(pouvoir, Canvas.GetLeft(Apparition2));
                        Canvas.SetTop(pouvoir, Canvas.GetTop(Apparition2));
                        break;
                    case 3:
                        Canvas.SetLeft(pouvoir, Canvas.GetLeft(Apparition3));
                        Canvas.SetTop(pouvoir, Canvas.GetTop(Apparition3));
                        break;
                    case 4:
                        Canvas.SetLeft(pouvoir, Canvas.GetLeft(Apparition4));
                        Canvas.SetTop(pouvoir, Canvas.GetTop(Apparition4));
                        break;
                }
                Le_Canvas.Children.Add(pouvoir);
                pouvoir.Fill = sprite_coeur;
                est_apparu[position - 1] = true;
                compteur_pouvoir = 0;
                duree_entre_pouvoir = random.Next(DELAI_MIN_COEUR, DELAI_MAX_COEUR);
                coeur[position-1] = pouvoir;
            }
        }
        public void TestWin()
        {
            if (vie_J1 == 0 || vie_J2 == 0)
            {
                dispatcherTimer.Stop();
                fin.ShowDialog();
                if (DialogResult == false)
                {
                    Application.Current.Shutdown();
                }
            }
        }
        private void InitialiseImage()
        {
            tank1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_bleu_" + numero_J1 + "_S.png"));
            tank2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image_Tanks/Tank_rouge_" + numero_J2 + "_N.png"));
            sol.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "sol.png"));

            sprite_interface.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Interface_Utilisateur.png"));
            sprite_apartition.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "plaque_aparition.png"));
            sprite_teleporter.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "portal.png"));

            murVertical_R.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "mur_vertical_R.jpg"));
            murVertical4.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "mur_vertical4.jpg"));
            murVertical3.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "mur_vertical3.jpg"));
            murVertical2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "mur_vertical2.jpg"));
            murVertical1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "mur_vertical1.jpg"));

            murHorizontal_R.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "mur_hor_R.jpg"));
            murHorizontal4.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "mur_hor4.jpg"));
            murHorizontal3.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "mur_hor3.jpg"));
            murHorizontal2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "mur_hor2.jpg"));
            murHorizontal1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "mur_hor1.jpg"));

            sprite_vie_J1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "3_coeur.png"));
            sprite_vie_J2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "3_coeur.png"));
            sprite_coeur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "coeur_Plein.png"));

            Apparition1.Fill = sprite_apartition;
            Apparition2.Fill = sprite_apartition;
            Apparition3.Fill = sprite_apartition;
            Apparition4.Fill = sprite_apartition;
            teleporter1.Fill = sprite_teleporter;
            teleporter2.Fill = sprite_teleporter;

            interfaceB.Fill = sprite_interface;
            interfaceR.Fill = sprite_interface;
            fond_Arene.Fill = sol;
            Rect_Tank_J1.Fill = tank1;
            Rect_Tank_J2.Fill = tank2;
            coeur_J1.Fill = sprite_vie_J1;
            coeur_J2.Fill = sprite_vie_J2;
        }

    }
}
