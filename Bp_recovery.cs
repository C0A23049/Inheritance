
using UnityEngine;

public class Bp_recovery : MonoBehaviour
{
    [Header("ÇaÇoÇÃëùâ¡ó ")]
    [SerializeField] protected int addSp = 10;
    protected ObjectPool objectPool_blood;
    protected CircleCollider2D col2D;
    void Start()
    {

        col2D = GetComponent<CircleCollider2D>();
        objectPool_blood = GameObject.Find("ObjectPool__Vfx_Blood").GetComponent<ObjectPool>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ìGÇ∆è’ìÀÇµÇΩÇÁ
        if (collision.gameObject.TryGetComponent<PlayerController>(out var pc))
        {
            var _blood = objectPool_blood.Generate(transform.position);
            _blood.GetComponent<VfxBlood>().SetAddSp(addSp);
            Destroy(gameObject);
        }

    }
}
