using Godot;
using System;

public partial class EnemyHitbox : Area2D
{
    [Export]
    public int Damage { get; set; }

    public Hurtbox PlayerHurtbox;

    public override void _Ready()
    {
        base._Ready();

        SetProcess(false);
    }

    public override async void _Process(double delta)
    {
        base._Process(delta);

        await ToSignal(GetTree().CreateTimer(1), "timeout");

        PlayerHurtbox.getDamage(Damage);
    }

    public void onAreaEntered(Area2D area) 
    {
        if(area is Hurtbox hurtbox && area.GetParent().HasMethod("Player"))
        {
            PlayerHurtbox = hurtbox;
            SetProcess(true);
        }
    }

    public void onAreaExited(Area2D area)
    {
        if(area is Hurtbox && area.GetParent().HasMethod("Player"))
        {
            PlayerHurtbox = null;
            SetProcess(false);
        }
    }


}
