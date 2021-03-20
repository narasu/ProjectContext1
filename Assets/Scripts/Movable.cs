using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
    public class Movable : Interactable
    {
        [HideInInspector] public float resizeScaler;
        public Color Color = Color.red;
        public bool SetColorOnStart = false;
        protected HSVPicker.ColorPicker picker;
        bool isGrabbed;
        Transform playerTransform;
        Transform hand;
        Quaternion startRotation;
        PlayerTransformInHand transformInHand;
        PlayerEditMaterial playerEditMaterial;

        protected override void Awake()
        {
            base.Awake();
            playerTransform = FindObjectOfType<Player>().transform;
            transformInHand = FindObjectOfType<PlayerTransformInHand>();
            hand = GameObject.FindGameObjectWithTag("Hand").transform;
        }

        //called from InteractingState.Update()
        public override void HandleInteraction()
        {
            base.HandleInteraction();
        }

        public virtual void Grab()
        {
            transform.SetParent(playerTransform.GetChild(1).transform);
            isGrabbed = true;

            Vector3 heading = transform.position - hand.position;
            hand.position += Vector3.Project(heading, Camera.main.transform.forward);

            startRotation = transform.localRotation;
        }

        protected override void Update()
        {
            base.Update();
            if(isGrabbed)
            {
                transform.position = new Vector3(transform.position.x, Player.Instance.Hand.transform.position.y, transform.position.z);
            }
        }

        public virtual void Drop()
        {
            fsm.GotoState(InteractableStateType.Normal);
            Debug.Log("drop2");
            transform.SetParent(null);
            isGrabbed = false;
        }
        public virtual void ActiveEditMaterial() {}
        public virtual void DeactiveEditMaterial() {}
    }
