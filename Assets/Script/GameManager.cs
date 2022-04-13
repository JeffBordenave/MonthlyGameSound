using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Note
{
    Do,
    Ré,
    Mi,
    Fa,
    None
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player = default;
    [SerializeField] private AudioSource source = default;

    [SerializeField] private AudioClip Do = default;
    [SerializeField] private AudioClip Ré = default;
    [SerializeField] private AudioClip Mi = default;
    [SerializeField] private AudioClip Fa = default;

    [SerializeField] private Text uiText = default;
    [SerializeField] private Image redScreen = default;

    public bool playerPlayed = false;
    public Note playerPlayedNote = Note.Do;

    private bool noteBeenPlayed = false;

    private Note playedNote = Note.Do;

    private float timeUntilNextNote = 3f;
    private float availableTime = 2.5f;
    private float counterNextNote = 0f;
    private float counterAvTime = 0f;
    private float currentScore = 0f;

    private float redColorA = 0f;
    private float redBlinkSpeed = 1.3f;
    private bool playAnim = false;

    void Update()
    {
        RoundPlayer();
        if (playAnim) AnimRedScreen();

        if (player.life == 0 && !playAnim)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void RoundPlayer()
    {
        counterNextNote += Time.deltaTime;

        if (counterNextNote >= timeUntilNextNote)
        {
            if (!noteBeenPlayed)
            {
                PlaySound(PickARandomNote());
                noteBeenPlayed = true;
            }
            
            counterAvTime += Time.deltaTime;

            if (playerPlayed)
            {
                if (playerPlayedNote == playedNote) PlayerWins();
                else PlayerLoses();
            }

            if (counterAvTime >= availableTime) PlayerLoses();
        }
    }

    private void PlaySound(Note note)
    {
        AudioClip clip = Do;

        if (note == Note.Do) clip = Do;
        else if (note == Note.Ré) clip = Ré;
        else if (note == Note.Mi) clip = Mi;
        else if (note == Note.Fa) clip = Fa;

        source.PlayOneShot(clip);
    }

    private Note PickARandomNote()
    {
        playedNote = Note.Do;

        int randomNum = Random.Range(0, 4);

        if(randomNum == 0) playedNote = Note.Do;
        else if(randomNum == 1) playedNote = Note.Ré;
        else if(randomNum == 2) playedNote = Note.Mi;
        else if(randomNum == 3) playedNote = Note.Fa;

        return playedNote;
    }

    private void PlayerWins()
    {
        Debug.Log("Player wins");

        currentScore++;
        uiText.text = currentScore.ToString();

        ResetParameters();
    }

    private void PlayerLoses()
    {
        playAnim = true;

        player.life -= 1;

        ResetParameters();
    }

    private void ResetParameters()
    {
        counterNextNote = 0f;
        counterAvTime = 0f;

        playerPlayed = false;
        noteBeenPlayed = false;
        playerPlayedNote = Note.None;
    }

    private void AnimRedScreen()
    {
        redColorA += redBlinkSpeed * Time.deltaTime;

        if (redColorA > 0.6) redBlinkSpeed *= -1;
        else if(redColorA < 0)
        {
            playAnim = false;
            redColorA = 0;
            redBlinkSpeed = 1.3f;
            redScreen.color = Color.clear;
        }

        redScreen.color = new Color(1, 1, 1, redColorA);
    }
}