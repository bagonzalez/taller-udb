package com.eduardo.chavez.game.desktop;

import com.badlogic.gdx.backends.lwjgl.LwjglApplication;
import com.badlogic.gdx.backends.lwjgl.LwjglApplicationConfiguration;
import com.eduardo.chavez.game.GameLoader;

public class DesktopLauncher {
    public static void main(String[] arg) {
        LwjglApplicationConfiguration config = new LwjglApplicationConfiguration();
        config.title = "Impossibru";
        config.width = 800;
        config.height = 480;
        new LwjglApplication(new GameLoader(), config);
    }
}
