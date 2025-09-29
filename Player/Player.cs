using Godot;
using System;

public partial class Player : CharacterBody2D
{

	private Hurtbox _hurtbox;

	[Export]
	public int Speed { get; set; }
	public override void _Ready()
	{
		base._Ready();

		_hurtbox = GetNode<Hurtbox>("hurtbox");
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		getInput();

		GD.Print(GlobalPosition);
	}

	public void die() {
		GD.Print("died");
	}

	public void isPlayer() {}

	public void getInput()
	{
		Vector2 InputDir = Input.GetVector("a", "d", "w", "s");

		GD.Print("Inputdir: ", InputDir);

		Velocity = InputDir.Normalized() * Speed;
	}
}
