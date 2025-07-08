using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState
{
    freeRoam,
    clothChanging,
    bookArranging
}
public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject playerController;

    GameState state;

    public static GameController Instance { get; private set; }

    public GameObject playerDefault;
    public GameObject playerChanged;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // Persist between scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }
    public void ChangeClothes()
    {
        // Disable the original player
        playerDefault.SetActive(false);

        // Enable the changed version
        playerChanged.SetActive(true);
    }

    private void Update()
    {
        if (state == GameState.freeRoam)
        {
            // Normal gameplay logic
        }
        else if (state == GameState.clothChanging)
        {
            // e.g. disable movement
        }
        else if (state == GameState.bookArranging)
        {
            // e.g. enable book interaction
        }
    }
}
