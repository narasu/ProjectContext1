using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableFSM
{
    public Interactable owner { get; private set; }


    private Dictionary<InteractableStateType, InteractableState> states;

    public InteractableStateType CurrentStateType { get; private set; }
    private InteractableState currentState;
    private InteractableState previousState;

    public void Initialize(Interactable owner)
    {
        this.owner = owner;

        states = new Dictionary<InteractableStateType, InteractableState>();
    }

    public void AddState(InteractableStateType newType, InteractableState newState)
    {
        states.Add(newType, newState);
        states[newType].Initialize(this);
    }

    public void UpdateState()
    {
        currentState?.Update();
    }

    public void GotoState(InteractableStateType key)
    {
        if (!states.ContainsKey(key))
        {
            return;
        }

        currentState?.Exit();

        previousState = currentState;
        CurrentStateType = key;
        currentState = states[CurrentStateType];

        currentState.Enter();
    }

    public InteractableState GetState(InteractableStateType type)
    {
        if (!states.ContainsKey(type))
        {
            return null;
        }
        return states[type];
    }
}