using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    private Player player;
    
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        //animator.SetBool(IS_WALKING, );
    }
}
