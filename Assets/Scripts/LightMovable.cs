using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightMovable : Movable, IBuildingBlock
{
    [SerializeField] GameObject lightGizmo;
    Light light;
    Slider slider;
    bool editLight;
    protected override void Awake()
    {
        base.Awake();
        light = GetComponent<Light>();
        slider = FindObjectOfType<HSVPicker.ColorPicker>().transform.parent.GetChild(1).GetComponent<Slider>();
        slider.gameObject.SetActive(false);
    }
    public override void Grab()
    {
        base.Grab();
        lightGizmo.SetActive(true);
    }
    public override void Drop()
    {
        base.Drop();
        Debug.Log("drop");
        lightGizmo.SetActive(false);
    }
    protected override void Update()
    {
        base.Update();
        if(editLight)
        {
            light.intensity = 0.1f + 20 * slider.value;
        }
    }
    public override void HighlightInteractionOn()
    {
        lightGizmo.SetActive(true);
    }
    public override void HighlightInteractionOff()
    {
        lightGizmo.SetActive(false);
    }
    public override void ActiveEditMaterial()
    {
        picker = FindObjectOfType<HSVPicker.ColorPicker>();
        picker.onValueChanged.AddListener(color =>
        {
            light.color = color;
            Color = color;
        });
        light.color = picker.CurrentColor;

        editLight = true;
        slider.gameObject.SetActive(true);
    }
    public override void DeactiveEditMaterial()
    {
        picker.onValueChanged.RemoveAllListeners();
        slider.gameObject.SetActive(false);
        editLight = false;
    }
}
