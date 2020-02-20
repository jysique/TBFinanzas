using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIScripts : MonoBehaviour
{
    public GameObject PanelRegisterBill;
    public GameObject PanelEditWallet;
    public GameObject PanelGestor;

    public GameObject PanelLogin;

    public GameObject PanelRegister;
    public GameObject PanelErrorMessage;

    public GameObject PanelErrorConfirmPass;

    public void GoToRegisterUser(){
        PanelLogin.gameObject.SetActive(false);
        PanelRegister.gameObject.SetActive(true);
    }

    public void HideErrorMessagePanel(){
        PanelErrorMessage.SetActive(false);
    }

    public void HideErrorConfirmPanel(){
        PanelErrorConfirmPass.SetActive(false);
    }

    public void RegisterUserBack(){
        PanelRegister.gameObject.SetActive(false);
        PanelLogin.gameObject.SetActive(true);
        
    }

    public void Error(){
        PanelErrorMessage.SetActive(true);
    }

    public void LoginEvent(){
        PanelLogin.gameObject.SetActive(false);
        PanelGestor.gameObject.SetActive(true);
    }


    public void ErrorConfirmPassword(){
        PanelErrorConfirmPass.SetActive(true);
    }

    public void GoToRegisterBill(){
        PanelGestor.gameObject.SetActive(false);
        PanelRegisterBill.gameObject.SetActive(true);
    }

    public void GoToEditWallet(){
        PanelGestor.gameObject.SetActive(false);
        PanelEditWallet.gameObject.SetActive(true);
    }

    public void RegisterBillToGestor(){
        PanelRegisterBill.gameObject.SetActive(false);
        PanelGestor.gameObject.SetActive(true);
        
    }
    public void EditWalletToGestor(){
        PanelEditWallet.gameObject.SetActive(false);
        PanelGestor.gameObject.SetActive(true);
    }

    public void BackSession(){
        PanelLogin.gameObject.SetActive(true);
        PanelGestor.gameObject.SetActive(false);
    }
    public void QuitApplication(){
        Application.Quit();
    }

}
