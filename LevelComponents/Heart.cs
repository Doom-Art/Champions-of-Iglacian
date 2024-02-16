using Godot;
using System;

public partial class Heart : Area2D
{
    public void OnAreaEntered(Area2D area)
    {
        if (area.GetParent().IsInGroup("player"))
        {
            var player = area.GetParent() as Player;
            player.Heal(GD.RandRange(4,11));
            QueueFree();
        }
    }
}
