using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public Sprite newSprite;
    public void change(){
        GetComponent<SpriteRenderer>().sprite = newSprite;
    }
}
