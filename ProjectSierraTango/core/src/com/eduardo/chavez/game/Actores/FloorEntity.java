package com.eduardo.chavez.game.Actores;

import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.Batch;
import com.badlogic.gdx.physics.box2d.Body;
import com.badlogic.gdx.physics.box2d.BodyDef;
import com.badlogic.gdx.physics.box2d.Fixture;
import com.badlogic.gdx.physics.box2d.PolygonShape;
import com.badlogic.gdx.physics.box2d.World;
import com.badlogic.gdx.scenes.scene2d.Actor;

import static com.eduardo.chavez.game.Constants.PIXELS_IN_METER;

/**
 * Created by Eduardo on 23/8/2017.
 */

public class FloorEntity extends Actor {

    private Texture floor, overfloor;
    private World world;
    private Body body, leftBody;
    private Fixture fixture, leftFixture;

    public FloorEntity(World world, Texture floor, Texture overfloor, float x, float width, float y) {
        this.floor = floor;
        this.overfloor = overfloor;
        this.world = world;

        //colocamos el suelo en la posicion correspondiente
        BodyDef def = new BodyDef();
        def.position.set(x + width / 2, y - 0.5f);
        body = world.createBody(def);

        //Generamos forma para la caja
        PolygonShape shape = new PolygonShape();
        shape.setAsBox(width / 2, 0.5f);
        fixture = body.createFixture(shape, 1);
        fixture.setUserData("floor");
        shape.dispose();

        setSize(width * PIXELS_IN_METER, PIXELS_IN_METER);
        setPosition(x * PIXELS_IN_METER, (y - 1) * PIXELS_IN_METER);
    }

    @Override
    public void draw(Batch batch, float parentAlpha) {
        // Render both textures.
        batch.draw(floor, getX(), getY(), getWidth(), getHeight());
        batch.draw(overfloor, getX(), getY() + 0.9f * getHeight(), getWidth(), 0.1f * getHeight());

    }

    public void detach() {
        body.destroyFixture(fixture);
        world.destroyBody(body);
    }
}
