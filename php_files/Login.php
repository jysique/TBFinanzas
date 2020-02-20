<?php
require 'ConnectionSettings.php';

//variables submited by user
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];


// Check connection
if ($conn->connect_error) {
    die("Error : Connection failed: " . $conn->connect_error);
}
//echo "Connected successfully <br>";


$sql = "SELECT password,id FROM user WHERE username = '".$loginUser."'";


$result = $conn->query($sql);

if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
       if($row["password"]== $loginPass){
            echo $row["id"];
       }
       else {
           echo "Error : Wrong Credentials";
       }
    }
} else {
    echo "Error : Username not exist";
}
$conn->close();
?>