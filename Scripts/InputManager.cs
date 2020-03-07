using System;
using Godot;

public class InputManager : Node2D
{
    //Outgoing signals for the input handler
    [Signal]
    delegate void leftMouseClicked();
    [Signal]
    delegate void leftMouseReleased();
    [Signal]
    delegate void rightMouseClicked();
    [Signal]
    delegate void rightMouseReleased();
    [Signal]
    delegate void upPressed();
    [Signal]
    delegate void downPressed();
    [Signal]
    delegate void leftPressed();
    [Signal]
    delegate void rightPressed();

    public override void _Input(InputEvent @event)
    {
        //If the mouse wa clicked
        if (@event is InputEventMouseButton)
        {
            if (@event.IsActionPressed("leftMouseButton"))
            {
                EmitSignal(nameof(leftMouseClicked));
            }
            if (@event.IsActionReleased("leftMouseButton"))
            {
                EmitSignal(nameof(leftMouseReleased));
            }
            if (@event.IsActionPressed("rightMouseButton"))
            {
                EmitSignal(nameof(rightMouseClicked));
            }
            if (@event.IsActionReleased("rightMouseButton"))
            {
                EmitSignal(nameof(rightMouseReleased));
            }
            if (@event.IsActionPressed("up"))
            {
                EmitSignal(nameof(upPressed));
            }
            if (@event.IsActionPressed("down"))
            {
                EmitSignal(nameof(downPressed));
            }
            if (@event.IsActionPressed("left"))
            {
                EmitSignal(nameof(leftPressed));
            }
            if (@event.IsActionPressed("right"))
            {
                EmitSignal(nameof(rightPressed));
            }
        }
    }
}