
using AngoraUtility;
using UnityEngine;

public class Distance_measurement : MonoBehaviour
{
    [SerializeField] private float circle_distance = 3;
    private float Distance_Percent;   // 直前のパーセント
    private float percent;    // 最新のパーセント
    private GameObject playerObject; // プレイヤーオブジェクト
    private float pl_distance;
    private bool in_out;
    protected CircleCollider2D col2D;
    private float col;
    private float re_pl_dis;
    private DistanceAttenuation DA;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        percent = 1;
        Distance_Percent = percent;
        pl_distance = Vector3.Distance(transform.position, playerObject.transform.position);
        in_out = false;
        col2D = GetComponent<CircleCollider2D>();
        col = col2D.radius;
        re_pl_dis = Mathf.Clamp(pl_distance, col, circle_distance);
        DA = GetComponent<DistanceAttenuation>();
    }

    // Update is called once per frame
    void Update()
    {
        pl_distance = Vector3.Distance(transform.position, playerObject.transform.position);
        if (pl_distance <= circle_distance)
        {
            in_out = true;
            re_pl_dis = Mathf.Clamp(pl_distance, col, circle_distance);
            percent = Mathf.InverseLerp(col, circle_distance, re_pl_dis);
            if (Distance_Percent != percent)
            {
                Distance_Percent = percent;
                Debug.Log(Distance_Percent);
            }

        }
        else
        {
            in_out = false;
        }
        DA.UpdateAttenuationRate(1 - Distance_Percent);
    }
    private void OnDrawGizmos()
    {
        var displayStatusStr = in_out ? "in" : "out";
        UnityEditor.Handles.Label(transform.position + new Vector3(0.2f, 1.2f), displayStatusStr);
        if (in_out)
        {
            Gizmos.color = Color.red;
            ExGizmos.DrawWireCircle2D(transform.position, circle_distance, 100);
        }
        else
        {
            Gizmos.color = Color.green;
            ExGizmos.DrawWireCircle2D(transform.position, circle_distance, 100);
        }
    }
}
