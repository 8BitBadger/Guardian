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
        }
        private void OnHit(UnitHitEvent unitHit)
        {
            if (GetParent().Name == unitHit.target.Name)
            {   
                //Reduce the tanks health with the given amount
                health -= unitHit.damage;
                //Broadcast the health after it has been modified to anyone who is listening
                HealthEvent hei = new HealthEvent();
                hei.health = health;
                hei.target = unitHit.target;
                hei.FireEvent();
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
