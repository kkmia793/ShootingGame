using System;
using UnityEngine;
using UnityEngine.UI;

public class ModeSelectionView : MonoBehaviour
{
    [SerializeField] private Button offlineModeButton;
    [SerializeField] private Button onlineRoomModeButton;
    [SerializeField] private Button onlineMatchingModeButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button nextButton;

    public event Action OnOfflineModeSelected;
    public event Action OnOnlineRoomModeSelected;
    public event Action OnOnlineMatchingModeSelected;
    public event Action OnBackButtonClicked;
    public event Action OnNextButtonClicked;

    private void Start()
    {
        offlineModeButton.onClick.AddListener(() => OnOfflineModeSelected?.Invoke());
        onlineRoomModeButton.onClick.AddListener(() => OnOnlineRoomModeSelected?.Invoke());
        onlineMatchingModeButton.onClick.AddListener(() => OnOnlineMatchingModeSelected?.Invoke());
        backButton.onClick.AddListener(() => OnBackButtonClicked?.Invoke());
        nextButton.onClick.AddListener(() => OnNextButtonClicked?.Invoke());
    }

    // UIに選択状態を反映する
    public void UpdateSelectedMode(GameMode mode)
    {
        offlineModeButton.interactable = (mode != GameMode.OfflineMode);
        onlineRoomModeButton.interactable = (mode != GameMode.OnlineRoomMode);
        onlineMatchingModeButton.interactable = (mode != GameMode.OnlineMatchingMode);

        Debug.Log($"View: 選択中のゲームモードは {mode} です。");
    }
}
