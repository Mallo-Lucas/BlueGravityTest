using System.Collections;
using System.Collections.Generic;
using BlueGravityTest.ScriptableObjects.Items;
using UnityEngine;

public interface I_Slot
{
    public void ReplaceSlotData(ClothesData data);

    public ClothesData GetSlotClotheData();
}
