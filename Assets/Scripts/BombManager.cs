using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    List<GameObject> bombs = new List<GameObject>();
    public GameObject bombSprite;

    private int localCount;

    void Start(){
        GlobalVar.instance.currentBombs = GlobalVar.instance.maxBombs;
        localCount = GlobalVar.instance.currentBombs;
        for(int i = 0; i < GlobalVar.instance.maxBombs; i++){
            bombs.Add(Instantiate(bombSprite, new Vector3(-13.4f,3.5f - i,0f), Quaternion.identity, gameObject.transform));
        }
    }

    void FixedUpdate(){
        if(GlobalVar.instance.currentBombs != localCount){
            deleteLast();
            localCount--;
        }
    }
    public void deleteLast(){

        Destroy(bombs[GlobalVar.instance.currentBombs]);
        
        
    }


}
