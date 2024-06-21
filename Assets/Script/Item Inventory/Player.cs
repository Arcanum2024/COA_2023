using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private Slider HPslider;
    [SerializeField] private Slider XPslider;

    public static Player Instance;
    public int Health;
    public int Exp;
    private int maxHP;
    private int maxXP;

    public TMP_Text HealthText;
    public TMP_Text ExpText;
    public GameObject Dagger;
    public GameObject Sword;
    public Animator anim;


    // Reference to ClickToMove script
    private ClickToMove clickToMoveScript;
    [SerializeField] public GameObject AccomplishedPanel;
    [SerializeField] public GameObject AccomplishedNotification;
    [SerializeField] public GameObject AccomplishedDescription;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Get reference to ClickToMove script
        clickToMoveScript = GetComponent<ClickToMove>();
        if (HPslider != null)
        {
            maxHP = Health;
            HPslider.maxValue = maxHP;
            HPslider.value = Health;
        }
        if (XPslider != null)
        {
            maxXP = Exp;
            XPslider.maxValue = maxXP;
            XPslider.value = Exp;
        }
    }

    public void IncreaseHealth(int value)
    {
        Health += value;
        Health = Mathf.Min(Health, maxHP); // Ensure health doesn't go above maxHP
        HealthText.text = $"HP:{Health}";

        // Update HP slider
        if (HPslider != null)
        {
            HPslider.value = Health;
        }
    }

    public void IncreaseExp(int value)
    {
        Exp += value;
        ExpText.text = $"Exp:{Exp}";
    }

    public void UseDagger()
    {
        Dagger.SetActive(true);
        Sword.SetActive(false);
    }

    public void UseSword()
    {
        Sword.SetActive(true);
        Dagger.SetActive(false);
    }

    // Method to attack enemy
    public void AttackEnemy(EnemyBehavior enemy)
    {
        if (Dagger.activeSelf)
        {
            enemy.TakeDamage(5); // Dagger damage
        }
        else if (Sword.activeSelf)
        {
            enemy.TakeDamage(10); // Sword damage
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Health = Mathf.Max(Health, 0); // Ensure health doesn't go below zero
        HealthText.text = $"HP:{Health}";

        // Update HP slider
        if (HPslider != null)
        {
            HPslider.value = Health;
        }

        // Check if player's health is zero or less
        if (Health <= 0)
        {
            // Player dies
            Die();
        }
    }

    void Die()
    {
        anim.SetBool("isDeath", true); // Perform any actions when the player dies, e.g., game over screen
        StartCoroutine (Deads());

        // Disable the ClickToMove script
        if (clickToMoveScript != null)
        {
            clickToMoveScript.enabled = false;
        }
    }

    IEnumerator Deads()
    {
        AccomplishedNotification.GetComponent<TMP_Text>().text = "MISSION FAILED";
        AccomplishedDescription.GetComponent<TMP_Text>().text = "BACK TO CHECKPOINT";
        yield return new WaitForSeconds(1);
        AccomplishedPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        AccomplishedPanel.SetActive(false);
        yield return new WaitForSeconds(18);
        gameObject.SetActive(false);
    
    }
}
