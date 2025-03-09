using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomThree : MonoBehaviour
{
    [SerializeField] GameObject brownBook;
    [SerializeField] GameObject greenBook;
    [SerializeField] GameObject redBook;

    private bool isBook1Selected = false;
    private bool isBook2Selected = false;
    private bool isBook3Selected = false;

    [SerializeField] GameObject emptyBookshelf;

    [SerializeField] GameObject doorToDestroy;

    // References to instantiated books
    private GameObject instantiatedBrownBook;
    private GameObject instantiatedGreenBook;
    private GameObject instantiatedRedBook;

    // Positions on the empty bookshelf for placing books
    private Vector3 book1PlacementPosition;
    private Vector3 book2PlacementPosition;
    private Vector3 book3PlacementPosition;

    private Quaternion bookPlacementRotation;

    private TextManager textManager;

    void Start()
    {
        textManager = GetComponent<TextManager>();

        book1PlacementPosition = new Vector3(-25.99779f, 0.8761549f, -28.59965f);
        book2PlacementPosition = new Vector3(-25.99779f, 0.8761549f, -28.452f);
        book3PlacementPosition = new Vector3(-25.99779f, 0.8761549f, -28.307f);
        bookPlacementRotation = Quaternion.Euler(0, 90, 0);
    }

    void Update()
    {
        // Check if all books have been placed
        if (isBook1Selected && isBook2Selected && isBook3Selected)
        {
            // Destroy the door to allow progression
            if (doorToDestroy != null)
            {
                doorToDestroy.SetActive(false);

                // Reset the flags to prevent multiple door destruction
                isBook1Selected = false;
                isBook2Selected = false;
                isBook3Selected = false;

                if (textManager != null)
                {
                    textManager.UpdateText("All books placed correctly! The door opens...");
                }
            }
        }
    }

    // Method to check if a GameObject is one of our book objects
    public bool IsBookObject(GameObject obj)
    {
        return obj == brownBook || obj == greenBook || obj == redBook;
    }

    // Method to handle when a book is clicked
    public void SelectBook(GameObject book)
    {
        if (book == brownBook && !isBook1Selected)
        {
            book.SetActive(false);
            if (textManager != null)
            {
                textManager.UpdateText("Brown book selected! Click on the empty bookshelf to place it.");
            }
        }
        else if (book == greenBook && !isBook2Selected)
        {
            book.SetActive(false);
            if (textManager != null)
            {
                textManager.UpdateText("Green book selected! Click on the empty bookshelf to place it.");
            }
        }
        else if (book == redBook && !isBook3Selected)
        {
            book.SetActive(false);
            if (textManager != null)
            {
                textManager.UpdateText("Red book selected! Click on the empty bookshelf to place it.");
            }
        }
    }

    // Method to handle when the empty bookshelf is clicked
    public void HandleBookshelfClick()
    {
        // Check which books have been selected but not placed
        if (!brownBook.activeSelf)
        {
            PlaceBrownBook();
        }
        else if (!greenBook.activeSelf)
        {
            PlaceGreenBook();
        }
        else if (!redBook.activeSelf)
        {
            PlaceRedBook();
        }

        // Check if all books have been placed after this placement
        CheckAllBooksPlaced();
    }

    // Place the brown book on the bookshelf
    private void PlaceBrownBook()
    {
        Vector3 placementPosition = book1PlacementPosition;

        Quaternion placementRotation = bookPlacementRotation;

        instantiatedBrownBook = Instantiate(brownBook, placementPosition, placementRotation);
        instantiatedBrownBook.SetActive(true);
        isBook1Selected = true;

        if (textManager != null)
        {
            textManager.UpdateText("Brown book placed! (" + CountBooksPlaced() + "/3)");
        }
    }

    // Place the green book on the bookshelf
    private void PlaceGreenBook()
    {
        Vector3 placementPosition = book2PlacementPosition;

        Quaternion placementRotation = bookPlacementRotation;

        instantiatedGreenBook = Instantiate(greenBook, placementPosition, placementRotation);
        instantiatedGreenBook.SetActive(true);
        isBook2Selected = true;

        if (textManager != null)
        {
            textManager.UpdateText("Green book placed! (" + CountBooksPlaced() + "/3)");
        }
    }

    // Place the red book on the bookshelf
    private void PlaceRedBook()
    {
        Vector3 placementPosition = book3PlacementPosition;

        Quaternion placementRotation = bookPlacementRotation;

        instantiatedRedBook = Instantiate(redBook, placementPosition, placementRotation);
        instantiatedRedBook.SetActive(true);
        isBook3Selected = true;

        if (textManager != null)
        {
            textManager.UpdateText("Red book placed! (" + CountBooksPlaced() + "/3)");
        }
    }

    // Count the number of books placed
    private int CountBooksPlaced()
    {
        int count = 0;
        if (isBook1Selected) count++;
        if (isBook2Selected) count++;
        if (isBook3Selected) count++;
        return count;
    }

    // Check if all books have been placed and update UI
    private void CheckAllBooksPlaced()
    {
        if (isBook1Selected && isBook2Selected && isBook3Selected)
        {
            if (textManager != null)
            {
                textManager.UpdateText("All books placed! The door is opening...");
            }
        }
    }
}
