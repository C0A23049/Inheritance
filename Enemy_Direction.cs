using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Direction : MonoBehaviour
{
    private GameObject playerObject; // プレイヤーオブジェクト
    private Vector3 PlayerPosition; // プレイヤーの位置情報
    private Vector3 EnemyPosition; // 敵の位置情報
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float padding;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        PlayerPosition = playerObject.transform.position;
        EnemyPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPosition = playerObject.transform.position;
        EnemyPosition = transform.position;
        if (PlayerPosition.x - EnemyPosition.x > padding)
        {
            spriteRenderer.flipX = true;
        }
        else if (padding <  EnemyPosition.x - PlayerPosition.x)
        {
            spriteRenderer.flipX = false;
        }
    }
}
