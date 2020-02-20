<?php
require 'ConnectionSettings.php';


//variables submited by user
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
echo "Connected successfully <br>";


$sql1 = "SELECT username FROM user WHERE username = '".$loginUser."'";


$result = $conn->query($sql1);

if ($result->num_rows > 0) {
    
    //Already taken
    echo "Username is already taken sql1 ";
} else {
    echo "Create user....";
    //Inser user and pass to db
    $sql2 = "INSERT INTO user (username, password,name_wallet) VALUES ('".$loginUser."', '".$loginPass."','New wallet')";


    if ($conn->query($sql2) === TRUE) {
        echo "New record created successfully sql2 ";
// ================
        // HALLAR ID DEL USUARIO CREADO
        $sql3 = "SELECT `id` FROM `user` WHERE `username` = '".$loginUser."'";

        $result2 = $conn->query($sql3);
        if($result2->num_rows > 0){
            echo "Encontrado <br> sql3";
            while($row = $result2->fetch_assoc()) {
                $userID_temp = $row["id"];
                echo $userID_temp;
                $sql4 = "INSERT INTO `bill_user`(`userID`, `billID`) VALUES ($userID_temp,'1')";
                if ($conn->query($sql4) === TRUE) {
                    echo "Insertado exitosamente sql4";
                }else{
                    echo "Fallo sql 4";
                }
            }
        }else{
            echo "No Encontrado <br> sql 3";
        }



// ================
    } else {
        echo "Error: " . $sql2 . "<br>" . $conn->error;
    }

}
$conn->close();
?>