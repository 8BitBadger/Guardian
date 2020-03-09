using Godot;
using System;
using EventCallback;

public class Missile : Area2D
{
    [Export]
    int speed = 300;
    [Export]
    float steerForce = 50.0f;
    Node2D target;
    Vector2 velocity;
    Vector2 acceleration;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {velocity = Vector2.One * speed;
    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        acceleration += Seek();
        velocity += acceleration * delta;
        velocity = velocity.Clamped(speed);
        Rotation = velocity.Angle();
    }

    private Vector2 Seek()
    {
        Vector2 steer = Vector2.Zero;

        if (target != null)
        {
            Vector2 desired = (target.Position - Position).Normalized() * speed;
            steer = (desired - velocity).Normalized() * steerForce;
            return steer;
        }
        return Vector2.Zero;
    }
    public void body_entered(Node node)
    {
        UnitHitEvent uhei = new UnitHitEvent();
        uhei.target = (Node2D)node;
        uhei.attacker = (Node2D)GetParent();
        uhei.damage = 30;
        uhei.Description = "Missile away";
    }
}
