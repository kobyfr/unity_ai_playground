// 9/14/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class SpriteSheetToAnimation : MonoBehaviour
{
    [MenuItem("Tools/animation/Create Animation From Sprite Sheet")]
    public static void CreateAnimationFromSpriteSheet()
    {
        // Prompt the user to select a sprite sheet
        string spriteSheetPath = EditorUtility.OpenFilePanel("Select Sprite Sheet", "Assets", "png");
        if (string.IsNullOrEmpty(spriteSheetPath))
        {
            Debug.LogWarning("No sprite sheet selected.");
            return;
        }

        Debug.Log("BBBB");
        // Convert the path to a relative Unity path
        string relativePath = "Assets" + spriteSheetPath.Substring(Application.dataPath.Length);

        // Load the sprite sheet
        Texture2D spriteSheet = AssetDatabase.LoadAssetAtPath<Texture2D>(relativePath);
        if (spriteSheet == null)
        {
            Debug.LogError("Failed to load sprite sheet at path: " + relativePath);
            return;
        }
        Debug.Log("AAAA");

        // Get the sprites from the sprite sheet
        string spriteSheetName = Path.GetFileNameWithoutExtension(relativePath);
        Debug.Log("spriteSheetName = " + spriteSheetName);
        Object[] sprites = AssetDatabase.LoadAllAssetsAtPath(relativePath);

        // Turn the sprites into an array
        List<Sprite> spriteList = new List<Sprite>();
        foreach (Object obj in sprites)
        {
            if (obj is Sprite sprite)
            {
                spriteList.Add(sprite);
            }
        }
        Sprite[] spriteArray = spriteList.ToArray();

        if (spriteArray.Length == 0)
        {
            Debug.LogError("No sprites found in the sprite sheet. Make sure it is sliced in the Sprite Editor.");
            return;
        }

        // Create a new animation clip
        AnimationClip animationClip = new AnimationClip();
        animationClip.frameRate = 12; // Set the frame rate (adjust as needed)

        // Add keyframes for each sprite
        EditorCurveBinding spriteBinding = new EditorCurveBinding
        {
            type = typeof(SpriteRenderer),
            path = "",
            propertyName = "m_Sprite"
        };

        ObjectReferenceKeyframe[] keyframes = new ObjectReferenceKeyframe[spriteArray.Length];
        for (int i = 0; i < spriteArray.Length; i++)
        {
            keyframes[i] = new ObjectReferenceKeyframe
            {
                time = i / animationClip.frameRate,
                value = spriteArray[i]
            };
        }

        AnimationUtility.SetObjectReferenceCurve(animationClip, spriteBinding, keyframes);

        // Save the animation clip
        string animationPath = Path.GetDirectoryName(relativePath) + "/" + spriteSheetName + "_Animation.anim";
        animationPath = animationPath.Replace("\\\\", "/");
        AssetDatabase.CreateAsset(animationClip, animationPath);
        AssetDatabase.SaveAssets();

        Debug.Log("Animation created at: " + animationPath);
    }
}