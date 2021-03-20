using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildingBlock
{
    void Grab();
    void Drop();
    void ActiveEditMaterial();
    void DeactiveEditMaterial();
}
