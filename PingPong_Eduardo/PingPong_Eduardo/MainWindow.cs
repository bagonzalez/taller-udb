using System;
using Gtk;
using PingPong_Eduardo;

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

    protected void openGameWindow(object sender, EventArgs e)
    {
        MainGame game = new MainGame();
        game.Show();
        this.Destroy();
    }
}
