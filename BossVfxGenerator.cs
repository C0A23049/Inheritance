using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVfxGenerator : VfxGenerator
{
    [SerializeField] GameObject vfx_punchR_1, vfx_punchL_1, vfx_punchR_2, vfx_punchL_2;
    [SerializeField] GameObject vfx_wave, vfx_debris_ground;
    [SerializeField] GameObject vfx_debris_ceil;

    public override void GenerateEffect(int id)
    {
        switch (id)
        {
            // 出現アニメーション中、天井からの瓦礫
            case -1:
                Instantiate(vfx_debris_ceil, new Vector3(8.27f, 4.35f, 0f), vfx_debris_ceil.transform.rotation);
                break;

            // 出現アニメーション中、着地
            case -2:
                Instantiate(vfx_debris_ground, transform.position + new Vector3(0f, -0.37f, 0f), vfx_debris_ground.transform.rotation);
                break;

            // 殴り、右、１回目
            case 0:
                var obj0 = Instantiate(vfx_punchR_1, transform.position + new Vector3(0f, 0.55f, 0f), vfx_punchR_1.transform.rotation);
                obj0.transform.SetParent(transform);
                break;
            // 殴り、左、１回目
            case 1:
                var obj1 = Instantiate(vfx_punchL_1, transform.position + new Vector3(0f, 0.55f, 0f), vfx_punchR_1.transform.rotation);
                obj1.transform.SetParent(transform);
                break;
            // 殴り、右、２回目
            case 2:
                var obj2 = Instantiate(vfx_punchR_2, transform.position + new Vector3(0f, 0.55f, 0f), vfx_punchR_2.transform.rotation);
                obj2.transform.SetParent(transform);
                break;
            // 殴り、左、２回目
            case 3:
                var obj3 = Instantiate(vfx_punchL_2, transform.position + new Vector3(0f, 0.55f, 0f), vfx_punchL_2.transform.rotation);
                obj3.transform.SetParent(transform);
                break;
            // 衝撃波
            case 4:
                Instantiate(vfx_debris_ground, transform.position + new Vector3(0f, -0.37f, 0f), vfx_debris_ground.transform.rotation);
                Instantiate(vfx_wave, transform.position + new Vector3(0f, -0.37f, 0f), vfx_wave.transform.rotation);
                break;
            // 突撃
            case 5:
                break;
        }
    }
}
