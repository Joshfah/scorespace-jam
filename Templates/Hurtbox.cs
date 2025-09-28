using Godot;
using System;
using System.Linq.Expressions;

public partial class Hurtbox : Area2D
{
    [Signal]
    public delegate void DieEventHandler();

    [Export]
    private int _hp;

    public int HP
    {
        get => _hp;
        set {
            int newVal = value;
            _hp = newVal;
            if(newVal <= 0) emitDie();
        }
    }

    public override void _Ready()
    {
        base._Ready();

        SetProcess(false);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        
    }

    public void getDamage(int Damage) 
    {
        HP -= Damage;
    }

    public void emitDie()
    {
        EmitSignal("die");
    }

}
