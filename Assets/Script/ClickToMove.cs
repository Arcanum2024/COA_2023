using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using cherrydev;

using UnityEngine.Video;


public class ClickToMove : MonoBehaviour
{
    public VideoPlayer openingCutscenePlayer;


    [Header("Stats")]
    public float attackDistance;
    public float attackRate;
    private float nextAttack;
    public float rotationSpeed;
    public float rotationThreshold;


    private NavMeshAgent navMeshAgent;
    private Animator anim;

    private Transform targetedEnemy;
    private bool EnemyClicked;
    private bool walking;

    //Interactables
    private Transform ClickedObject;
    private bool objectClicked;


    //DoubleClick
    private bool OneClick;
    private bool DoubleClick;
    private float TimerForDoubleClick;
    private float Delay = 0.25f;

    [SerializeField] private DialogNodeGraph dialogGraph;

    void Awake()
    {
        PlayOpeningCutscene();
    }

    // Start is called before the first frame update
    void Start()
    {   
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void PlayOpeningCutscene()
{
    Debug.Log("Attempting to play opening cutscene...");
    
    if (openingCutscenePlayer != null && openingCutscenePlayer.clip != null)
    {
        // Play the opening cutscene
        openingCutscenePlayer.Play();
    }
    else
    {
        Debug.LogError("Opening cutscene player or clip is not assigned!");
    }
}
    
    
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetButtonDown("Fire2"))
        {
            navMeshAgent.ResetPath();
            if(Physics.Raycast(ray, out hit, 100))
            {
                if(hit.collider.tag == "Enemy")
                {
                    EnemyClicked = true;
                    targetedEnemy = hit.transform;
                }
                //For Interactable Objects
                else if(hit.collider.tag == "Chest")
                {
                    objectClicked = true;
                    ClickedObject = hit.transform;
                }

                else if(hit.collider.tag == "Info")
                {
                    objectClicked = true;
                    ClickedObject = hit.transform;
                }

                else if(hit.collider.tag == "Friend")
                {
                    objectClicked = true;
                    ClickedObject = hit.transform;
                }

                else
                {
                    walking = true;
                    EnemyClicked = false;
                    navMeshAgent.isStopped = false;
                    navMeshAgent.destination = hit.point;
                }
            }
        }

        if(EnemyClicked)
        {
            MoveAndAttack();
        }
        else if(objectClicked && ClickedObject.gameObject.tag == "Info")
        {
            ReadInfos(ClickedObject);
        }

        else if(objectClicked && ClickedObject.gameObject.tag == "Chest")
        {
            Open(ClickedObject);
        }

        else if(objectClicked && ClickedObject.gameObject.tag == "Friend")
        {
            talkToFriend(ClickedObject);
        }

        else
        {
            if(!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                walking = false;
            }
            else if(!navMeshAgent.pathPending && navMeshAgent.remainingDistance >= navMeshAgent.stoppingDistance)

            {
                walking = true;
            }
        }
        //anim.SetBool("isWalking", walking);

        if(Input.GetKey(KeyCode.LeftShift))
        {
            navMeshAgent.speed = 8f;
            anim.SetBool("isRunning", walking);
            anim.SetBool("isWalking", false);
        }
        else
        {
            navMeshAgent.speed = 3.5f;
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", walking);
        }

        if (!walking)
        {
            anim.SetBool("isIdling",true);
        }
        else
        {
            anim.SetBool("isIdling", false);
        }
        
    }

    void MoveAndAttack()
    {
        if (targetedEnemy == null)
        {
            return;
        }

        navMeshAgent.destination = targetedEnemy.position;

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance > attackDistance)
        {
            navMeshAgent.isStopped = false;
            walking = true;
        }
        else if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= attackDistance)
        {
            Vector3 direction = (targetedEnemy.position - transform.position).normalized;
            direction.y = 0; // Ensure no tilting

            // Calculate the angle between the current facing direction and the direction towards the enemy
            float angle = Vector3.Angle(transform.forward, direction);

            // Only rotate if the angle exceeds a certain threshold
            if (angle > rotationThreshold)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }

            // Check if not already attacking
            if (!anim.GetBool("isAttacking"))
            {
                // Check if the attack animation is not already playing
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("SwordAttack"))
                {
                    anim.SetBool("isAttacking", true);

                    if (Time.time > nextAttack)
                    {
                        nextAttack = Time.time + attackRate;
                        // Perform attack actions here
                    }
                }
            }
            else
            {
                // If already attacking, reset the isAttacking flag after a short delay
                StartCoroutine(ResetIsAttacking());
            }

            navMeshAgent.isStopped = true;
            walking = false;
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
    }

    // Coroutine to reset isAttacking after a short delay
    IEnumerator ResetIsAttacking()
    {
        yield return new WaitForSeconds(0.1f); // Adjust the delay as needed
        anim.SetBool("isAttacking", false);
    }


    void ReadInfos(Transform target)
    {
        //set target 
        navMeshAgent.destination = target.position;

        //go close 
        if(!navMeshAgent.pathPending && navMeshAgent.remainingDistance > attackDistance)
        {
            navMeshAgent.isStopped = false;
            walking = true;
        }
        
        //then read info
        else if(!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= attackDistance)
        {
            navMeshAgent.isStopped = true;
            transform.LookAt(target);
            walking = false;

            //print
            print(target.GetComponent<Infos>().Info);

            objectClicked = false;
            navMeshAgent.ResetPath();
        }
        
    }

    void talkToFriend(Transform target)
    {
        //set target 
        navMeshAgent.destination = target.position;

        //go close 
        if(!navMeshAgent.pathPending && navMeshAgent.remainingDistance > attackDistance)
        {
            navMeshAgent.isStopped = false;
            walking = true;
        }
        
        //then read info
        else if(!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= attackDistance)
        {
            navMeshAgent.isStopped = true;
            transform.LookAt(target);
            walking = false;

            
            // Accessing the Infos component attached to the target
            Infos infos = target.GetComponent<Infos>();
            Quest2 villager = target.GetComponent<Quest2>();
            ElderlyLoriQuest lori = target.GetComponent<ElderlyLoriQuest>();

            if (infos != null)
            {
                // Call the StartDialog method of Infos
                infos.StartDialog();
            }

            if (villager != null)
            {
                // Call the StartDialog method of Infos
                villager.StartDialog();
            }

            if (lori != null)
            {
                // Call the StartDialog method of Infos
                lori.StartDialog();
            }


            objectClicked = false;
            navMeshAgent.ResetPath();
        }
        
    }


    //info with animation
    void Open(Transform target)
    {
        //set target 
        navMeshAgent.destination = target.position;

        //go close 
        if(!navMeshAgent.pathPending && navMeshAgent.remainingDistance > attackDistance)
        {
            navMeshAgent.isStopped = false;
            walking = true;
        }
        
        //then read info
        else if(!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= attackDistance)
        {
            navMeshAgent.isStopped = true;
            transform.LookAt(target);
            walking = false;


            // animation
            target.gameObject.GetComponent<Animator>().SetTrigger("Play");
            

            objectClicked = false;
            navMeshAgent.ResetPath();
        }
        
    }

    void CheckDoubleClick()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (!OneClick)
            {
                OneClick = true;
                TimerForDoubleClick = Time.time;
            }
            else
            {
                if ((Time.time - TimerForDoubleClick) < Delay)
                {
                    OneClick = false;
                    DoubleClick = true;
                }
            }
        }

        if (OneClick)
        {
            if ((Time.time - TimerForDoubleClick) > Delay)
            {
                OneClick = false;
                DoubleClick = false;
            }
        }
    }

}