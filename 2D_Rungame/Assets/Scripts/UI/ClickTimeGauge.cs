using UnityEngine;
using UnityEngine.UI;

public class ClickTimeGauge : MonoBehaviour
{
    public Slider clickGauge;
    public float maxClickTime = 1.0f; // �N���b�N�̍ő勖�e����
    private float clickStartTime; // �N���b�N�̊J�n����

    void Start()
    {
        clickGauge.maxValue = maxClickTime;
        clickGauge.value = 0;
       
    }

    void Update()
    {
        // �N���b�N�̊J�n
        if (Input.GetMouseButtonDown(0))
        {
            clickStartTime = Time.time;
            clickGauge.value = 0;
           
        }

        // �N���b�N��
        if (Input.GetMouseButton(0))
        {
            float clickDuration = Time.time - clickStartTime;
            clickGauge.value = Mathf.Clamp(clickDuration, 0, maxClickTime);
        }

        // �N���b�N�̏I��
        if (Input.GetMouseButtonUp(0))
        {
            float clickDuration = Time.time - clickStartTime;
            Debug.Log("Click Duration: " + clickDuration);

            
        }
    }
}