using UnityEngine;
using UnityEngine.UI;

public class ModeSelectionView : MonoBehaviour
{
    [SerializeField] private Button offlineModeButton;
    [SerializeField] private Button onlineRoomModeButton;
    [SerializeField] private Button onlineMatchingModeButton;

    public void AddOfflineModeListener(UnityEngine.Events.UnityAction action)
    {
        offlineModeButton.onClick.RemoveAllListeners();
        offlineModeButton.onClick.AddListener(action);
    }

    public void AddOnlineRoomModeListener(UnityEngine.Events.UnityAction action)
    {
        onlineRoomModeButton.onClick.RemoveAllListeners();
        onlineRoomModeButton.onClick.AddListener(action);
    }

    public void AddOnlineMatchingModeListener(UnityEngine.Events.UnityAction action)
    {
        onlineMatchingModeButton.onClick.RemoveAllListeners();
        onlineMatchingModeButton.onClick.AddListener(action);
    }
}