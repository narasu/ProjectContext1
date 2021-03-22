using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableStateType { Normal, Highlighted, Interacting }

public abstract class InteractableState
{
    protected InteractableFSM owner;
    protected Interactable interactable;

    public void Initialize(InteractableFSM owner)
    {
        this.owner = owner;
        interactable = owner.owner;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

public class NormalState : InteractableState 
{
    public override void Enter()
    {
        //interactable.spriteRenderer.sprite = interactable.normalSprite;
    }
    public override void Update()
    {
        
    }
    public override void Exit()
    {
        
    }
}
public class HighlightedState : InteractableState
{
    public override void Enter()
    {
        interactable.HighlightInteractionOn();
    }
    public override void Update()
    {
        if (PlayerLook.Instance.GetTarget()!=interactable)
        {
            interactable.GotoNormal();
        }
    }
    public override void Exit()
    {
        interactable.HighlightInteractionOff();
    }
}
public class InteractingState : InteractableState
{
    public override void Enter()
    {
        
    }
    public override void Update()
    {
        interactable.HandleInteraction();
    }
    public override void Exit()
    {
        
    }
}