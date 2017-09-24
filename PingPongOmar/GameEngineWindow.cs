using System;
using Gtk;
using Cairo;
using System.Threading;


namespace PongRevenge
{
	public partial class GameEngineWindow : Gtk.Window
	{	
		DrawingArea darea = new DrawingArea();
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
		int puntajePlayer1 = 0;
		int puntajePlayer2 = 0;
		int rando=1;
		Random rand;


		//TODO: El ancho y el alto son la mitad de la altura real
		//porque el eje x & y en 0 se encuentra en el centro
		//de la pantalla
		int width = 300;
		int height = 200;



		public GameEngineWindow () : base (Gtk.WindowType.Toplevel)
		{
			rand = new Random();
			darea.ExposeEvent += OnExpose;
			KeyPressEvent += KeyPress;
			SetDefaultSize(width * 2, height * 2);
			diametroBola = radioBola * 2;
			SetPosition(WindowPosition.Center);
			DeleteEvent += delegate { Application.Quit(); };;
			Thread loopFrames = new Thread (new ThreadStart (DoBackgroundWork));
			loopFrames.Start ();

		}

		[GLib.ConnectBefore]
		protected void KeyPress(object sender, KeyPressEventArgs args)
		{

			if (args.Event.Key == Gdk.Key.Down)
			{
				if (yPlayer1 == height-40)
				{

				}
				else { 
				yPlayer1 += 20;
				}
			}
			if (args.Event.Key == Gdk.Key.Up) {
				if (yPlayer1 == -height)
				{

				}
				else {
					yPlayer1 -= 20;
				}
			}


		}

		protected void OnDeleteEvent(object sender, DeleteEventArgs a)
		{
			KeyPressEvent -= KeyPress;
			Application.Quit();
			a.RetVal = true;
		}

		void newMakeFrame(){
			
			Remove (darea);
			Add(darea);
			ShowAll();
		}

		void OnExpose(object sender, ExposeEventArgs args)
		{
			DrawingArea area = (DrawingArea) sender;
			Cairo.Context cr =  Gdk.CairoHelper.Create(area.GdkWindow);
			cr.Translate(width, height);
			//cr.LineWidth = 9;

			cr.SetSourceRGB(0,0,0);
			cr.Rectangle(-width, -height, width*2, height*2);
			cr.StrokePreserve();
			cr.Fill();

			//TODO:player1
			cr.SetSourceRGB(0.1, 0.8, 0);
			cr.Rectangle(xPlayer1, yPlayer1, 10, 40);
			cr.StrokePreserve();
			cr.Fill();

			//TODO:player2
			cr.SetSourceRGB(1, 1, 0);
			cr.Rectangle(xPlayer2, yPlayer2, 10, 40);
			cr.StrokePreserve();
			cr.Fill();

			//TODO:ball
			cr.SetSourceRGB(1, 0.5, 0);
			cr.Arc(x, y, 10, 0, 2*Math.PI);
			cr.StrokePreserve();
			cr.Fill();



			cr.SetSourceRGB(1,1,1);
			cr.SelectFontFace("Free Sans", FontSlant.Normal, FontWeight.Bold);
			cr.SetFontSize(20);
			cr.MoveTo(-260, -height + 30);
			cr.ShowText("Score:   "+puntajePlayer1);
			cr.Fill();

		

			cr.SetSourceRGB(1, 1, 1);
			cr.SelectFontFace("Free Sans", FontSlant.Normal, FontWeight.Bold);
			cr.SetFontSize(20);
			cr.MoveTo(170, -height + 30);
			cr.ShowText("Score 2:  " + puntajePlayer2);
			cr.Restore();



			if (puntajePlayer1 == 6) {


				gameActive = false;
			
			}



			if (puntajePlayer2 == 6)
			{


				gameActive = false;



			}

			cr.Fill();


			((IDisposable) cr.Target).Dispose();                                      
			((IDisposable) cr).Dispose();
		}

	
		public void DoBackgroundWork ()
		{
			
			do {
				Thread.Sleep (50);
				if(x > width - diametroBola){
					x = 0;
					y = 0;
					direccionHorizontal = -1;
					puntajePlayer1++;
				}

				if(x < -width + diametroBola){
					x = 0;
					y = 0;
					direccionHorizontal = 1;
					puntajePlayer2++;
				}

				if(y > height - diametroBola){
					 direccionVertical = -1;
				}

				if(y < -height + diametroBola){
					direccionVertical = 1;
				}

				if (x < xPlayer1 + 20 &&(y > yPlayer1 - 30 && y < yPlayer1 + 30)
				) {
					direccionHorizontal = 1;
				}

				if (x > xPlayer2 - 20  &&(y > yPlayer2 - 30 && y < yPlayer2 + 30)
				) {
					direccionHorizontal = -1;
				}


				if (yPlayer2 < y)
				{
					yPlayer2 += velocidad - 1 * rando;
				}

				if (yPlayer2 > y)
				{
					yPlayer2 -= velocidad - 1 * rando;
				}


				if (x < 0)
				{
					if (rand.Next(0, 2) == 0)
					{
						rando = -1;
					}
					else {
						rando = 1;
					}
				}


				x += direccionHorizontal*velocidad;
				y += direccionVertical*velocidad;
				//yPlayer2 = y-10;
				Gtk.Application.Invoke (delegate {newMakeFrame ();});

				Console.WriteLine(x+","+ y);
			} while (gameActive);
		}


			
			
	}
}

