using Godot;
using System;

namespace EventCallback
{
    public class Health : Node2D
    {
        //Make the health of the enemy tank set able outside in the inspector
        [Export]
        int health = 100;
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            UnitHitEvent.RegisterListener(OnHit);
            //Connect to the players Hit emmit
            //GetTree().GetNodesInGroup("Player");
            //GetNode<Shoot>("../../Player/Body/Gun/BulletSpawnPoint").Connect("Hit", this, nameof(WasHit));
            //Connection to the AIs Hit emmit
            //GetNode<AIMovement>("../../SmallTank").Connect("Hit", this, nameof(WasHit));
            //GetNode<AIMovement>("../../SmallTank2").Connect("Hit", this, nameof(WasHit));

        }

        public override void _Input(InputEvent @event)
        {
            //If the mouse wa clicked
            if (@event is InputEventMouseButton)
            {
                if (@event.IsActionPressed("leftMouseButton"))
                {
                    Vector2 mousePos = GetGlobalMousePosition();
                }
            }
        }
        private void OnHit(UnitHitEvent unitHit)
        {
            if (GetParent().Name == unitHit.target.Name)
            {
                //if(unitHit.target.GetScript().HasMethod("BeingAttacked")) unitHit.target.GetScript().BeingAttacked(unitHit.attacker);
                //Reduce the enemy tank health with a fixed amount for now
                health -= 10;
                GD.Print(this.GetParent().Name + " has " + health + " health");
                //Check if the health has gone down to zero
                CheckHealth();
            }
        }
        private void CheckHealth()
        {
            if (health <= 0)
            {
                //Make sure the health does not go below 0
                health = 0;
                Die();
            }
        }
        void Die()
        {
            // I am dying for some reason.
            UnitDeathEvent udei = new UnitDeathEvent();
            udei.Description = "Unit " + this.Name + " has died.";
            udei.UnitNode = (Node2D)GetParent();
            udei.FireEvent();
            //Remove the parent node forn the scene
            udei.UnitNode.QueueFree();
        }
        public override void _ExitTree()
        {
            UnitHitEvent.UnregisterListener(OnHit);
        }
    }
}
