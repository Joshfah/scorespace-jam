using Godot;
using System;

public partial class Enemy : CharacterBody2D
{

    private Hurtbox _hurtbox;

    public override void _Ready()
    {
        base._Ready();

        _hurtbox = GetNode<Hurtbox>("hurtbox");
        _hurtbox.Die += die;
    }

    private void die()
    {
        GD.Print("died");
    }

}
