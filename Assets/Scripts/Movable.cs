using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
    public class Movable : Interactable
    {
        bool isGrabbed;
        public HSVPicker.ColorPicker picker;
        public Color Color = Color.red;
        public bool SetColorOnStart = false;
        new Renderer renderer;
        Rigidbody rb;
        Collider collider;
        Transform playerTransform;
        Transform hand;
        Quaternion startRotation;
        PlayerTransformInHand transformInHand;
        PlayerEditMaterial playerEditMaterial;
        Material material;
        public Color color;

        protected override void Awake()
        {
            base.Awake();
            rb = GetComponent<Rigidbody>();
            collider = GetComponent<Collider>();
            playerTransform = FindObjectOfType<Player>().transform;
            transformInHand = FindObjectOfType<PlayerTransformInHand>();
            picker = FindObjectOfType<HSVPicker.ColorPicker>();
            renderer = gameObject.GetComponent<Renderer>();
            material = gameObject.GetComponent<MeshRenderer>().material;
            material.color = color;
            hand = GameObject.FindGameObjectWithTag("Hand").transform;
        }

        //called from InteractingState.Update()
        public override void HandleInteraction()
        {
            base.HandleInteraction();
        }

        public override void HighlightInteraction()
        {
            Debug.Log("highlihgt");
            gameObject.GetComponent<Outline>().enabled = !gameObject.GetComponent<Outline>().enabled;
        }

        // Set position to player's hand, and set all velocities to zero.
        public void Grab()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.SetParent(playerTransform.GetChild(1).transform);
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.useGravity = true;
            collider.isTrigger = true;
            isGrabbed = true;

            Vector3 heading = transform.position - hand.position;
            hand.position += Vector3.Project(heading, Camera.main.transform.forward);

            startRotation = transform.localRotation;

            Color c = GetComponent<Renderer>().material.color;
            c.a = 0.5f;
            GetComponent<Renderer>().material.color = c;
        }

        void Update()
        {
            if(isGrabbed)
            {
                transform.position = new Vector3(transform.position.x, Player.Instance.Hand.transform.position.y, transform.position.z);
            }
        }

        // turn the gravity back on
        public void Drop()
        {
            transform.SetParent(null);
            //rb.useGravity = true;
            //rb.constraints = RigidbodyConstraints.None;
            collider.isTrigger = false;
            isGrabbed = false;
            Color c = GetComponent<Renderer>().material.color;
            c.a = 1.0f;
            GetComponent<Renderer>().material.color = c;
        }

        public void ActiveEditMaterial()
        {
            Debug.Log("luisteren");
            picker.onValueChanged.AddListener(color =>
            {
                material.color = color;
                Color = color;
            });

            material.color = picker.CurrentColor;
        }

        public void DeactiveEditMaterial()
        {
            Debug.Log("niet luisteren");
            picker.onValueChanged.RemoveListener(color =>{});
            picker.onValueChanged.RemoveAllListeners();
        }
    }
