using System;
using Gtk;

namespace PongRevenge
{
public partial class MainWindow: Gtk.Window
	{
		String user;
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
			GameEngineWindow win = new GameEngineWindow ();
		win.Show();
			this.Destroy();
	}

		protected void registrarse(object sender, EventArgs e)
		{

			user = entry1.Text;
			playBtn.Sensitive = true;
			
		}
	}
}
