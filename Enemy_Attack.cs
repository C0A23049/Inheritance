using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    [Header("攻撃力")]
    [SerializeField] int attackPow = 5;
    private bool isSuccess;
    private SoundManager soundManager;

    public bool IsSuccess
    {
        get
        {
            return isSuccess;
        }
    }

    private void Start()
    {
        soundManager = GetComponent<SoundManager>();
    }

    private void OnEnable()
    {
        isSuccess = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 敵と衝突したら
        if (collision.gameObject.TryGetComponent<PlayerController>(out var pc))
        {
            isSuccess = true;

            // 音が設定されている場合はそれを鳴らす
            if (soundManager != null) soundManager.Play("special");

            // ダメージを与える
            pc.TakeDamage(attackPow);
        }
        
    }
}
