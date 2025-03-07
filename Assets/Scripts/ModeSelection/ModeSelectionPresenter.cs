using UnityEngine;

public class ModeSelectionPresenter : MonoBehaviour
{
    private ModeSelectionModel model;
    [SerializeField] private ModeSelectionView view;

    private void Start()
    {
        model = new ModeSelectionModel();
        
        view.AddOfflineModeListener(() => OnModeSelected(GameMode.OfflineMode));
        view.AddOnlineRoomModeListener(() => OnModeSelected(GameMode.OnlineRoomMode));
        view.AddOnlineMatchingModeListener(() => OnModeSelected(GameMode.OnlineMatchingMode));
    }

    private void OnModeSelected(GameMode mode)
    {
        model.SetMode(mode);
        GameManager.Instance.SetGameMode(mode);
    }
}