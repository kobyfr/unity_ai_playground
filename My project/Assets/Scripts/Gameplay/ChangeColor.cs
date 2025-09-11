// 9/9/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float timer = 0f;
    private float changeInterval = 5f;

    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on this GameObject!");
        }
    }

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if it's time to change color
        if (timer >= changeInterval)
        {
            ChangeColorRandomly();
            timer = 0f; // Reset the timer
        }
    }

    void ChangeColorRandomly()
    {
        if (spriteRenderer != null)
        {
            // Generate a random color using UnityEngine.Random
            Color newColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
            spriteRenderer.color = newColor;

            // Log the new color
            Debug.Log($"Color changed to: {newColor}");
        }
    }
}