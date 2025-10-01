using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private string firstSceneName = "MainMenu";

    private IEnumerator Start()
    {
        // Initialize localization
        var asyncOperationHandle = LocalizationSettings.InitializationOperation;
        yield return asyncOperationHandle;

        if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log(("Localization initialized"));
        }
        else
        {
            Debug.LogError("Failed to initialize localization");
        }

        // Load the first scene
        SceneManager.LoadScene(firstSceneName);

        // Initialize other systems (audio, input, etc.)
        InitializeSystems();
    }

    private void InitializeSystems()
    {
        // Initialize audio
        // ToDo: Initialize Save System

        Debug.Log("Game systems initialized.");
    }
    
    // ToDo: Use DontDestroyOnLoad for objects that should persist across scenes (e.g., audio manager, game manager)
    
    // ToDo: Check for required systems and log errors if something fails to initialize.
}