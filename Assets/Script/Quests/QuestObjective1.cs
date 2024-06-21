using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestObjective1 : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; 
    public GameObject MissionAccomplishedIcon;
    [SerializeField] public GameObject AccomplishedPanel;
    [SerializeField] public GameObject AccomplishedNotification;
    [SerializeField] public GameObject AccomplishedDescription;

    // Check if the collider entering the trigger zone is the player
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MissionAccomplished();
        }
    }

    void MissionAccomplished()
    {
        StartCoroutine(Accomplished());
    }

    IEnumerator Accomplished()
    {
        AccomplishedNotification.GetComponent<TMP_Text>().text = "QUEST ACCOMPLISHED";
        AccomplishedDescription.GetComponent<TMP_Text>().text = "GO TO THE VILLAGE";
        yield return new WaitForSeconds(1);
        AccomplishedPanel.SetActive(true);
        MissionAccomplishedIcon.SetActive(true);
        yield return new WaitForSeconds(1);
        AccomplishedPanel.SetActive(false);   
        yield return null;
        gameObject.SetActive(false);
    }
}
