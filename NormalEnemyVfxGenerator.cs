using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyVfxGenerator : VfxGenerator
{
    [SerializeField] GameObject Attack_R;
    [SerializeField] GameObject Attack_L;

    public override void GenerateEffect(int id)
    {
        switch(id)
        {
            case 0:
                Instantiate(Attack_R, transform.position + new Vector3(0f, 0.55f, 0f), Attack_R.transform.rotation);
                break;
            case 1:
                Instantiate(Attack_L, transform.position + new Vector3(0f, 0.55f, 0f), Attack_L.transform.rotation);
                break;
        }
    }
}
