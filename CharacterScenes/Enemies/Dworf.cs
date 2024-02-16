using Godot;
using System;

public partial class Dworf : HellKnight
{
    private int currentHP;
    private float stunTime;
    public override void _Ready()
    {
        currentHP = GetNode<HealthComponent>("HealthComponent").MaxHealth;
        base._Ready();
        stunTime = 1;
    }
    public override void Hurt()
    {
        stunTime = 0;
        int newHP = GetNode<HealthComponent>("HealthComponent").Health;
        int increase =  currentHP - newHP;
        currentHP = newHP;
        atkDmg += (int)(0.4 * increase);
        Speed += 15 * increase;
        base.Hurt();
    }
    public override void _PhysicsProcess(double delta)
    {
        if (stunTime > 0.9f || sprite.Animation == "Hurt")
        {
            base._PhysicsProcess(delta);
        }
        else if (!Dead)
        {
            var velocity = Velocity;
            if (!IsOnFloor())
            {
                velocity.Y += gravity * (float)delta;
            }
            velocity.X = 0;
            Velocity = velocity;
            MoveAndSlide();
            stunTime += (float)delta;
        }
    }
}
