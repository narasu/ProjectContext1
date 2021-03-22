using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingBlockMoveable : Movable, IBuildingBlock
{
    [SerializeField] Color selectionOutline = new Color32(255, 213, 143, 255);
    [SerializeField] Color grabOutline = new Color32(194, 238, 102, 255);
    Rigidbody rb;
    Collider collider;
    Material material;
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        material = renderer.material;

        resizeScalerX = transform.localScale.x / 10;
        resizeScalerY = transform.localScale.y / 10;
        resizeScalerZ = transform.localScale.z / 10;

        if(SetColorOnStart)
        {
            renderer.material.color = Color;
        }
    }
    public override void Grab()
    {
        base.Grab();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        collider.isTrigger = true;
        gameObject.GetComponent<Outline>().enabled = true;
        gameObject.GetComponent<Outline>().OutlineColor = grabOutline;
    }

    public override void Drop()
    {
        base.Drop();
        collider.isTrigger = false;
        gameObject.GetComponent<Outline>().enabled = false;
        gameObject.GetComponent<Outline>().OutlineColor = selectionOutline;
    }

    public override void HighlightInteractionOn()
    {
        gameObject.GetComponent<Outline>().enabled = true;
    }
    public override void HighlightInteractionOff()
    {
        gameObject.GetComponent<Outline>().enabled = false;
    }

    public override void ActiveEditMaterial()
    {
        picker = FindObjectOfType<HSVPicker.ColorPicker>();
        picker.onValueChanged.AddListener(color =>
        {
            material.color = color;
            Color = color;
        });

        //material.color = picker.CurrentColor;
    }
    public override void DeactiveEditMaterial()
    {
        picker.onValueChanged.RemoveAllListeners();
    }
}
