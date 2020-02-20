<?php

require 'ConnectionSettings.php';

//User submited variables
$userID = $_POST["userID"];
 
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
echo "Connected successfully <br>";
 

$sql = "SELECT billID FROM bill_user WHERE userID = '".$userID."'";


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