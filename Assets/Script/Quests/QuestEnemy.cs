using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestEnemy : MonoBehaviour
{
    [SerializeField] public GameObject AccomplishedPanel;
    [SerializeField] public GameObject AccomplishedNotification;
    [SerializeField] public GameObject AccomplishedDescription;
    
    [SerializeField] public GameObject MissionAccomplished;
    [SerializeField] public GameObject ActiveQuestBox;
    [SerializeField] public GameObject NotifBox;
    [SerializeField] public GameObject NotifText;
    [SerializeField] public GameObject Objective05;
    [SerializeField] public GameObject Objective06;


    public void EnemyFound()
    {
		StartCoroutine(EnemyAccomplished());
    }

    IEnumerator EnemyAccomplished()
    {
        AccomplishedNotification.GetComponent<TMP_Text>().text = "QUEST ACCOMPLISHED";
        AccomplishedDescription.GetComponent<TMP_Text>().text = "FIND THE UNKNOWN CREATURES";
        yield return new WaitForSeconds(1);
        AccomplishedPanel.SetActive(true);
        MissionAccomplished.SetActive(true);
        yield return new WaitForSeconds(2);
        AccomplishedPanel.SetActive(false);

        NotifText.GetComponent<TMP_Text>().text = "Remember that you are looking for your father. Kill the unknown creatures and look for Elderly Lori after";
        Objective05.GetComponent<TMP_Text>().text = "Kill unknown creatures";
        Objective06.GetComponent<TMP_Text>().text = "Find Elderly Lori";
        yield return new WaitForSeconds(2.0f);
        ActiveQuestBox.SetActive(true);
        NotifBox.SetActive(true);
        yield return new WaitForSeconds(1);
        Objective05.SetActive(true);
        NotifText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Objective06.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        NotifBox.SetActive(false);
        yield return null;
    }

}
