using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class MainMenuController : MonoBehaviour
{
    public TMP_Dropdown playerSelectionDropdown;
    public Button StartBtn;
    public Button ChooseBtn;
    public Button QuitBtn;

    public GameObject quitPromptPanel;

    public static int NumberOfPlayers;

    public void ChoosePlayers()
    {
        ChooseBtn.gameObject.SetActive(false);
        StartBtn.gameObject.SetActive(true);
        playerSelectionDropdown.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        NumberOfPlayers = playerSelectionDropdown.value + 2;  // DropDown value starts at 0 so we add 2 for number of players

        // Initialize players
        GameController.Instance.players = new List<Player>();

        for (int i = 0; i < NumberOfPlayers; i++)
        {
            GameController.Instance.players.Add(new Player(i + 1));
        }

        SceneManager.LoadScene("Game"); 
    }

    public void QuitGamePrompt()
    {
        quitPromptPanel.SetActive(true);
        QuitBtn.gameObject.SetActive(false);
    }

    public void QuitGameConfirm()
    {
        Application.Quit();
    }

    public void QuitGameCancel()
    {
        quitPromptPanel.SetActive(false);
        QuitBtn.gameObject.SetActive(true);
    }
}