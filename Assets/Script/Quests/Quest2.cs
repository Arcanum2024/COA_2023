using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quest3 : MonoBehaviour
{
  [SerializeField] private Transform playerTransform; // Reference to the player's transform
  [SerializeField] public GameObject ActiveQuestBox;
  [SerializeField] public GameObject MissionCompleted;

  [SerializeField] public GameObject AccomplishedPanel;
  [SerializeField] public GameObject AccomplishedNotification;
  [SerializeField] public GameObject AccomplishedDescription;

  // Method to start the dialog



  public void OnMouseDown()
  {
    MissionCompleted.SetActive(true);
    MissionAccomplished();
    
  }
   void MissionAccomplished()
    {
        StartCoroutine(Accomplished());
    }

    IEnumerator Accomplished()
    {
        AccomplishedNotification.GetComponent<TMP_Text>().text = "QUEST ACCOMPLISHED";
        AccomplishedDescription.GetComponent<TMP_Text>().text = "FIND THE DAGGER";
        yield return new WaitForSeconds(1);
        AccomplishedPanel.SetActive(true);
        yield return new WaitForSeconds(3);
        AccomplishedPanel.SetActive(false);   
        yield return null;
    }

 

}
