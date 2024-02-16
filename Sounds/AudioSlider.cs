using Godot;
using System;

public partial class AudioSlider : HSlider
{
	[Export]
	private string _busName;
	private int _busIndex;
	public override void _Ready()
	{
		_busIndex = AudioServer.GetBusIndex(_busName);
		Value = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(_busIndex));
		this.ValueChanged += ChangeSound;
	}
	public void ChangeSound(double value)
	{
		AudioServer.SetBusVolumeDb(_busIndex, Mathf.LinearToDb((float)value));
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
