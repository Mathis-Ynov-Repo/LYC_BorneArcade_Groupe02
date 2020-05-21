using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2InputWindow : MonoBehaviour
{
    private Button saveBtn;
    private InputField inputField;

    private void Awake()
    {
        saveBtn = transform.Find("Player2Save").GetComponent<Button>();
        inputField = transform.Find("Player2Field").GetComponent<InputField>();

        inputField.characterLimit = 8;

        saveBtn.onClick.AddListener(delegate { Save(inputField.text); });

    }

    private void Save(string pseudo)
    {
        FindObjectOfType<PseudoManager>().SetPlayer2Pseudo(pseudo);
        Debug.Log(pseudo);
    }
}
