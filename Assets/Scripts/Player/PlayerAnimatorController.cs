using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Script for managing the animations of the player.
/// </summary>
[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour
{
    private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();        
    }

    public void Die()
    {
        _anim.SetTrigger("death");
    }

    public void Jump()
    {
        _anim.SetTrigger("jump");
    }
}
