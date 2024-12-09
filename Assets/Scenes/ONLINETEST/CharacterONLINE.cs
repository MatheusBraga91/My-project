using UnityEngine;
using UnityEngine.UI;  // Add this to access the UI components

public class CharacterOnline : MonoBehaviour
{
    public CharacterStats userCharacterStats; // To hold the reference to your character's asset
    public Image characterImage; // Reference to the UI Image to display the front view sprite

    void Start()
    {
        // Check if the character stats and the image component are properly assigned
        if (userCharacterStats != null && characterImage != null)
        {
            // Set the front view image to the Image component
            characterImage.sprite = userCharacterStats.frontViewImage;  // Set the front image of the character
        }
        else
        {
            Debug.LogError("Front View Image or character's frontViewImage is missing!");
        }

    
    }
}