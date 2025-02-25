using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player passed through
        {
            Debug.Log("Player passed through the door! Loading Win Scene...");
            SceneManager.LoadScene("WinScene"); // Load the Win scene
        }
    }
}
