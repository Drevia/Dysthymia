using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    public int level;
    public float[] position;

    public PlayerData (InventoryPlayer inventory)
    {
        level = InventoryPlayer.instance.level;

        position = new float[3];
        position[0] = inventory.transform.position.x;
        position[1] = inventory.transform.position.y;
        position[2] = inventory.transform.position.z;
    }
}
