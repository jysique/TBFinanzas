<?php
require 'ConnectionSettings.php';

$loginID = $_POST["loginID"];
//===============================
$loginNameWallet = $_POST["loginNameWallet"];
//===============================
$loginInitialCosts = $_POST["InitialCost"];
$loginFinalCosts = $_POST["FinalCost"];
//===============================
$loginYearDisc = $_POST["YearDisc"];
$loginMonthDisc = $_POST["MonthDisc"];
$loginDayDisc = $_POST["DayDisc"];
//===============================
$TEA = $_POST["TEA"];
$daysPerYears = $_POST["daysPerYear"];


// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
echo "Connected successfully <br>";

$sql = "UPDATE user SET name_wallet = '".$loginNameWallet."' ,                    
                        initialCosts = '".$loginInitialCosts."' ,
                        finalCosts = '".$loginFinalCosts."' ,
                        disccount_date = '20{$loginYearDisc}-{$loginMonthDisc}-{$loginDayDisc}',
                        TEA = '".$TEA."',
                        daysPerYear = '".$daysPerYears."'
WHERE id = '".$loginID."' ";

$result = $conn->query($sql);


if ($conn->query($sql) === TRUE) {
    echo "Wallet edit succesfully";
} else {
    echo "Error: " . $sql . "<br>" . $conn->error;
}
$conn->close();
?>