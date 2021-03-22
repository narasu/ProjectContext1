using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildingBlock
{
    void Grab(Transform player);
    void Drop();
    void ActiveEditMaterial();
    void DeactiveEditMaterial();
}
