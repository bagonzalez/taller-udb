using System;

namespace PongRevenge
{
	public partial class GameMultiplayerWindow : Gtk.Window
	{
		public GameMultiplayerWindow () :
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
	}
}

