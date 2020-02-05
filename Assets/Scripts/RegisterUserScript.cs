using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterUserScript : MonoBehaviour
{

    public InputField UsernameInput;
    public InputField PasswordInput;
    public InputField ConfirmPasswordInput;
    public Button RegisterButton;
    //public UIScripts UIScripts;

    public GameObject PanelErrorConfirmPass;

    // Start is called before the first frame update
    void Start()
    {
        //UIScripts = GetComponent<UIScripts>();
        RegisterButton.onClick.AddListener(()=>{
            if(PasswordInput.text == ConfirmPasswordInput.text){
                    StartCoroutine(MainScript.Instance.WebService.RegisterUser(UsernameInput.text,PasswordInput.text));
            }else
            {
                    Debug.Log("Confirm password again"); //Añadir mensaje de error confirmar contraseña
                    PanelErrorConfirmPass.SetActive(true);
            }

        });

    }

    public void VerifyInputs(){
        RegisterButton.interactable = (UsernameInput.text.Length >= 2 && PasswordInput.text.Length >=2);  
    }
}
