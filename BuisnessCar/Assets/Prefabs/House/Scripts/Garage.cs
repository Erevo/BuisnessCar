using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Garage : MonoBehaviour
{
    [SerializeField] Transform SpawnPoint;
    [SerializeField] List<CarProfile> Cars;


    [SerializeField] RectTransform Item;
    [SerializeField] RectTransform Content;


    PlayerCar PlayerCar;
    CarProfile Car;
    void Start()
    {
        PlayerCar = GameObject.Find("CharacterController").GetComponent<PlayerCar>();
    }


    void Update()
    {

    }

    public void SpawnCar(int id)
    {
        Car = Cars.Find(c => c.Id == id);

        Car.transform.position = SpawnPoint.position;
        Car.transform.rotation = Quaternion.identity;
        Car.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.layer == 8)
        {
            Car = coll.gameObject.GetComponent<CarProfile>();
            Cars.Add(Car);

            if (Car.isActive)
                PlayerCar.LeaveCar();

            Car.gameObject.SetActive(false);
            CreateUiButton();
        }
    }

    void CreateUiButton()
    {
        var instance = GameObject.Instantiate(Item.gameObject, Content.transform);
        var text = instance.GetComponentInChildren<Text>();
        text.text = $" [{Car.Id}] {Car.Name}";
        instance.GetComponent<CarButtonScr>().Id = Car.Id;

        instance.GetComponent<Button>().onClick.AddListener(() =>
        {
            SpawnCar(instance.GetComponent<CarButtonScr>().Id);
            Destroy(instance);
        });
    }
}
