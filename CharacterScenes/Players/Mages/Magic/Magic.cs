using Godot;
using System;

public partial class Magic : RigidBody2D
{
	private float _despawnTimer;
    public float Speed { get; set; }
    public int Attack { get; set; }
    public Vector2 Velocity { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_despawnTimer = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void AnimationFinished()
	{
		QueueFree();
	}
	private void Despawn()
	{
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
        GetNode<CollisionShape2D>("Attack/CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
        GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Disappear");
		Speed = 0;
    }
    public override void _Process(double delta)
	{
		MoveAndCollide(Velocity * (float)delta * Speed);
		_despawnTimer += (float)delta;
		if (_despawnTimer > 2)
		{
			Despawn();
		}
	}
	public void EnemyEntered(Area2D area)
	{
		if (area.HasMethod("TakeDmg"))
		{
            var hit = area as HitBoxComponent;
            hit.TakeDmg(Attack);
            Despawn();
        }
	}
}
