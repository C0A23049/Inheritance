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
            // �o���A�j���[�V�������A�V�䂩��̊��I
            case -1:
                Instantiate(vfx_debris_ceil, new Vector3(8.27f, 4.35f, 0f), vfx_debris_ceil.transform.rotation);
                break;

            // �o���A�j���[�V�������A���n
            case -2:
                Instantiate(vfx_debris_ground, transform.position + new Vector3(0f, -0.37f, 0f), vfx_debris_ground.transform.rotation);
                break;

            // ����A�E�A�P���
            case 0:
                var obj0 = Instantiate(vfx_punchR_1, transform.position + new Vector3(0f, 0.55f, 0f), vfx_punchR_1.transform.rotation);
                obj0.transform.SetParent(transform);
                break;
            // ����A���A�P���
            case 1:
                var obj1 = Instantiate(vfx_punchL_1, transform.position + new Vector3(0f, 0.55f, 0f), vfx_punchR_1.transform.rotation);
                obj1.transform.SetParent(transform);
                break;
            // ����A�E�A�Q���
            case 2:
                var obj2 = Instantiate(vfx_punchR_2, transform.position + new Vector3(0f, 0.55f, 0f), vfx_punchR_2.transform.rotation);
                obj2.transform.SetParent(transform);
                break;
            // ����A���A�Q���
            case 3:
                var obj3 = Instantiate(vfx_punchL_2, transform.position + new Vector3(0f, 0.55f, 0f), vfx_punchL_2.transform.rotation);
                obj3.transform.SetParent(transform);
                break;
            // �Ռ��g
            case 4:
                Instantiate(vfx_debris_ground, transform.position + new Vector3(0f, -0.37f, 0f), vfx_debris_ground.transform.rotation);
                Instantiate(vfx_wave, transform.position + new Vector3(0f, -0.37f, 0f), vfx_wave.transform.rotation);
                break;
            // �ˌ�
            case 5:
                break;
        }
    }
}
