using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 movement;
    Quaternion rotation = Quaternion.identity;

    Animator animator;
    Rigidbody rb;
    AudioSource audioSource;

    [SerializeField] float turnSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movement.Set(horizontal, 0f, vertical);
        movement.Normalize();

        bool isWalking = movement.sqrMagnitude > 0f;
        animator.SetBool("IsWalking", isWalking);
        if (isWalking && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.deltaTime, 0f);
        rotation = Quaternion.LookRotation(desiredForward);
    }

    private void OnAnimatorMove()
    {
        rb.MovePosition(rb.position + movement * animator.deltaPosition.magnitude);
        rb.MoveRotation(rotation);
    }
}
