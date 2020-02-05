using UnityEngine;

public class UserInfo : MonoBehaviour
{

    public string UserID {get;private set;} //solo set private
    string UserName;
    string UserPassword;

    //Toda la informacion del user
    public void SetCredentials(string username, string userpassword){
        UserName = username;
        UserPassword = userpassword; 
    }
    public void SetID(string id){
        UserID = id;
    }



}
