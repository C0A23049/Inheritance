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
    private Image image;    // UIのImageコンポーネント
    private float Boss_Percent;   // 直前のパーセント
    private float percent;    // 最新のパーセント
    [SerializeField] EnemyStatus boss_Status; //EnemyStatusを読み込む

    void Start()
    {
        image = GetComponent<Image>();
        percent = boss_Status.HpRate;
        Boss_Percent = percent;
        image.fillAmount = percent;
    }

    void Update()
    {
        // ＨＰが変化していたら
        
        percent = boss_Status.HpRate * 100;
        if (Boss_Percent != percent)
        {
            // 直前の値を更新
            Boss_Percent = percent;

            // UIに反映する
            if (savedCoroutine != null) StopCoroutine(savedCoroutine);
            savedCoroutine = StartCoroutine(UpdateUI(percent));
        }
    }

    /// <summary>
    /// UIに反映する
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