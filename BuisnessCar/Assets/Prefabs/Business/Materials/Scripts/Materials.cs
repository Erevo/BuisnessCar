using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Materials : MonoBehaviour
{
    public int Count;
    Text text;

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
        if (collision.gameObject.layer == 8)
        {
            transform.SetParent(collision.transform);
            transform.position = collision.gameObject.GetComponent<CarProfile>().materialsPoint.position;
            Destroy(GetComponent<Rigidbody2D>());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 11)
        {
            collision.gameObject.GetComponent<Buisness>().Materials += Count;
            Destroy(gameObject);
        }
    }
}
