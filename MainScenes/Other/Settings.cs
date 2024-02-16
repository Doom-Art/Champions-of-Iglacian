using Godot;
using System;

public partial class Settings : Control
{
    [Export(PropertyHint.File)]
    String menuFilePath;

    private int _attackKeyID, _jumpKeyID, _leftKeyID, _rightKeyID;
    private int _selectedKeyButton;
    private bool _setKey = false;
    ConfigFile config;
    public override void _Ready()
    {
        config = new ConfigFile();
        config.Load("GameSaveData.cfg");

        _attackKeyID = (int)config.GetValue("Keybindings", "Attack", (int)Key.S);
        _jumpKeyID = (int)config.GetValue("Keybindings", "Jump", (int)Key.Space);
        _leftKeyID = (int)config.GetValue("Keybindings", "Left", (int)Key.A);
        _rightKeyID = (int)config.GetValue("Keybindings", "Right", (int)Key.D);
        GetNode<Button>("Keybindings/Grid/Jump").Text = ((Key)_jumpKeyID).ToString();
        GetNode<Button>("Keybindings/Grid/Jump").Pressed += SetJump;
        GetNode<Button>("Keybindings/Grid/Attack").Text = ((Key)_attackKeyID).ToString();
        GetNode<Button>("Keybindings/Grid/Attack").Pressed += SetAttack;
        GetNode<Button>("Keybindings/Grid/Left").Text = ((Key)_leftKeyID).ToString();
        GetNode<Button>("Keybindings/Grid/Left").Pressed += SetLeft;
        GetNode<Button>("Keybindings/Grid/Right").Text = ((Key)_rightKeyID).ToString();
        GetNode<Button>("Keybindings/Grid/Right").Pressed += SetRight;
    }
    private void SetJump()
    {
        _setKey = true;
        _selectedKeyButton = 0;
        GetNode<Button>("Keybindings/Grid/Jump").Text = "";
    }
    private void SetRight()
    {
        _setKey = true;
        _selectedKeyButton = 3;
        GetNode<Button>("Keybindings/Grid/Right").Text = "";
    }
    private void SetLeft()
    {
        _setKey = true;
        _selectedKeyButton = 2;
        GetNode<Button>("Keybindings/Grid/Left").Text = "";
    }
    private void SetAttack()
    {
        _setKey = true;
        _selectedKeyButton = 1;
        GetNode<Button>("Keybindings/Grid/Attack").Text = "";
    }
    public void ResetKeys()
    {
        var oldKey = new InputEventKey();
        oldKey.Keycode = (Key)_jumpKeyID;
        InputMap.ActionEraseEvent("Jump", oldKey);
        oldKey.Keycode = (Key)_attackKeyID;
        InputMap.ActionEraseEvent("Attack", oldKey);
        oldKey.Keycode = (Key)_leftKeyID;
        InputMap.ActionEraseEvent("Left", oldKey);
        oldKey.Keycode = (Key)_rightKeyID;
        InputMap.ActionEraseEvent("Right", oldKey);

        GetNode<Button>("Keybindings/Grid/Jump").Text = "Space";
        GetNode<Button>("Keybindings/Grid/Attack").Text = "S";
        GetNode<Button>("Keybindings/Grid/Left").Text = "A";
        GetNode<Button>("Keybindings/Grid/Right").Text = "D";
        _attackKeyID = (int)Key.S;
        _jumpKeyID = (int)Key.Space;
        _leftKeyID = (int)Key.A;
        _rightKeyID = (int)Key.D;
    }
    public override void _Input(InputEvent @event)
    {
        if (_setKey)
            if (@event is InputEventKey)
            {
                var newKey = (@event as InputEventKey);
                var oldKey = new InputEventKey();
                switch (_selectedKeyButton)
                {
                    case 0:
                        oldKey.Keycode = (Key)_jumpKeyID;
                        InputMap.ActionEraseEvent("Jump", oldKey);
                        _jumpKeyID = (int)newKey.Keycode;
                        GetNode<Button>("Keybindings/Grid/Jump").Text = newKey.Keycode.ToString();
                        break;
                    case 1:
                        oldKey.Keycode = (Key)_attackKeyID;
                        InputMap.ActionEraseEvent("Attack", oldKey);
                        _attackKeyID = (int)newKey.Keycode;
                        GetNode<Button>("Keybindings/Grid/Attack").Text = newKey.Keycode.ToString();
                        break;
                    case 2:
                        oldKey.Keycode = (Key)_leftKeyID;
                        InputMap.ActionEraseEvent("Left", oldKey);
                        _leftKeyID = (int)newKey.Keycode;
                        GetNode<Button>("Keybindings/Grid/Left").Text = newKey.Keycode.ToString();
                        break;
                    case 3:
                        oldKey.Keycode = (Key)_rightKeyID;
                        InputMap.ActionEraseEvent("Right", oldKey);
                        _rightKeyID = (int)newKey.Keycode;
                        GetNode<Button>("Keybindings/Grid/Right").Text = newKey.Keycode.ToString();
                        break;
                }
                _setKey = false;
            }
    }
    public void ReturnToMenu()
    {
        config.SetValue("Game", "MasterVolume", GetNode<HSlider>("VolumeSliders/Sliders/MasterVolume").Value);
        config.SetValue("Game", "MusicVolume", GetNode<HSlider>("VolumeSliders/Sliders/MusicVolume").Value);
        config.SetValue("Game", "SFXVolume", GetNode<HSlider>("VolumeSliders/Sliders/SoundEffectVolume").Value);

        config.SetValue("Keybindings", "Jump", _jumpKeyID);
        config.SetValue("Keybindings", "Attack", _attackKeyID);
        config.SetValue("Keybindings", "Left", _leftKeyID);
        config.SetValue("Keybindings", "Right", _rightKeyID);

        config.Save("GameSaveData.cfg");

        GetTree().ChangeSceneToFile(menuFilePath);
    }


}
