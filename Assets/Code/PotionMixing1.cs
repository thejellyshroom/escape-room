//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using System.Linq;

//public class PotionMixing : MonoBehaviour
//{
 //   public List<string> correctIngredients = new List<string> { "Red", "Blue" }; // Correct combination
 //   private List<string> currentIngredients = new List<string>();
  //  public GameObject hiddenDoor; // Assign in Inspector
  //  public Renderer cauldronRenderer; // Assign in Inspector
  //  public Color redColor = Color.red;
  //  public Color blueColor = Color.blue;
   // public Color greenColor = Color.green;
  //  public Color defaultColor = Color.black; // Default cauldron color

//    private Dictionary<string, Color> potionColors = new Dictionary<string, Color>();

 //   void Start()
  //  {
 //       potionColors["Red"] = redColor;
   //     potionColors["Blue"] = blueColor;
  //      potionColors["Green"] = greenColor;
   //     cauldronRenderer.material.color = defaultColor;
//
   //     hiddenDoor.SetActive(true); // Ensure the door starts active (closed)
   // }

   // public void AddIngredient(string ingredient)
  //  {
   //     if (!currentIngredients.Contains(ingredient))
   //     {
     //       currentIngredients.Add(ingredient);
     //       Debug.Log("Added: " + ingredient);
     //
            // Change cauldron color to match the latest potion added
    //        if (potionColors.ContainsKey(ingredient))
   //        {
   //             cauldronRenderer.material.color = potionColors[ingredient];
     //       }
   //     }

    //    if (currentIngredients.Count == 2) // Only check after two potions are added
//{
    //        if (CheckPotion())
    //        {
     //           UnlockSecretDoor();
     //       }
     //       else
      //      {
       //         Debug.Log("Wrong combination! Resetting...");
       //         StartCoroutine(ResetScene());
      //      }
       // }
  //  }

   // private bool CheckPotion()
  //  {
 //       return currentIngredients.Count == correctIngredients.Count && !currentIngredients.Except(correctIngredients).Any();
  //  }

  //  private void UnlockSecretDoor()
  //  {
   //     Debug.Log("Correct potion mixed! Unlocking secret door...");
  //      hiddenDoor.SetActive(false); // Door disappears (opens)
  //  }

  //  private IEnumerator ResetScene()
  //  {
  //      yield return new WaitForSeconds(2); // Small delay before reset
  //      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  //  }
//}
