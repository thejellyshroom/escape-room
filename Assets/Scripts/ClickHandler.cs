using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    private RoomOne roomOneScript;
    private RoomTwo roomTwoScript;

    [SerializeField] private GameObject cauldronObject; // Reference to the cauldron

    void Start()
    {
        roomOneScript = GetComponent<RoomOne>();
        roomTwoScript = GetComponent<RoomTwo>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void HandleMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject clickedObject = hit.collider.gameObject;

            // Room One interactions
            if (roomOneScript != null)
            {
                // Check if the clicked object is one of the potions by asking RoomOne
                if (roomOneScript.IsPotionObject(clickedObject))
                {
                    Debug.Log("Clicked on potion: " + clickedObject.name);
                    roomOneScript.ApplyPotion(clickedObject);
                }
                // Check if the clicked object is the cauldron
                else if (cauldronObject != null && clickedObject == cauldronObject)
                {
                    Debug.Log("Clicked on cauldron");
                    roomOneScript.HandleCauldronClick();
                }
            }

            // Room Two interactions
            if (roomTwoScript != null)
            {
                // Check if the clicked object is a torch
                if (roomTwoScript.IsTorchObject(clickedObject))
                {
                    Debug.Log("Clicked on torch: " + clickedObject.name);
                    roomTwoScript.SelectTorch(clickedObject);
                }
            }

            // Generic click feedback
            if (clickedObject == gameObject)
            {
                Debug.Log("Clicked on " + gameObject.name);
            }
        }
    }

    // Called every frame to check for mouse clicks
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            HandleMouseClick();
        }
    }
}
