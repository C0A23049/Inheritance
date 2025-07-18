using UnityEngine;


public class Guardman_Status : EnemyStatus
{
    private GameObject playerObject; // �v���C���[�I�u�W�F�N�g
    private Vector3 PlayerPosition; // �v���C���[�̈ʒu���
    private Vector3 EnemyPosition; // �G�̈ʒu���
    // Start is called before the first frame update

    // Update is called once per frame
    public override void TakeDamage (int amount, Object attackobj)
    {
        playerObject = GameObject.Find("Player");
        PlayerPosition = playerObject.transform.position;
        EnemyPosition = transform.position;
        if (PlayerPosition.x > EnemyPosition.x)
        {
            base.TakeDamage(amount, attackobj);
        }
        else
        {
            Debug.Log("guard");
            SoundManager.Play("guard");
        }
    }
}
