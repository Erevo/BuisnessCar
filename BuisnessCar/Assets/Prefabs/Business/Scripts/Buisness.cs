using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buisness : MonoBehaviour
{
    [SerializeField] GameObject MaterialPref;
    public int Balance;
    public int Dohod;
    public int ChistyDohod;
    public Profile Character;
    public int Materials;
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
                Balance += Dohod;
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
        if (Balance >= GetComponent<BuisnessUI>().OrderMaterials * 2)
        {
            var materials = Instantiate(MaterialPref, new Vector2(0, 5), Quaternion.identity);
            materials.GetComponent<Materials>().Count = GetComponent<BuisnessUI>().OrderMaterials;
            Balance -= GetComponent<BuisnessUI>().OrderMaterials * 2;
        }
    }
}
