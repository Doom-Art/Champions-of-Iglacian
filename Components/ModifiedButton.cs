using Godot;
using System;

public partial class ModifiedButton : TextureButton
{
	[Signal]
	public delegate void ModifiedPressedEventHandler(int i);
	[Export]
	private int _num;
	public override void _Ready()
	{
		Pressed += EmitModifiedPressed;
	}
	public void EmitModifiedPressed()
	{
		EmitSignal(SignalName.ModifiedPressed, _num);
	}
	
}
