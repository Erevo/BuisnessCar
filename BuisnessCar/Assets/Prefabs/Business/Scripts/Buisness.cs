using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buisness : MonoBehaviour
{
    public int Balance;
    public int Profit;
    public Profile Character;
    public int Materials;
    [SerializeField] private GameObject materialPref;
    [SerializeField] private Transform spanwPoint;
    void Start()
    {
        StartCoroutine("Work");
    }


    IEnumerator Work()
    {
        while (true)
        {
            if (Materials > 0)
            {
                Materials--;
                Balance += Profit;
                yield return new WaitForSeconds(1);
            }
            else
                yield return new WaitForSeconds(1);
        }
    }

    public void Withdraw(int count)
    {
        if (count <= Balance)
        {
            if (count == 0)
            {
                Character.Money += Balance;
                Balance = 0;
            }
            else if (count > 0)
            {
                Character.Money += count;
                Balance -= count;
            }
        }
    }

    public void BuyMaterials()
    {
        int orderMaterials = GetComponent<BuisnessUI>().OrderMaterials;
        if (orderMaterials > 0)
        {
            if (Balance >= orderMaterials * 2)
            {
                var materials = Instantiate(materialPref, spanwPoint.position, Quaternion.identity);
                materials.GetComponent<BizMaterials>().Count = orderMaterials;
                Balance -= orderMaterials * 2;
            }
        }
    }
}