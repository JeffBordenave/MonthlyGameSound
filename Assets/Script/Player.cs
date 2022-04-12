using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameManager gameManager = default;

    public float life = 3;

    void Update()
    {
        Inputs();
    }

    private void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            gameManager.playerPlayedNote = Note.Do;
            gameManager.playerPlayed = true;
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            gameManager.playerPlayedNote = Note.Ré;
            gameManager.playerPlayed = true;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            gameManager.playerPlayedNote = Note.Mi;
            gameManager.playerPlayed = true;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            gameManager.playerPlayedNote = Note.Fa;
            gameManager.playerPlayed = true;
        }
    }
}
