<?php
require 'ConnectionSettings.php';

//variables submited by user
//$loginUser = $_POST["loginUser"];
//$loginPass = $_POST["loginPass"];
$itemID = $_POST["itemID"];

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
echo "Connected successfully <br>";


$sql = "SELECT billname,amount,retencion,expirationdate,emissiondate,recievedAmount,deliveredAmount,days,TCEA
         FROM bill WHERE id = '".$itemID."'";


$result = $conn->query($sql);

if ($result->num_rows > 0) {
    $rows  = array();
    // output data of each row
    while($row = $result->fetch_assoc()) {
        $rows[] = $row;
    }
    //after the whole array is created
    echo json_encode($rows);
} else {
    echo "0 results";
}
$conn->close();
?>