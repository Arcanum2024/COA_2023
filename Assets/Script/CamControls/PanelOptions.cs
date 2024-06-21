using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public GameObject aboutGamePanel;
    public GameObject gameControlsPanel;
    public GameObject soundPanel;
    public GameObject aboutDevelopersPanel;

    void Start()
    {
        // Set the default panel active
        aboutGamePanel.SetActive(true);
        gameControlsPanel.SetActive(false);
        soundPanel.SetActive(false);
        aboutDevelopersPanel.SetActive(false);
    }

    public void ShowAboutGamePanel()
    {
        aboutGamePanel.SetActive(true);
        gameControlsPanel.SetActive(false);
        soundPanel.SetActive(false);
        aboutDevelopersPanel.SetActive(false);
    }

    public void ShowAControlsPanel()
    {
        aboutGamePanel.SetActive(false);
        gameControlsPanel.SetActive(true);
        soundPanel.SetActive(false);
        aboutDevelopersPanel.SetActive(false);
    }

    public void ShowSoundPanel()
    {
        aboutGamePanel.SetActive(false);
        gameControlsPanel.SetActive(false);
        soundPanel.SetActive(true);
        aboutDevelopersPanel.SetActive(false);
    }

    public void ShowDevelopersPanel()
    {
        aboutGamePanel.SetActive(false);
        gameControlsPanel.SetActive(false);
        soundPanel.SetActive(false);
        aboutDevelopersPanel.SetActive(true);
    }

    
}

