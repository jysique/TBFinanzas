<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "appfinance";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
echo "Connected successfully <br>";


$sql = "SELECT username, level FROM user";


$result = $conn->query($sql);

if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
        echo "username: " . $row["username"]. " - Level " . $row["level"]. "<br>";
    }
} else {
    echo "0 results";
}
$conn->close();
?>