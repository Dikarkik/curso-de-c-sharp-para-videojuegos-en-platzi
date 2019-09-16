﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables del movimiento del personaje
    public float jumpForce = 6f;
    Rigidbody2D rigidBody;
    Animator animator;
    public LayerMask groundMask;
    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";
    private const string VERTICAL_FORCE = "verticalForce";

    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start() {
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, false);
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            Jump();
        }
        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());
        Debug.DrawRay(this.transform.position, Vector2.down*1f, Color.green);

        animator.SetFloat(VERTICAL_FORCE, rigidBody.velocity.y);
    }

    void Jump() {
        if (IsTouchingTheGround()) {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
    }

    //Nos indica si el personaje está o no tocando el suelo
    bool IsTouchingTheGround() {
        if (Physics2D.Raycast(this.transform.position, 
                              Vector2.down,
                              1f, //0.2 = cm
                              groundMask)) {
            //TODO: programar lógica de contacto con el suelo
            return true;
        } else {
            //TODO: programar lógica de no contacto
            return false;
        }
    }
}
