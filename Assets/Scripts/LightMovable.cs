using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMovable : Movable
{
    [SerializeField] GameObject lightGizmo;
    bool isActive;
    Light light;
    protected override void Awake()
    {
        base.Awake();
        light = GetComponent<Light>();
    }
    public override void Grab()
    {
        base.Grab();
    }
    public override void Drop()
    {
        base.Drop();
    }
    public override void HighlightInteraction()
    {
        isActive = !isActive;
        lightGizmo.SetActive(isActive);
    }
    public override void ActiveEditMaterial()
    {
        picker.onValueChanged.AddListener(color =>
        {
            light.color = color;
            Color = color;
        });

        light.color = picker.CurrentColor;
    }
    public override void DeactiveEditMaterial()
    {
        picker.onValueChanged.RemoveAllListeners();
    }
}
