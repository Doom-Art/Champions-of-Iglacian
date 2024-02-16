using Godot;

public partial class coin : Area2D
{
	public void OnAreaEntered(Area2D area)
	{
		if (area.GetParent().IsInGroup("player"))
		{
			var player = area.GetParent() as Player;
			player.GetCoin();
			QueueFree();
		}
	}
}
