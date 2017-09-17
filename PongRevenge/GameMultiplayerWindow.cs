using System;
using Gtk;
using Cairo;
using System.Threading;
using System.ServiceModel;
using ServiceMultiGame;

namespace PongRevenge
{
	public partial class GameMultiplayerWindow : Gtk.Window
	{	

		ServiceMultiplayerClient client;
		DrawingArea darea = new DrawingArea();
		string userName = "";
		int x = 0;
		int y = 0;
		int velocidad = 10;
		int direccionHorizontal = 1;
		int direccionVertical = 1;
		int radioBola = 10;
		int diametroBola = 0;
		bool gameActive = true;
		int yPlayer1 = 0;
		int xPlayer1 = -270;
		int yPlayer2 = 0;
		int xPlayer2 = 260;
		int scorePlayer1 = 0;
		int scorePlayer2 = 0;
		int framesGames = 20;
		int positionPlayer1 = 0;
		Random rnd;
		Thread loopFrames;


		public static int visitanteY = 0;
		public static string player1Token = "";

		//TODO: El ancho y el alto son la mitad de la altura real
		//porque el eje x & y en 0 se encuentra en el centro
		//de la pantalla
		int width = 300;
		int height = 200;
		
		public GameMultiplayerWindow (string userName, int numPlayer) :base (Gtk.WindowType.Toplevel)
		{
			
			this.Build ();

			if (numPlayer == 1)
			{
				direccionHorizontal = 1;
			}
			else {

				direccionHorizontal = -1;
			}

			InstanceContext instanceContext = new InstanceContext(new OperationService());

			//Inicia cliente
			client = new ServiceMultiplayerClient(instanceContext);
			this.userName = userName;
			Console.WriteLine("Press <ENTER> to terminate client once the output is displayed.");
			Console.WriteLine();

			// Call the AddTo service operation.
			client.Register();
			initComunication();

			KeyPressEvent += KeyPress;
			rnd = new Random();
			SetDefaultSize(width * 2, height * 2);
			diametroBola = radioBola * 2;
			SetPosition(WindowPosition.Center);
			DeleteEvent += delegate { Application.Quit(); }; ;
			darea.ExposeEvent += OnExpose;
			loopFrames = new Thread(new ThreadStart(DoBackgroundWork));
			loopFrames.Start();
			Console.WriteLine(userName);
		}

		private void initComunication()
		{
			player1Token = client.RegisterUser(userName);
			Console.WriteLine("Player token " + player1Token);
			Console.WriteLine("Player name " + userName);
			string room = client.GetRoom();
			if (room == "")
			{
				room = client.CreateRoom("juego");
			}

			client.PlayGame(player1Token, room);
			Console.WriteLine("room" + room);
		}

		[GLib.ConnectBefore]
		protected void KeyPress(object sender, KeyPressEventArgs args)
		{

			if (args.Event.Key == Gdk.Key.Down)
			{
				positionPlayer1 = 1;
				client.KeyDown(player1Token);
				Console.WriteLine(visitanteY);
			}

			if (args.Event.Key == Gdk.Key.Up)
			{
				positionPlayer1 = -1;
				client.KeyUp(player1Token);
				Console.WriteLine(visitanteY);
			}


		}

		protected void OnDeleteEvent(object sender, DeleteEventArgs a)
		{
			KeyPressEvent -= KeyPress;
			Application.Quit();
			a.RetVal = true;
		}

		void newMakeFrame()
		{

			Remove(darea);
			Add(darea);


			ShowAll();
		}

