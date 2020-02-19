using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField UsernameInput;
    public InputField PasswordInput;
    public Button LoginButton;

    // Start is called before the first frame update
    void Start()
    {
        LoginButton.onClick.AddListener(()=>{
                StartCoroutine(MainScript.Instance.WebService.Login(UsernameInput.text,PasswordInput.text));
                //LoginIn();
        });
    }

    public void VerifyInputs(){
        LoginButton.interactable = (UsernameInput.text.Length >= 2 && PasswordInput.text.Length >=2);  
    }

    public void LoginIn(){
        StartCoroutine(MainScript.Instance.WebService.Login(UsernameInput.text,PasswordInput.text));
    }

    public void SetInputs(){
        UsernameInput.text = "";
        PasswordInput.text = "";
    }

}
