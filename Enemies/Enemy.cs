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
    private EnemyHitbox _hitbox;
    private NavigationAgent2D _navAgent;
    private Area2D _detectionRange;
    //public Player PlayerRef;

    public override void _Ready()
    {
        base._Ready();

        _hitbox = GetNode<EnemyHitbox>("EnemyHitbox");
        _hitbox.Visible = false;
        _hurtbox = GetNode<Hurtbox>("Hurtbox");
        _hurtbox.Die += die;
        _detectionRange = GetNode<Area2D>("DetectionRange");
        CurState = State.IDLE;
        _navAgent = GetNode<NavigationAgent2D>("NavigationAgent2D");

    }

    public override async void _Process(double delta)
    {
        base._Process(delta);
/* 
        Random rnd = new Random();

        switch(CurState)
        {
            case State.CHASE:
            {
                _hitbox.Visible = false;
                _navAgent.TargetPosition = Player.GlobalPosition;
                break;
            }
            case State.IDLE:
            { 
                _hitbox.Visible = false;
                await ToSignal(GetTree().CreateTimer(rnd.Next(2, 5)), "timeout");
                CurState = State.PATROL;
                break;
            }
            case State.PATROL:
            {
                _hitbox.Visible = false;
                handlePatrol();
                break;
            }
            case State.ATTACK:
            {
                _hitbox.Visible = true;
                break;
            }

        }

        Velocity = _navAgent.GetNextPathPosition().Normalized() * speed;
        MoveAndSlide(); */
    }

    private void onNavigationAgent2dTargetReached()
    {
        CurState = State.ATTACK;
    }

    public void handlePatrol() {

        bool reached = GlobalPosition.DistanceTo(_navAgent.TargetPosition) <= 2f;
        Random rnd = new();
        _navAgent.TargetPosition = new(GlobalPosition.X + rnd.Next(-10, 10), GlobalPosition.Y + rnd.Next(-10, 10));
        if(!_navAgent.IsTargetReachable() || reached)
        {
            _navAgent.TargetPosition = new(GlobalPosition.X + rnd.Next(-10, 10), GlobalPosition.Y + rnd.Next(-10, 10));
        }

    }

    private void onDetectionRangeAreaEntered(Area2D area)
    {
        if(area.Name == "DetectedRange")
        {
            CurState = State.CHASE;
            //PlayerRef = area.GetParent<Player>();
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
