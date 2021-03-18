using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IClickable
{
    protected InteractableFSM fsm;
    protected bool shouldHighlight;

    protected Renderer renderer;
    //[SerializeField] private GameObject light;

    //Create FSM
    protected virtual void Awake()
    {
        renderer = GetComponent<Renderer>();
        //startingMaterial = renderer.material;

        fsm = new InteractableFSM();
        
    }

    protected virtual void Start()
    {
        //Initialize FSM and add states
        fsm.Initialize(this);

        fsm.AddState(InteractableStateType.Normal, new NormalState());
        fsm.AddState(InteractableStateType.Highlighted, new HighlightedState());
        fsm.AddState(InteractableStateType.Interacting, new InteractingState());

        //start in normal state
        GotoNormal();
    }

    protected virtual void Update()
    {
        fsm.UpdateState();
    }

    //Go to highlighted state, called from PlayerLook when player is looking at an interactable object
    public void Highlight()
    {
        if (fsm.CurrentStateType==InteractableStateType.Normal)
        {
            GotoHighlighted();
        }
        
    }
    /*
    public void SetLight(bool highlighted)
    {
        if (highlighted)
        {
            light.SetActive(true);
        }
        else
        {
            light.SetActive(false);
        }
    }
    */

    public virtual void HandleInteraction() {}
    public virtual void HighlightInteraction() {}
    public virtual void GotoNormal()
    {
        fsm.GotoState(InteractableStateType.Normal);
    }
    public virtual void GotoHighlighted()
    {
        fsm.GotoState(InteractableStateType.Highlighted);
    }
    public virtual void GotoInteracting()
    {
        fsm.GotoState(InteractableStateType.Interacting);
    }
}