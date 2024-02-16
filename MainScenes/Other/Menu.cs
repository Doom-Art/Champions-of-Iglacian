using Godot;
using System;

public partial class Menu : Control
{
	[Export(PropertyHint.File)]
	String LevelSelect;
	[Export(PropertyHint.File)]
	String CharacterSelect;
	[Export(PropertyHint.File)]
	String Settings;
	[Export(PropertyHint.File)]
	String InfoPage;
	[Export(PropertyHint.File)]
	String Shop;

	public override void _Ready()
	{
		ConfigFile config = new ConfigFile();
		config.Load("GameSaveData.cfg");
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), Mathf.LinearToDb((float)config.GetValue("Game", "MasterVolume", 1)));
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Music"), Mathf.LinearToDb((float)config.GetValue("Game", "MusicVolume", 1)));
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("SFX"), Mathf.LinearToDb((float)config.GetValue("Game", "SFXVolume", 1)));
		InputEventKey tempKey = new InputEventKey
		{
			Keycode = (Key)(int)config.GetValue("Keybindings", "Jump", (int)Key.Space)
		};
		InputMap.ActionAddEvent("Jump", tempKey);

		tempKey = new InputEventKey
		{
			Keycode = (Key)(int)config.GetValue("Keybindings", "Left", (int)Key.A)
		};
		InputMap.ActionAddEvent("Left", tempKey);

		tempKey = new InputEventKey
		{
			Keycode = (Key)(int)config.GetValue("Keybindings", "Right", (int)Key.D)
		};
		InputMap.ActionAddEvent("Right", tempKey);

		tempKey = new InputEventKey
		{
			Keycode = (Key)(int)config.GetValue("Keybindings", "Attack", (int)Key.S)
		};
		InputMap.ActionAddEvent("Attack", tempKey);
	}

	public void OnPlayPressed()
	{
		GetTree().ChangeSceneToFile(LevelSelect);
	}
	public void OnSettingsPressed()
	{
		GetTree().ChangeSceneToFile(Settings);
	}
	public void OnCharacterPressed()
	{
		GetTree().ChangeSceneToFile(CharacterSelect);
	}
	public void OnInfoPressed()
	{
		GetTree().ChangeSceneToFile(InfoPage);
	}
	public void OnShopPressed()
	{
		GetTree().ChangeSceneToFile(Shop);
	}
}
