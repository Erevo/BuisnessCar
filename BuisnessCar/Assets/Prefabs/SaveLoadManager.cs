using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SaveLoadManager : MonoBehaviour
{
    private string filePath;

    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        filePath = Path.Combine(Application.persistentDataPath, "Save.json");
#else   
        filePath = Path.Combine(Application.dataPath, "Save.json");
#endif
    }
    public void SaveGame()
    {

    }

    public void LoadGame()
    {

    }
}
