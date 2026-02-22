using System.Collections.Generic;
using combat_system;
using model;
using model.entity;
using UnityEngine;

public class MainTurnBasedManager :  MonoBehaviour
{
    //TODO: finish it. 
    //1. Mana system , or end turn button , perhaps both 
    //2. That means on both i need UI regardless
    public static MainTurnBasedManager instance;
    private Entity currentActor;
    //Needed for enemy generation later
    public List<Entity> enemies;
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
        
    }
}