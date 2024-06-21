using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using cherrydev;

public class NPcController : MonoBehaviour
{
    public Transform player;
    public float interactionDistance = 5f;
    public Animator animator;
    public DialogBehaviour dialogBehaviour;
    public DialogNodeGraph dialogGraph;

    private bool isTalking = false;
    private bool isDoneTalking = false;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        
        // Check if the NPC is in interaction distance and not already talking or done talking
        if (distance <= interactionDistance && !isTalking && !isDoneTalking)
        {
            Debug.Log("NPC is in interaction distance.");
            
            // If the player clicks while the mouse is over the NPC and the NPC is not already talking or done talking
            if (Input.GetMouseButtonDown(0))
            {
                StartTalking();
            }
        }
    }

    void StartTalking()
    {
        Debug.Log("NPC starts talking.");
        
        isTalking = true;
        
        // Start the dialogue
        if (dialogBehaviour != null && dialogGraph != null)
        {
            dialogBehaviour.StartDialog(dialogGraph);
        }
        else
        {
            Debug.LogError("DialogBehaviour or DialogNodeGraph is not assigned.");
        }
        
        // You can add logic here to trigger animations
        // For now, let's simulate talking for 3 seconds
        Invoke("FinishTalking", 3f);
    }

    void FinishTalking()
    {
        Debug.Log("NPC finishes talking.");
        
        isTalking = false;
        isDoneTalking = true;
        
        // Set the "isDoneTalking" parameter in the animator to trigger appropriate animation, if any
        if (animator != null)
        {
            animator.SetBool("isDoneTalking", true);
        }
        else
        {
            Debug.LogWarning("Animator is not assigned.");
        }

        // Set the NPC inactive after talking is finished
        gameObject.SetActive(false);
    }
}
