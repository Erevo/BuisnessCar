using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScr : MonoBehaviour
{
    [SerializeField] GameObject SpawnPoint;
    [SerializeField] List<GameObject> CarsCatalog;
    [SerializeField] Profile Character;

    public void BuyCar(string carName)
    {
        var ChoosedCar = CarsCatalog.Find(car => car.name == carName);

        if (Character.Money < ChoosedCar.GetComponent<CarProfile>().Price)
            return;
        else
            Character.Money -= ChoosedCar.GetComponent<CarProfile>().Price;

        var NewCar = Instantiate(ChoosedCar, SpawnPoint.transform.position, Quaternion.identity);

        CarProfile NewCarProfile = NewCar.GetComponent<CarProfile>();

        NewCar.name = NewCarProfile.Name;
        NewCarProfile.Id = Character.Cars.Count + 1;
        NewCarProfile.Owner = Character;




        Character.Cars.Add(NewCar.GetComponent<CarProfile>());
    }
}
