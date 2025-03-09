using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectionView : MonoBehaviour
{
    [SerializeField] private Image[] characterImages;
    [SerializeField] private TextMeshProUGUI[] characterNames;
    [SerializeField] private Button[] characterButtons;
    [SerializeField] private Button backButton;
    [SerializeField] private Button nextButton;

    public event Action<int> OnCharacterButtonClicked;
    public event Action OnBackButtonClicked;
    public event Action OnNextButtonClicked;

    private void Start()
    {
        for (int i = 0; i < characterButtons.Length; i++)
        {
            int index = i;
            characterButtons[i].onClick.AddListener(() => OnCharacterButtonClicked?.Invoke(index));
        }

        backButton.onClick.AddListener(() => OnBackButtonClicked?.Invoke());
        nextButton.onClick.AddListener(() => OnNextButtonClicked?.Invoke());
    }

    public void UpdateCharacterDisplay(CharacterData[] characterDataArray)
    {
        for (int i = 0; i < characterImages.Length; i++)
        {
            characterImages[i].sprite = characterDataArray[i].characterSprite;
            characterNames[i].text = characterDataArray[i].characterName;
        }
    }

    public void HighlightSelectedCharacter(int index)
    {
        for (int i = 0; i < characterButtons.Length; i++)
        {
            characterButtons[i].interactable = i != index;
        }
    }
    public void ResetCharacterSelection()
    {
        foreach (var button in characterButtons)
        {
            button.interactable = true;
        }
        
        HighlightSelectedCharacter(0);
    }
}