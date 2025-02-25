using System.Collections;
using UnityEngine;
using TMPro; // Required for TextMeshPro

public class DisplayMessage : MonoBehaviour
{
    [Tooltip("UI TextMeshPro element to display the message.")]
    public TMP_Text messageText; // Change from "Text" to "TMP_Text"

    [Tooltip("Message to display.")]
    public string message = "Walk to potions to pick them up and place them one at a time in the cauldron in the correct order.";

    [Tooltip("Duration (in seconds) for which the message is visible.")]
    public float displayDuration = 3f;

    void Start()
    {
        if (messageText != null)
        {
            // Set the message and ensure the text is visible
            messageText.text = message;
            messageText.gameObject.SetActive(true);

            // Start coroutine to hide the message after a delay
            StartCoroutine(HideMessageAfterDelay());
        }
        else
        {
            Debug.LogWarning("DisplayMessage: No TextMeshPro UI assigned in the Inspector.");
        }
    }

    IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        messageText.gameObject.SetActive(false);
    }
}
