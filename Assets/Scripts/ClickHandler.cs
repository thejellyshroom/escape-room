using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    private RoomOne roomOneScript;
    private RoomTwo roomTwoScript;
    private RoomThree roomThreeScript;

    [SerializeField] private GameObject cauldronObject; // Reference to the cauldron
    [SerializeField] private GameObject emptyBookshelfObject; // Reference to the empty bookshelf

    void Start()
    {
        roomOneScript = GetComponent<RoomOne>();
        roomTwoScript = GetComponent<RoomTwo>();
        roomThreeScript = GetComponent<RoomThree>();
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

            // Room Three interactions
            if (roomThreeScript != null)
            {
                // Check if the clicked object is one of the books
                if (roomThreeScript.IsBookObject(clickedObject))
                {
                    Debug.Log("Clicked on book: " + clickedObject.name);
                    roomThreeScript.SelectBook(clickedObject);
                }
                // Check if the clicked object is the empty bookshelf
                else if (emptyBookshelfObject != null && clickedObject == emptyBookshelfObject)
                {
                    Debug.Log("Clicked on empty bookshelf");
                    roomThreeScript.HandleBookshelfClick();
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
