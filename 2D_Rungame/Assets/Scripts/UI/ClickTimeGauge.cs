using UnityEngine;
using UnityEngine.UI;

public class ClickTimeGauge : MonoBehaviour
{
    public Slider clickGauge;
    public float maxClickTime = 1.0f; // クリックの最大許容時間
    private float clickStartTime; // クリックの開始時間

    void Start()
    {
        clickGauge.maxValue = maxClickTime;
        clickGauge.value = 0;
       
    }

    void Update()
    {
        // クリックの開始
        if (Input.GetMouseButtonDown(0))
        {
            clickStartTime = Time.time;
            clickGauge.value = 0;
           
        }

        // クリック中
        if (Input.GetMouseButton(0))
        {
            float clickDuration = Time.time - clickStartTime;
            clickGauge.value = Mathf.Clamp(clickDuration, 0, maxClickTime);
        }

        // クリックの終了
        if (Input.GetMouseButtonUp(0))
        {
            float clickDuration = Time.time - clickStartTime;
            Debug.Log("Click Duration: " + clickDuration);

            
        }
    }
}
