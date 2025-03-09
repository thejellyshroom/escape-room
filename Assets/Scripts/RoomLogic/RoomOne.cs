using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomOne : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject[] potions;
    [SerializeField] Material[] potionMaterials;

    private GameObject redPotion;
    private GameObject bluePotion;
    private GameObject greenPotion;

    private Material redMaterial;
    private Material blueMaterial;
    private Material greenMaterial;

    [SerializeField] GameObject hiddenDoor;
    [SerializeField] GameObject cauldronObject; // Renamed from cauldronRenderer to better reflect its purpose
    private Renderer cauldronRenderer; // Actual renderer component

    private List<GameObject> currentIngredients = new List<GameObject>();
    private List<GameObject> correctIngredients = new List<GameObject>();

    private GameObject pendingPotion = null; // Track the pending potion that hasn't been applied yet

    private TextManager textManager;

    // The material index to change (third material)
    private int cauldronMaterialIndex = 2; // This is 0-based, so 2 is the third material

    void Start()
    {
        textManager = GetComponent<TextManager>();
        redPotion = potions[0];
        bluePotion = potions[1];
        greenPotion = potions[2];

        redMaterial = potionMaterials[0];
        blueMaterial = potionMaterials[1];
        greenMaterial = potionMaterials[2];

        correctIngredients.Add(redPotion);
        correctIngredients.Add(greenPotion);


        cauldronRenderer = cauldronObject.GetComponent<Renderer>();

        hiddenDoor.SetActive(true); // Ensure the door starts active (closed)
    }

    // Method to check if a GameObject is one of our potion objects
    public bool IsPotionObject(GameObject obj)
    {
        // Check if the object is in the potions array
        foreach (GameObject potion in potions)
        {
            if (obj == potion)
            {
                return true;
            }
        }
        return false;
    }

    // Method to apply a potion when it's clicked
    public void ApplyPotion(GameObject potion)
    {
        // Store the potion as pending instead of immediately adding it
        pendingPotion = potion;

        // Make the potion GameObject disappear
        potion.SetActive(false);

        textManager.UpdateText("Potion selected. Click the cauldron to apply it!");
    }

    // New method to handle cauldron clicks
    public void HandleCauldronClick()
    {
        if (pendingPotion != null)
        {
            // Add the potion to ingredients (this will also set the color)
            AddIngredient(pendingPotion);

            // Reset the pending potion
            pendingPotion = null;
        }
    }

    public void AddIngredient(GameObject ingredient)
    {
        if (!currentIngredients.Contains(ingredient))
        {
            currentIngredients.Add(ingredient);
            Debug.Log("Added ingredient: " + ingredient.name);

            // Change cauldron color to match the latest potion added
            if (ingredient == redPotion)
            {
                Debug.Log("Setting cauldron color to RED");
                SetCauldronMaterialColor(redMaterial);
            }
            else if (ingredient == bluePotion)
            {
                Debug.Log("Setting cauldron color to BLUE");
                SetCauldronMaterialColor(blueMaterial);
            }
            else if (ingredient == greenPotion)
            {
                Debug.Log("Setting cauldron color to GREEN");
                SetCauldronMaterialColor(greenMaterial);
            }
        }

        // Only check after two potions are added
        if (currentIngredients.Count == 2)
        {
            if (CheckPotion())
            {
                UnlockSecretDoor();
            }
            else
            {
                textManager.UpdateText("Wrong combination! Resetting...");
                StartCoroutine(ResetScene());
            }
        }
    }

    // Method to set the color of the specific material index on the cauldron
    private void SetCauldronMaterialColor(Material newMaterial)
    {
        cauldronRenderer.materials[cauldronMaterialIndex] = newMaterial;
    }

    private bool CheckPotion()
    {
        return currentIngredients.Count == correctIngredients.Count && !currentIngredients.Except(correctIngredients).Any();
    }

    private void UnlockSecretDoor()
    {
        textManager.UpdateText("Correct potion mixed! Unlocking secret door...");
        hiddenDoor.SetActive(false); // Door disappears (opens)
    }

    private IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(2); // Small delay before reset
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
