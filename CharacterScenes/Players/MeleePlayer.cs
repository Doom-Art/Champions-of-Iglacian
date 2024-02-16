using Godot;
using System;

public partial class MeleePlayer : Player
{
    [Export]
    private float _atkCooldown = 0.5f;
    private float _timeSinceAtk;
    public override void AnimationFinished()
    {
        GetNode<CollisionShape2D>("Flip/HurtBox/CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
        if (sprite.Animation == "Attack")
        {
            _timeSinceAtk = 0;
        }
        base.AnimationFinished();
    }
    public override void _Ready()
    {
        GetNode<CollisionShape2D>("Flip/HurtBox/CollisionShape2D").Disabled = true;
        _timeSinceAtk = _atkCooldown + 1;
        base._Ready();
    }
    public override void Hurt()
    {
        GetNode<CollisionShape2D>("Flip/HurtBox/CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
        base.Hurt();
    }
    public void HurtBoxAreaEntered(Area2D area)
    {
        if (area.HasMethod("TakeDmg"))
        {
            var hitbox = area as HitBoxComponent;
            hitbox.TakeDmg(attackDmg);
        }
    }
    public override void _PhysicsProcess(double delta)
    {
        if (!death)
        {
            Vector2 velocity = Velocity;
            if (attack)
            {
                if (Input.IsActionJustPressed("Jump"))
                {
                    GetNode<CollisionShape2D>("Flip/HurtBox/CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
                    attack = false;
                }
            }
            // Add the gravity.
            if (!IsOnFloor())
                velocity.Y += gravity * (float)delta;
            if (sprite.Animation != "Hurt")
            {
                // Handle Jump.
                if (Input.IsActionJustPressed("Jump") && IsOnFloor())
                {
                    velocity.Y = JumpVelocity;
                    sprite.Play("Jump");
                }

                if (Input.IsActionPressed("Right"))
                {
                    velocity.X = Speed;
                    var scale = Flip.Transform;
                    scale.X.X = 1;
                    Flip.Transform = scale;
                }
                else if (Input.IsActionPressed("Left"))
                {
                    velocity.X = -Speed;
                    var scale = Flip.Transform;
                    scale.X.X = -1;
                    Flip.Transform = scale;
                }
                else
                {
                    velocity.X = 0;
                }

                if (Input.IsActionJustPressed("Attack") && velocity.Y == 0 && !attack && _timeSinceAtk > _atkCooldown)
                {
                    attack = true;
                    sprite.Play("Attack");
                    GetNode<CollisionShape2D>("Flip/HurtBox/CollisionShape2D").Disabled = false;
                    var audio = GetNodeOrNull<AudioStreamPlayer>("Slash");
                    audio?.Play();
                }
                if (!attack)
                {
                    if (velocity == Vector2.Zero)
                    {
                        sprite.Play("Idle");
                    }
                    else
                    {
                        if (sprite.Animation != "Jump" || velocity.Y == 0)
                        {
                            sprite.Play("Walk");
                        }
                    }
                    _timeSinceAtk += (float)delta;
                }
            }
            else
            {
                if (fallRight)
                {
                    velocity.X = Speed;
                }
                else
                {
                    velocity.X = -Speed;
                }
            }

            Velocity = velocity;
            MoveAndSlide();
        }
    }
}
