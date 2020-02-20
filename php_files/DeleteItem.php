<?php
require 'ConnectionSettings.php';

//variables submited by user
$itemID = $_POST["itemID"];
$userID = $_POST["userID"];


// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
//echo $itemID, " ", $userID ;


//$sql = "SELECT FROM bill_user WHERE billID = '".$itemID."' AND userID ='".$userID."'";

$sql = "DELETE FROM bill_user WHERE billID = ".$itemID." AND userID =".$userID." ";


$result = $conn->query($sql);
echo $sql;

if ($result->num_rows > 0) {
    //$sql2 = "DELETE FROM bill_user WHERE itemID = '".$itemID."'AND userID ='".$userID."'";
    //Already taken
    echo "Item delete Succesfully";
} else {
    echo "Error: couldn't delete items ";
}
$conn->close();
?>