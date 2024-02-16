using Godot;
using System;

public partial class level_selection : Control
{
  [Export(PropertyHint.File)]
  private String menu;

  [Export]
  private String[] _levels;
  public void OnNextLevel(int levelNum)
  {
    GetTree().ChangeSceneToFile(_levels[levelNum - 1]);
  }
  public void ReturnToMenu()
  {
    GetTree().ChangeSceneToFile(menu);
  }
}
