using System;
using Godot;

public class InputManager : Node2D
{
    //Outgoing signals for the input handler
    [Signal]
    delegate void mouseClicked(Vector2 clickPos);
    [Signal]
    delegate void mouseReleased(Vector2 releasePos);
    public override void _Input(InputEvent @event)
    {
        //If the mouse wa clicked
        if (@event is InputEventMouseButton)
        {
            if (@event.IsPressed())
            {
                Vector2 mousePos = GetGlobalMousePosition();
                EmitSignal(nameof(mouseClicked), mousePos);
            }
            else
            {
                 Vector2 mousePos = GetGlobalMousePosition();
                EmitSignal(nameof(mouseReleased), mousePos);
            }
        }
    }
}