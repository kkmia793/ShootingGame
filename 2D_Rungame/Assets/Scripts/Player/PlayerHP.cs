using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public Slider slider_01;
    public Slider slider_02;

    public Slider MP_slider_01;
    public Slider MP_slider_02;

    private PlayerManager player;

    private float beforeHP;
    private float MaxHP;
    private float beforeMP;
    private float MaxMP;
    private float mpRecoveryRate;

    [System.Obsolete]
    void Start()
    {
        player = FindObjectOfType<PlayerManager>();

        beforeHP = player.GetHP();
        MaxHP = player.GetMaxHP();

        beforeMP = player.GetMP();
        MaxMP = player.GetMaxMP();

        mpRecoveryRate = player.MP_RecoverySpeed;

        // Slider���ő�ɂ���B
        slider_01.value = 1;
        slider_02.value = 1;

        MP_slider_01.value = 1;
        MP_slider_02.value = 1;
    }

    void Update()
    {
        Player_HPManager();
        Player_MPManager();
    }

    void Player_HPManager()
    {
        if (beforeHP == player.GetHP()) return;

        beforeHP = player.GetHP();
        // HP��Slider�ɔ��f�B
        slider_01.value = (float)beforeHP / (float)MaxHP;

        float targetValue = (float)beforeHP / (float)MaxHP;

        StartCoroutine(UpdateSliderWithDelay(targetValue, 1.0f)); // 1.0�b��ɔ��f
    }

    void Player_MPManager()
    {
        if (beforeMP == player.GetMP()) return;

        beforeMP = player.GetMP();

        MP_slider_01.value = (float)beforeMP / (float)MaxMP;
        Debug.Log("Slider Value: " + MP_slider_01.value);
        float targetMPValue = (float)beforeMP / (float)MaxMP;

        StartCoroutine(UpdateMPSliderWithDelay(targetMPValue, 1.0f)); // 1.0�b��ɔ��f
    }

    IEnumerator UpdateSliderWithDelay(float targetValue, float delay)
    {
        yield return new WaitForSeconds(delay);

        // ���X�ɔ��f����HP�o�[�𑦍��ɍX�V
        slider_02.value = targetValue;
    }

    IEnumerator UpdateMPSliderWithDelay(float targetMPValue, float delay)
    {
        yield return new WaitForSeconds(delay);

        // ���X�ɔ��f����HP�o�[�𑦍��ɍX�V
        MP_slider_02.value = targetMPValue;
    }
}
