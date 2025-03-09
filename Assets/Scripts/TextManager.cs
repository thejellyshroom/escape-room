using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Start()
    {
        text.text = "";
    }

    public void UpdateText(string newText)
    {
        text.text = newText;

        // Wait for 5 seconds before clearing the text
        Invoke("HideText", 2f);
    }

    public void HideText()
    {
        text.text = "";
    }
}
