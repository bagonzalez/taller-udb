using System;
using Gtk;
using pingpongproject_ed;

public partial class MainWindow : Gtk.Window
{
	public MainWindow() : base(Gtk.WindowType.Toplevel)
	{
		Build();
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}

	protected void onClick(object sender, EventArgs e)
	{
		Console.WriteLine("Que paso amiguito");
		MainGame win = new MainGame();
		win.Show();

	}
}
