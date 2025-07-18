using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    [Header("UŒ‚—Í")]
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
        // “G‚ÆÕ“Ë‚µ‚½‚ç
        if (collision.gameObject.TryGetComponent<PlayerController>(out var pc))
        {
            isSuccess = true;

            // ‰¹‚ªİ’è‚³‚ê‚Ä‚¢‚éê‡‚Í‚»‚ê‚ğ–Â‚ç‚·
            if (soundManager != null) soundManager.Play("special");

            // ƒ_ƒ[ƒW‚ğ—^‚¦‚é
            pc.TakeDamage(attackPow);
        }
        
    }
}
