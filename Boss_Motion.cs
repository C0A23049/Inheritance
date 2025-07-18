using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Motion : MonoBehaviour
{
    [SerializeField] private float timer_wait_Punch;
    [SerializeField] private float timer_first_short_wait;
    [SerializeField] private float timer_second_short_wait;
    [SerializeField] private float timer_Success_wait;
    [SerializeField] private float timer_wait_Wave;
    [SerializeField] private float timer_harden_Wave;
    [SerializeField] private float timer_Rush_wait;
    [SerializeField] private float timer_Rush_harden;
    [SerializeField] private float distance;
    [SerializeField] private float distance_Y;
    [SerializeField] private float unnecessary_angle_half;
    [SerializeField] private float Rush_Speed;
    [SerializeField] private int count_attention;
    private GameObject playerObject; // プレイヤーオブジェクト
    private Vector3 PlayerPosition; // プレイヤーの位置情報
    private Vector3 BossPosition; // 敵の位置情報
    protected bool Attack_Time;
    private Boss_Move boss_Move;
    private Boss_Direction boss_Direction;
    private Enemy_Attack Boss_Attack_Success_Right;
    private Enemy_Attack Boss_Attack_Success_Left;
    private SpriteRenderer Boss_Attack_Attention;
    private Enemy_Attack Boss_Wave_Attack_Check;
    private PlayerController Player_recovery;
    private EnemyStatus boss_Status;
    private Rigidbody2D RB2D;
    private VfxGenerator vfxGenerator;
    private Collider2D col2D_wave;
    private bool isDead = false;
    private bool isDuringStartAnim = true;
    private AudioSource bgmBox;
    private MixerManager mixerManager;
    [SerializeField] GameObject boss_attack_Right;
    [SerializeField] GameObject boss_attack_Left;
    [SerializeField] GameObject boss_attack_Wave;
    [SerializeField] Boss_Stop_Col boss_stopCol;
    [SerializeField] Transform mainCamera;
    [SerializeField] GameObject boss_HpBar;

    [Header("各スプライト")]
    private SpriteRenderer spriteRenderer;
    [SerializeField] Sprite sprite_idle;
    [SerializeField] Sprite sprite_attack;
    [SerializeField] Sprite sprite_wave_up;
    [SerializeField] Sprite sprite_wave_down;

    private IEnumerator Boss_Double_Attack()
    {
        bool Boss_face_Right = false;
        boss_Move.enabled = false;
        boss_Direction.enabled = false;
        if (PlayerPosition.x > BossPosition.x)
        {
            Boss_face_Right = true;
        }
        else if (PlayerPosition.x < BossPosition.x)
        {
            Boss_face_Right = false;
        }
        boss_Status.SoundManager.Play("shockWaveAndDoublePunch_up");
        yield return new WaitForSeconds(timer_wait_Punch);
        if (Boss_face_Right)
        {
            boss_attack_Right.SetActive(true);
            boss_attack_Left.SetActive(false);
            vfxGenerator.GenerateEffect(0);
        }
        else if (!Boss_face_Right)
        {
            boss_attack_Right.SetActive(false);
            boss_attack_Left.SetActive(true);
            vfxGenerator.GenerateEffect(1);
        }
        spriteRenderer.sprite = sprite_attack;
        boss_Status.SoundManager.Play("doublePunch");
        yield return new WaitForSeconds(timer_first_short_wait);
        boss_attack_Right.SetActive(false);
        boss_attack_Left.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        if (Boss_face_Right && !Boss_Attack_Success_Right.IsSuccess)
        {
            boss_attack_Right.SetActive(true);
            boss_attack_Left.SetActive(false);
            vfxGenerator.GenerateEffect(2);
            boss_Status.SoundManager.Play("doublePunch");
            yield return new WaitForSeconds(timer_second_short_wait);
            Debug.Log("右");
        }
        else if (!Boss_face_Right && !Boss_Attack_Success_Left.IsSuccess)
        {
            boss_attack_Right.SetActive(false);
            boss_attack_Left.SetActive(true);
            vfxGenerator.GenerateEffect(3);
            boss_Status.SoundManager.Play("doublePunch");
            yield return new WaitForSeconds(timer_second_short_wait);
            Debug.Log("左");
        }
        else
        {
            yield return new WaitForSeconds(timer_Success_wait);
        }
        Debug.Log($"右{Boss_Attack_Success_Right.IsSuccess}");
        Debug.Log($"左{Boss_Attack_Success_Left.IsSuccess}");
        boss_attack_Right.SetActive(false);
        boss_attack_Left.SetActive(false);

        boss_Move.enabled = true;
        boss_Direction.enabled = true;
        Attack_Time = false;
        spriteRenderer.sprite = sprite_idle;

    }
    private IEnumerator Boss_Wave()
    {
        boss_attack_Wave.transform.localPosition = new Vector3(0f, -0.37f, 0f);
        boss_attack_Wave.SetActive(true);
        boss_Move.enabled = false;
        boss_Direction.enabled = false;
        Boss_Wave_Attack_Check.enabled = false;
        col2D_wave.enabled = false;
        spriteRenderer.sprite = sprite_wave_up;
        boss_Status.SoundManager.Play("shockWaveAndDoublePunch_up");
        for (int i = 0;i < count_attention;i++)
        {
            Boss_Attack_Attention.enabled = true;
            boss_attack_Wave.transform.localPosition = new Vector3(0f, -0.37f, 0f);
            yield return new WaitForSeconds(timer_wait_Wave);
            Boss_Attack_Attention.enabled = false;
            boss_attack_Wave.transform.localPosition = new Vector3(0f, -0.37f, 0f);
            yield return new WaitForSeconds(timer_wait_Wave);
        }
        boss_attack_Wave.transform.localPosition = new Vector3(0f, -0.37f, 0f);
        Boss_Wave_Attack_Check.enabled = true;
        col2D_wave.enabled = true;
        vfxGenerator.GenerateEffect(4);
        spriteRenderer.sprite = sprite_wave_down;
        boss_Status.SoundManager.Play("shockWave_down");
        boss_Status.SoundManager.Play("shockWave_attack");
        yield return new WaitForSeconds(0.1f);
        col2D_wave.enabled = false;
        yield return new WaitForSeconds(timer_harden_Wave);
        boss_attack_Wave.SetActive(false);
        boss_Move.enabled = true;
        boss_Direction.enabled = true;
        Attack_Time = false;
        spriteRenderer.sprite = sprite_idle;
    }
    private IEnumerator Boss_Rush()
    {
        boss_Move.enabled = false;
        boss_Status.SoundManager.Play("dush");
        yield return new WaitForSeconds(timer_Rush_wait);
        var Vec = PlayerPosition - BossPosition;
        RB2D.velocity = Vec.normalized * Rush_Speed;
        boss_Direction.enabled = false;

        // 壁にぶつかるまで待つ
        yield return new WaitUntil(() => boss_stopCol.IsCollisionStay);
        boss_Status.SoundManager.Play("colWithWall");
        vfxGenerator.GenerateEffect(5);
        RB2D.velocity = Vector2.zero;

        yield return new WaitForSeconds(timer_Rush_harden);
        boss_Move.enabled = true;
        boss_Direction.enabled = true;
        Attack_Time = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        PlayerPosition = playerObject.transform.position;
        BossPosition = transform.position;

        Attack_Time = false;

        boss_Move = GetComponent<Boss_Move>();
        boss_Move.enabled = true;

        boss_Direction = GetComponent<Boss_Direction>();
        boss_Direction.enabled = true;

        boss_attack_Right.SetActive(false);
        boss_attack_Left.SetActive(false);
        boss_attack_Wave.SetActive(false);
        Boss_Attack_Success_Right = boss_attack_Right.GetComponent<Enemy_Attack>();
        Boss_Attack_Success_Left = boss_attack_Left.GetComponent<Enemy_Attack>();
        Boss_Attack_Attention = boss_attack_Wave.GetComponent<SpriteRenderer>();
        Boss_Wave_Attack_Check = boss_attack_Wave.GetComponent<Enemy_Attack>();
        Player_recovery = playerObject.GetComponent<PlayerController>();
        Boss_Attack_Attention.enabled = false;
        Boss_Wave_Attack_Check.enabled = false;
        boss_Status = GetComponent<EnemyStatus>();
        RB2D = GetComponent<Rigidbody2D>();
        vfxGenerator = GetComponent<VfxGenerator>();
        col2D_wave = boss_attack_Wave.GetComponent<Collider2D>();
        mixerManager = GetComponent<MixerManager>();

        bgmBox = GameObject.Find("BGMBox").GetComponent<AudioSource>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(StartAnim());
    }

    private IEnumerator StartAnim()
    {
        // 環境音を下げる
        mixerManager.ChangeSpecificVolume("EnvVol", 0.2f);

        // プレイヤーの動きを止める
        Player_recovery.IsControlable = false;

        // カメラを移動させる
        var pCameraPos = mainCamera.transform.position;
        for(int i = 1; i <= 120; i++)
        {
            float t = Mathf.Sin(i * 1f / 120f * Mathf.PI * 0.5f);
            mainCamera.transform.position = Vector3.Lerp(pCameraPos, new Vector3(4.048f, 0.1019974f, -10f), t);
            yield return new WaitForSeconds(2f / 120f);
        }

        // 瓦礫を落とす
        vfxGenerator.GenerateEffect(-1);
        yield return new WaitForSeconds(2f);

        // 敵を落下させる
        for (int i = 1; i <= 60; i++)
        {
            float t = i * 1f / 60f;
            transform.position = Vector3.Lerp(new Vector3(8.32f, 15f, 0f), new Vector3(8.32f, 0.8f, 0f), t);
            yield return new WaitForSeconds(0.3f / 60f);
        }
        boss_Status.SoundManager.Play("shockWave_down");
        vfxGenerator.GenerateEffect(-2);

        // カメラを揺らす
        var bossCameraPos = mainCamera.transform.position;
        for (int i = 1; i <= 60; i++)
        {
            float t = Mathf.Sin(i * 1f / 60f * Mathf.PI * 4f);
            mainCamera.transform.position = bossCameraPos + Vector3.right * t * 1.1f * (60 - i) / 60f;
            yield return new WaitForSeconds(1f / 60f);
        }

        yield return new WaitForSeconds(1f);

        // カメラを戻す
        bgmBox.Play();
        for (int i = 1; i <= 60; i++)
        {
            float t = 1f - i * 1f / 60f;
            mainCamera.transform.position = Vector3.Lerp(playerObject.transform.position + new Vector3(0f, 0f, -7.142857f), new Vector3(4.048f, 0.1019974f, -10f), t);
            yield return new WaitForSeconds(1.5f / 60f);
        }

        // 環境音を戻す
        mixerManager.ChangeSpecificVolume("EnvVol", 1f);

        // HPバーを出す
        boss_HpBar.SetActive(true);

        // 戦闘を開始する
        Player_recovery.IsControlable = true;
        isDuringStartAnim = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 出現アニメーション中なら、または死亡しているなら抜ける
        if (isDuringStartAnim || isDead)
        {
            boss_Move.enabled = false;
            return;
        }

        // 死亡処理
        if (boss_Status.Hp <= 0 && !isDead)
        {
            isDead = true;

            // 音楽を止める
            bgmBox.Stop();

            // プレイヤーの動きを止める
            Player_recovery.IsControlable = false;
            global::PlayerStatus.Hp = global::PlayerStatus.MaxHp;

            return;
        }

        if (!Attack_Time) 
        {
            boss_Move.enabled = true;
        }
        

        PlayerPosition = playerObject.transform.position;
        BossPosition = transform.position;
        var angle = Vector3.Angle(PlayerPosition - BossPosition, new Vector3(-1f, 0f, 0f));
        if (!Attack_Time && ((0 <= angle  && angle <= 90 - unnecessary_angle_half) || (90 + unnecessary_angle_half <= angle && angle <= 180))
            && Player_recovery.IsUsingHealSkill && boss_Status.HpRate <= 0.5)
        {
            Attack_Time = true;
            StartCoroutine(Boss_Rush());
        }
        if ((distance >= Vector3.Distance(PlayerPosition, BossPosition)) && !Attack_Time)
        {
            if (distance_Y >= Mathf.Abs(BossPosition.y - PlayerPosition.y))
            {
                Attack_Time = true;
                StartCoroutine(Boss_Double_Attack());
            }
            else
            {
                Attack_Time = true;
                StartCoroutine(Boss_Wave());
            }
        }
    }
}