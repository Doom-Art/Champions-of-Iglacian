using Godot;
using System;

public partial class InGameUI : Control
{
    private String menuFilePath = "res://MainScenes/Other/menu.tscn";
    public void Exit()
    {
        GetTree().Paused = false;
        GetTree().ChangeSceneToFile(menuFilePath);
    }
    public void Pause()
    {
        GetTree().Paused = true;
        GetNode<Control>("PausePanel").Show();
    }
    public void Resume()
    {
        GetTree().Paused = false;
        GetNode<Control>("PausePanel").Hide();
    }
    public void Restart()
    {
        Resume();
        GetTree().ReloadCurrentScene();
    }
}
