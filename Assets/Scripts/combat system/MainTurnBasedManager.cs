using combat_system;
using model;
using model.entity;
using UnityEngine;

public class MainTurnBasedManager :  MonoBehaviour
{
    public static MainTurnBasedManager instance;
    private Entity currentActor;
    public int turnCounter;
    public void Awake()
    {
        turnCounter = 1;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        StartGameLoop();
    }
    //So the first turn will be the players and then the enemies.
    private void StartGameLoop()
    {
        if (turnCounter % 2 == 1)
        {
            PlayerTurn();
        }
        else
        {
            EnemyTurn();
        }
        turnCounter++;
    }

    private void EnemyTurn()
    {
        Debug.Log("Enemy turn");  
    }

    private void PlayerTurn()
    {
        PlayerController.instance.RedrawCards();
        //now we have to listen to player actions i guess. Each card either will just have a function or perhaps an event.
        // Attack, ArmorUp, and skill cards are separate events i would guess;
        //or maybe each action inside a card is a separate fundamental event;
        
    }
}