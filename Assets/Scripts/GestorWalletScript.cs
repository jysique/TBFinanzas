using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;
public class GestorWalletScript : MonoBehaviour
{
    string userID;
    string nameWallet;
    Action<string> _createInfoCallback;

    public Text TextName; 

    // Start is called before the first frame update
    void Start()
    {
        
        userID = MainScript.Instance.UserInfo.UserID;
       // _createInfoCallback=(jsonArrayString)=>{
        //    StartCoroutine(GetName(jsonArrayString));
       // };
       // CreateItems();
    }

    // Update is called once per frame
    void Update()
    {
        _createInfoCallback=(jsonArrayString)=>{
            StartCoroutine(GetName(jsonArrayString));
        };
        CreateItems();
    }
    public void CreateItems()
    {
        //string userID = MainScript.Instance.UserInfo.UserID;
        StartCoroutine(MainScript.Instance.WebService.GetNameWallet(userID,_createInfoCallback)) ;
    }

    IEnumerator GetName(string jsonArrayString){
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;
        //Debug.Log(jsonArray);

        for(int i = 0 ; i <jsonArray.Count ;i++){
            nameWallet = jsonArray[i].AsObject["name_wallet"];
            TextName.text = nameWallet;
            yield return 0;
        }
    }
}
