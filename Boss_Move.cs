using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Move : MonoBehaviour
{
    private GameObject playerObject; // プレイヤーオブジェクト
    private Vector3 PlayerPosition; // プレイヤーの位置情報
    private Vector3 BossPosition; // 敵の位置情報
    [SerializeField] private float BossSpeed;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        PlayerPosition = playerObject.transform.position;
        BossPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPosition = playerObject.transform.position;
        BossPosition = transform.position;
        if (PlayerPosition.x > BossPosition.x)
        {
            BossPosition.x = BossPosition.x + BossSpeed * Time.deltaTime;
        }
        else if (PlayerPosition.x < BossPosition.x)
        {
            BossPosition.x = BossPosition.x - BossSpeed * Time.deltaTime;
        }
        if (PlayerPosition.y > BossPosition.y)
        {
            BossPosition.y = BossPosition.y + BossSpeed * Time.deltaTime;
        }
        else if (PlayerPosition.y < BossPosition.y)
        {
            BossPosition.y = BossPosition.y - BossSpeed * Time.deltaTime;
        }
        transform.position = BossPosition;
    }
}