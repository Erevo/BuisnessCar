using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SaveLoadManager : MonoBehaviour
{
    private Save sv = new Save();
    //private CarSave carSave = new CarSave(); 
    private string filePath;


    [SerializeField] private Garage garage;
    [SerializeField] private Profile profile;
    private void Start()
    {

#if UNITY_ANDROID && !UNITY_EDITOR
        filePath = Path.Combine(Application.persistentDataPath, "Save.json");
#else   
        filePath = Application.dataPath + "/Save.json";
#endif
        Debug.Log(filePath);

        if (File.Exists(filePath))
        {
            sv = JsonUtility.FromJson<Save>(File.ReadAllText(filePath));
            LoadGame();
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            SaveGame();
        if (Input.GetKeyDown(KeyCode.J))
            LoadGame();
    }
    public void SaveGame()
    {
        //sv.Cars = garage.CarsInGarage;
        //sv.playerMoney = profile.Money;

        foreach (var car in garage.CarsInGarage)
        {
            sv.Cars.Add(car);
        }
        File.WriteAllText(filePath, JsonUtility.ToJson(sv));
        Debug.Log(JsonUtility.ToJson(sv));
    }

    public void LoadGame()
    {
        //LoadCars();
        LoadProfile();
    }
    private void LoadCars()
    {
        //garage.CarsInGarage = sv.Cars;
        foreach (var car in sv.Cars)
        {
            CarProfile carProfile = new CarProfile
            {
                Id = car.Id,
                Name = car.Name,
                prefabId = car.prefabId,
                Price = car.Price,
                isActive = car.isActive,
            };
            garage.CreateUiButton(carProfile);
        }
    }
    private void LoadProfile()
    {
        profile.Money = sv.playerMoney;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause) SaveGame();
    }
}

[Serializable]
class Save
{
    public List<CarProfile> Cars;
    public int playerMoney;
    // public List<int> Cars;
}
[Serializable]
public class CarSave
{
    public int prefabId;
    public int Price;
    public int Id;
    public string Name;
    public bool isActive;
}