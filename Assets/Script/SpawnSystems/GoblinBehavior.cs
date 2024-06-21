using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class GoblinBehavior : MonoBehaviour
{
    [SerializeField] private Slider slider;
    // NavMeshAgent
    private NavMeshAgent navMeshAgent;
    private Animator anim;
    private bool walking;

    // RadiusRange
    public float LookRadius;
    public float AttackRadius;

    // Attack
    private float NextAttack;
    public float AttackRate = 1f;

    // HP (Health Points)
    public int HP = 50;
    private int maxHP;

    // public GameObject MissionAccomplishedIcon;
    // [SerializeField] public GameObject AccomplishedPanel;
    // [SerializeField] public GameObject AccomplishedNotification;
    // [SerializeField] public GameObject AccomplishedDescription;

    public TMP_Text EnemyCounter;

    // Target
    public Transform TargetPlayer;

    // Static variable to track the number of spawned enemies
    public static int EnemyCount = 0;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (!navMeshAgent.isOnNavMesh)
        {
            Debug.LogError("NavMeshAgent is not placed on a NavMesh at start.");
        }

        // Initialize the slider
        if (slider != null)
        {
            maxHP = HP;
            slider.maxValue = maxHP;
            slider.value = HP;
        }

        // Increment the enemy count when an enemy is spawned
        EnemyCount++;
        Debug.Log("Enemy spawned. Total enemies: " + EnemyCount);
        EnemyCounter.text = $"Enemy to kill: {EnemyCount}";
    }


    // Update is called once per frame
    void Update()
    {
        anim.SetBool("walking", walking);

        if (!walking)
        {
            anim.SetBool("idling", true);
        }
        else
        {
            anim.SetBool("idling", false);
        }

        float distance = Vector3.Distance(transform.position, TargetPlayer.position);

        if (distance <= LookRadius)
        {
            // move to the player
            MoveAndAttack();
            EnemyCounter.gameObject.SetActive(true);
        }
        else
        {
            walking = false;
        }
    }

    void MoveAndAttack()
    {
        if (navMeshAgent == null || !navMeshAgent.isOnNavMesh)
        {
            Debug.LogWarning("NavMeshAgent is not properly initialized or not on a valid NavMesh.");
            return;
        }

        navMeshAgent.destination = TargetPlayer.position;

        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance > AttackRadius)
            {
                // move closer to player
                navMeshAgent.isStopped = false;
                walking = true;
            }
            else if (navMeshAgent.remainingDistance <= AttackRadius)
            {
                // within attack range
                // perform attack on player
                AttackPlayer();
            }
        }
    }
    void AttackPlayer()
    {
        // play attack animation
        anim.SetBool("attacking", true);

        // check if it's time for the next attack
        if (Time.time > NextAttack)
        {
            // decrease enemy's HP
            HP -= 1;
            UpdateHealthSlider();
            NextAttack = Time.time + AttackRate;

            // check if enemy's HP reaches zero
            if (HP <= 0)
            {
                // enemy dies
                Die();
            }
        }

        // stop movement
        navMeshAgent.isStopped = true;
        walking = false;
    }

    void Die()
    {
        // Play death animation
        anim.SetBool("isDeath", true);

        // Disable the NavMeshAgent to stop any further movement
        navMeshAgent.enabled = false;

        // Decrement the enemy count when an enemy dies
        EnemyCount--;
        EnemyCounter.text = $"Enemy to kill: {EnemyCount}";

        // Check if there are no more enemies
        if (EnemyCount <= 0)
        {
            Debug.Log("All enemies defeated!");
            // StartCoroutine(Accomplished());
        }

        // Start a coroutine to delete the enemy after 5 seconds
        StartCoroutine(DeleteAfterDelay(5f));
    }

    IEnumerator DeleteAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Destroy the game object after the delay
        Destroy(gameObject);
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        HP -= damage;
        UpdateHealthSlider();
        if (HP <= 0)
        {
            Die();
        }
    }

    void UpdateHealthSlider()
    {
        if (slider != null)
        {
            slider.value = HP;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered with object: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected");
            var healthComponent = other.GetComponent<Player>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(1);
                Debug.Log("Player health decreased by 1");
            }
        }
    }

    // IEnumerator Accomplished()
    // {
    //     AccomplishedNotification.GetComponent<TMP_Text>().text = "QUEST ACCOMPLISHED";
    //     AccomplishedDescription.GetComponent<TMP_Text>().text = "KILL THE UNKNOWN CREATURES";
    //     yield return new WaitForSeconds(1);
    //     AccomplishedPanel.SetActive(true);
    //     EnemyCounter.gameObject.SetActive(false);
    //     MissionAccomplishedIcon.SetActive(true);
    //     yield return new WaitForSeconds(3);
    //     AccomplishedPanel.SetActive(false);
    //     yield return null;
    // }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireArc(transform.position + new Vector3(0, 0.2f, 0), transform.up, transform.right, 360, LookRadius);

        Handles.color = Color.red;
        Handles.DrawWireArc(transform.position + new Vector3(0, 0.2f, 0), transform.up, transform.right, 360, AttackRadius);
    }
#endif
}
