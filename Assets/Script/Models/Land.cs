using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land
{
    public GameObject land;
    public int priestCount, devilCount;
    public Land(Vector3 position)
    {
        land = GameObject.Instantiate(Resources.Load("Prefabs/Land", typeof(GameObject))) as GameObject;
        land.transform.position = position;
        priestCount = devilCount = 0;
    }
}