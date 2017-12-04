<?php
session_start();
ob_start();
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Fraværssystem - Rediger Bruger</title>
</head>
<body>

<?php
$getresturi = "http://restfravaerservice.azurewebsites.net/service1.svc/Person/" . $_GET['id'];

$content = file_get_contents($getresturi);

$decodedContent = json_decode($content);
print_r($_SERVER['REQUEST_URI']);

?>


<table>
    <tr>
        <td colspan="2"><h2>Rediger Bruger</h2></td>
    </tr>
    <form method="post">
        <tr>
            <td>Fornavn:</td>
            <td><input type="text" name="firstname" value="<?php echo $decodedContent[0]->firstname ?>"></td>
        </tr>
        <tr>
            <td>Efternavn:</td>
            <td><input type="text" name="lastname" value="<?php echo $decodedContent[0]->lastname ?>"></td>
        </tr>
        <tr>
            <td>Email:</td>
            <td><input type="text" name="email" value="<?php echo $decodedContent[0]->email ?>"></td>
        </tr>
        <tr>
            <td>Brugernavn:</td>
            <td><input type="text" name="username" value="<?php echo $decodedContent[0]->username ?>"></td>
        </tr>
        <tr>
            <td>Password:</td>
            <td><input type="text" name="password" value="<?php echo $decodedContent[0]->password ?>"></td>
        </tr>
        <tr>
            <td>Student ID:</td>
            <td><input type="text" name="studentid" value="<?php echo $decodedContent[0]->studentid ?>"></td>
        </tr>
        <tr>
            <td>Rolle:</td>
            <td><select name="rolle">
                    <option<?php if($decodedContent[0]->fkrolesid == 0){ echo " selected ";} ?> value="0">--None--</option>
                    <option<?php if($decodedContent[0]->fkrolesid == 1){ echo " selected ";} ?> value="1">Studerende</option>
                    <option<?php if($decodedContent[0]->fkrolesid == 2){ echo " selected ";} ?> value="2">Underviser</option>
                    <option<?php if($decodedContent[0]->fkrolesid == 3){ echo " selected ";} ?> value="3">Uddannelses Leder</option>
                </select>
            </td>
        </tr>
        <tr>
            <td></td>
            <td><input type="submit" name="submit" value="Submit" style="float:right;"></td>
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


    $uri = "http://restfravaerservice.azurewebsites.net/service1.svc/Person/" . $_GET['id'];
    $ch = curl_init($uri);


// curl is good for more complex operations than just plain GET
    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "PUT");
    curl_setopt($ch, CURLOPT_POSTFIELDS, $json_string);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
// TRUE to return the transfer as a string of the return value of curl_exec() instead of outputting it directly.
    curl_setopt($ch, CURLOPT_HTTPHEADER, array(
            'Content-Type: application/json',
            'Content-Length: ' . strlen($json_string))
    );


    $jsondata = curl_exec($ch);
    $newuser = json_decode($jsondata, true);


    header("Location: ".$_SERVER['REQUEST_URI']);

}
?>
</body>

</html>