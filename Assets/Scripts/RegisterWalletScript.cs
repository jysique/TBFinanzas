using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterWalletScript : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField NameWalletInput; //string
    public InputField DayDiscountDateInput; //string
    public InputField MonthDiscountDateInput; //string
    public InputField YearDiscountDateInput; //string

    public Dropdown CapitalizacionInput;

//=============================================0
    public InputField InitialCostsInput; //string
    public InputField FinalCostsInput; //string


//=============================================0
    public InputField Rate;
    public Dropdown DaysPerDaysInput;
    public Dropdown RateTypeInput;
    public Dropdown RateTermInput;

    public Button EditWalletButtom;

    string userID;

    double TEA;
    double daysPerYear;

    void Start()
    {
        userID = MainScript.Instance.UserInfo.UserID;
        EditWalletButtom.onClick.AddListener(()=>{
            TEA = CalculateTEA();
            daysPerYear = CalculateDaysPerDays();
            StartCoroutine(MainScript.Instance.WebService.UpdateWallet(userID, NameWalletInput.text, InitialCostsInput.text, FinalCostsInput.text,
                        YearDiscountDateInput.text,MonthDiscountDateInput.text, DayDiscountDateInput.text,
                        TEA.ToString(),daysPerYear.ToString()));
        });
    }
    void Update(){
        if (VerifyNominal() == true){
            CapitalizacionInput.interactable = true;
        }else
        {
            CapitalizacionInput.interactable = false;
        }

    }
    bool VerifyNominal(){
        if (RateTypeInput.value==0)
        {
            return true;
        }else
        {
            return false;
        }
    }
    public double CalculateDaysPerDays(){
        if (DaysPerDaysInput.value == 0)
        {
            daysPerYear = 360;
        }else
        {
            daysPerYear = 365;
        }
        return daysPerYear;
    }
    //Dias en funcion de la tasa a convertir
    public double CalculateRateTerm(){
        double rateTerm = 0;
        if (RateTermInput.value == 0)
        {
            rateTerm = 360;
        }else if(RateTermInput.value == 1)
        {
            rateTerm = 180;
        }else if(RateTermInput.value == 2)
        {
            rateTerm = 120;
        }else if(RateTermInput.value == 3)
        {
            rateTerm = 90;
        }else if(RateTermInput.value == 4)
        {
            rateTerm = 60;
        }else if(RateTermInput.value == 5)
        {
            rateTerm = 30;
        }
        return rateTerm;
    }
    public double CalculateCapitalizacion(){
        double cap = 0;
        if (CapitalizacionInput.value == 0)
        {
            cap = 1;
        }else if(CapitalizacionInput.value == 1)
        {
            cap = 15;
        }else if(CapitalizacionInput.value == 2)
        {
            cap = 30;
        }else if(CapitalizacionInput.value == 3)
        {
            cap = 60;
        }else if(CapitalizacionInput.value == 4)
        {
            cap = 90;
        }else if(CapitalizacionInput.value == 5)
        {
            cap = 120;
        }else if(CapitalizacionInput.value == 6)
        {
            cap = 180;
        }else if(CapitalizacionInput.value == 8)
        {
            cap = 360;
        }
        return cap;
    }



    public double CalculateRateNominalToEfective(double rate, double timeNominal,double timeEfecive,double cap){
        double TE = 0;
        double m = timeNominal/cap;
        double n = timeEfecive/cap;
        TE = System.Math.Pow(1+(rate/100)/m,n)-1;
        //Debug.Log("TEA: "+TE); //retorna TE en decimales
        return TE;
    }

    public double CalculateRateEfectiveToEfective(double rate, double timeTE_1, double timeTE_2){
        double TE = 0;
        TE = System.Math.Pow(1+ rate/100,timeTE_2/timeTE_1)-1;
        //Debug.Log("TEA: "+TE); //retorna TE en decimales
        return TE;
    }

    public double CalculateTEA(){
        double TEA = 0;
        double rate = double.Parse(Rate.text);
        double cap = CalculateCapitalizacion(); // capitalizacion
        double rateTerm = CalculateRateTerm();
        double dPY = CalculateDaysPerDays();
        
        if (RateTypeInput.value == 0)
        {
            TEA = CalculateRateNominalToEfective(rate,rateTerm,dPY,cap);
        }else{
            TEA = CalculateRateEfectiveToEfective(rate,rateTerm,daysPerYear);
        }
        return TEA;
    }

    public void SetInputs(){
        NameWalletInput.text = "";
        InitialCostsInput.text = "";
        FinalCostsInput.text = "";
        DayDiscountDateInput.text ="";
        MonthDiscountDateInput.text = "";
        YearDiscountDateInput.text = "";
        DayDiscountDateInput.text= "";
        RateTypeInput.value = 0;
        RateTermInput.value = 0;
    }
    
}
