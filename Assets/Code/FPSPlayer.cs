using UnityEngine;

public class FPSPlayer : MonoBehaviour
{
    int moveSpeed = 5;
    float lockSpeedx = 6f; // left-right sensitivity
    float lockSpeedy = 3f; // up-down sensitivity

    Transform camTrans;
    float xRotation;
    float yRotation;
    Rigidbody rb;
    int jumpForce = 300;
    public LayerMask groundLayer;
    public Transform feetTrans;
    float groundCheckDist = .5f;
    public bool grounded = false;

    private string heldPotion = null; // Stores the picked-up potion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camTrans = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        Vector3 moveDir = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        moveDir *= moveSpeed;
        moveDir.y = rb.linearVelocity.y;
        rb.linearVelocity = moveDir;

        grounded = Physics.CheckSphere(feetTrans.position, groundCheckDist, groundLayer);
    }

    void Update()
    {
        yRotation += Input.GetAxis("Mouse X") * lockSpeedx;
        xRotation -= Input.GetAxis("Mouse Y") * lockSpeedy;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        camTrans.localEulerAngles = new Vector3(xRotation, 0, 0);
        transform.eulerAngles = new Vector3(0, yRotation, 0);

        if (grounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Pickup potion
        if (other.CompareTag("Potion"))
        {
            if (heldPotion == null) // Pick up only if not already holding one
            {
                heldPotion = other.GetComponent<Potion>().ingredientColor;
                Destroy(other.gameObject); // Remove potion from world
                Debug.Log("Picked up: " + heldPotion);
            }
        }

        // Drop potion into cauldron
        if (other.CompareTag("Cauldron") && heldPotion != null)
        {
            other.GetComponent<PotionMixing>().AddIngredient(heldPotion);
            Debug.Log("Dropped " + heldPotion + " into cauldron");
            heldPotion = null; // Clear held potion
        }
    }
}
