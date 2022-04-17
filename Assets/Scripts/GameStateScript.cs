using TMPro;
using UnityEngine;

public class GameStateScript : MonoBehaviour
{
    [SerializeField] private GameObject mainCanvas, menuPanel, resultsPanel;
    [SerializeField] private TextMeshProUGUI resultsTextObject;
    [SerializeField] private string youAreLoseText, youAreWinText, oldResultText, secondsName;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!mainCanvas.activeSelf || (mainCanvas.activeSelf && resultsPanel.activeSelf)) MenuEnter(false);
            else { MenuExit(); GameController.GameLogic.StartNewGame(); }
        }
    }
    public void GameEnd(bool win)
    {
        mainCanvas.SetActive(true);
        menuPanel.SetActive(false);
        if (win) WinEvent();
        else LoseEvent();
    }

    public void MenuEnter(bool openResults)
    {
        GameController.GameLogic.DestroySphere();
        resultsTextObject.text = $"{oldResultText}\n{GameController.oldResult()} {secondsName}";
        mainCanvas.SetActive(true);
        resultsPanel.SetActive(openResults);
        menuPanel.SetActive(!openResults);
    }

    public void MenuExit()
    {
        mainCanvas.SetActive(false);        
    }

    private void WinEvent()
    {
        resultsTextObject.text = $"{youAreWinText}\n{GameController.finishTime()} {secondsName}";
        resultsPanel.SetActive(true);
    }

    private void LoseEvent()
    {
        resultsTextObject.text = $"{youAreLoseText}";
        resultsPanel.SetActive(true);
    }
}
