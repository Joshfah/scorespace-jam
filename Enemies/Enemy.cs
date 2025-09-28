using Godot;
using System;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

public partial class Enemy : CharacterBody2D
{

    [Export]
    public int speed = 100;

    public enum State
    {
        CHASE,
        PATROL,
        IDLE,
        ATTACK
    }

    State CurState;

    private Hurtbox _hurtbox;
    private NavigationAgent2D _navAgent;
    private Area2D _detectionRange;
    public Player Player;

    public override void _Ready()
    {
        base._Ready();

        _hurtbox = GetNode<Hurtbox>("Hurtbox");
        _hurtbox.Die += die;
        _detectionRange = GetNode<Area2D>("DetectionRange");
        CurState = State.IDLE;
        _navAgent = GetNode<NavigationAgent2D>("NavigationAgent2D");
    }

    public override async void _Process(double delta)
    {
        base._Process(delta);

        Random rnd = new Random();

        int i = (int)CurState;
        switch(CurState)
        {
            case State.CHASE:
            {
                _navAgent.TargetPosition = Player.GlobalPosition;
                break;
            }
            case State.IDLE:
            { 
                await ToSignal(GetTree().CreateTimer(rnd.Next(2, 5)), "timeout");
                CurState = State.PATROL;
                break;
            }
            case State.PATROL:
            {
                Vector2 Dir = new Vector2(rnd.Next(-10, 10), rnd.Next(-10, 10));
                Velocity = Dir * speed;
                break;
            }
        }
    }


    private void onDetectionRangeAreaEntered(Area2D area)
    {
        if(area.Name == "DetectedRange")
        {
            CurState = State.CHASE;
            Player = area.GetParent<Player>();
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
