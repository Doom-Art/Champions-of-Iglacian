using Godot;
using System;

public partial class character_selection : Control
{
    [Export(PropertyHint.File)]
    public string NextPath = "";

    string[] charFilePaths = new string[]
    {
        "res://CharacterScenes/Players/Rogues/ninja.tscn",
        "res://CharacterScenes/Players/Knights/silver_knight.tscn",
        "res://CharacterScenes/Players/Mages/red.tscn",
        "res://CharacterScenes/Players/Mages/violet.tscn",
        "res://CharacterScenes/Players/Rogues/forest.tscn",
        "res://CharacterScenes/Players/Knights/bronze.tscn",
        "res://CharacterScenes/Players/Knights/viking.tscn",
        "res://CharacterScenes/Players/Mages/amber.tscn",
        "res://CharacterScenes/Players/Rogues/shadow.tscn"
    };
    int[] charType = new int[] //0 = rogue, 1 = knight, 2 = mage
    {
        0, 1, 2, 2, 0, 1, 1,2,0
    };
    int[] charSpecialty = new int[] //0 = atk, 1 = health, 2 = speed
    {
        0,1,0,1,2,2,0,2,1
    };
    public String configSavePath = "res://GameSaveData.cfg";
    private ConfigFile config;
    private int _currentChar;
    private string _selectedCharPath;
    private Sprite2D[] _previewSprites;
    private TextureProgressBar _hpBar;
    private TextureProgressBar _speedBar;
    private TextureProgressBar _atkBar;

    private float baseRHP = 60, baseRSpeed = 310, baseRAtk = 6; //Base rogue stats
    private float baseKHP = 90, baseKSpeed = 290, baseKAtk = 4; //Base knight stats
    private float baseMHP = 50, baseMSpeed = 300, baseMAtk = 9; //Base mage stats
    public override void _Ready()
    {
        config = new ConfigFile();
        config.Load(configSavePath);
        _previewSprites = new Sprite2D[9]
        {
            GetNodeOrNull<Sprite2D>("CharButtons/Ninja/Preview"),
            GetNodeOrNull<Sprite2D>("CharButtons/Silver/Preview"),
            GetNodeOrNull<Sprite2D>("CharButtons/Red/Preview"),
            GetNodeOrNull<Sprite2D>("CharButtons/Violet/Preview"),
            GetNodeOrNull<Sprite2D>("CharButtons/Forest/Preview"),
            GetNodeOrNull<Sprite2D>("CharButtons/Bronze/Preview"),
            GetNodeOrNull<Sprite2D>("CharButtons/Viking/Preview"),
            GetNodeOrNull<Sprite2D>("CharButtons/Amber/Preview"),
            GetNodeOrNull<Sprite2D>("CharButtons/Shadow/Preview")
        };
        _hpBar = GetNode<TextureProgressBar>("Health");
        _speedBar = GetNode<TextureProgressBar>("Speed");
        _atkBar = GetNode<TextureProgressBar>("Attack");
        _currentChar = (int)config.GetValue("CharSelection", "CurrentChar", 0);
        _previewSprites[_currentChar].Visible = true;
        CharSelect(_currentChar);
    }

    public void NextScreen()
    {
        config.SetValue("CharSelection", "CurrentChar", _currentChar);
        config.SetValue("Game", "Player", _selectedCharPath);
        config.Save(configSavePath);
        if (NextPath != "")
            GetTree().ChangeSceneToFile(NextPath);
    }
    public void CharSelect(int charNum)
    {
        _previewSprites[_currentChar].Visible = false;
        _currentChar = charNum;
        _previewSprites[charNum].Visible = true;
        _selectedCharPath = charFilePaths[charNum];
        switch (charType[charNum])
        {
            case 0:
                _hpBar.Value = baseRHP;
                _speedBar.Value = baseRSpeed;
                _atkBar.Value = baseRAtk;
                break;
            case 1:
                _hpBar.Value = baseKHP;
                _speedBar.Value = baseKSpeed;
                _atkBar.Value = baseKAtk;
                break;
            case 2:
                _hpBar.Value = baseMHP;
                _speedBar.Value = baseMSpeed;
                _atkBar.Value = baseMAtk;
                break;
        }
        switch (charSpecialty[charNum])
        {
            case 0:
                _atkBar.Value += 2;
                break;
            case 1:
                _hpBar.Value += 20;
                break;
            case 2:
                _speedBar.Value += 20;
                break;
        }

    }
}
