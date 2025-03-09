using System;

public enum GameMode
{
    OfflineMode,
    OnlineRoomMode,
    OnlineMatchingMode
}

public class ModeSelectionModel
{
    private GameMode selectedMode = GameMode.OfflineMode; 

    public event Action<GameMode> OnGameModeChanged;

    public GameMode SelectedMode => selectedMode;

    public ModeSelectionModel()
    {
        OnGameModeChanged?.Invoke(selectedMode);
    }

    public void SetMode(GameMode mode)
    {
        if (selectedMode != mode)
        {
            selectedMode = mode;
            OnGameModeChanged?.Invoke(selectedMode);
        }
    }
}
