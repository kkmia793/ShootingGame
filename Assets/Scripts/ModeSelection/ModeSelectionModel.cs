public enum GameMode
{
    OfflineMode,
    OnlineRoomMode,
    OnlineMatchingMode
}

public class ModeSelectionModel
{
    public GameMode SelectedMode { get; private set; }

    public void SetMode(GameMode mode)
    {
        SelectedMode = mode;
    }
}