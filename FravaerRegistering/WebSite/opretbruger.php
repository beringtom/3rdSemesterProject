<?php
/**
 * Created by PhpStorm.
 * User: BeringTom
 * Date: 28-11-2017
 * Time: 09:54
 */

?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Fraværssystem - Opret Bruger</title>
</head>
<body>
<table>
    <tr>
        <td colspan="2"><h2>Opret Bruger</h2></td>
    </tr>
    <form action="#" method="post">
        <tr>
            <td>Fornavn:</td>
            <td><input type="text" name="firstname" placeholder="First Name"></td>
        </tr>
        <tr>
            <td>Efternavn:</td>
            <td><input type="text" name="lastname" placeholder="Last Name"></td>
        </tr>
        <tr>
            <td>Email:</td>
            <td><input type="text" name="email" placeholder="username@edu.easj.dk"></td>
        </tr>
        <tr>
            <td>Brugernavn:</td>
            <td><input type="text" name="username" placeholder="Username"></td>
        </tr>
        <tr>
            <td>Password:</td>
            <td><input type="text" name="password" placeholder="eg. password"></td>
        </tr>
        <tr>
            <td>Student ID:</td>
            <td><input type="text" name="studentid" placeholder="eg. 48484848"></td>
        </tr>
        <tr>
            <td>Rolle:</td>
            <td><select name="rolle">
                    <option value="1">Studerende</option>
                    <option value="2">Underviser</option>
                    <option value="3">Uddannelses Leder</option>
                </select>
            </td>
        </tr>
        <tr>
            <td></td>
            <td><input type="submit" value="Submit" name="submit" style="float:right;"></td>
        </tr>
    </form>
</table>





<?php
if(isset($_POST['submit']))
{

    $fname = $_POST['firstname'];
    $lname = $_POST['lastname'];
    $email = $_POST['email'];
    $username = $_POST['username'];
    $passw = $_POST['password'];
    $rolle = $_POST['rolle'];
    $studentid = $_POST['studentid'];
    $teamid = 1;

    $data = array("fname" => $fname, "lname" => $lname, "email" => $email, "username" => $username, "password" => $passw, "roles" => $rolle, "studentid" => $studentid, "teamid" => $teamid);
    $json_string = json_encode($data);

    print_r($json_string);
    $uri = "http://restfravaerservice.azurewebsites.net/service1.svc/Person/";
    $ch = curl_init($uri);


// curl is good for more complex operations than just plain GET
    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
    curl_setopt($ch, CURLOPT_POSTFIELDS, $json_string);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
// TRUE to return the transfer as a string of the return value of curl_exec() instead of outputting it directly.
    curl_setopt($ch, CURLOPT_HTTPHEADER, array(
            'Content-Type: application/json',
            'Content-Length: ' . strlen($json_string))
    );


    $jsondata = curl_exec($ch);
    $newuser = json_decode($jsondata, true);
}
?>
</body>
</html>
