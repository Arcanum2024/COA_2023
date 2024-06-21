using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cherrydev;
using TMPro;

public class Quest2 : MonoBehaviour
{
    [SerializeField] private DialogBehaviour dialogBehaviour;
    [SerializeField] private DialogNodeGraph dialogGraph;
    [SerializeField] private Transform playerTransform; // Reference to the player's transform
    [SerializeField] public GameObject ActiveQuestBox;
    [SerializeField] public GameObject MissionAccomplished;
    
    [SerializeField] public GameObject AccomplishedPanel;
    [SerializeField] public GameObject AccomplishedNotification;
    [SerializeField] public GameObject AccomplishedDescription;


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
		StartCoroutine(AccomplishedTask());
    }

    IEnumerator AccomplishedTask()
    {
        AccomplishedNotification.GetComponent<TMP_Text>().text = "QUEST ACCOMPLISHED";
        AccomplishedDescription.GetComponent<TMP_Text>().text = "ASK THE VILLAGERS";
        yield return new WaitForSeconds(1);
        AccomplishedPanel.SetActive(true);
        MissionAccomplished.SetActive(true);
        yield return new WaitForSeconds(2);
        AccomplishedPanel.SetActive(false);
        yield return new WaitForSeconds(18);
        gameObject.SetActive(false);
    }

}
