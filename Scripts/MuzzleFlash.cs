using Godot;
using System;
using EventCallback;
public class MuzzleFlash : Sprite
{
    bool missileUpgrade = false;
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
        //Register the missile event 
        //MissilePickupEvent.RegisterListener(GotMissile);
    }
    private void ShowFlash()
    {
        //if (!missileUpgrade)
        //{
            flashTimer.Start();
            this.Visible = true;
        //}
    }
    private void HideFlash()
    {
        this.Visible = false;
    }
    //private void GotMissile(MissilePickupEvent mpe)
    //{
    //   missileUpgrade = mpe.missileUpgrade;
    //}
    public override void _ExitTree()
    {
        //MissilePickupEvent.UnregisterListener(GotMissile);
    }
}
