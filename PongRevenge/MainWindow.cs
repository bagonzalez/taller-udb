using System;
using Gtk;

namespace PongRevenge
{
public partial class MainWindow: Gtk.Window
{
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void playInit (object sender, EventArgs e)
	{
		GameWindow win = new GameWindow ();
		win.Show();
			this.Destroy();
	}

		protected void multiBtn (object sender, EventArgs e)
		{
			GameMultiplayerWindow win = new GameMultiplayerWindow ();
			win.Show ();
			this.Destroy ();
		}
}
}
