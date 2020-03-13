using Godot;
using System;
using EventCallback;

public class MisslePickup : Node2D
{
    int speed = 2;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<Area2D>("Sprite/Are2D").Connect("body_entered", this, nameof(Collided));
        Position = new Vector2(75 * 64, 45 *64);
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (Rotation >= 360) Rotation = 0;
        Rotation = Rotation + speed * delta;
    }

    private void Collided(Node coll)
    {
      if(coll.IsInGroup("Player"))
      {
        MissilePickupEvent mpe = new MissilePickupEvent();
        mpe.missileUpgrade = true;
        mpe.FireEvent();
        
        QueueFree();
      }
    }

}
