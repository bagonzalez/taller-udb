package com.eduardo.chavez.game.Actores;

import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.Batch;
import com.badlogic.gdx.scenes.scene2d.Actor;

/**
 * Created by eduardo3150 on 22/8/17.
 */

public class MainActorStatic extends Actor {
    private Texture mainActor;
    private boolean alive;

    public boolean isAlive() {
        return alive;
    }

    public void setAlive(boolean alive) {
        this.alive = alive;
    }

    public MainActorStatic(Texture mainActor) {
        this.mainActor = mainActor;
        alive = true;
        setSize(mainActor.getWidth() / 2, mainActor.getHeight() / 2);
    }

    @Override
    public void act(float delta) {

    }

    @Override
    public void draw(Batch batch, float parentAlpha) {
        batch.draw(mainActor, getX(), getY(), getWidth() / 2, getHeight() / 2);
    }
}
