using Godot;
using System;

public class AIMovement : KinematicBody2D
{
    Vector2 target;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Set the target to the dome by defualt so it goes and attacks it by defualt

    }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {

      //If im being attacked then switch target to the attacker

      if(Position.DistanceTo(target) < 10)
      {
          GD.Print("Bang Bang I shoot you!!!");
          //Shoot it now
      }
  }

  private void SetTarget(Vector2 target)
  {

  }
}
