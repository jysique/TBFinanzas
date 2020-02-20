<?php
require 'ConnectionSettings.php';


//variables submited by user


$userID = $_POST["loginID"];
//===============================
$loginCode = $_POST["loginCode"];
$Amount = $_POST["Amount"];
$Retencion = $_POST["Retencion"];
//===============================
$DayExp = $_POST["DayExp"]; #Day
$MonthExp = $_POST["MonthExp"]; #month
$YearExp = $_POST["YearExp"]; #year (2 digitos)
//===============================
$DayEmis = $_POST["DayEmis"]; #Day
$MonthEmis = $_POST["MonthEmis"]; #month
$YearEmis = $_POST["YearEmis"]; #year (2 digitos)
//==================================

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
echo "Connected successfully <br>";


$sql = "SELECT billname FROM bill WHERE billname = '".$loginCode."'";


$result = $conn->query($sql);

if ($result->num_rows > 0) {
    //Already taken
    echo "Bill name is already taken";
} else {
    //================================================
    // DAYS = fecha de descuento - fecha de vencimiento
    $discountDate= "0";
    $sqlDiscountDate = "SELECT `disccount_date` FROM `user` WHERE id ='".$userID."' ";
    $resultDiscountDate = $conn->query($sqlDiscountDate);
    if ($resultDiscountDate->num_rows > 0) {
        while($row = $resultDiscountDate->fetch_assoc()) {
            $discountDate = $row["disccount_date"];
            
        }
    }
    
    //================================================
    //DATE_DIFF("vencimiento,descuento ")
    $aux2 = date_create($discountDate);
    $aux3 = "20".$YearExp."-".$MonthExp."-".$DayExp."";
    echo $aux3." ";
    $aux4 = date_create($aux3);
    $interval = date_diff($aux2, $aux4);
    $tiempo = array();

    foreach ($interval as $valor) {
        $tiempo[] = $valor;
    }
    $days = $tiempo[11]; //Diferencias de dias
    //$daysString = (string)$days;
    //echo gettype($days);
    //echo gettype($daysString);
    //================================================
    //Hallar la TEA de user
    $TEA = 0;
    $sqlTEA = "SELECT `TEA` FROM `user` WHERE id ='".$userID."' ";
    $resultTEA = $conn->query($sqlTEA);
    if ($resultTEA->num_rows > 0) {
        while($row = $resultTEA->fetch_assoc()) {
            $TEA = $row["TEA"];
        }
    }
    //echo "TEA: ".$TEA. "<br>";
    //================================================
    $daysPerYear = 0;
    $sqlDaysPerYear = "SELECT `daysPerYear` FROM `user` WHERE id ='".$userID."' ";
    $resultDaysPerYear = $conn->query($sqlDaysPerYear);
    if ($resultDaysPerYear->num_rows > 0) {
        while($row = $resultDaysPerYear->fetch_assoc()) {
            $daysPerYear = $row["daysPerYear"];
        }
    }

    //================================================
    //TEA para convertirlo a TE(days)P
    $TEP = pow(1+$TEA,$days/$daysPerYear)-1;

    //================================================
    //d%
    $d = $TEP/(1+$TEP);

    //================================================
    //descuento
    $discount = $Amount * $d;

    //================================================
    //net amount 
    $netAmount = $Amount - $discount;

    //================================================
    //InitialCosts
    $initialCosts = 0;
    $sqlInitialCosts = "SELECT `initialCosts` FROM `user` WHERE id ='".$userID."' ";
    $resultInitialCosts = $conn->query($sqlInitialCosts);
    if ($resultInitialCosts->num_rows > 0) {
        while($row = $resultInitialCosts->fetch_assoc()) {
            $initialCosts = $row["initialCosts"];
        }
    }
    //FinalCost
    $finalCosts= 0;
    $sqlFinalCosts = "SELECT `finalCosts` FROM `user` WHERE id ='".$userID."' ";
    $resultFinalCosts = $conn->query($sqlFinalCosts);
    if ($resultFinalCosts->num_rows > 0) {
        while($row = $resultFinalCosts->fetch_assoc()) {
            $finalCosts = $row["finalCosts"];
        }
    }
    
    //================================================
    $recievedAmount = $netAmount - $initialCosts - $Retencion;
    $deliveredAmount = $Amount  + $finalCosts - $Retencion;

    $TCEA = pow($deliveredAmount/$recievedAmount,$daysPerYear/$days)-1;

    $sql2 = "INSERT INTO bill (billname,amount, retencion,expirationdate,emissiondate,recievedAmount,deliveredAmount,days,TCEA) 
        VALUES ('".$loginCode."', '".$Amount."','".$Retencion."',
                '20{$YearExp}-{$MonthExp}-{$DayExp}',
                '20{$YearEmis}-{$MonthEmis}-{$DayEmis}',
                '".$recievedAmount."', '".$deliveredAmount."','".$days."','".$TCEA."')";

    if ($conn->query($sql2) === TRUE) {
        echo "New record created successfully";
        // HALLAR ID DEL RECIBO CREADO
        $sql3 = "SELECT `id` FROM `bill` WHERE `billname` = '".$loginCode."'";

        $result2 = $conn->query($sql3);
        if($result2->num_rows > 0){
            echo "Encontrado <br>";
            while($row = $result2->fetch_assoc()) {
                $billID = $row["id"];
                echo $billID;
                $sql4 = "INSERT INTO `bill_user`(`userID`, `billID`) VALUES ($userID,$billID)";
                if ($conn->query($sql4) === TRUE) {
                    echo "Insertado exitosamente";
                }else{
                    echo "Fallo";
                }
            }
        }else{
            echo "No Encontrado <br>";
        }

    } else {
        echo "Error: " . $sql2 . "<br>" . $conn->error;
    }

}
$conn->close();
?>