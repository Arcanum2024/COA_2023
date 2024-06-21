using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public VideoPlayer openingCutscenePlayer;

    void Start()
    {
        // Call the method to play the opening cutscene when the game starts
        PlayOpeningCutscene();
    }

    void PlayOpeningCutscene()
    {
        // Ensure the VideoPlayer component is assigned
        if (openingCutscenePlayer != null)
        {
            // Play the opening cutscene
            openingCutscenePlayer.Play();
        }
        else
        {
            Debug.LogError("Opening cutscene player is not assigned!");
        }
    }
}

