using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Garage : MonoBehaviour
{
    [SerializeField]public List<GameObject> CarPrefabs;

    [SerializeField] Transform spawnPoint;
    [SerializeField] public List<int> CarsInGarage;


    [SerializeField] RectTransform Item;
    [SerializeField] RectTransform Content;

    Profile player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Profile>();
    }


    public void SpawnCar(int prefabId)
    {
        var car = Instantiate(CarPrefabs.Find(c => c.GetComponent<CarProfile>().prefabId == prefabId));
        car.transform.position = spawnPoint.position;
        car.transform.rotation = Quaternion.identity;
        car.GetComponent<CarProfile>().Owner = player;
        player.Cars.Add(car.GetComponent<CarProfile>());
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            var car = coll.gameObject.GetComponent<CarProfile>();
            if (car.isActive == true)
                return;

            player.Cars.Remove(car);

            CarsInGarage.Add(car.prefabId);
            Destroy(car.gameObject);
            CreateUiButton(car);
        }
    }
    public void CreateUiButton(CarProfile car)
    {
        var instance = GameObject.Instantiate(Item.gameObject, Content.transform);
        var text = instance.GetComponentInChildren<Text>();
        text.text = $" {car.Name}";
        instance.GetComponent<CarButtonScr>().prefabId = car.prefabId;

        instance.GetComponent<Button>().onClick.AddListener(() =>
        {
            SpawnCar(instance.GetComponent<CarButtonScr>().prefabId);
            Destroy(instance);
        });
    }
}
