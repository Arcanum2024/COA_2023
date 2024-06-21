using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cherrydev;
using TMPro;

public class PlayerBehavior : MonoBehaviour
{
    public BaseClass playerInfo;

    [SerializeField] private DialogBehaviour dialogBehaviour;
    [SerializeField] private DialogNodeGraph dialogGraph;
    
    [SerializeField] public GameObject ActiveQuestBox;
    [SerializeField] public GameObject Objective01;


   

    void Start()
    {
        if (dialogBehaviour != null)
        {
        dialogBehaviour.StartDialog(dialogGraph);
        dialogBehaviour.BindExternalFunction("StartObj", StartObj);
        }
        
        else
            Debug.LogWarning("DialogBehaviour is not assigned in Infos component.");
        }
    

    void StartObj()
    {
		StartCoroutine (GenObjective());
    }

    IEnumerator GenObjective()
    {
        
        Objective01.GetComponent<TMP_Text>().text = "Ask villagers if they have seen your father";
        yield return new WaitForSeconds(2.0f);
        ActiveQuestBox.SetActive(true);
        yield return new WaitForSeconds(1);
        Objective01.SetActive(true);
        yield return null;
    }
}
