using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image[] heartIcons; // Array to store references to the heart icon images
    public Sprite fullHeartSprite; // Sprite for a full heart
    public Sprite emptyHeartSprite; // Sprite for an empty heart

    // Update the heart icons based on the player's current health
    public void UpdateHearts(int currentHealth)
    {
        // Iterate through each heart icon
        for (int i = 0; i < heartIcons.Length; i++)
        {
            // Check if the index is less than the current health
            if (i < currentHealth)
            {
                // Set the sprite to represent a full heart
                heartIcons[i].sprite = fullHeartSprite;
            }
            else
            {
                // Set the sprite to represent an empty heart
                heartIcons[i].sprite = emptyHeartSprite;
            }
        }
    }
}
