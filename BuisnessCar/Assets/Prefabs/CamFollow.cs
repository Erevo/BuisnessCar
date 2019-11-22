using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    Camera cam;
    [SerializeField] float CamSpeed = 0.1f;

    GameObject player;

    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 pos = new Vector3(player.transform.position.x, cam.transform.position.y, cam.transform.position.z);
        cam.transform.position = Vector3.Lerp(cam.transform.position, pos, CamSpeed * Time.deltaTime);
    }
}
