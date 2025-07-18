using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Stop_Col : MonoBehaviour
{
    private bool isCollisionWithWall = false;
    public bool IsCollisionStay { get { return isCollisionWithWall; } }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isCollisionWithWall = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isCollisionWithWall = false;
    }
}
