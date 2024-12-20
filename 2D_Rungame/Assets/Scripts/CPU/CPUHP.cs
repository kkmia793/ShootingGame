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

        // Slider���ő�ɂ���B
        slider_01.value = 1;
        slider_02.value = 1;
    }

    void Update()
    {
        if (beforeHP == cpu.GetHP()) return;

        beforeHP = cpu.GetHP();
        // HP��Slider�ɔ��f�B
        slider_01.value = (float)beforeHP / (float)MaxHP;

        float targetValue = (float)beforeHP / (float)MaxHP;

        StartCoroutine(UpdateSliderWithDelay(targetValue, 1.0f)); // 1.0�b��ɔ��f

    }

    IEnumerator UpdateSliderWithDelay(float targetValue, float delay)
    {
        yield return new WaitForSeconds(delay);

        // ���X�ɔ��f����HP�o�[�𑦍��ɍX�V
        slider_02.value = targetValue;
    }


}



