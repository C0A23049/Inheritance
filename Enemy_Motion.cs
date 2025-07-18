using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Motion : MonoBehaviour
{
    [SerializeField] private float timer_wait;
    [SerializeField] private float timer_attack;
    [SerializeField] private float distance;
    [SerializeField] private float distance_Y;
    private GameObject playerObject; // プレイヤーオブジェクト
    private Vector3 PlayerPosition; // プレイヤーの位置情報
    private Vector3 EnemyPosition; // 敵の位置情報
    protected bool Attack_Time;
    private Enamy_Move enamy_Move;
    private Enemy_Direction enemy_Direction;
    private NormalEnemyVfxGenerator vfxGenerator;
    private EnemyStatus enemyStatus;
    [SerializeField] GameObject enemy_attack_Right;
    [SerializeField] GameObject enemy_attack_Left;
    
    protected virtual IEnumerator Enemy_Wait()
    {
        bool Enemy_face_Right = false;
        enamy_Move.enabled = false;
        enemy_Direction.enabled = false;
        if (PlayerPosition.x > EnemyPosition.x)
        {
            Enemy_face_Right = true;
        }
        else if (PlayerPosition.x < EnemyPosition.x)
        {
            Enemy_face_Right = false;
        }
        enemyStatus.SoundManager.Play("scratch_up");
        yield return new WaitForSeconds(timer_wait);
        enemyStatus.SoundManager.Play("scratch_down");
        if (Enemy_face_Right)
        {
            enemy_attack_Right.SetActive(true);
            enemy_attack_Left.SetActive(false);
            vfxGenerator.GenerateEffect(0);

        }
        else if (!Enemy_face_Right)
        {
            enemy_attack_Right.SetActive(false);
            enemy_attack_Left.SetActive(true);
            vfxGenerator.GenerateEffect(1);
        }
        yield return new WaitForSeconds(timer_attack);
        enemy_attack_Right.SetActive(false);
        enemy_attack_Left.SetActive(false);

        enamy_Move.enabled = true;
        enemy_Direction.enabled = true;
        Attack_Time = false;

    }
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        PlayerPosition = playerObject.transform.position;
        EnemyPosition = transform.position;
        vfxGenerator = GetComponent<NormalEnemyVfxGenerator>();
        enemyStatus = GetComponent<EnemyStatus>();

        Attack_Time = false;

        enamy_Move = GetComponent<Enamy_Move>();
        enamy_Move.enabled = true;

        enemy_Direction = GetComponent<Enemy_Direction>();
        enemy_Direction.enabled = true;

        enemy_attack_Right.SetActive(false);
        enemy_attack_Left.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPosition = playerObject.transform.position;
        EnemyPosition = transform.position;
        if ((distance >= Vector3.Distance(PlayerPosition, EnemyPosition)) && !Attack_Time)
        {
            if (distance_Y >= Mathf.Abs(EnemyPosition.y - PlayerPosition.y))
            {
                Attack_Time = true;
                StartCoroutine(Enemy_Wait());
            }
        }

    }


}
