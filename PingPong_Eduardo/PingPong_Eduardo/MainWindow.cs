using System;
using Gtk;
using PingPong_Eduardo;

public partial class MainWindow : Gtk.Window
{
    string usernameString; 
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
        this.buttonGame.Sensitive = false;
        this.buttonMultiplayer.Sensitive = false;
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

    protected void newUser(object sender, EventArgs e)
    {
		if (username.Text.Equals("")||username.Text.Equals(" ")){
			userLabel.TextWithMnemonic = "Usuario invalido"; 
		}
		else
		{
			this.buttonGame.Sensitive = true;
			this.buttonMultiplayer.Sensitive = true;
			usernameString = username.Text;
			this.username.Sensitive = false;
			this.buttonNewUser.Sensitive = false;
			userLabel.TextWithMnemonic = "Bienvenid@ " + usernameString;
			Console.WriteLine(usernameString);
		}
    }

    protected void promptMultiplayer(object sender, EventArgs e)
    {
		GameMultiplayer multiplayer = new GameMultiplayer();
		multiplayer.Show();
		this.Destroy();
    }
}
