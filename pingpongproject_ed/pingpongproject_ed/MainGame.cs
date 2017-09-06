using System;
using Gdk;

namespace pingpongproject_ed
{
	public partial class MainGame : Gtk.Window
	{
		public MainGame() :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
		}


		Gtk.Image[] Score_Player = new Gtk.Image[5];  //Array to hold the score pictureboxes
		Gtk.Image[] Score_Enemy = new Gtk.Image[5];


	}
}
