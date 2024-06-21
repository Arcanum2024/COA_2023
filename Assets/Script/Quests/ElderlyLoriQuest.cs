using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cherrydev;
using TMPro;


public class ElderlyLoriQuest : MonoBehaviour
{
    public string Info;
    [SerializeField] private DialogBehaviour dialogBehaviour;
    [SerializeField] private DialogNodeGraph dialogGraph;
    [SerializeField] private Transform playerTransform; // Reference to the player's transform
    [SerializeField] public GameObject ActiveQuestBox;
    [SerializeField] public GameObject NotifBox;
    [SerializeField] public GameObject NotifText;
    [SerializeField] public GameObject Objective07;
    [SerializeField] public GameObject Objective08;
    [SerializeField] public GameObject Objective09;	
    [SerializeField] public GameObject MissionAccomplished;
    // Method to start the dialog
    public void StartDialog()
    {
        if (dialogBehaviour != null)
        {
            dialogBehaviour.StartDialog(dialogGraph);
            dialogBehaviour.BindExternalFunction("LoriDone", DoneYey);
        }
        else
        {
            Debug.LogWarning("DialogBehaviour is not assigned in Infos component.");
        }
    }

    public void DoneYey()
    {
		StartCoroutine (ObjectiveStart());
    }

    IEnumerator ObjectiveStart()
    {
        NotifText.GetComponent<TMP_Text>().text = "The Forbidden Forest, accessible to few, shelters a select variety of creatures that have thrived since the shattering of the Orb. Protect yourself";
        Objective07.GetComponent<TMP_Text>().text = "Find Silver Sword 'Silver Squire' of your father.";
        Objective08.GetComponent<TMP_Text>().text = "Follow the trail through the Forbidden Forest to reach Hiddenbrook.";
        Objective09.GetComponent<TMP_Text>().text = "Find Shadovale";
        yield return new WaitForSeconds(2.0f);
        ActiveQuestBox.SetActive(true);
        MissionAccomplished.SetActive(true);
        NotifBox.SetActive(true);
        yield return new WaitForSeconds(1);
        Objective07.SetActive(true);
        NotifText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Objective08.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Objective09.SetActive(true);
        yield return new WaitForSeconds(4);
        NotifBox.SetActive(false);
        yield return new WaitForSeconds(9);
        gameObject.SetActive(false);
        yield return null;
    }

}
