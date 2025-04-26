using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControllerValidator : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private TextMeshProUGUI listControllerText;
    private string controllers;


    // Update is called once per frame
    void Update()
    {
        controllers = "";
        int count = 0;
        
        ReadOnlyArray<Gamepad> gamepads = Gamepad.all;
        for(int i = 0; i < gamepads.Count; i++)
        {
            controllers += "Gamepad" + i + "\n";
            count++;
        }

        listControllerText.text = controllers;
        if (count >= 2)
        {
            playButton.interactable = true;
        }
        else
        {
            playButton.interactable = false;
        }
    }
}
