using Godot;
using System;

public partial class InfoPage : Control
{
    [Export(PropertyHint.File)]
    public string Menu = "";

    private int _page = 0;
    private Container[] _pages;
    public override void _Ready()
    {
        _pages = new Container[]
        {
            GetNode<Container>("Welcome"),
            GetNode<Container>("HowToPlay"),
            GetNode<Container>("GameObjective"),
            GetNode<Container>("ClassInfo"),


            GetNode<Container>("Credits"),
        };
    }
    public void ReturnToMenu()
    {
        if (Menu != "")
        {
            GetTree().ChangeSceneToFile(Menu);
        }
    }
    public void NextButton()
    {
        _pages[_page].Visible = false;
        if (_page +1 < _pages.Length)
        {
            _page++;
        }
        else
        {
            _page = 0;
        }
        _pages[_page].Visible = true;
    }
    public void BackButton()
    {
        _pages[_page].Visible = false;
        if (_page > 0)
        {
            _page--;
        }
        else
        {
            _page = _pages.Length-1;
        }
        _pages[_page].Visible = true;
    }
}
