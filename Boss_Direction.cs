using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Direction : MonoBehaviour
{
    private GameObject playerObject; // �v���C���[�I�u�W�F�N�g
    private Vector3 PlayerPosition; // �v���C���[�̈ʒu���
    private Vector3 BossPosition; // �G�̈ʒu���
    private SpriteRenderer spriteRenderer;
    [SerializeField] bool isFlip = false;
    [SerializeField] private float padding;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        PlayerPosition = playerObject.transform.position;
        BossPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPosition = playerObject.transform.position;
        BossPosition = transform.position;
        if (PlayerPosition.x - BossPosition.x > padding)
        {
            spriteRenderer.flipX = !isFlip;
        }
        else if (padding < BossPosition.x - PlayerPosition.x)
        {
            spriteRenderer.flipX = isFlip;
        }
    }
}
