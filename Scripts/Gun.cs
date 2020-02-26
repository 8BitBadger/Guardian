using Godot;
using System;

public class Gun : Sprite
{
    Vector2 mousePos;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delt)
  {
      mousePos = GetGlobalMousePosition();
      LookAt(mousePos);
  }
}
