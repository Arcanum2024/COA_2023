using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cherrydev;

public class QuestAsk : MonoBehaviour
{
    public string Info;
    [SerializeField] private DialogBehaviour dialogBehaviour;
    [SerializeField] private DialogNodeGraph dialogGraph;
    [SerializeField] private Transform playerTransform; // Reference to the player's transform
    [SerializeField] public GameObject ActiveQuestBox;
    [SerializeField] public GameObject MissionAccomplished;

    // Method to start the dialog
    public void StartDialog()
    {
        if (dialogBehaviour != null)
        {
            dialogBehaviour.StartDialog(dialogGraph);
            dialogBehaviour.BindExternalFunction("MissionComplete", Accomplished);
        }
        else
        {
            Debug.LogWarning("DialogBehaviour is not assigned in Infos component.");
        }
    }

    public void Accomplished()
    {
		MissionAccomplished.SetActive(true);
    }

}
