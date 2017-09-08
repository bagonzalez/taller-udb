using System;
using Gtk;
using Cairo;
using System.Threading;

namespace PingPong_Eduardo
{
    public partial class MainGame : Gtk.Window
    {
        DrawingArea drawingArea = new DrawingArea();
        int width = 320;
        int height = 240;
        int ballX;
        int ballY;
        int player1X = 0, player1Y = 0;
        int ballSpeed = 10;
        int BallForce = 3;
        int ballRadius = 10, ballDiameter = 20;
		int Speed_Player = 10;                           //Dont change these, change them from the settings page
		int Speed_Enemy = 10;
        int ballHorizontalDirection = 1, ballVerticalDirection = 1;
        Boolean Player_Up, Player_Down = false;         //Booleans to see if player is going up or down
        Boolean BallGoingLeft = true;                   //Is the ball going left or right?
        Boolean renderGame = true;
        Boolean GameOn = false;//Is the game on or paused

        public MainGame() :
                base(Gtk.WindowType.Toplevel)
        {
            SetDefaultSize(width * 2, height * 2);
            SetPosition(WindowPosition.Center);
            DeleteEvent += delegate { Application.Quit(); };
            //probando input de usuario
            KeyPressEvent += Keypress;
            KeyReleaseEvent += KeyRelease;
            ThreadStart childref = new ThreadStart(move_scene);
			Console.WriteLine("In Main: Creating the Child thread");
			Thread childThread = new Thread(childref);
			childThread.Start();			

        }

        [GLib.ConnectBefore]
        private void KeyRelease(object o, KeyReleaseEventArgs args)
        {
			Console.WriteLine(args.Event.Key);

			switch (args.Event.Key)      //Regular key input, if press the right keys it moves in its direction
			{
				case Gdk.Key.W:
				case Gdk.Key.Up:					
                    Player_Up = false;
					break;
				case Gdk.Key.S:
				case Gdk.Key.Down:					
                    Player_Down = false;
					break;				
			}
        }

        [GLib.ConnectBefore]
        private void Keypress(object o, KeyPressEventArgs args)
        {
            Console.WriteLine(args.Event.Key);

            switch (args.Event.Key)      //Regular key input, if press the right keys it moves in its direction
			{
                case Gdk.Key.W:
				case Gdk.Key.Up:
					Player_Down = false;
					Player_Up = true;
					break;
                case Gdk.Key.S:
                case Gdk.Key.Down:
					Player_Up = false;
					Player_Down = true;
					break;
                case Gdk.Key.space:    //If hit space it starts the game,
				GameOn = true;
				break;
			}
        }



        void MakeNewFrame()
		{
			drawingArea.ExposeEvent += OnExpose;
			Remove(drawingArea);
			Add(drawingArea);
			ShowAll();
		}

        void OnExpose(object sender, ExposeEventArgs args)
        {
            DrawingArea drawingAr = (DrawingArea)sender;
            Cairo.Context cairo = Gdk.CairoHelper.Create(drawingAr.GdkWindow);
            cairo.Translate(width, height);

            cairo.SetSourceRGB(0.0, 0.0, 0.2);
            cairo.Rectangle(-width, -height, width * 2, height * 2);
            cairo.StrokePreserve();
            cairo.Fill();

            cairo.SetSourceRGB(0.6, 0.5, 0);
            cairo.Rectangle(-width+60, player1Y, 10, 40);
            cairo.StrokePreserve();
            cairo.Fill();

            cairo.SetSourceRGB(0, 0.5, 1);
            cairo.Arc(ballX, ballY, ballRadius, 0, 2 * Math.PI);
            cairo.StrokePreserve();
            cairo.Fill();

            ((IDisposable)cairo.GetTarget()).Dispose();
            ((IDisposable)cairo).Dispose();

        }

        void move_scene(){
            do
            {
                Thread.Sleep(75);
                if(GameOn){
                    move_player();
                    move_ball();
                Gtk.Application.Invoke(delegate { 
                    MakeNewFrame(); 
                });
                    }
            } while (renderGame);
        }


        void move_ball(){
			if (ballX > width - ballDiameter)
			{
				ballHorizontalDirection = -1;
			}

			if (ballX < -width + ballDiameter)
			{
				ballHorizontalDirection = 1;
			}

			if (ballY > height - ballDiameter)
			{
				ballVerticalDirection = -1;
			}

			if (ballY < -height + ballDiameter)
			{
				ballVerticalDirection = 1;
			}

			ballX += ballHorizontalDirection * ballSpeed;
			ballY += ballVerticalDirection * ballSpeed;
        }


        void move_player(){
            if(Player_Up){
                if (player1Y < -height+10)
                {
                    Speed_Player = -0;
                } else {
                    Speed_Player = -10;
                }
                player1Y += Speed_Player;
                Console.WriteLine(player1Y);
            }
            if(Player_Down){
                if(player1Y > height -10){
                    Speed_Player = 0;
                } else{
                Speed_Player = 10;
                    }
                player1Y += Speed_Player;
                Console.WriteLine(player1Y);
            }


        }
    }
}
