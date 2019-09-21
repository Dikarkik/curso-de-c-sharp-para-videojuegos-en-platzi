using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;

    public GameState currentGameState = GameState.menu;

    private void Awake() {
        if (sharedInstance == null)
            sharedInstance = this;
    }

    void Update() {
        if (Input.GetButtonDown("Submit")) {
            StartGame();
        }
    }

    public void StartGame() {
        SetGameState(GameState.inGame);
    }

    public void GameOver() {
        SetGameState(GameState.gameOver);
    }

    public void BackToMenu() {
        SetGameState(GameState.menu);
    }

    private void SetGameState(GameState newGameState) {
        if (newGameState == GameState.menu) {
            //TODO: colocar la lógica del menú
        } else if (newGameState == GameState.inGame) {
            //TODO: hay que preparar la escena para jugar
        } else if (newGameState == GameState.gameOver) {
            //TODO: preparar el juego para el Game Over
        }

        this.currentGameState = newGameState;
    } 
}
