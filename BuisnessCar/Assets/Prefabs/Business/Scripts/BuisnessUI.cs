using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuisnessUI : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] InputField inputField;
    [HideInInspector] public int OrderMaterials;
    private Buisness buisness;
    void Start()
    {
        buisness = GetComponent<Buisness>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text =
            $"Balance = {buisness.Balance}\n" +
            $"Profit = {buisness.Profit}/sec\n" +
            $"Materials = {buisness.Materials}\n";

        if (inputField.textComponent.text.Length != 0)
            OrderMaterials = int.Parse(inputField.textComponent.text);
    }
}
