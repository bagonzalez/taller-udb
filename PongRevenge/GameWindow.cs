using System;
using Gtk;
using Cairo;
using System.Threading;


namespace PongRevenge
{
	public partial class GameWindow : Gtk.Window
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

		//TODO: El ancho y el alto son la mitad de la altura real
		//porque el eje x & y en 0 se encuentra en el centro
		//de la pantalla
		int width = 300;
		int height = 200;



		public GameWindow () : base (Gtk.WindowType.Toplevel)
		{
			SetDefaultSize(width * 2, height * 2);
			diametroBola = radioBola * 2;
			SetPosition(WindowPosition.Center);
			DeleteEvent += delegate { Application.Quit(); };;
			Thread loopFrames = new Thread (new ThreadStart (DoBackgroundWork));
			loopFrames.Start ();

		}

		void newMakeFrame(){
			darea.ExposeEvent += OnExpose;
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

			cr.SetSourceRGB(0.1, 0.5, 0.5);
			cr.Rectangle(-width, -height, width*2, height*2);
			cr.StrokePreserve();
			cr.Fill();

			//TODO:player1
			cr.SetSourceRGB(0.1, 0.8, 0);
			cr.Rectangle(-width+30, 0, 10, 40);
			cr.StrokePreserve();
			cr.Fill();

			//TODO:player2
			cr.SetSourceRGB(1, 1, 0);
			cr.Rectangle(width-40, 0, 10, 40);
			cr.StrokePreserve();
			cr.Fill();

			//TODO:ball
			cr.SetSourceRGB(1, 0.5, 0);
			cr.Arc(x, y, 10, 0, 2*Math.PI);
			cr.StrokePreserve();
			cr.Fill();

			((IDisposable) cr.Target).Dispose();                                      
			((IDisposable) cr).Dispose();
		}

		public void moveBall(int x, int y){
			if(x > width - diametroBola){
				direccionHorizontal = -1;
			}

			if(x < -width + diametroBola){
				direccionHorizontal = 1;
			}

			if(y > height - diametroBola){
				direccionVertical = -1;
			}

			if(y < -height + diametroBola){
				direccionVertical = 1;
			}

			x += direccionHorizontal*velocidad;
			y += direccionVertical*velocidad;
		}

		public void DoBackgroundWork ()
		{
			
			do {
				Thread.Sleep (100);
				if(x > width - diametroBola){
					direccionHorizontal = -1;
				}

				if(x < -width + diametroBola){
					direccionHorizontal = 1;
				}

				if(y > height - diametroBola){
					direccionVertical = -1;
				}

				if(y < -height + diametroBola){
					direccionVertical = 1;
				}

				x += direccionHorizontal*velocidad;
				y += direccionVertical*velocidad;
				Gtk.Application.Invoke (delegate {newMakeFrame ();});
				Console.WriteLine(x+","+ y);
			} while (gameActive);
		}




			
	}
}

