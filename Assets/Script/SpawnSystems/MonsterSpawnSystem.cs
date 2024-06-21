using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
public class MonsterSpawnSystem : MonoBehaviour
{
    private bool HasSpawned;
    public Transform Player;

    public GameObject[] EnemiesToSpawn;
    public int MaxSpawnAmount;

    //rAnge
    public float SpawnRange;
    public float DetectionRange;

    
    // [SerializeField] public GameObject AccomplishedPanel;
    // [SerializeField] public GameObject AccomplishedNotification;
    // [SerializeField] public GameObject AccomplishedDescription;
    
    // [SerializeField] public GameObject MissionAccomplished;
    // [SerializeField] public GameObject ActiveQuestBox;
    // [SerializeField] public GameObject NotifBox;
    // [SerializeField] public GameObject NotifText;
    // [SerializeField] public GameObject Objective05;
    // [SerializeField] public GameObject Objective06;

    void Start()
    {
        
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.position);
        if (distance <= DetectionRange)
        {
            if (!HasSpawned)
            {
                if(!HasSpawned)
                {
                    SpawnEnemies();
                    
                    // EnemyAccomplishedOnce();
                }
                
            } 
            
        }
    }


    
    // void EnemyAccomplishedOnce()
    // {
    //     StartCoroutine(EnemyAccomplished());
    // }


    void SpawnEnemies()
    {
        HasSpawned =  true;
        int SpawnAmount = Random.Range(1,MaxSpawnAmount+1);

        for(int i = 0; i < SpawnAmount ; i++)
        {
            float xSpawnPos = transform.position.x + Random.Range(-SpawnRange, SpawnRange);
            float zSpawnPos = transform.position.z + Random.Range(-SpawnRange, SpawnRange);

            Vector3 SpawnPoint = new Vector3(xSpawnPos, 0, zSpawnPos);
            GameObject newEnemy = Instantiate(EnemiesToSpawn[Random.Range(0, EnemiesToSpawn.Length)]);
        
        }

    }

    
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Handles.color = Color.blue;
        Handles.DrawWireArc(transform.position, transform.up, transform.right, 360, DetectionRange);

        Handles.color = Color.red;
        Handles.DrawWireArc(transform.position, transform.up, transform.right, 360, SpawnRange);
    }
#endif

    //  IEnumerator EnemyAccomplished()
    // {
    //     AccomplishedNotification.GetComponent<TMP_Text>().text = "QUEST ACCOMPLISHED";
    //     AccomplishedDescription.GetComponent<TMP_Text>().text = "FIND THE UNKNOWN CREATURES";
    //     yield return new WaitForSeconds(1);
    //     AccomplishedPanel.SetActive(true);
    //     MissionAccomplished.SetActive(true);
    //     yield return new WaitForSeconds(2);
    //     AccomplishedPanel.SetActive(false);

    //     NotifText.GetComponent<TMP_Text>().text = "Remember that you are looking for your father. Kill the unknown creatures and look for Elderly Lori after";
    //     Objective05.GetComponent<TMP_Text>().text = "Kill unknown creatures";
    //     Objective06.GetComponent<TMP_Text>().text = "Find Elderly Lori";
    //     yield return new WaitForSeconds(2.0f);
    //     ActiveQuestBox.SetActive(true);
    //     NotifBox.SetActive(true);
    //     yield return new WaitForSeconds(1);
    //     Objective05.SetActive(true);
    //     NotifText.SetActive(true);
    //     yield return new WaitForSeconds(0.5f);
    //     Objective06.SetActive(true);
    //     yield return new WaitForSeconds(0.5f);
    //     NotifBox.SetActive(false);
    //     yield return null;
    // }


}
