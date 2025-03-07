using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleView : MonoBehaviour
{
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private TextMeshProUGUI startMessageText;

    public void SetStartMessage(string message)
    {
        if (startMessageText != null)
        {
            startMessageText.text = message;
        }
    }

    public void ShowTitleScreen(bool show)
    {
        if (titleScreen != null)
        {
            titleScreen.SetActive(show);
        }
    }
    
}