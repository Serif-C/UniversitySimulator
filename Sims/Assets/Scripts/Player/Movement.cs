using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    /*
     * WASD movement for now
     */

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator animator;
    private float xInput;
    private float yInput;

    private void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal") * moveSpeed;
        yInput = Input.GetAxisRaw("Vertical") * moveSpeed;

        animator.SetFloat("Movement_X", xInput);
        animator.SetFloat("Movement_Y", yInput);
        
        if (xInput != 0 || yInput != 0)
            animator.SetBool("Moving", true);
        else
            animator.SetBool("Moving", false);

        Vector3 movement = new Vector3(xInput, yInput, 0);

        transform.position += (movement * Time.deltaTime);
    }
}
