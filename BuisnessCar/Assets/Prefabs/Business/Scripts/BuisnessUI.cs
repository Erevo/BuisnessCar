using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuisnessUI : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] InputField inputField;
    public int OrderMaterials;
    Buisness buisness;
    void Start()
    {
        buisness = GetComponent<Buisness>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text =
            $"Balance = {buisness.Balance}\n" +
            $"Dohod = {buisness.Dohod}/sec\n" +
            $"ChistyDohod = {buisness.ChistyDohod}/sec\n" +
            $"Materials = {buisness.Materials}\n";

        if (inputField.textComponent.text.Length != 0)
            OrderMaterials = int.Parse(inputField.textComponent.text);
    }
}
