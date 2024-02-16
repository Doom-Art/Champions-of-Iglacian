using BasicSpawnTest.Components;
using Godot;

public partial class Shop : Control
{
    [Export(PropertyHint.File)]
    public string Menu = "";

    private int _coins;
    ConfigFile config;
    Weapon[] weapons;
    Label[] itemInfo;
    private int _selectedWeapon, _currentWeapon;
    public override void _Ready()
    {
        config = new ConfigFile();
        config.Load("GameSaveData.cfg");
        _coins = (int)config.GetValue("Player", "Coins", 0);
        _selectedWeapon = (int)config.GetValue("Shop", "SelectedWeapon", -1);
        GetNode<Label>("Coins").Text = $"{_coins} Coins";
        weapons = new Weapon[]
        {
            new Weapon("Rogue", 1, 0, 0, (bool)config.GetValue("Shop", "Item1", false), 20),
            new Weapon("Knight", 0, 0, 10, (bool)config.GetValue("Shop", "Item2", false), 20),
            new Weapon("Mage", 1, 0, 0, (bool) config.GetValue("Shop", "Item3", false), 30),
            new Weapon("Rogue", 0, 10, 0, (bool)config.GetValue("Shop", "Item4", false), 30),
            new Weapon("Knight", 2, 0, -10, (bool) config.GetValue("Shop", "Item5", false), 30),
            new Weapon("Mage", 0, 10, 0, (bool) config.GetValue("Shop", "Item6", false), 20),
        };
        itemInfo = new Label[6];
        for (int i = 0; i < itemInfo.Length; i++)
        {
            itemInfo[i] = GetNode<Label>($"ItemDescriptions/Item{i+1}");
        }
        _currentWeapon = 0;
        PressItem(0);
    }
    public void ReturnToMenu()
    {
        config.SetValue("Shop", "SelectedWeapon", _selectedWeapon);
        if (_selectedWeapon != -1)
        {
            config.SetValue("Player", "HasWeapon", true);
            config.SetValue("Player", "WeaponClass", weapons[_selectedWeapon].Class);
            config.SetValue("Player", "WeaponAtk", weapons[_selectedWeapon].AtkBoost);
            config.SetValue("Player", "WeaponSpeed", weapons[_selectedWeapon].SpeedBoost);
            config.SetValue("Player", "WeaponHealth", weapons[_selectedWeapon].HealthBoost);
        }
        else
        {
            config.SetValue("Player", "HasWeapon", false);
        }
        for (int i = 0; i < weapons.Length; i++)
        {
            config.SetValue("Shop", $"Item{i + 1}", weapons[i].Bought);
        }
        config.SetValue("Player", "Coins", _coins);
        config.Save("GameSaveData.cfg");
        GetTree().ChangeSceneToFile(Menu);
    }
    public void PressItem(int i)
    {
        itemInfo[_currentWeapon].Visible = false;
        _currentWeapon = i;
        itemInfo[i].Visible = true;
        if (weapons[i].Bought)
        {
            if (_selectedWeapon == i)
            {
                GetNode<Button>("Buttons/Unequip").Visible = true;
                GetNode<Button>("Buttons/Equip").Visible = false;
                GetNode<Button>("Buttons/Buy").Visible = false;
            }
            else
            {
                GetNode<Button>("Buttons/Equip").Visible = true;
                GetNode<Button>("Buttons/Unequip").Visible = false;
                GetNode<Button>("Buttons/Buy").Visible = false;
            }
        }
        else
        {
            GetNode<Button>("Buttons/Buy").Visible = true;
            GetNode<Button>("Buttons/Equip").Visible = false;
            GetNode<Button>("Buttons/Unequip").Visible = false;
        }
    }
    public void Equip()
    {
        _selectedWeapon = _currentWeapon;
        GetNode<Button>("Buttons/Unequip").Visible = true;
        GetNode<Button>("Buttons/Equip").Visible = false;
    }
    public void Unequip()
    {
        _selectedWeapon = -1;
        GetNode<Button>("Buttons/Equip").Visible = true;
        GetNode<Button>("Buttons/Unequip").Visible = false;
    }
    public void Buy()
    {
        if (_coins >= weapons[_currentWeapon].Price)
        {
            _coins -= weapons[_currentWeapon].Price;
            weapons[_currentWeapon].Bought = true;
            GetNode<Label>("Coins").Text = $"{_coins} Coins";
        }
        GetNode<Button>("Buttons/Equip").Visible = true;
        GetNode<Button>("Buttons/Buy").Visible = false;
    }
}
