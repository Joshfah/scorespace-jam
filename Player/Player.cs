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
        _hurtbox.Die += die;

        Velocity = Vector2.Zero;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        getInput();
        MoveAndSlide();
    }

    private void die() {
        QueueFree();
    }

    public void getInput()
    {
        Vector2 InputDir = Input.GetVector("a", "d", "w", "s");

        Velocity = InputDir.Normalized() * Speed;

    }
}