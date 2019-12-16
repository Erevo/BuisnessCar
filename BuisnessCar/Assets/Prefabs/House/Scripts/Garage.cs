using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Garage : MonoBehaviour
{
    public List<GameObject> CarPrefabs;
    public List<CarProfile> CarsInGarage;

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

        CarProfile spawnedCarProfile = spawnedCar.GetComponent<CarProfile>();
        CopyCarProfile(spawnedCarProfile, car);

        player.Cars.Add(spawnedCarProfile);
        CarsInGarage.Remove(spawnedCarProfile);
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            var car = coll.GetComponent<CarProfile>();
            if (car.isActive == true)
                return;

            CreateUiButton(car);
            Destroy(car.gameObject);
            player.Cars.Remove(car);
        }
    }
    public void CreateUiButton(CarProfile car)
    {
        var button = Instantiate(Item.gameObject, Content.transform);
        var text = button.GetComponentInChildren<Text>();
        text.text = $" {car.Name}";

        CarProfile btn_CarProfile = button.AddComponent<CarProfile>();
        CopyCarProfile(btn_CarProfile, car);
        CarsInGarage.Add(btn_CarProfile);

        button.GetComponent<Button>().onClick.AddListener(() =>
        {
            SpawnCar(btn_CarProfile);
            Destroy(button);
        });
    }

    private CarProfile CopyCarProfile(CarProfile btn_carProfile, CarProfile carProfile)
    {
        btn_carProfile.Id = carProfile.Id;
        btn_carProfile.isActive = carProfile.isActive;
        //btn_carProfile.materialsPoint = carProfile.materialsPoint;
        btn_carProfile.Name = carProfile.Name;
        btn_carProfile.Owner = carProfile.Owner;
        btn_carProfile.prefabId = carProfile.prefabId;
        btn_carProfile.Price = carProfile.Price;

        return btn_carProfile;
    }
}
