﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController sharedInstance;

    //Variables del movimiento del personaje
    public float jumpForce = 6f;
    public float runningSpeed = 2f;

    Rigidbody2D rigidBody;
    Animator animator;
    Vector3 startPosition;
    
    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";
    private const string VERTICAL_FORCE = "verticalForce";

    public LayerMask groundMask;

    void Awake() {
        if (sharedInstance == null) 
            sharedInstance = this;

        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start() {
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, false);

        startPosition = this.transform.position;
    }

    public void StartGame() {
        this.transform.position = startPosition;
        this.rigidBody.velocity = Vector2.zero;
    }

    void Update() {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame) {
            if (Input.GetButtonDown("Jump")) {
                Jump();
            }
        }
       
        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());
        Debug.DrawRay(this.transform.position, Vector2.down*1f, Color.green);

        animator.SetFloat(VERTICAL_FORCE, rigidBody.velocity.y);

        //Update es recomendado para inputs
        if (Input.GetAxis("Horizontal") < 0) {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetAxis("Horizontal") > 0) {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    void FixedUpdate() {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame) 
        {
            if (rigidBody.velocity.x < runningSpeed) 
            {
                rigidBody.velocity = new Vector2(runningSpeed, rigidBody.velocity.y);
            } 
        } else {//Si no estamos dentro de la partida
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }


        //FixedUpdate es recomendado para Physics
        //Mover el personaje a la izquierda o derecha
        //rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal")*runningSpeed, rigidBody.velocity.y);
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
            return true;
        } else {
            return false;
        }
    }

    public void Die() {
        this.animator.SetBool(STATE_ALIVE, false);
        GameManager.sharedInstance.GameOver();
    }
}
