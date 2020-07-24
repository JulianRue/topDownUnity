using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerTakesItAll : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("player"))
        {
            Destroy(gameObject);
            GlobalVar.instance.levelUp();
        }
    }
}
