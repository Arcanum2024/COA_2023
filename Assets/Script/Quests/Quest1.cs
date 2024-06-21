using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cherrydev;
using TMPro;

public class Infos : MonoBehaviour
{
    public string Info;
    [SerializeField] private DialogBehaviour dialogBehaviour;
    [SerializeField] private DialogNodeGraph dialogGraph;
    [SerializeField] private Transform playerTransform; // Reference to the player's transform
    [SerializeField] public GameObject ActiveQuestBox;
    [SerializeField] public GameObject NotifBox;
    [SerializeField] public GameObject NotifText;
    [SerializeField] public GameObject Objective02;
    [SerializeField] public GameObject Objective03;
    [SerializeField] public GameObject Objective04;	
    // [SerializeField] public GameObject MissionAccomplished;
    // Method to start the dialog
    public void StartDialog()
    {
        if (dialogBehaviour != null)
        {
            dialogBehaviour.StartDialog(dialogGraph);
            dialogBehaviour.BindExternalFunction("DoneTalk", Done);
            // dialogBehaviour.BindExternalFunction("MissionComplete", Accomplished);
        }
        else
        {
            Debug.LogWarning("DialogBehaviour is not assigned in Infos component.");
        }
    }

    // public void Accomplished()
    // {
	// 	MissionAccomplished.SetActive(true);
    // }
    public void Done()
    {
		StartCoroutine (ObjectiveStart());
    }

    IEnumerator ObjectiveStart()
    {
        NotifText.GetComponent<TMP_Text>().text = "You can find the dagger inside your house. Right click to obtain any item";
        Objective02.GetComponent<TMP_Text>().text = "Find Dagger";
        Objective03.GetComponent<TMP_Text>().text = "Go to the Village";
        Objective04.GetComponent<TMP_Text>().text = "Find the unknown creatures";
        yield return new WaitForSeconds(2.0f);
        ActiveQuestBox.SetActive(true);
        NotifBox.SetActive(true);
        yield return new WaitForSeconds(1);
        Objective02.SetActive(true);
        NotifText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Objective03.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Objective04.SetActive(true);
        yield return new WaitForSeconds(4);
        NotifBox.SetActive(false);
        yield return new WaitForSeconds(9);
        gameObject.SetActive(false);
        yield return null;
    }

}
