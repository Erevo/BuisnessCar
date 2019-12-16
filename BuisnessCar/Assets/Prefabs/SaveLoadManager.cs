using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SaveLoadManager : MonoBehaviour
{
    private Save sv = new Save();
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
        sv.playerMoney = profile.Money;
        sv.Cars.Clear();
        foreach (var car in garage.CarsInGarage)
        {
            sv.Cars.Add(CarProfileToCarSave(car));
        }
        File.WriteAllText(filePath, JsonUtility.ToJson(sv));
        Debug.Log(JsonUtility.ToJson(sv));
    }

    private CarSave CarProfileToCarSave(CarProfile carProfile)
    {
        CarSave carSave = new CarSave
        {
            Id = carProfile.Id,
            IsActive = carProfile.isActive,
            Name = carProfile.Name,
            PrefabId = carProfile.prefabId,
            Price = carProfile.Price
            
        };
        return carSave;
    }
    private CarProfile CarSaveToCarProfile(CarSave carSave)
    {
        CarProfile carProfile = gameObject.AddComponent<CarProfile>();
        carProfile.Id = carSave.Id;
        carProfile.isActive = carSave.IsActive;
        carProfile.Name = carSave.Name;
        carProfile.prefabId = carSave.PrefabId;
        carProfile.Price = carSave.Price;
        
        
        return carProfile;
    }

    public void LoadGame()
    {
        LoadCars();
        LoadProfile();
    }
    private void LoadCars()
    {
        foreach (var car in sv.Cars)
        {
            garage.CreateUiButton(CarSaveToCarProfile(car));
            Destroy(gameObject.GetComponent<CarProfile>());
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
    public List<CarSave> Cars = new List<CarSave>();
    public int playerMoney;
}
[Serializable]
struct CarSave
{
    public int PrefabId;
    public int Price;
    public int Id;
    public string Name;
    public bool IsActive;
}