		void OnExpose(object sender, ExposeEventArgs args)
		{
			DrawingArea area = (DrawingArea)sender;
			Cairo.Context cr = Gdk.CairoHelper.Create(area.GdkWindow);
			cr.Translate(width, height);
			//cr.LineWidth = 9;

			cr.Save();
			//TODO: Fondo
			cr.SetSourceRGB(0.1, 0.5, 0.5);
			cr.Rectangle(-width, -height, width * 2, height * 2);
			cr.StrokePreserve();
			cr.Fill();
			cr.Restore();

			cr.Save();
			//TODO:Disenio
			cr.SetSourceRGB(1, 1, 1);
			cr.Rectangle(0, -height, 2, height * 2);
			cr.StrokePreserve();
			cr.Fill();
			cr.Restore();

			cr.Save();
			//TODO:player1
			cr.SetSourceRGB(0.1, 0.8, 0);
			cr.Rectangle(xPlayer1, yPlayer1, 10, 40);
			cr.StrokePreserve();
			cr.Fill();
			cr.Restore();

			cr.Save();
			//TODO:player2

			cr.SetSourceRGB(1, 1, 0);
			cr.Rectangle(xPlayer2, yPlayer2, 10, 40);
			cr.StrokePreserve();
			cr.Fill();
			cr.Restore();

			cr.Save();
			//TODO:ball
			cr.SetSourceRGB(1, 0.5, 0);
			cr.Arc(x, y, 10, 0, 2 * Math.PI);
			cr.StrokePreserve();
			cr.Fill();
			cr.Restore();

			cr.Save();
			//TODO: Score 1
			cr.SetSourceRGB(1, 1, 1);
			cr.SelectFontFace("Free Sans", FontSlant.Normal, FontWeight.Bold);
			cr.SetFontSize(20);
			cr.MoveTo(-60, -height + 30);
			cr.ShowText(scorePlayer1.ToString());
			cr.Restore();

			cr.Save();
			//TODO: Score 2
			cr.SetSourceRGB(1, 1, 1);
			cr.SelectFontFace("Free Sans", FontSlant.Normal, FontWeight.Bold);
			cr.SetFontSize(20);
			cr.MoveTo(50, -height + 30);
			cr.ShowText(scorePlayer2.ToString());
			cr.Fill();
			cr.Restore();

			cr.Save();
			if (scorePlayer2 >= 10)
			{
				cr.SetSourceRGB(1, 1, 1);
				cr.SelectFontFace("Free Sans", FontSlant.Normal, FontWeight.Bold);
				cr.SetFontSize(20);
				cr.MoveTo(-100, 0);
				cr.ShowText("El player 2 ha ganado!");
				cr.Fill();
				gameActive = false;

				/*MessageDialog md = new MessageDialog(this, 
					DialogFlags.DestroyWithParent, MessageType.Info, 
					ButtonsType.Close, "Gano el jugador 1");
				md.Run();
				md.Destroy();
				*/
				loopFrames.Abort();
			}

			if (scorePlayer1 >= 10)
			{
				cr.SetSourceRGB(1, 1, 1);
				cr.SelectFontFace("Free Sans", FontSlant.Normal, FontWeight.Bold);
				cr.SetFontSize(20);
				cr.MoveTo(-100, 0);
				cr.ShowText("El player 1 ha ganado!");
				cr.Fill();
				gameActive = false;
				/*
				MessageDialog md = new MessageDialog(this, 
					DialogFlags.DestroyWithParent, MessageType.Info, 
					ButtonsType.Close, "Gano el jugador 2");
				md.Run();
				md.Destroy();
*/
				loopFrames.Abort();
			}
			cr.ClosePath();
			cr.Restore();
			((IDisposable)cr.Target).Dispose();
			((IDisposable)cr).Dispose();

		}

		public void DoBackgroundWork()
		{

			do
			{
				Thread.Sleep(1000 / framesGames);
				if (x > width - diametroBola)
				{
					direccionHorizontal = -1;
					scorePlayer1++;
				}

				if (x < -width + diametroBola)
				{
					direccionHorizontal = 1;
					scorePlayer2++;
				}

				if (y > height - diametroBola)
				{
					direccionVertical = -1;
				}

				if (y < -height + diametroBola)
				{
					direccionVertical = 1;
				}

				if (x < xPlayer1 + 20 && (y > yPlayer1 - 10 && y < yPlayer1 + 60)
				)
				{
					direccionHorizontal = 1;
				}

				yPlayer2 += (7) * visitanteY;
				yPlayer1 += (7) * positionPlayer1;

				if (x > xPlayer2 - 20 && (y > yPlayer2 - 10 && y < yPlayer2 + 60)
				)
				{
					direccionHorizontal = -1;
				}


				x += direccionHorizontal * velocidad;
				y += direccionVertical * velocidad;



				Gtk.Application.Invoke(delegate { newMakeFrame(); });
			} while (gameActive);
		}

	}
}

