using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    [Header("�U����")]
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
        // �G�ƏՓ˂�����
        if (collision.gameObject.TryGetComponent<PlayerController>(out var pc))
        {
            isSuccess = true;

            // �����ݒ肳��Ă���ꍇ�͂����炷
            if (soundManager != null) soundManager.Play("special");

            // �_���[�W��^����
            pc.TakeDamage(attackPow);
        }
        
    }
}
