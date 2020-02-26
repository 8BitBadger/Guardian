using System;
using Godot;

public class InputManager : Node2D
{
    //Outgoing signals for the input handler
    [Signal]
    delegate void leftMouseClicked(Vector2 clickPos);
    [Signal]
    delegate void leftMouseReleased(Vector2 releasePos);
    [Signal]
    delegate void rightMouseClicked(Vector2 clickPos);
    [Signal]
    delegate void rightMouseReleased(Vector2 releasePos);
        [Signal]
    delegate void upPressed();
    public override void _Input(InputEvent @event)
    {
        //If the mouse wa clicked
        if (@event is InputEventMouseButton)
        {
            if (@event.IsActionPressed("leftMouseButton"))
            {
                Vector2 mousePos = GetGlobalMousePosition();
                EmitSignal(nameof(leftMouseClicked), mousePos);
            }
            if (@event.IsActionReleased("leftMouseButton"))
            {
                Vector2 mousePos = GetGlobalMousePosition();
                EmitSignal(nameof(leftMouseReleased), mousePos);
            }
            if (@event.IsActionPressed("rightMouseButton"))
            {
                Vector2 mousePos = GetGlobalMousePosition();
                EmitSignal(nameof(rightMouseClicked), mousePos);
            }
            if (@event.IsActionReleased("rightMouseButton"))
            {
                Vector2 mousePos = GetGlobalMousePosition();
                EmitSignal(nameof(rightMouseReleased), mousePos);
            }
            if (@event.IsActionPressed("up"))
            {
                EmitSignal(nameof(upPressed));
            }
        }
    }
}