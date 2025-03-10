using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomThree : MonoBehaviour
{
    [SerializeField] GameObject brownBook;
    [SerializeField] GameObject greenBook;
    [SerializeField] GameObject redBook;

    private bool isBrownBookPlaced = false;
    private bool isGreenBookPlaced = false;
    private bool isRedBookPlaced = false;

    [SerializeField] GameObject emptyBookshelf;

    [SerializeField] GameObject doorToDestroy;

    // Positions on the empty bookshelf for placing books
    private Vector3 brownBookPlacementPosition;
    private Vector3 greenBookPlacementPosition;
    private Vector3 redBookPlacementPosition;

    private Quaternion bookPlacementRotation;

    private TextManager textManager;

    void Start()
    {
        textManager = GetComponent<TextManager>();

        brownBookPlacementPosition = new Vector3(2.083f, 16.525f, 6.855f);
        greenBookPlacementPosition = new Vector3(2.083f, 16.525f, 6.994f);
        redBookPlacementPosition = new Vector3(2.083f, 16.525f, 7.133f);
        bookPlacementRotation = Quaternion.Euler(0, 90, 0);
    }

    void Update()
    {

    }

    // Method to check if a GameObject is one of our book objects
    public bool IsBookObject(GameObject obj)
    {
        return obj == brownBook || obj == greenBook || obj == redBook;
    }

    // Method to handle when a book is clicked
    public GameObject SelectBook(GameObject book)
    {
        GameObject selectedBook = null;
        if (book == brownBook)
        {
            book.SetActive(false);
            if (textManager != null)
            {
                textManager.UpdateText("Brown book selected! Click on the empty bookshelf to place it.");
            }
            selectedBook = brownBook;
        }
        else if (book == greenBook)
        {
            book.SetActive(false);
            if (textManager != null)
            {
                textManager.UpdateText("Green book selected! Click on the empty bookshelf to place it.");
            }
            selectedBook = greenBook;
        }
        else if (book == redBook)
        {
            book.SetActive(false);
            if (textManager != null)
            {
                textManager.UpdateText("Red book selected! Click on the empty bookshelf to place it.");
            }
            selectedBook = redBook;
        }
        return selectedBook;
    }

    // Method to handle when the empty bookshelf is clicked
    public void HandleBookshelfClick(GameObject selectedBook)
    {
        if (selectedBook == brownBook)
        {
            PlaceBook(selectedBook);
        }
        else if (selectedBook == greenBook)
        {
            PlaceBook(selectedBook);
        }
        else if (selectedBook == redBook)
        {
            PlaceBook(selectedBook);
        }

        // Check if all books have been placed after this placement
        CheckAllBooksPlaced();
    }

    // Place the book on the bookshelf
    private void PlaceBook(GameObject book)
    {
        if (book == brownBook)
        {
            // Set local position/rotation relative to parent
            book.transform.localPosition = brownBookPlacementPosition;
            book.transform.localRotation = bookPlacementRotation;
            book.SetActive(true);
            isBrownBookPlaced = true;
        }
        else if (book == greenBook)
        {
            book.transform.localPosition = greenBookPlacementPosition;
            book.transform.localRotation = bookPlacementRotation;
            book.SetActive(true);
            isGreenBookPlaced = true;
        }
        else if (book == redBook)
        {
            book.transform.localPosition = redBookPlacementPosition;
            book.transform.localRotation = bookPlacementRotation;
            book.SetActive(true);
            isRedBookPlaced = true;
        }
    }

    // Check if all books have been placed and update UI
    private void CheckAllBooksPlaced()
    {
        if (isBrownBookPlaced && isGreenBookPlaced && isRedBookPlaced)
        {
            if (textManager != null)
            {
                textManager.UpdateText("All books placed! The door is opening...");
            }
            Destroy(doorToDestroy);
        }
    }
}
