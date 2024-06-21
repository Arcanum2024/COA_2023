using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitRoaming : MonoBehaviour
{
    public float speed = 2f;
    public float changeDirectionInterval = 3f;
    public float detectionRadius = 5f;
    public Transform playerTransform;

    private Animator animator;
    private List<Vector3> targetPositions = new List<Vector3>();
    private int currentTargetIndex = 0;
    private bool isPlayerDetected = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        SetNewTargetPositions();
        StartCoroutine(ChangeDirectionRoutine());
    }

    void Update()
    {
        DetectPlayer();

        if (!isPlayerDetected || targetPositions.Count == 0)
        {
            animator.SetBool("isWalking", false);
            return;
        }

        Vector3 moveDirection = (targetPositions[currentTargetIndex] - transform.position).normalized;
        transform.position += moveDirection * speed * Time.deltaTime;

        if (moveDirection != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        }

        if (Vector3.Distance(transform.position, targetPositions[currentTargetIndex]) < 0.5f)
        {
            currentTargetIndex = (currentTargetIndex + 1) % targetPositions.Count;
        }

        animator.SetBool("isWalking", true);
    }

    IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeDirectionInterval);
            if (isPlayerDetected)
            {
                SetNewTargetPositions();
            }
        }
    }

    void SetNewTargetPositions()
    {
        targetPositions.Clear();
        for (int i = 0; i < 5; i++) // Change 5 to the number of desired target positions
        {
            targetPositions.Add(GetRandomPosition());
        }
        Shuffle(targetPositions);
    }

    Vector3 GetRandomPosition()
    {
        float x = Random.Range(-10f, 10f);
        float z = Random.Range(-10f, 10f);
        return new Vector3(x, transform.position.y, z);
    }

    void Shuffle(List<Vector3> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Vector3 temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        isPlayerDetected = false;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.transform == playerTransform)
            {
                isPlayerDetected = true;
                break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
