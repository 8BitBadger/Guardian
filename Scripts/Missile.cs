using Godot;
using System;
using EventCallback;

public class Missile : Node2D
{
    [Export]
    int speed = 300;
    Vector2 vel = Vector2.Zero;
    Vector2 accel = Vector2.Zero;
    int life = 5;
    [Export]
    float steerForce = 50.0f;
    Node2D target = null;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<Area2D>("TargetFinder").Connect("body_entered", this, nameof(EnemyDetected));
        GetNode<Area2D>("HitBox").Connect("body_entered", this, nameof(TargetHit));
        GetNode<Timer>("Life").Start();
        GetNode<Timer>("Life").Connect("timeout", this, nameof(Die));
    }
    /*
    public void Start(Transform2D transform, Node2D newTarget)
    {
        GlobalTransform = transform;
        vel = transform.x * speed;
        target = newTarget;
    }
    */
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        Position += (Transform.x * speed).Normalized();
        /*
        accel += Seek();
        vel += accel * speed;
        vel = vel.Clamped(speed);
        Rotation = vel.Angle();
        Position += vel * delta;
    */
    }
    /*
    private Vector2 Seek()
    {
        Vector2 steer = Vector2.Zero;

        if (target != null)
        {
            Vector2 desired = (target.Position - Position).Normalized() * speed;
            steer = (desired - vel).Normalized() * steerForce;
            return steer;
        }
        return Vector2.Zero;
    }
    */
    public void EnemyDetected(Node node)
    {
        if (node.IsInGroup("Enemies")) target = ((Node2D)node);
    }
    public void TargetHit(Node node)
    {
        if (node.IsInGroup("Enemies"))
        {
            UnitHitEvent uhei = new UnitHitEvent();
            uhei.target = (Node2D)node;
            uhei.attacker = (Node2D)GetParent();
            uhei.damage = 70;
            uhei.Description = "Missile hit enemy";
        }

        Die();
    }
    private void Die()
    {
        QueueFree();
    }
}
