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
    public Dropdown MonetaryUnit;
    public InputField TypeExchange;
    public Button RegisterBillButtom;

     void Start()
    {
        
        RegisterBillButtom.onClick.AddListener(()=>{
           StartCoroutine(MainScript.Instance.WebService.RegisterBill(CodeBillInput.text,AmountInput.text,RetencionInput.text,
                            YearExpirationDateInput.text,MonthExpirationDateInput.text, DayExpirationDateInput.text));           
        });

    }
    void Update(){
        if (MonetaryUnit.value==0)
        {
            //Debug.Log("Soles");
            TypeExchange.interactable = false;
        }else
        {
            //Debug.Log("D");
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

    public void VerifyTypeExchange(){
        return;
    }

}
