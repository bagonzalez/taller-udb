package com.eduardo.chavez.game;

import com.badlogic.gdx.Game;
import com.badlogic.gdx.assets.AssetManager;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.BitmapFont;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;

public class GameLoader extends Game {
    public SpriteBatch batch;
    public BitmapFont font;

    private AssetManager manager;


    @Override
    public void create() {
        batch = new SpriteBatch();
        font = new BitmapFont();
        manager = new AssetManager();
        manager.load("actor/IDLE000.png", Texture.class);
        manager.load("actor/enemy1.png", Texture.class);
        manager.load("actor/floor.png",Texture.class);
        manager.load("actor/overfloor.png",Texture.class);
        manager.finishLoading();
        this.setScreen(new MainMenuScreen(this));
    }

    @Override
    public void render() {
        super.render();
    }

    @Override
    public void dispose() {
        batch.dispose();
        font.dispose();
    }

    public AssetManager getManager() {
        return manager;
    }

    public void setManager(AssetManager manager) {
        this.manager = manager;
    }
}
