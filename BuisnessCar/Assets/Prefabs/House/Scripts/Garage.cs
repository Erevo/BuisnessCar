using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Garage : MonoBehaviour
{
    [SerializeField] public List<GameObject> CarPrefabs;
    [SerializeField] public List<CarProfile> CarsInGarage;

    [SerializeField] private Transform spawnPoint;


    [SerializeField] private RectTransform Item;
    [SerializeField] private RectTransform Content;

    Profile player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Profile>();
    }


    public void SpawnCar(CarProfile car)
    {
        var spawnedCar = Instantiate(CarPrefabs.Find(c => c.GetComponent<CarProfile>().prefabId == car.prefabId));
        spawnedCar.transform.position = spawnPoint.position;
        spawnedCar.transform.rotation = Quaternion.identity;

        CarProfile spawnedCarpr = spawnedCar.GetComponent<CarProfile>();
        spawnedCarpr = car;

        player.Cars.Add(spawnedCar.GetComponent<CarProfile>());
        CarsInGarage.Remove(spawnedCar.GetComponent<CarProfile>());
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            var car = coll.gameObject.GetComponent<CarProfile>();
            if (car.isActive == true)
                return;

            CreateUiButton(car);
            player.Cars.Remove(car);
            Destroy(car.gameObject);
        }
    }
    public void CreateUiButton(CarProfile car)
    {
        var button = Instantiate(Item.gameObject, Content.transform);
        var text = button.GetComponentInChildren<Text>();
        text.text = $" {car.Name}";

        CarProfile btn_CarProfile = button.AddComponent<CarProfile>();
        btn_CarProfile = car;

        CarsInGarage.Add(btn_CarProfile);

        button.GetComponent<Button>().onClick.AddListener(() =>
        {
            SpawnCar(btn_CarProfile);
            Destroy(button);
        });
    }
}
