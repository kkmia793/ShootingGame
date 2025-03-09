using UnityEngine;

public class ModeSelectionPresenter : MonoBehaviour
{
    private ModeSelectionModel model;
    [SerializeField] private ModeSelectionView view;

    private void Start()
    {
        model = new ModeSelectionModel();

        model.OnGameModeChanged += OnGameModeChanged;

        view.OnOfflineModeSelected += () => model.SetMode(GameMode.OfflineMode);
        view.OnOnlineRoomModeSelected += () => model.SetMode(GameMode.OnlineRoomMode);
        view.OnOnlineMatchingModeSelected += () => model.SetMode(GameMode.OnlineMatchingMode);
        view.OnBackButtonClicked += OnBackButtonClicked;
        view.OnNextButtonClicked += OnNextButtonClicked;

        OnGameModeChanged(GameMode.OfflineMode);
    }

    private void OnGameModeChanged(GameMode mode)
    {
        GameManager.Instance.SetGameMode(mode);
        view.UpdateSelectedMode(mode);
    }

    private void OnBackButtonClicked()
    {
        SceneController.Instance.LoadScene("Title");
    }

    private void OnNextButtonClicked()
    {
        GameMode currentMode = model.SelectedMode;

        switch (currentMode)
        {
            case GameMode.OfflineMode:
                SceneController.Instance.LoadScene("CharacterSelection");
                break;

            case GameMode.OnlineRoomMode:
                SceneController.Instance.LoadScene("RoomCreation");
                break;

            case GameMode.OnlineMatchingMode:
                SceneController.Instance.LoadScene("CharacterSelection");
                break;

            default:
                break;
        }
    }

    private void OnDestroy()
    {
        model.OnGameModeChanged -= OnGameModeChanged;
        view.OnOfflineModeSelected -= () => model.SetMode(GameMode.OfflineMode);
        view.OnOnlineRoomModeSelected -= () => model.SetMode(GameMode.OnlineRoomMode);
        view.OnOnlineMatchingModeSelected -= () => model.SetMode(GameMode.OnlineMatchingMode);
        view.OnBackButtonClicked -= OnBackButtonClicked;
        view.OnNextButtonClicked -= OnNextButtonClicked;
    }
}
