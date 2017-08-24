package com.eduardo.chavez.game.Actores;

import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.Batch;
import com.badlogic.gdx.math.Vector2;
import com.badlogic.gdx.physics.box2d.Body;
import com.badlogic.gdx.physics.box2d.BodyDef;
import com.badlogic.gdx.physics.box2d.Fixture;
import com.badlogic.gdx.physics.box2d.PolygonShape;
import com.badlogic.gdx.physics.box2d.World;
import com.badlogic.gdx.scenes.scene2d.Actor;

import static com.eduardo.chavez.game.Constants.PIXELS_IN_METER;


/**
 * Created by eduardo3150 on 21/8/17.
 */

public class GreenEnemy extends Actor {
    private Texture enemyTexture;
    private World world;
    private Body body;
    private Fixture fixture;

    public GreenEnemy(World world, Texture enemyTexture, float x, float y) {
        this.world = world;
        this.enemyTexture = enemyTexture;

        BodyDef def = new BodyDef();
        def.position.set(x, y + 0.5f);
        body = world.createBody(def);

        PolygonShape shape = new PolygonShape();
        shape.setAsBox(0.5f, 0.5f);
        fixture = body.createFixture(shape, 3);
        fixture.setUserData("enemy");
        shape.dispose();


        setPosition((x - 0.5f) * PIXELS_IN_METER, y * PIXELS_IN_METER);
        setSize(PIXELS_IN_METER, PIXELS_IN_METER);
    }

    @Override
    public void act(float delta) {
        //setX(getX() - 250 * delta);
    }

    @Override
    public void draw(Batch batch, float parentAlpha) {
        batch.draw(enemyTexture, getX(), getY(), getWidth(), getHeight());

    }

    public void detach() {
        body.destroyFixture(fixture);
        world.destroyBody(body);
    }
}
