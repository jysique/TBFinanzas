using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BillRegister : MonoBehaviour
{
    public InputField CodeBillInput; //string
    public InputField AmountInput; // double
    public InputField RetencionInput;// string
    public InputField DayExpirationDateInput;
    public InputField MonthExpirationDateInput;
    public InputField YearExpirationDateInput;

    public InputField DayEmissionDateInput;
    public InputField MonthEmissionDateInput;
    public InputField YearEmissionDateInput;
    public Dropdown MonetaryUnit;
    public InputField TypeExchange;
    public Button RegisterBillButtom;

    string auxAmount;
    string auxRetencion;
    string userID;
    double recievedAmount;
    double deliveredAmount;
    double TCEA;

    string rateDiscountString{get;set;}
    string daysPerYear{get;set;}
//    double days=180;

    RegisterWalletScript oper;    
     void Start()
    {
        userID = MainScript.Instance.UserInfo.UserID;
        oper = GetComponent<RegisterWalletScript>();
        RegisterBillButtom.onClick.AddListener(()=>{    
            if (VerifySoles() == true)
            {
                auxAmount = AmountInput.text;
                auxRetencion = RetencionInput.text;
            }else
            {
                auxAmount = ConvertDollarSolesString(AmountInput.text);
                auxRetencion = ConvertDollarSolesString(RetencionInput.text);
            }

            StartCoroutine(MainScript.Instance.WebService.RegisterBill(userID,CodeBillInput.text,auxAmount,auxRetencion,
                        YearExpirationDateInput.text,MonthExpirationDateInput.text, DayExpirationDateInput.text,
                        YearEmissionDateInput.text,MonthEmissionDateInput.text, DayEmissionDateInput.text
                        ));
        });
    }
    void Update(){
        if (VerifySoles() == true){
            TypeExchange.interactable = false;
            
        }else
        {

            TypeExchange.interactable = true;
        }
    }

    public void VerifyInputs(){
        RegisterBillButtom.interactable = (CodeBillInput.text.Length >= 2 && 
                                            AmountInput.text.Length >=2 &&
                                            RetencionInput.text.Length >=2 &&
                                            DayExpirationDateInput.text.Length ==2 &&
                                            MonthExpirationDateInput.text.Length ==2 &&
                                            YearExpirationDateInput.text.Length ==2 );  
    }
    public void SetInputs(){
        CodeBillInput.text = " ";
        AmountInput.text = "";
        RetencionInput.text = "";
        DayExpirationDateInput.text ="";
        MonthExpirationDateInput.text = "";
        YearExpirationDateInput.text = "";
    }
    string ConvertDollarSolesString(string auxString){
        double auxReturn = double.Parse(auxString)*double.Parse(TypeExchange.text);
        return auxReturn.ToString();
    }
    bool VerifySoles(){
        if (MonetaryUnit.value==0)
        {
            return true;
        }else
        {
            return false;
        }
    }

}
