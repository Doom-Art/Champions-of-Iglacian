using Godot;
using System;

public partial class world : Node2D
{
    [Export]
    private int requiredKills = 10; 
    [Export]
    private int _maxEnemies = 4;
    private int kills;
    [Export(PropertyHint.File)]
    private String nextLevel = "";
    Player player;
    private Label doorLabel;
	private int enemiesExist;
    private PackedScene _coin, _heart;
    // Called when the node enters the scene tree for the first time.
    public String configSavePath = "res://GameSaveData.cfg";
    private ConfigFile config;
    public override void _Ready()
	{
        config = new ConfigFile();
        config.Load(configSavePath);
        var playerPath = (String)config.GetValue("Game","Player","res://CharacterScenes/Players/Rogues/ninja.tscn");
        player = ResourceLoader.Load<PackedScene>(playerPath).Instantiate<Player>();
        player.Position = GetNode<Marker2D>("PlayerStartPos").Position;
        AddChild(player);

        GetTree().CallGroup("spawners", EnemySpawner.MethodName.SetPlayer, player);
        _coin = ResourceLoader.Load<PackedScene>("res://LevelComponents/coin.tscn");
        _heart = ResourceLoader.Load<PackedScene>("res://LevelComponents/heart.tscn");
        kills = 0;
		enemiesExist = 0;
        doorLabel = GetNodeOrNull<Label>("ExitDoor/Label");
        if (doorLabel != null)
            doorLabel.Text = $"{kills}/{requiredKills} Kills";
        //SpawnAll();
        GD.Randomize();
        //var e = new InputEventKey();
        //InputMap.ActionEraseEvent("Jump", e);
    }
	public void SpawnAll()
	{
		if (enemiesExist < _maxEnemies)
	        GetTree().CallGroup("spawners", EnemySpawner.MethodName.Spawn);
    }
    public void OnChildEntered(Node child)
    {
        if (child.IsInGroup("mobs"))
        {
            enemiesExist++;
        }
    }
    public void OnChildExit(Node child)
    {
        if (child.IsInGroup("mobs"))
        {
            var enemy = child as Enemy;
            if (enemy.Dead)
            {
                if (GD.RandRange(0,3) == 0)
                {
                    var heart = _heart.Instantiate<Area2D>();
                    heart.Position = enemy.GlobalPosition;
                    CallDeferred(Node.MethodName.AddChild, heart);
                }
                else
                {
                    var coin = _coin.Instantiate<Area2D>();
                    coin.Position = enemy.GlobalPosition;
                    CallDeferred(Node.MethodName.AddChild, coin);
                }
                kills++;
                if (doorLabel != null)
                    doorLabel.Text = $"{kills}/{requiredKills} Kills";
            }
            enemiesExist--;
        }
    }
    public void DoorEntered(Area2D area)
    {
        if (kills >= requiredKills && nextLevel != "")
        {
            player.Save();
            GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToFile, nextLevel);
        }
    }
}
