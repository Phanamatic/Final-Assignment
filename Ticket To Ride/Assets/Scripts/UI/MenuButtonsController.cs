using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MenuButtonsController : MonoBehaviour
{
    public Button QuitBtn;
    public Button MenuBtn;
    public Button RulesBtn;
    public GameObject RulesPanel;
    public GameObject quitPromptPanel;
    public GameObject menuPromptPanel;
    public GameObject PlayerNumberPanel;
    public GameObject PlayerAreaPanel;
    public GameObject DecksAreaPanel;

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGamePrompt()
    {
        quitPromptPanel.SetActive(true);
        QuitBtn.gameObject.SetActive(false);
        MenuBtn.gameObject.SetActive(false);
        RulesBtn.gameObject.SetActive(false);
        PlayerNumberPanel.gameObject.SetActive(false);
    }

    public void QuitGameConfirm()
    {
        Application.Quit();
    }

    public void QuitGameCancel()
    {
        quitPromptPanel.SetActive(false);
        menuPromptPanel.SetActive(false);
        QuitBtn.gameObject.SetActive(true);
        MenuBtn.gameObject.SetActive(true);
        RulesBtn.gameObject.SetActive(true); 
        PlayerNumberPanel.gameObject.SetActive(true);
    }

    public void MenuPrompt()
    {
        menuPromptPanel.SetActive(true);
        QuitBtn.gameObject.SetActive(false);
        MenuBtn.gameObject.SetActive(false);
        RulesBtn.gameObject.SetActive(false);
        PlayerNumberPanel.gameObject.SetActive(false);
    }

    public void ShowRules()
    {
        PlayerAreaPanel.gameObject.SetActive(false);
        DecksAreaPanel.gameObject.SetActive(false);
        QuitBtn.gameObject.SetActive(false);
        MenuBtn.gameObject.SetActive(false);
        RulesBtn.gameObject.SetActive(false);
        PlayerNumberPanel.gameObject.SetActive(false);
        RulesPanel.gameObject.SetActive(true);   
    }

    public void CloseRules()
    {
        PlayerAreaPanel.gameObject.SetActive(true);
        DecksAreaPanel.gameObject.SetActive(true);
        QuitBtn.gameObject.SetActive(true);
        MenuBtn.gameObject.SetActive(true);
        RulesBtn.gameObject.SetActive(true); 
        PlayerNumberPanel.gameObject.SetActive(true);
        RulesPanel.gameObject.SetActive(false);
    }
}
