using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class WebService : MonoBehaviour
{
    public UIScripts UIScripts;

    // Start is called before the first frame update
    void Start()
    {
        UIScripts = GetComponent<UIScripts>();
    }

    public void ShowUserItems(){ 
        //StartCoroutine(GetItemsIDs(MainScript.Instance.UserInfo.UserID),callback);
        //return;
    }


public IEnumerator GetUsers(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
               // Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
               // Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }

    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/AppFinance/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Unity: Error 1");
                Debug.Log(www.error);
            }
            else
            {  
                Debug.Log(www.downloadHandler.text);
                MainScript.Instance.UserInfo.SetID(www.downloadHandler.text);
                MainScript.Instance.UserInfo.SetCredentials(username,password);

                if (!www.downloadHandler.text.Contains("Error"))
                {
                    //Debug.Log("Unity: Todo correcto");
                    MainScript.Instance.PanelGestor.SetActive(true);
                    MainScript.Instance.Login.gameObject.SetActive(false);
                }else{
                    //Debug.Log("Unity: Error 2");
                    UIScripts.Error();
                }
                
                
            }
        }
    }

    
    public IEnumerator RegisterUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/AppFinance/RegisterUser.php", form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            { 
                //Debug.Log(www.downloadHandler.text);

            }
        };

    }    
    public IEnumerator RegisterBill(string id, string codename, string amount,string retencion, 
                                    string yearExp, string monthExp, string dayExp,
                                    string yearEmis, string monthEmis, string dayEmis)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginID", id);
        form.AddField("loginCode", codename);
        form.AddField("Amount", amount);
        form.AddField("Retencion", retencion);
        //==================================
        form.AddField("YearExp", yearExp);
        form.AddField("MonthExp", monthExp);
        form.AddField("DayExp", dayExp);
        //==================================
        form.AddField("YearEmis", yearEmis);
        form.AddField("MonthEmis", monthEmis);
        form.AddField("DayEmis", dayEmis);



        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/AppFinance/RegisterBill.php", form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
            }
        
        };

    }
public IEnumerator UpdateWallet(string id, string nameWallet, string initialCost,string finalCost, 
                                    string yearDisc, string monthDisc, string dayDisc,
                                    string TEA, string daysPerYear)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginID", id);
        form.AddField("loginNameWallet", nameWallet);
        //===============================
        form.AddField("InitialCost", initialCost);
        form.AddField("FinalCost", finalCost);
        //===============================
        form.AddField("YearDisc", yearDisc);
        form.AddField("MonthDisc", monthDisc);
        form.AddField("DayDisc", dayDisc);
        //===============================
        form.AddField("TEA", TEA);
        form.AddField("daysPerYear", daysPerYear);
        

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/AppFinance/UpdateWallet.php", form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
               // Debug.Log(www.downloadHandler.text);
            }
            

        };

    }

    

public IEnumerator GetItemsIDs(string userID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/AppFinance/GetItemIDs.php",form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Show results as a text 
                //Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;
                //Call callback function to pass results
                callback(jsonArray);
            }
        };
    }

public IEnumerator GetNameWallet(string userID,System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/AppFinance/GetNameWallet.php",form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Show results as a text 
                //Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;
                //Debug.Log(jsonArray);
                //Call callback function to pass results
                callback(jsonArray);

            }
        };
    }
/*
public IEnumerator GetDays(string userID, string yearExp, string monthExp, string dayExp,System.Action<string> callback)
    {
        string daysString = "";
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);
        form.AddField("YearExp", yearExp);
        form.AddField("MonthExp", monthExp);
        form.AddField("DayExp", dayExp);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/AppFinance/GetDays.php",form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Show results as a text 
                //Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;
                //Call callback function to pass results
                callback(jsonArray);

            }
        };
        yield return daysString;
    }
*/
public IEnumerator GetItem(string itemID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);


        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/AppFinance/GetItem.php",form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {

                //Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;
                //Call callback function to pass results
                callback(jsonArray);
            }
        };
    }
public IEnumerator DeleteItem(string itemID,string userID)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);
        form.AddField("userID", userID);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/AppFinance/DeleteItem.php",form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {

                //Debug.Log(www.downloadHandler.text);
                //string jsonArray = www.downloadHandler.text;
                //Call callback function to pass results
            }
        };
    }

}
