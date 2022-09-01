using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddGameObjectFile : MonoBehaviour
{
    public void addSprite(GameObject parent, Sprite image, string name){
        GameObject objects = new GameObject(name);
        objects.transform.SetParent(parent.transform, false);
        objects.AddComponent<Image>().sprite = image;
        addPoint(objects);
    }

    public void addPoint(GameObject parent){
        GameObject points = new GameObject("points");
        points.transform.SetParent(parent.transform, false);
        Vector3 pos = new Vector3(0f, (parent.transform.GetComponent<RectTransform>().rect.height), 0f);
        points.transform.Translate(pos);
    }
}
