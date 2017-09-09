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
        int player1X, player1Y, player2X, player2Y;
        int playersHeight = 60, playersWidth = 10;
        int ballSpeed = 10;
        int ballRadius = 10, ballDiameter = 20;
		int Speed_Player = 10;                           //Dont change these, change them from the settings page
		int Speed_Enemy = 8;
        int ballHorizontalDirection = 1, ballVerticalDirection = 1;
        int scorePlayer1 = 0, scorePlayer2 = 0;
        Boolean Player_Up, Player_Down = false;        //Booleans to see if player is going up or down
        Boolean renderGame = true;
        Boolean GameOn = false;
        Boolean playerOneWon = false, playerTwoWon = false;

        public MainGame() :
                base(Gtk.WindowType.Toplevel)
        {
            SetDefaultSize(width * 2, height * 2);
            SetPosition(WindowPosition.Center);
            //probando input de usuario
            KeyPressEvent += Keypress;
            KeyReleaseEvent += KeyRelease;
            ThreadStart getInput = new ThreadStart(handle_input);
            ThreadStart moveScene = new ThreadStart(move_scene);
            ThreadStart renderScene = new ThreadStart(render_scene);
            Thread inputThread = new Thread(getInput);
            Thread sceneThread = new Thread(moveScene);
            Thread renderThread = new Thread(renderScene);
            inputThread.Start();
			sceneThread.Start();			
            renderThread.Start();

            DeleteEvent += delegate { 
                Application.Quit();
                inputThread.Abort();
                sceneThread.Abort();
                renderThread.Abort();            
            };
        }

        [GLib.ConnectBefore]
        private void KeyRelease(object o, KeyReleaseEventArgs args)
        {
			//Console.WriteLine(args.Event.Key);

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
            //Console.WriteLine(args.Event.Key);

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
            player1X = -width + 60;
            player2X = width - 60;
            DrawingArea drawingAr = (DrawingArea)sender;
            Cairo.Context cairo = Gdk.CairoHelper.Create(drawingAr.GdkWindow);
            cairo.Translate(width, height);

            cairo.SetSourceRGB(0.0, 0.0, 0.2);
            cairo.Rectangle(-width, -height, width * 2, height * 2);
            cairo.StrokePreserve();
            cairo.Fill();

            //BALL
			cairo.SetSourceRGB(0, 0.5, 1);
			cairo.Arc(ballX, ballY, ballRadius, 0, 2 * Math.PI);
			cairo.StrokePreserve();
			cairo.Fill();

            //PLAYER 1
            cairo.SetSourceRGB(0.6, 0.5, 0);
            cairo.Rectangle(player1X, player1Y, playersWidth, playersHeight);
            cairo.StrokePreserve();
            cairo.Fill();

			//PLAYER 2
			cairo.SetSourceRGB(0.6, 0.5, 0);
            cairo.Rectangle(player2X, player2Y, playersWidth, playersHeight);
			cairo.StrokePreserve();
			cairo.Fill();

			//Scores
			cairo.SetSourceRGB(1, 1, 1);
            cairo.SelectFontFace("Purisa", FontSlant.Normal, FontWeight.Bold);
            cairo.SetFontSize(13);
            cairo.MoveTo(-width+10, -height+30);
            cairo.ShowText("Player 1: " + scorePlayer1);
            cairo.MoveTo(-width+10, -height + 60);
            cairo.ShowText("Player 2: " + scorePlayer2);

            if(playerOneWon){
				cairo.SetSourceRGB(1, 1, 1);
				cairo.SelectFontFace("Purisa", FontSlant.Normal, FontWeight.Bold);
				cairo.SetFontSize(15);
				cairo.MoveTo(-50, 0);
				cairo.ShowText("Player One Won : " + scorePlayer1);
            }

            if(playerTwoWon){
				cairo.SetSourceRGB(1, 1, 1);
				cairo.SelectFontFace("Purisa", FontSlant.Normal, FontWeight.Bold);
				cairo.SetFontSize(15);
				cairo.MoveTo(-50, 0);
                cairo.ShowText("Player Two Won : " + scorePlayer2);
            }

            ((IDisposable)cairo.GetTarget()).Dispose();
            ((IDisposable)cairo).Dispose();

        }

        //Thread solo para manejar el input del usuario
        void handle_input(){
            do
            {
                Thread.Sleep(100);
                if (GameOn)
                {
                    move_player();
                }
            } while (renderGame);
        }

        //Thread para calcular todas las variables de la esfera
        void move_scene(){
            do
            {
                Thread.Sleep(100);
                if(GameOn){
                    move_ball();                
                    }
            } while (renderGame);
        }

        //Thread para crear y borrar frames
		void render_scene()
		{
			do
			{
				    Thread.Sleep(100);	
					Gtk.Application.Invoke(delegate {
						MakeNewFrame();
					});
				
			} while (renderGame);
		}

        void move_ball(){
            //Begin, Box collisions
			if (ballX > width - ballDiameter)
			{
				ballHorizontalDirection = -1;
                scorePlayer1 += 10;
			}

			if (ballX < -width + ballDiameter)
			{
				ballHorizontalDirection = 1;
                scorePlayer2 += 10;
			}

			if (ballY > height - ballDiameter)
			{
				ballVerticalDirection = -1;
			}

			if (ballY < -height + ballDiameter)
			{
				ballVerticalDirection = 1;
			}
			//End, Box collisions
            if (DidCollideWithPlayer2())
			{
				ballHorizontalDirection = -1;
			}

            if (DidCollideWithPlayer1())
			{
				ballHorizontalDirection = 1;
			}
            //Begin Player collisions
			ballX += ballHorizontalDirection * ballSpeed;
			ballY += ballVerticalDirection * ballSpeed;
            player2Y += ballVerticalDirection * Speed_Enemy;

            if(scorePlayer1 == 50 || scorePlayer2 == 50){
                GameOn = false;
                if(scorePlayer1 == 50){
                    playerOneWon = true;
                }

                if(scorePlayer2 == 50){
                    playerTwoWon = true;
                }

            }

            }

        private bool DidCollideWithPlayer2(){
            bool Collision = false;
            if ((ballX > player2X) &&
                (ballY >= player2Y) &&
                (ballY <= player2Y + playersHeight))
            {
                Collision = true;
            }
            return Collision;
        }

        private bool DidCollideWithPlayer1(){
            bool Collision = false;
            if ((ballX < player1X + playersWidth) &&
                (ballY >= player1Y) &&
                (ballY <= player1Y + playersHeight))
            {
                Collision = true;
            }
            return Collision;
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

            }
            if(Player_Down){
                if(player1Y > height -50){
                    Speed_Player = 0;
                } else{
                Speed_Player = 10;
                    }
                player1Y += Speed_Player;

            }


        }
    }
}
