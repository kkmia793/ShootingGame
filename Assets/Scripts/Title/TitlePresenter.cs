using UnityEngine;

public class TitlePresenter : MonoBehaviour
{
    private TitleModel model;
    [SerializeField] private TitleView view;

    private void Start()
    {
        model = new TitleModel();
        
        view.SetStartMessage(model.StartMessage);
        view.ShowTitleScreen(true);
        
    }

    private void OnTaped()
    {
        SceneController.Instance.LoadScene("ModeSelection");
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            OnTaped();
        }
    }
}