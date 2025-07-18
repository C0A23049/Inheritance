using System.Collections;
using AngoraUtility;
using UnityEngine;

public class StrongEnemyMotion : MonoBehaviour
{
    [SerializeField] private float timerAttack1RunWait;
    [SerializeField] private float timerAttack1Wait;
    [SerializeField] private float timerAttack1NextAttackWait;
    [SerializeField] private float timerAttack2RunWait;
    [SerializeField] private float timerAttack2Wait;
    [SerializeField] private float timerAttack2NextAttackWait;
    [Header("�������~�͈̔�")]
    [SerializeField] private float distance_1;
    [Header("�傫���~�͈̔�")]
    [SerializeField] private float distance_2;
    [SerializeField] private float startAttackDistance;
    [Header("�������~�ł̍U���P�̊m���i0�`1�j")]
    [SerializeField] private float area1Attack1Probability;
    [Header("�傫���~�ł̍U��1�̊m���i0�`1�j")]
    [SerializeField] private float area2Attack1Probability;
    [SerializeField] private float retrunEnemySpeed;
    [SerializeField] private float attack1RunSpeed;
    [SerializeField] private float attack2RunSpeed;
    private GameObject playerObject; // �v���C���[�I�u�W�F�N�g
    private Vector3 PlayerPosition; // �v���C���[�̈ʒu���
    private Vector3 EnemyPosition; // �G�̈ʒu���
    private Vector3 HomePosition;
    private Vector3 TargetPosition;
    protected bool Attack_Time;
    private Enemy_Direction enemy_Direction;
    private EnemyStatus enemyStatus;
    [SerializeField] GameObject enemy_attack1_Right;
    [SerializeField] GameObject enemy_attack1_Left;
    [SerializeField] GameObject enemy_attack2_Right;
    [SerializeField] GameObject enemy_attack2_Left;
    [Space(10)]
    [Header("�摜")]
    [SerializeField] Sprites sprites;
    private SpriteRenderer sr;
    private int countNoAttacktime;
    private SoundManager soundManager;

    // �f�����U���i����j
    protected virtual IEnumerator EnemyAttack1()
    {
        // �����ύX
        bool Enemy_face_Right = false;
        bool attackPosition = false;
        sr.sprite = sprites.Normal;
        enemy_Direction.enabled = false;
        Debug.Log("attack1");
        if (PlayerPosition.x > EnemyPosition.x)
        {
            Enemy_face_Right = true;
        }
        else if (PlayerPosition.x < EnemyPosition.x)
        {
            Enemy_face_Right = false;
        }

        // �v���C���[�ɋ߂Â�
        yield return new WaitForSeconds(timerAttack1RunWait);
        TargetPosition = PlayerPosition;
        while (!attackPosition)
        {

            if (startAttackDistance >= Vector3.Distance(TargetPosition, EnemyPosition))
            {
                attackPosition = true;
            }
            else
            {
                if (TargetPosition.x > EnemyPosition.x)
                {
                    EnemyPosition.x = EnemyPosition.x + attack1RunSpeed * Time.deltaTime;
                }
                else if (TargetPosition.x < EnemyPosition.x)
                {
                    EnemyPosition.x = EnemyPosition.x - attack1RunSpeed * Time.deltaTime;
                }
                if (TargetPosition.y > EnemyPosition.y)
                {
                    EnemyPosition.y = EnemyPosition.y + attack1RunSpeed * Time.deltaTime;
                }
                else if (TargetPosition.y < EnemyPosition.y)
                {
                    EnemyPosition.y = EnemyPosition.y - attack1RunSpeed * Time.deltaTime;
                }
                transform.position = EnemyPosition;
            }
            yield return null;
        }
        yield return new WaitUntil(() => attackPosition == true);

        // �\������J�n
        enemyStatus.SoundManager.Play("preAttack");
        sr.sprite = sprites.PreQuickAttack;

        // ����
        yield return new WaitForSeconds(timerAttack1Wait);
        sr.sprite = sprites.QuickAttack;
        enemyStatus.SoundManager.Play("punchWindBreak");
        if (Enemy_face_Right)
        {
            enemy_attack1_Right.SetActive(true);
            enemy_attack1_Left.SetActive(false);

        }
        else if (!Enemy_face_Right)
        {
            enemy_attack1_Right.SetActive(false);
            enemy_attack1_Left.SetActive(true);
        }

        // �U���I��
        yield return new WaitForSeconds(timerAttack1NextAttackWait);
        enemy_attack1_Right.SetActive(false);
        enemy_attack1_Left.SetActive(false);
        enemy_Direction.enabled = true;
        Attack_Time = false;
        sr.sprite = sprites.Normal;
    }

