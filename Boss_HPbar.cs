using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AngoraUtility;
using UnityEngine.UI;

public class Boss_HPbar : MonoBehaviour
{
    private const float UPDATEUI_TIME = 0.4f;
    private const float UPDATEUI_SINEPOW = 0.5f;

    private Coroutine savedCoroutine;
    private Image image;    // UI��Image�R���|�[�l���g
    private float Boss_Percent;   // ���O�̃p�[�Z���g
    private float percent;    // �ŐV�̃p�[�Z���g
    [SerializeField] EnemyStatus boss_Status; //EnemyStatus��ǂݍ���

    void Start()
    {
        image = GetComponent<Image>();
        percent = boss_Status.HpRate;
        Boss_Percent = percent;
        image.fillAmount = percent;
    }

    void Update()
    {
        // �g�o���ω����Ă�����
        
        percent = boss_Status.HpRate * 100;
        if (Boss_Percent != percent)
        {
            // ���O�̒l���X�V
            Boss_Percent = percent;

            // UI�ɔ��f����
            if (savedCoroutine != null) StopCoroutine(savedCoroutine);
            savedCoroutine = StartCoroutine(UpdateUI(percent));
        }
    }

    /// <summary>
    /// UI�ɔ��f����
    /// </summary>
    /// <param name="newPercent"></param>
    /// <returns></returns>
    private IEnumerator UpdateUI(float newPercent)
    {
        var pFillAmount = image.fillAmount;
        var newFillAmount = newPercent / 100f;

        const int countMax = 30;
        for (int i = 1; i <= countMax; i++)
        {
            float t = Mathf.Pow(Mathf.Sin(i * 0.5f / countMax * Mathf.PI), UPDATEUI_SINEPOW);
            image.fillAmount = Mathf.Lerp(pFillAmount, newFillAmount, t);
            yield return new WaitForSeconds(UPDATEUI_TIME / countMax);
        }
    }
}