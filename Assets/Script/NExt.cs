using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterListController : MonoBehaviour
{
    public GameObject[] characters; // Array of character panels
    private int currentIndex = 0;

    void Start()
    {
        ShowCharacter(currentIndex);
    }

    public void NextCharacter()
    {
        currentIndex = (currentIndex + 1) % characters.Length;
        ShowCharacter(currentIndex);
    }

    private void ShowCharacter(int index)
    {
        // Disable all character panels
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }

        // Enable the character panel at the specified index
        characters[index].SetActive(true);
    }
}
