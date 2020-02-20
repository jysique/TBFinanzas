using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;
//using System.Coll7ections.Generic;

public class ItemsManager : MonoBehaviour
{
    
    //Callbacks
    
    Action<string> _createItemsCallback;



    double sumaRecieved;
    double sumaDelivered;

    public Text TextTotalRecieved;

    public Text TextTotalDelivered;
    public Text PTCEA;

    // Start is called before the first frame update
    void Start()
    {
        _createItemsCallback=(jsonArrayString)=>{
            StartCoroutine(CreateItemsRoutine(jsonArrayString));
            
        };
        CreateItems();
    }

    public void BeginAlgorithm(){
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
        List<double> arrayDelivered = new List<double>();
        List<double> arrayDays= new List<double>();

        //Parsin json array string as an array
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;
        sumaRecieved = 0;
        sumaDelivered = 0;
        
        for(int i = 0 ; i <jsonArray.Count ;i++){
            bool isDone = false;
            string itemID = jsonArray[i].AsObject["billID"];
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
            float recieved = itemInfoJson["recievedAmount"];
            float delivered = itemInfoJson["deliveredAmount"];
            float tcea = itemInfoJson["TCEA"];
            double diff_Day = itemInfoJson["days"];

            item.transform.Find("Name").GetComponent<Text>().text= itemInfoJson["billname"]; //
            item.transform.Find("Amount").GetComponent<Text>().text= itemInfoJson["amount"]; //
            item.transform.Find("EmissionDate").GetComponent<Text>().text= itemInfoJson["emissiondate"];
            item.transform.Find("ExpirationDate").GetComponent<Text>().text= itemInfoJson["expirationdate"]; //
            item.transform.Find("AmountReceived").GetComponent<Text>().text= System.Math.Round(recieved,2).ToString();
            item.transform.Find("AmountDelivered").GetComponent<Text>().text= System.Math.Round(delivered,2).ToString();
            item.transform.Find("TCEA").GetComponent<Text>().text= System.Math.Round(tcea*100,7).ToString();


            sumaRecieved = sumaRecieved + System.Math.Round(recieved,2);
            sumaDelivered = sumaDelivered + System.Math.Round(delivered,2);

            //Set Delete Button
            item.transform.Find("Delete").GetComponent<Button>().onClick.AddListener(()=>{
                string iID = itemID;
                string uID = MainScript.Instance.UserInfo.UserID;
                StartCoroutine(MainScript.Instance.WebService.DeleteItem(iID,uID));
                //Destroy(this.gameObject);
                //Debug.Log("Eliminando");
                DeleteItem(item);
            });

            TextTotalDelivered.GetComponent<Text>().text = "S/. " + sumaDelivered.ToString();
            TextTotalRecieved.text = "S/. " + sumaRecieved.ToString();
        
            
            arrayDelivered.Insert(i,delivered); // ENTREGADO
            arrayDays.Insert(i,diff_Day); // DAYS

        }

        double ptcea = System.Math.Round(CalculatePTCEA(arrayDelivered,arrayDays,sumaRecieved),7); 
        PTCEA.GetComponent<Text>().text = ptcea.ToString()+ " % ";
    }

    double CalculatePTCEA(List<double> array_entregado,List<double> array_time, double recibido_total){
        double lo = 0.0;
        double hi = 1.1;
        double mi = 0;
        double diff = 0.00000000001;

        while (hi - lo > diff)
        {
            mi = lo + (hi - lo) / 2;
            double aux_mi = 0;
           // Debug.Log("array 1: " + array_entregado.Count +" array 2: " +array_time.Count +" rcibidoTotal: "+ recibido_total);
            for (int i = 0; i < array_entregado.Count ; i++)
            {
                double a = 1 + mi;
                double b = array_time[i] / 360.0;
                double denominador = System.Math.Pow(a,b);
                aux_mi = aux_mi + (array_entregado[i] / denominador);

            }
            if (aux_mi> recibido_total)
            {
                lo = mi;
            }else
            {
                hi = mi;
            }
        }
      //  Debug.Log("TCEA de la cartera="+ mi*100 + "%");

        return mi*100;
    }



    void DeleteItem(GameObject itemAux){
        Destroy(itemAux);
    }
    //Funcion para actualizar los elementos
    public void UpdateElements(){
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Item")) {
            o.SetActive(false);
            //Destroy(this.gameObject);
        }
        _createItemsCallback=(jsonArrayString)=>{
            StartCoroutine(CreateItemsRoutine(jsonArrayString));
        };
        CreateItems();

    }
    public void DestroyAllElements(){
      //  Debug.Log("Elimando gameObject");
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Item")) {
            //o.SetActive(false);
            Destroy(o.gameObject);
        }
    }

}
