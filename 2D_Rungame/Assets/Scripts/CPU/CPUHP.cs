using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class CPUHP : MonoBehaviour
{
    public Slider slider_01;
    public Slider slider_02;
    private CPUManager cpu;

    private float beforeHP;
    private float MaxHP;

    [System.Obsolete]
    void Start()
    {
        cpu = FindObjectOfType<CPUManager>();
        beforeHP = cpu.GetHP();
        MaxHP = cpu.GetMaxHP();

        // Sliderを最大にする。
        slider_01.value = 1;
        slider_02.value = 1;
    }

    void Update()
    {
        if (beforeHP == cpu.GetHP()) return;

        beforeHP = cpu.GetHP();
        // HPをSliderに反映。
        slider_01.value = (float)beforeHP / (float)MaxHP;

        float targetValue = (float)beforeHP / (float)MaxHP;

        StartCoroutine(UpdateSliderWithDelay(targetValue, 1.0f)); // 1.0秒後に反映

    }

    IEnumerator UpdateSliderWithDelay(float targetValue, float delay)
    {
        yield return new WaitForSeconds(delay);

        // 徐々に反映するHPバーを即座に更新
        slider_02.value = targetValue;
    }


}



