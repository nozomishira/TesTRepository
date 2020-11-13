using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiEnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float Speed = 1;

    static int idSpeed = Animator.StringToHash("Speed");

    Transform cameraTransform;
    Animator animator;
    NavMeshAgent agent;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        var move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
       
        var cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;


        if (move != Vector2.zero)
        {
            Vector3 direction = cameraForward * move.y + cameraTransform.right * move.x;
            transform.localRotation = Quaternion.LookRotation(direction);
            agent.Move(direction * (Time.deltaTime * Speed));
        }
        animator.SetFloat(idSpeed, move.magnitude);
    }

}
