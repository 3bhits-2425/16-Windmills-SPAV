using UnityEngine;
using UnityEngine.UI;

public class ButtonInteraction : MonoBehaviour
{
    public Button myButton;

    void Start()
    {
        myButton.onClick.AddListener(DisableButton);
    }

    void DisableButton()
    {
        myButton.interactable = false;
    }
}
