using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role
{
    public GameObject role;//model��Ӧ����Ϸ����
    public bool isPriest;
    public bool inBoat;
    public bool onRight;
    public int id;

    public Role(Vector3 position, bool isPriest, int id)
    {
        this.isPriest = isPriest;
        this.id = id;
        onRight = false;
        inBoat = false;
        role = GameObject.Instantiate(Resources.Load("Prefabs/" + (isPriest ? "Priest" : "Devil"), typeof(GameObject))) as GameObject;
        role.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        role.transform.position = position;
        role.name = "role" + id;
        role.AddComponent<Click>();
        role.AddComponent<BoxCollider>();
    }
}