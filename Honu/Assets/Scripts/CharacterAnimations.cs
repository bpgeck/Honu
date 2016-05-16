using UnityEngine;
using System.Collections;

public class CharacterAnimations : MonoBehaviour
{
    Animator animator;

    void Start ()
    {
        animator = this.GetComponent<Animator>();
    }

    void Update ()
    {
        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.W))
            {
                animator.SetInteger("MoveDirection", 1);
                this.GetComponent<CharacterInteractionAndInventory>().directionFacing = 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                animator.SetInteger("MoveDirection", 2);
                this.GetComponent<CharacterInteractionAndInventory>().directionFacing = 2;
            }
            if (Input.GetKey(KeyCode.S))
            {
                animator.SetInteger("MoveDirection", 3);
                this.GetComponent<CharacterInteractionAndInventory>().directionFacing = 3;
            }
            if (Input.GetKey(KeyCode.D))
            {
                animator.SetInteger("MoveDirection", 4);
                this.GetComponent<CharacterInteractionAndInventory>().directionFacing = 4;
            }
        }
        else
        {
            animator.SetInteger("MoveDirection", 0);
        }
    }
}
