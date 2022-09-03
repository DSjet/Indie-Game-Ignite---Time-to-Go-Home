using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddGameObjectFile : MonoBehaviour
{
    public GameObject HPpref;
    public void addSprite(GameObject parent, Sprite image, string name){
        GameObject objects = new GameObject(name);
        objects.transform.SetParent(parent.transform, false);
        objects.AddComponent<Image>().sprite = image;
        Vector2 pos = new Vector2(300, 300);
        objects.GetComponent<Image>().rectTransform.sizeDelta = pos;
        addPoint(objects);
    }

    public void addPoint(GameObject parent){
        GameObject points = new GameObject("points");
        points.transform.SetParent(parent.transform, false);
        Vector3 pos = new Vector3(0f, (parent.transform.GetComponent<RectTransform>().rect.height), 0f);
        points.transform.Translate(pos);
    }

    public GameObject addHP(GameObject parent, CharacterSO so){
        GameObject objects = Instantiate(HPpref);
        objects.transform.SetParent(parent.transform, false);
        objects.GetComponent<Slider>().maxValue = so.MaxHP;
        objects.GetComponent<Slider>().value = so.MaxHP;
        return objects;
    }
}
