using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Garage : MonoBehaviour
{
    [SerializeField] List<GameObject> CarPrefabs;

    [SerializeField] Transform spawnPoint;
    [SerializeField] List<CarProfile> Cars;


    [SerializeField] RectTransform Item;
    [SerializeField] RectTransform Content;


    PlayerCarController PlayerCarController;
    private void Start()
    {
        PlayerCarController = GameObject.Find("CharacterController").GetComponent<PlayerCarController>();
    }


    public void SpawnCar(int prefabId)
    {
        Profile player = GameObject.FindGameObjectWithTag("Player").GetComponent<Profile>();
        var car = Instantiate(CarPrefabs.Find(c => c.GetComponent<CarProfile>().prefabId == prefabId));
        car.transform.position = spawnPoint.position;
        car.transform.rotation = Quaternion.identity;
        car.GetComponent<CarProfile>().Owner = player;
        player.Cars.Add(car.GetComponent<CarProfile>());


        //Car = Cars.Find(c => c.Id == id);
        //Car.transform.position = SpawnPoint.position;
        //Car.transform.rotation = Quaternion.identity;
        //Car.gameObject.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            var car = coll.gameObject.GetComponent<CarProfile>();
            if (car.isActive == true)
                return;

            Cars.Add(car);
            Destroy(car.gameObject);
            CreateUiButton(car);
        }
    }
    void CreateUiButton(CarProfile car)
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
