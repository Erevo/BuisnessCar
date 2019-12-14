using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarProfile : MonoBehaviour
{
    public enum Models
    {
        DodgeCaravan,
        BMW,
        Porche
    }

    public int prefabId;
    public Transform materialsPoint;
    public int Price;
    public int Id;
    public string Name;
    public Profile Owner;
    public bool isActive = false;
}
