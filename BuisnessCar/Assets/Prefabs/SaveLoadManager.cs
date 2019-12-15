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
        sv.Cars = garage.CarsInGarage;
        sv.playerMoney = profile.Money;
        File.WriteAllText(filePath, JsonUtility.ToJson(sv));
        Debug.Log(JsonUtility.ToJson(sv));
    }

    public void LoadGame()
    {
        LoadCars();
        LoadProfile();
    }
    private void LoadCars()
    {
        garage.CarsInGarage = sv.Cars;
        foreach (var car in garage.CarsInGarage)
        {
            CarProfile carProfile = garage.CarPrefabs.Find(c => c.GetComponent<CarProfile>().prefabId == car).GetComponent<CarProfile>();
            carProfile.prefabId = car;
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
public class Save
{
    public List<int> Cars;
    public int playerMoney;
}

