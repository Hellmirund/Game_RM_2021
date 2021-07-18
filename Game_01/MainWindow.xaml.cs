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

namespace Game_01
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // create a new instance of the dispatcher timer class called gametimer
        DispatcherTimer gameTimer = new DispatcherTimer();

        // create three new Rect class instance called player hit box and ground hit box and obstacle hit box
        Rect playerHitBox;
        Rect groundHitBox;
        Rect obstacleHitBox;

        // create a new boolean called jumping, by default this will set to false
        bool jumping;
        // make a new integer called force and set the value to 20
        int force = 20;
        // make another integer called speed and set the value to 5
        int speed = 5;
        // create a new instance of the random class called rand
        Random rand = new Random();
        // game over boolean
        bool gameover = false;
        // make a sprite int double variable, this will be used to swap the sprites for player
        double spriteInt = 0;

        // make three image brush instances called player sprite, background sprite and obstacle sprite
        ImageBrush playerSprite = new ImageBrush();
        ImageBrush backgroundSprite = new ImageBrush();
        ImageBrush obstacleSprite = new ImageBrush();

        //integer array which we will use to change the obstacle position on screen
        int[] obstaclePosition = { 320, 310, 300, 305, 315 };
        // empty integer called score
        int score = 0;

        public MainWindow()
        {
            InitializeComponent();
            //set the focus on my canvas from the WPF
            myCanvas.Focus();

            // assign the game engine event to the game timer tick
            gameTimer.Tick += GameEngine;
            // set the game timer interval to 20 milliseconds
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);

            // first set the background sprite image
            backgroundSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/background.gif"));
            // add the background sprite to both rectangles
            background.Fill = backgroundSprite;
            background2.Fill = backgroundSprite;
            // run the start game function
            StartGame();
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && gameover)
            {
                // run the start game function
                StartGame();
            }
        }

        private void Canvas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && !jumping && Canvas.GetTop(player) > 260)
            {
                // set jumping to true
                jumping = true;
                // set force integer to 15
                force = 30;
                // set speed integer to -12
                speed = -12;
                // change the player sprite so it looks like he's jumping
                playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/newRunner_02.gif"));
            }
        }

        private void StartGame()
        {
            // this is the start game function

            Canvas.SetLeft(background, 0); // set the first background to 0
            Canvas.SetLeft(background2, 1262); // set the second background to 1262

            // set the player x to 110 and y to 140
            Canvas.SetLeft(player, 110);
            Canvas.SetTop(player, 140);

            // set the obstacle x to 950 and y to 310
            Canvas.SetLeft(obstacle, 950);
            Canvas.SetTop(obstacle, 310);
            // set run sprite function to 1
            RunSprite(1);

            // set the obstacle sprite, load the image from the images folder
            obstacleSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/obst2.gif"));
            obstacle.Fill = obstacleSprite; // assign the obstacle sprite to the obstacle object 

            // set jumping to false
            jumping = false;
            // set game over to false
            gameover = false;
            // set score to 0
            score = 0;
            // set the score text to the score integer
            scoreText.Content = "Score: " + score;

            // start the game timer
            gameTimer.Start();
        }

        private void GameEngine(object sender, EventArgs e)
        {
            // move the player character down using the speed integer
            Canvas.SetTop(player, Canvas.GetTop(player) + speed);
            // move the background 3 pixels to the left each tick
            Canvas.SetLeft(background, Canvas.GetLeft(background) - 3);
            Canvas.SetLeft(background2, Canvas.GetLeft(background2) - 3);
            // move the obstacle rectangle to the left 12 pixels per tick
            Canvas.SetLeft(obstacle, Canvas.GetLeft(obstacle) - 12);
            // link the score text label to the score integer
            scoreText.Content = "Score: " + score;

            // assign the player hit box to the player, gound hit box to the ground rectangle and obstacle hit box to the obstacle rectangle
            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
            groundHitBox = new Rect(Canvas.GetLeft(ground), Canvas.GetTop(ground), ground.Width, ground.Height);
            obstacleHitBox = new Rect(Canvas.GetLeft(obstacle), Canvas.GetTop(obstacle), obstacle.Width, obstacle.Height);

            // check player and ground collision
            // IF player hits the ground 
            if (playerHitBox.IntersectsWith(groundHitBox))
            {
                //if the player is on the ground set the speed to 0
                speed = 0;
                // place the character on top of the ground rectangle
                Canvas.SetTop(player, Canvas.GetTop(ground) - player.Height);
                // set jumping to false
                jumping = false;
                // add .5 to the sprite int double
                spriteInt += .5;
                // if the sprite int goes above 8
                if (spriteInt > 8)
                {
                    // reset the sprite int to 1
                    spriteInt = 1;
                }
                // pass the sprite int values to the run sprite function
                RunSprite(spriteInt);
            }

            //if the player hit the obstacle
            if (playerHitBox.IntersectsWith(obstacleHitBox))
            {
                // set game over boolean to true
                gameover = true;
                // stop the game timer
                gameTimer.Stop();

            }

            //if jumping boolean is true
            if (jumping)
            {
                // set speed integer to -9 so the player will go upwards
                speed = -9;
                // reduce the force integer
                force--;
            }
            else
            {
                // if jumping is not true then set speed to 12
                speed = 12;
            }

            // if force is less than 0 
            if (force < 0)
            {
                // set jumping boolean to false
                jumping = false;
            }

            // parallax scrolling code for c#
            // the code below will scroll the background simlutaniously and make it seem endless

            // check the first background
            // if the first background X position goes below -1262 pixels
            if (Canvas.GetLeft(background) < -1262)
            {
                // position the first background behind the second background
                // below we are setting the backgrounds left, to background2 width position
                Canvas.SetLeft(background, Canvas.GetLeft(background2) + background2.Width);
            }
            // we do the same for the background 2
            // if background 2 X position goes below -1262
            if (Canvas.GetLeft(background2) < -1262)
            {
                // position the second background behind the first background
                // below we are setting background 2s left position or X position to backgrounds width position
                Canvas.SetLeft(background2, Canvas.GetLeft(background) + background.Width);
            }

            // if the obstacle goes beyond -50 location
            if (Canvas.GetLeft(obstacle) < -50)
            {
                // set the left position of the obstacle to 950 pixels
                Canvas.SetLeft(obstacle, 950);
                // randomly set the top positio of the obstacle from the array we created earlier
                // this will randomly pick a position from the array so it won't be the same each time it comes around the screen
                Canvas.SetTop(obstacle, obstaclePosition[rand.Next(0, obstaclePosition.Length)]);
                // add 1 to the score
                score += 1;
            }

            // if the game over boolean is set to true
            if (gameover)
            {
                // draw a black border around the obstacle
                // and set the border size to 1 pixel
                obstacle.Stroke = Brushes.Black;
                obstacle.StrokeThickness = 1;

                // draw a red border around the player
                // and set the border size to 1 pixel
                player.Stroke = Brushes.Red;
                player.StrokeThickness = 1;
                // add the following to the existing score text label
                scoreText.Content += "   Press Enter to retry";
            }
            else
            {
                // if the game is not order then reset the border thickness to 0 pixel
                player.StrokeThickness = 0;
                obstacle.StrokeThickness = 0;
            }
        }

        private void RunSprite(double i)
        {
            switch (i)
            {

                case 1:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/newRunner_01.gif"));
                    break;
                case 2:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/newRunner_02.gif"));
                    break;
                case 3:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/newRunner_03.gif"));
                    break;
                case 4:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/newRunner_04.gif"));
                    break;
                case 5:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/newRunner_05.gif"));
                    break;
                case 6:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/newRunner_05.gif"));
                    break;
                case 7:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/newRunner_07.gif"));
                    break;
                case 8:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/newRunner_08.gif"));
                    break;

            }
            // finally assign the player rectangle to the player sprite
            player.Fill = playerSprite;
        }
    }
}