    // �d���U���i���j
    protected virtual IEnumerator EnemyAttack2()
    {
        // �\������J�n
        bool Enemy_face_Right = false;
        enemy_Direction.enabled = false;
        bool attackPosition = false;
        Debug.Log("attack2");
        if (PlayerPosition.x > EnemyPosition.x)
        {
            Enemy_face_Right = true;
        }
        else if (PlayerPosition.x < EnemyPosition.x)
        {
            Enemy_face_Right = false;
        }
        enemyStatus.SoundManager.Play("preAttack");
        sr.sprite = sprites.PreHeavyAttack;

        // �����Őڋ�
        yield return new WaitForSeconds(timerAttack2RunWait);
        TargetPosition = PlayerPosition;
        Debug.Log("attack2-1");
        while (!attackPosition)
        {
            if (startAttackDistance >= Vector3.Distance(TargetPosition, EnemyPosition))
            {
                attackPosition = true;
            }
            else
            {
                if (TargetPosition.x > EnemyPosition.x)
                {
                    EnemyPosition.x = EnemyPosition.x + attack2RunSpeed * Time.deltaTime;
                }
                else if (TargetPosition.x < EnemyPosition.x)
                {
                    EnemyPosition.x = EnemyPosition.x - attack2RunSpeed * Time.deltaTime;
                }
                if (TargetPosition.y > EnemyPosition.y)
                {
                    EnemyPosition.y = EnemyPosition.y + attack2RunSpeed * Time.deltaTime;
                }
                else if (TargetPosition.y < EnemyPosition.y)
                {
                    EnemyPosition.y = EnemyPosition.y - attack2RunSpeed * Time.deltaTime;
                }
                transform.position = EnemyPosition;
            }
            yield return null;
        }
        Debug.Log("attack2-2");

        // ���Ő؂��
        enemyStatus.SoundManager.Play("stepInto");
        yield return new WaitForSeconds(timerAttack2Wait);
        sr.sprite = sprites.HeavyAttack;
        Debug.Log("attack2-3");
        enemyStatus.SoundManager.Play("swordWindBreak");
        if (Enemy_face_Right)
        {
            enemy_attack2_Right.SetActive(true);
            enemy_attack2_Left.SetActive(false);

        }
        else if (!Enemy_face_Right)
        {
            enemy_attack2_Right.SetActive(false);
            enemy_attack2_Left.SetActive(true);
        }

        // �U���I��
        yield return new WaitForSeconds(timerAttack2NextAttackWait);
        Debug.Log("attack2-4");
        enemy_attack2_Right.SetActive(false);
        enemy_attack2_Left.SetActive(false);
        enemy_Direction.enabled = true;
        Attack_Time = false;
        sr.sprite = sprites.Normal;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        PlayerPosition = playerObject.transform.position;
        EnemyPosition = transform.position;
        enemyStatus = GetComponent<EnemyStatus>();

        Attack_Time = false;

        enemy_Direction = GetComponent<Enemy_Direction>();
        enemy_Direction.enabled = true;

        enemy_attack1_Right.SetActive(false);
        enemy_attack1_Left.SetActive(false);
        enemy_attack2_Right.SetActive(false);
        enemy_attack2_Left.SetActive(false);
        HomePosition = transform.position;

        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites.Normal;

        soundManager = GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Attack_Time)
        {
            countNoAttacktime = 0;
        }
        else
        {
            countNoAttacktime++;
        }
        PlayerPosition = playerObject.transform.position;
        EnemyPosition = transform.position;
        if (countNoAttacktime > 1 && (distance_2 >= Vector3.Distance(PlayerPosition, EnemyPosition)) && !Attack_Time)
        {
            Debug.Log("ok");
            float lotteryResult = Random.value;
            if (distance_1 >= Vector3.Distance(PlayerPosition, EnemyPosition))
            {
                Debug.Log("1");
                if (lotteryResult <= area1Attack1Probability)
                {
                    Debug.Log("11");
                    Attack_Time = true;
                    StartCoroutine(EnemyAttack1());
                }
                else
                {
                    Debug.Log("12");
                    Attack_Time = true;
                    StartCoroutine(EnemyAttack2());
                }
            }
            else
            {
                Debug.Log("2");
                if (lotteryResult <= area2Attack1Probability)
                {
                    Debug.Log("21");
                    Attack_Time = true;
                    StartCoroutine(EnemyAttack1());
                }
                else
                {
                    Debug.Log("22");
                    Attack_Time = true;
                    StartCoroutine(EnemyAttack2());
                }
            }
        }
        else if ((HomePosition != EnemyPosition) && !Attack_Time)
        {
            if (HomePosition.x > EnemyPosition.x)
            {
                EnemyPosition.x = EnemyPosition.x + retrunEnemySpeed * Time.deltaTime;
            }
            else if (HomePosition.x < EnemyPosition.x)
            {
                EnemyPosition.x = EnemyPosition.x - retrunEnemySpeed * Time.deltaTime;
            }
            if (HomePosition.y > EnemyPosition.y)
            {
                EnemyPosition.y = EnemyPosition.y + retrunEnemySpeed * Time.deltaTime;
            }
            else if (HomePosition.y < EnemyPosition.y)
            {
                EnemyPosition.y = EnemyPosition.y - retrunEnemySpeed * Time.deltaTime;
            }
            transform.position = EnemyPosition;
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        ExGizmos.DrawWireCircle2D(transform.position, distance_1, 100);
        Gizmos.color = Color.green;
        ExGizmos.DrawWireCircle2D(transform.position, distance_2, 100);
        Gizmos.color = Color.magenta;
        ExGizmos.DrawWireCircle2D(transform.position, startAttackDistance, 100);
    }
}

[System.Serializable]
public class Sprites
{
    [Header("�ҋ@/�ڋ�")]
    public Sprite Normal;
    [Header("�d���U���̐ڋ�")]
    public Sprite PreHeavyAttack;
    [Header("�d���U����")]
    public Sprite HeavyAttack;
    [Header("�f�����U���̗\������")]
    public Sprite PreQuickAttack;
    [Header("�f�����U����")]
    public Sprite QuickAttack;
}
