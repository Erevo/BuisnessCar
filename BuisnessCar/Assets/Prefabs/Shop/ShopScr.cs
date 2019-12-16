using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScr : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] List<CarProfile> CarsCatalog;
    Profile player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Profile>();
    }
    public void BuyCar(int prefabId)
    {
        var ChoosedCar = CarsCatalog.Find(car => car.prefabId == prefabId);
        ChoosedCar.Id = player.Cars.Count + 1;

        if (player.Money < ChoosedCar.GetComponent<CarProfile>().Price)
            return;
        else
            player.Money -= ChoosedCar.GetComponent<CarProfile>().Price;

        SpawnCar(ChoosedCar);
    }

    public void SpawnCar(CarProfile carProfile)
    {
        var car = Instantiate(carProfile.gameObject);
        car.transform.position = spawnPoint.position;
        car.transform.rotation = Quaternion.identity;

        car.GetComponent<CarProfile>().Owner = player;
        player.Cars.Add(car.GetComponent<CarProfile>());
    }
}
