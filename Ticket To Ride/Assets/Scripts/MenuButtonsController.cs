using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MenuButtonsController : MonoBehaviour
{
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public Button QuitBtn;
    public Button MenuBtn;
    public Button RulesBtn;
    public GameObject RulesPanel;
    public GameObject quitPromptPanel;
    public GameObject menuPromptPanel;
    public GameObject PlayerNumberPanel;
    public GameObject PlayerAreaPanel;
    public GameObject DecksAreaPanel;
    public GameObject MenuBackground;

    public Button nextPageBtn;
    public Button previousPageBtn;
    public GameObject[] rulesPages;  
    public TMP_Text pageNumberText;
    private int currentPage = 1;


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void QuitGamePrompt()
    {
        quitPromptPanel.SetActive(true);
        QuitBtn.gameObject.SetActive(false);
        MenuBtn.gameObject.SetActive(false);
        RulesBtn.gameObject.SetActive(false);
        MenuBackground.gameObject.SetActive(false);
        PlayerNumberPanel.gameObject.SetActive(false);
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void QuitGameConfirm()
    {
        Application.Quit();
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void QuitGameCancel()
    {
        quitPromptPanel.SetActive(false);
        menuPromptPanel.SetActive(false);
        QuitBtn.gameObject.SetActive(true);
        MenuBtn.gameObject.SetActive(true);
        RulesBtn.gameObject.SetActive(true); 
        MenuBackground.gameObject.SetActive(true);
        PlayerNumberPanel.gameObject.SetActive(true);
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void MenuPrompt()
    {
        menuPromptPanel.SetActive(true);
        QuitBtn.gameObject.SetActive(false);
        MenuBtn.gameObject.SetActive(false);
        RulesBtn.gameObject.SetActive(false);
        MenuBackground.gameObject.SetActive(false);
        PlayerNumberPanel.gameObject.SetActive(false);
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void ShowRules()
    {
        PlayerAreaPanel.gameObject.SetActive(false);
        DecksAreaPanel.gameObject.SetActive(false);
        QuitBtn.gameObject.SetActive(false);
        MenuBtn.gameObject.SetActive(false);
        RulesBtn.gameObject.SetActive(false);
        PlayerNumberPanel.gameObject.SetActive(false);
        RulesPanel.gameObject.SetActive(true);  
        MenuBackground.gameObject.SetActive(false); 

        for (int i = 0; i < rulesPages.Length; i++)
        {
        rulesPages[i].SetActive(i == 0);
        }

        nextPageBtn.interactable = true;
        previousPageBtn.interactable = false;

        currentPage = 1;
        pageNumberText.text = $"Page {currentPage}";
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void CloseRules()
    {
        PlayerAreaPanel.gameObject.SetActive(true);
        DecksAreaPanel.gameObject.SetActive(true);
        QuitBtn.gameObject.SetActive(true);
        MenuBtn.gameObject.SetActive(true);
        RulesBtn.gameObject.SetActive(true); 
        PlayerNumberPanel.gameObject.SetActive(true);
        RulesPanel.gameObject.SetActive(false);
        MenuBackground.gameObject.SetActive(true);
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void GoToNextPage()
    {

    if (currentPage < rulesPages.Length)
    {
        rulesPages[currentPage - 1].SetActive(false);
        
        currentPage++;

        rulesPages[currentPage - 1].SetActive(true);

        pageNumberText.text = $"Page {currentPage}";  

        if (currentPage == rulesPages.Length)
        {
            nextPageBtn.interactable = false;
        }

        previousPageBtn.interactable = true;
    }
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void GoToPreviousPage()
    {

    if (currentPage > 1)
    {

        rulesPages[currentPage - 1].SetActive(false);
        
        currentPage--;
        
        rulesPages[currentPage - 1].SetActive(true);

        pageNumberText.text = $"Page {currentPage}";

        if (currentPage == 1)
        {
            previousPageBtn.interactable = false;
        }

        nextPageBtn.interactable = true;
    }
    }

}
