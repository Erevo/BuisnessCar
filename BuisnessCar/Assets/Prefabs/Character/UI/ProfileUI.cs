using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileUI : MonoBehaviour
{
    Profile Character;
    [SerializeField] Text balance;
    void Start()
    {
        Character = GetComponentInChildren<Profile>();
    }

    void Update()
    {
        balance.text = Character.Money.ToString();
    }
}
