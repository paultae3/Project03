using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineMB : MonoBehaviour
{
    public State CurrentState { get; private set; }
    private State _previousState;
    private bool _inTransition = false;

    // Change to new state
    public void ChangeState(State newState)
    {
        if (CurrentState == newState || _inTransition)
            return;
        ChangeStateSequence(newState);
    }

    // State transition sequence
    private void ChangeStateSequence(State newState)
    {
        _inTransition = true;

        // Exit current state
        CurrentState?.Exit();

        // Store previous state logic
        StoreStateAsPrevious(newState);

        // Assign new state
        CurrentState = newState;

        // Enter new state
        CurrentState?.Enter();

        _inTransition = false;
    }

    // Store previous state logic
    private void StoreStateAsPrevious(State newState)
    {
        if (_previousState == null && newState != null)
            _previousState = newState;
        else if (_previousState != null && CurrentState != null)
            _previousState = CurrentState;
    }

    // Revert to previous state
    public void ChangeStateToPrevious()
    {
        if (_previousState != null)
            ChangeState(_previousState);
        else
            Debug.LogWarning("There is no previous state to change to!");
    }

    // Update loop
    protected virtual void Update()
    {
        if (CurrentState != null && !_inTransition)
            CurrentState.Tick();
    }

    // Physics loop
    protected virtual void FixedUpdate()
    {
        if (CurrentState != null && !_inTransition)
            CurrentState.FixedTick();
    }

    // Cleanup on destruction
    protected virtual void OnDestroy()
    {
        CurrentState?.Exit();
    }
}