using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MenuButtonsController : MonoBehaviour
{
    public Button QuitBtn;
    public Button MenuBtn;
    public GameObject quitPromptPanel;

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGamePrompt()
    {
        quitPromptPanel.SetActive(true);
        QuitBtn.gameObject.SetActive(false);
        MenuBtn.gameObject.SetActive(false);
    }

    public void QuitGameConfirm()
    {
        Application.Quit();
    }

    public void QuitGameCancel()
    {
        quitPromptPanel.SetActive(false);
        QuitBtn.gameObject.SetActive(true);
        MenuBtn.gameObject.SetActive(true);
    }
}
