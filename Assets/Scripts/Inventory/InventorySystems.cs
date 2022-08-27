using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystems : MonoBehaviour
{
    public static InventorySystems Instance;
    public List<ItemData> items;

    void Start()
    {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(this);
        }
    }

    public void storeData(ItemData data){
        if(items.Contains(data)){
            items[items.IndexOf(data)].stack++;
            return; 
        }
        items.Add(data);
    }

    public void deleteData(ItemData data){
        if(!items.Contains(data)) return;

        if(items[items.IndexOf(data)].stack > 1){
            items[items.IndexOf(data)].stack--;
            return;
        }
        
        deleteAllData(data);
    }

    public void deleteAllData(ItemData data){
        items.Remove(data);
    }
}
