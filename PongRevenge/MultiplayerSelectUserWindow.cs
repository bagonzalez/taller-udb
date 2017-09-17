using System;
using Gtk;
namespace PongRevenge
{
	public partial class MultiplayerSelectUserWindow : Gtk.Window
	{
		
		public MultiplayerSelectUserWindow() :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
		}

		protected void OnDeleteEvent(object sender, DeleteEventArgs a)
		{
			MainWindow win = new MainWindow();
			win.Show();
			Application.Run();
			this.Destroy();

		}

		protected void OnButton1Clicked(object sender, EventArgs e)
		{
			string nameUser = this.entry1.Text;
			int numberPlayer = 1;
			if (numberPlayer == 1 || numberPlayer == 2)
			{
				GameMultiplayerWindow win = new GameMultiplayerWindow(nameUser, numberPlayer);
			}
			else 
			{
				label1.Text = "El numero de jugador no es valido";
			}

		}

		protected void OnNumEntryTextDeleted(object o, TextDeletedArgs args)
		{
		}
	}
}
