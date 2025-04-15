using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public float StateDuration { get; private set; } = 0;

    // Run once when state is entered
    public virtual void Enter()
    {
        StateDuration = 0;
    }

    // Run once when state is exited
    public virtual void Exit()
    {
    }

    // For Physics
    public virtual void FixedTick()
    {
    }

    // For Update
    public virtual void Tick()
    {
        StateDuration += Time.deltaTime;
    }
}