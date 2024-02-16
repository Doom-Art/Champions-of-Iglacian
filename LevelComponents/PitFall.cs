using Godot;
using System;

public partial class PitFall : StaticBody2D
{
    [Export]
    private int atkDmg = 10;
    private bool _didCollapse;
    private float _timer;

    public override void _Process(double delta)
    {
        if (_didCollapse)
        {
            _timer += (float)delta;
            if (_timer > 5)
            {
                GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Rebuild");
                _timer = 0;
                _didCollapse = false;
            }
        }
    }
    public void AnimationFinished()
    {
        if (GetNode<AnimatedSprite2D>("AnimatedSprite2D").Animation == "Rebuild")
        {
            GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, false);
        }
    }
    public void Collapse(Area2D area)
    {
        if (!_didCollapse)
        {
            //var hitBox = area as HitBoxComponent;
            //hitBox.ResetImmunity();
            GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Fall");
            GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
            _didCollapse = true;
        }
    }
    public void Damage(Area2D area)
    {
        //GD.Print(area.HasMethod("TakeDmg"));
        if (area.HasMethod("TakeDmg"))
        {
            var hitbox = area as HitBoxComponent;
            //GD.Print(hitbox);
            hitbox.ResetImmunity();
            hitbox.TakeDmg(atkDmg);
        }
    }
}
