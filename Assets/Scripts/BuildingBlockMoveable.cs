using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlockMoveable : Movable
{
    Rigidbody rb;
    Collider collider;
    Material material;
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        material = renderer.material;
    }
    public override void Grab()
    {
        base.Grab();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        collider.isTrigger = true;
    }

    public override void Drop()
    {
        base.Drop();
        collider.isTrigger = false;
    }

    public override void HighlightInteraction()
    {
        gameObject.GetComponent<Outline>().enabled = !gameObject.GetComponent<Outline>().enabled;
    }

    public override void ActiveEditMaterial()
    {
        picker.onValueChanged.AddListener(color =>
        {
            material.color = color;
            Color = color;
        });

        material.color = picker.CurrentColor;
    }
    public override void DeactiveEditMaterial()
    {
        picker.onValueChanged.RemoveAllListeners();
    }
}
