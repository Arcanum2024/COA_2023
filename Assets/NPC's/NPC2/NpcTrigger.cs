using UnityEngine;

public class NpcTrigger : MonoBehaviour
{
    // Define NPC class
    public class NPC
    {
        public bool is_done_talking = true; // Set as default

        public void Talk()
        {
            Debug.Log("NPC: Hello there!");
            is_done_talking = true;
        }
    }

    private NPC npc;
    private Animator npcAnimator;

    void Start()
    {
        // Instantiate the NPC
        npc = new NPC();

        // Get the Animator component attached to the NPC GameObject
        npcAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Trigger the "isDoneTalking" animation
            npcAnimator.SetTrigger("isDoneTalking");

            // Trigger the NPC's talk function
            npc.Talk();
        }
    }
}