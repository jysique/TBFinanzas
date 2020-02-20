<?php
require 'ConnectionSettings.php';




// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
echo "Connected successfully <br>";

$userID = $_POST["userID"];
 
$sql = "SELECT name_wallet FROM user WHERE id = '".$userID."'";

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