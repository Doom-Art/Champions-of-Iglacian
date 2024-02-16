using Godot;
using System;

public partial class MagePlayer : Player
{
	[Export]
	private PackedScene MagicAttack;
    public void OnAttackEntered(HitBoxComponent hitBox)
    {
        hitBox.TakeDmg(attackDmg);
    }
    private void CastSpell()
    {
        var Magic = MagicAttack.Instantiate<Magic>();
        var atkPos = Position;
        atkPos.X += 42 * Flip.Transform.X.X;
        Magic.Position = atkPos;
        Magic.LookAt(GetGlobalMousePosition());
        Magic.Velocity = (GetGlobalMousePosition() - Magic.Position).Normalized();
        Magic.Speed = 600;
        Magic.Attack = attackDmg;
        GetParent().AddChild(Magic);
    }
    public override void AnimationFinished()
    {
        if (sprite.Animation == "Attack")
            CastSpell();
        base.AnimationFinished();
    }
    public override void _PhysicsProcess(double delta)
	{
		if (!death)
		{
            Vector2 velocity = Velocity;

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

                if (Input.IsActionJustPressed("Attack") && velocity.Y == 0 && !attack)
                {
                    sprite.Play("Attack");
                    attack = true;
                    
                }
                else if (attack)
                    attack = velocity == Vector2.Zero;
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
