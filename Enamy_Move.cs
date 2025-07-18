using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enamy_Move : MonoBehaviour
{
    private GameObject playerObject; // プレイヤーオブジェクト
    private Vector3 PlayerPosition; // プレイヤーの位置情報
    private Vector3 EnemyPosition; // 敵の位置情報
    [SerializeField] private float enemySpeed;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        PlayerPosition = playerObject.transform.position;
        EnemyPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPosition = playerObject.transform.position;
        EnemyPosition = transform.position;
        if (PlayerPosition.x > EnemyPosition.x)
        {
            EnemyPosition.x = EnemyPosition.x + enemySpeed * Time.deltaTime;
        }
        else if (PlayerPosition.x <  EnemyPosition.x)
        {
            EnemyPosition.x = EnemyPosition.x - enemySpeed * Time.deltaTime;
        }
        if (PlayerPosition.y > EnemyPosition.y)
        {
            EnemyPosition.y = EnemyPosition.y + enemySpeed * Time.deltaTime;
        }
        else if (PlayerPosition.y < EnemyPosition.y)
        {
            EnemyPosition.y = EnemyPosition.y - enemySpeed * Time.deltaTime;
        }
        transform.position = EnemyPosition;
    }
}
