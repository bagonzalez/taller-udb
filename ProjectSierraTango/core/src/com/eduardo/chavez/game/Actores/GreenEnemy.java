package com.eduardo.chavez.game.Actores;

import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.Batch;
import com.badlogic.gdx.scenes.scene2d.Actor;

/**
 * Created by eduardo3150 on 21/8/17.
 */

public class GreenEnemy extends Actor {
    Texture enemyTexture;

    public GreenEnemy(Texture enemyTexture) {
        this.enemyTexture = enemyTexture;
        setSize(enemyTexture.getWidth(), enemyTexture.getHeight());
    }

    @Override
    public void act(float delta) {
        setX(getX() - 250 * delta);
    }

    @Override
    public void draw(Batch batch, float parentAlpha) {
        batch.draw(enemyTexture, getX(), getY());

    }
}
