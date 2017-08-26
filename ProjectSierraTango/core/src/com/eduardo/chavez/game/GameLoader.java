package com.eduardo.chavez.game;

import com.badlogic.gdx.Game;
import com.badlogic.gdx.assets.AssetManager;
import com.badlogic.gdx.audio.Music;
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
        manager.load(AssetsLocation.ARROW_UP, Texture.class);
        manager.load(AssetsLocation.ARROW_DOWN, Texture.class);
        manager.load(AssetsLocation.ARROW_LEFT, Texture.class);
        manager.load(AssetsLocation.ARROW_RIGHT, Texture.class);
        manager.load(AssetsLocation.GREEN_ENEMY, Texture.class);
        manager.load(AssetsLocation.MAIN_FLOOR, Texture.class);
        manager.load(AssetsLocation.OVERFLOOR, Texture.class);
        manager.load(AssetsLocation.MUSIC_MAIN, Music.class);
        manager.load(AssetsLocation.MUSIC_MENU, Music.class);
        manager.load(AssetsLocation.MUSIC_SECOND, Music.class);
        manager.load(AssetsLocation.MUSIC_CREDITS, Music.class);
        manager.load(AssetsLocation.GAME_BACKGROUND, Texture.class);
        manager.load(AssetsLocation.GAME_FALLING_ENEMY, Texture.class);
        manager.load(AssetsLocation.MAIN_ACTOR_ANIMATED, Texture.class);
        manager.load(AssetsLocation.GAME_HEART, Texture.class);
        manager.load(AssetsLocation.GAME_ALT_FLOOR, Texture.class);
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
