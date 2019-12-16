using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BizMaterials : MonoBehaviour
{
    public int Count;
    private CarTrunk trunk;
    private Text text;

    void Start()
    {
        text = GetComponentInChildren<Text>();
    }

    void Update()
    {
        text.text = Count.ToString();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            trunk = collision.gameObject.GetComponent<CarTrunk>();

            if (trunk.IsUsed == false && Count <= trunk.Capacity)
            {
                trunk.IsUsed = true;

                Transform materialsPoint = collision.gameObject.GetComponent<CarProfile>().materialsPoint;

                transform.SetParent(collision.transform);
                transform.position = materialsPoint.position;
                transform.rotation = materialsPoint.rotation;
                Destroy(GetComponent<Rigidbody2D>());
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Buisness"))
        {
            if (trunk != null) trunk.IsUsed = false;
            collision.gameObject.GetComponent<Buisness>().Materials += Count;
            Destroy(gameObject);
        }
    }
}
