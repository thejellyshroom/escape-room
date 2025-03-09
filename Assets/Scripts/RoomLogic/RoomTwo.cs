using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class RoomTwo : MonoBehaviour
{
    [SerializeField] GameObject[] correctTorches;
    [SerializeField] GameObject doorToDestroy;
    [SerializeField] Material highlightMaterial;
    [SerializeField] Material originalTorchMaterial;

    private List<GameObject> selectedTorches = new List<GameObject>();
    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();
    private TextManager textManager;

    void Start()
    {
        textManager = GetComponent<TextManager>();

        if (doorToDestroy == null)
        {
            Debug.LogError("Door to destroy is not assigned in RoomTwo script!");
        }

        if (correctTorches.Length == 0)
        {
            Debug.LogError("No correct torches assigned in RoomTwo script!");
        }
    }

    // Method to check if a GameObject is one of our torch objects
    public bool IsTorchObject(GameObject obj)
    {
        // Check if the object has a tag like "Torch" or you can do a name check
        return obj.name.ToLower().Contains("torch");
    }

    // Method to handle torch selection
    public void SelectTorch(GameObject torch)
    {
        if (!selectedTorches.Contains(torch))
        {
            // Highlight the torch (change its material or add a visual indicator)
            Destroy(torch);

            // Add the torch to selected list
            selectedTorches.Add(torch);

            if (textManager != null)
            {
                textManager.UpdateText("Torch selected! (" + selectedTorches.Count + "/2)");
            }

            // Check if we have selected 2 torches
            if (selectedTorches.Count == 2)
            {
                // Check if the selected torches match the correct ones
                if (CheckTorches())
                {
                    if (textManager != null)
                    {
                        textManager.UpdateText("Correct torches selected! The door opens...");
                    }
                    DestroyDoor();
                }
                else
                {
                    if (textManager != null)
                    {
                        textManager.UpdateText("Wrong combination! Resetting...");
                    }
                    StartCoroutine(ResetScene());
                }
            }
        }
    }


    // Check if the selected torches match the correct ones
    private bool CheckTorches()
    {
        if (selectedTorches.Count != correctTorches.Length)
            return false;

        // Check if all correct torches are in the selected torches list
        foreach (GameObject correctTorch in correctTorches)
        {
            if (!selectedTorches.Contains(correctTorch))
            {
                return false;
            }
        }

        return true;
    }

    // Destroy the door when the correct torches are selected
    private void DestroyDoor()
    {
        if (doorToDestroy != null)
        {
            doorToDestroy.SetActive(false);
        }
    }

    // Reset the scene (like RoomOne does)
    private IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(2); // Small delay before reset
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {

    }
}
