using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;

    private bool isWalking;
    private Vector3 lastInteractDir;
    
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = new Vector2(0, 0);
        
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
        }

        inputVector = inputVector.normalized;
        
        Vector3 moveDirecction = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDirecction != Vector3.zero)
        {
            lastInteractDir = moveDirecction;
        }
        

        float interactDistance = 2f;
        
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                // Has ClearCounter
                clearCounter.Interact();   
            }
        }
        else
        {
            Debug.Log("-");
        }
    }
    
    public bool IsWalking() {
        return isWalking;
    }

    private void HandleMovement()
    {
        Vector2 inputVector = new Vector2(0, 0);
        
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
        }

        inputVector = inputVector.normalized;

        Vector3 moveDirecction = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius ,moveDirecction, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDirecction.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius ,moveDirX, moveDistance);

            if (canMove)
            {
                moveDirecction = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDirecction.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius ,moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDirecction = moveDirZ;
                }
                else
                {
                    // annot move in any direcction
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDirecction * moveDistance;
        }
        

        isWalking = moveDirecction != Vector3.zero;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirecction, Time.deltaTime * rotateSpeed);

    }
}

