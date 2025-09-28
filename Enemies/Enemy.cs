using Godot;
using System;

public partial class Enemy : CharacterBody2D
{

    enum State
    {
        CHASE,
        PATROL,
        IDLE,
        ATTACK
    }

    State CurState;

    private Hurtbox _hurtbox;
    private Area2D _detectionRange;

    public override void _Ready()
    {
        base._Ready();

        _hurtbox = GetNode<Hurtbox>("Hurtbox");
        _hurtbox.Die += die;
        _detectionRange = GetNode<Area2D>("DetectionRange");
        CurState = State.IDLE;
    }

    private void onDetectionRangeAreaEntered(Area2D area)
    {
        if(area.Name == "DetectedRange")
        {
            CurState = State.CHASE;
        }
    }

    private void onDetectionRangeAreaExited(Area2D area)
    {
        if(area.Name == "DetectedRange")
        {
            CurState = State.IDLE;
        }
    }


    private void die()
    {
        GD.Print("died");
    }

}
