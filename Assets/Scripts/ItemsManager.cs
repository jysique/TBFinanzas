using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;

public class ItemsManager : MonoBehaviour
{
    
    //Callbacks

    Action<string> _createItemsCallback;


    // Start is called before the first frame update
    void Start()
    {
        
        _createItemsCallback=(jsonArrayString)=>{
            StartCoroutine(CreateItemsRoutine(jsonArrayString));
        };
        CreateItems();
    }

    public void CreateItems()
    {
        string userID = MainScript.Instance.UserInfo.UserID;
        StartCoroutine(MainScript.Instance.WebService.GetItemsIDs(userID,_createItemsCallback)) ;
        
    }

    IEnumerator CreateItemsRoutine(string jsonArrayString ){
        //Parsin json array string as an array

        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;

        for(int i = 0 ; i <jsonArray.Count ;i++){
            bool isDone = false;
            string itemID = jsonArray[i].AsObject["billID"];
            Debug.Log(itemID);

            JSONObject itemInfoJson = new JSONObject();
            
            //Create callback to get the information from WebService
            Action<string> getItemInfoCallback = (itemInfo)=>{
                isDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;
            };
            StartCoroutine(MainScript.Instance.WebService.GetItem(itemID,getItemInfoCallback));

            //Wait until the callback is called from webService (info finish downloading)
            yield return new WaitUntil(()=>isDone==true);

            

            // Instantiate GameObject (item prefab)
            GameObject item = Instantiate(Resources.Load("Prefabs/Item") as GameObject);
            item.transform.SetParent(this.transform);
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;
 
            //Fill Information
            item.transform.Find("Name").GetComponent<Text>().text= itemInfoJson["billname"];
            item.transform.Find("Amount").GetComponent<Text>().text= itemInfoJson["amount"];
            item.transform.Find("ExpirationDate").GetComponent<Text>().text= itemInfoJson["expirationdate"];
             
            //Set Delete Button
            item.transform.Find("Delete").GetComponent<Button>().onClick.AddListener(()=>{
                string iID = itemID;
                string uID = MainScript.Instance.UserInfo.UserID;
                StartCoroutine(MainScript.Instance.WebService.DeleteItem(iID,uID));
            });

        }
    }

}
