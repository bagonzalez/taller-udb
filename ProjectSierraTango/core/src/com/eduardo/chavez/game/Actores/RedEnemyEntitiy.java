package com.eduardo.chavez.game.Actores;

import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.Batch;
import com.badlogic.gdx.math.Rectangle;
import com.badlogic.gdx.physics.box2d.Body;
import com.badlogic.gdx.physics.box2d.BodyDef;
import com.badlogic.gdx.physics.box2d.Fixture;
import com.badlogic.gdx.physics.box2d.PolygonShape;
import com.badlogic.gdx.physics.box2d.World;
import com.badlogic.gdx.scenes.scene2d.Actor;

import static com.eduardo.chavez.game.Constants.PIXELS_IN_METER;

/**
 * Created by Eduardo on 25/8/2017.
 */

public class RedEnemyEntitiy extends Actor {
    private Texture enemyTexture;
    private World world;
    private Body body;
    private Fixture fixture;
    private float randomSpeed;

    public Rectangle getBounds() {
        return bounds;
    }


    private Rectangle bounds;

    public RedEnemyEntitiy(World world, Texture enemyTexture, float x, float y, float randomSpeed) {
        this.world = world;
        this.enemyTexture = enemyTexture;
        this.randomSpeed = randomSpeed;

        BodyDef def = new BodyDef();
        def.position.set(x, y + 0.5f);
        def.type = BodyDef.BodyType.DynamicBody;
        body = world.createBody(def);

        PolygonShape shape = new PolygonShape();
        shape.setAsBox(0.5f * 0.65f, 0.5f * 0.65f);
        fixture = body.createFixture(shape, 3);
        fixture.setUserData("enemy");
        shape.dispose();


        setPosition((x - 0.5f) * PIXELS_IN_METER, y * PIXELS_IN_METER);
        setSize(PIXELS_IN_METER * 0.65f, PIXELS_IN_METER * 0.65f);
        bounds = new Rectangle((int) getX(), (int) getY(), (int) getWidth(), (int) getHeight());
    }

    @Override
    public void act(float delta) {
        body.setLinearVelocity(0, -randomSpeed);
    }

    @Override
    public void draw(Batch batch, float parentAlpha) {
        setPosition((body.getPosition().x - 0.5f) * PIXELS_IN_METER,
                (body.getPosition().y - 0.5f) * PIXELS_IN_METER);
        batch.draw(enemyTexture, getX(), getY(), getWidth(), getHeight());

    }

    public void detach() {
        body.destroyFixture(fixture);
        world.destroyBody(body);
    }
}
