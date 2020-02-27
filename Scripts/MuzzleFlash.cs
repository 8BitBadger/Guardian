using Godot;
using System;

public class MuzzleFlash : Sprite
{
    Timer flashTimer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        flashTimer = new Timer();
        //Set up the timer for the muzzle flash
        flashTimer.WaitTime = .05f;
        flashTimer.OneShot = true;
        flashTimer.Name = "flashTimer";
        //Create the new timer
        AddChild(flashTimer, true);
        GetNode<InputManager>("../../../../InputManager").Connect("leftMouseClicked", this, nameof(ShowFlash));
        GetNode<Timer>(flashTimer.Name).Connect("timeout", this, nameof(HideFlash));
    }

    private void ShowFlash()
    {
        flashTimer.Start();
        this.Visible = true;
    }

    private void HideFlash()
    {
        this.Visible = false;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
