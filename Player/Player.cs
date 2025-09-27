using Godot;
using System;

public partial class Player : CharacterBody2D
{

    [Export]
    public int Speed = 100;
    public override void _Ready()
    {
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        getInput();
    }

    public void getInput()
    {
        Vector2 InputDir = Input.GetVector("a", "d", "w", "s");

        Velocity = InputDir.Normalized() * Speed;

        GD.Print(GlobalPosition);

        MoveAndSlide();

    }
}