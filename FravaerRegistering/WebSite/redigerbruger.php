<?php
session_start();
ob_start();
require("Funktion/hentHoldFunktion.php")
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
            <td><input type="text" name="firstname" value="<?php echo $decodedContent->firstname ?>"></td>
        </tr>
        <tr>
            <td>Efternavn:</td>
            <td><input type="text" name="lastname" value="<?php echo $decodedContent->lastname ?>"></td>
        </tr>
        <tr>
            <td>Email:</td>
            <td><input type="text" name="email" value="<?php echo $decodedContent->email ?>"></td>
        </tr>
        <tr>
            <td>Brugernavn:</td>
            <td><input type="text" name="username" value="<?php echo $decodedContent->username ?>"></td>
        </tr>
        <tr>
            <td>Password:</td>
            <td><input type="text" name="password" value="<?php echo $decodedContent->password ?>"></td>
        </tr>
        <tr>
            <td>Student ID:</td>
            <td><input type="text" name="studentid" value="<?php echo $decodedContent->studentid ?>"></td>
        </tr>
        <tr>
            <td>Klasse:</td>
            <td><select name="klasse">
                    <option value="0">-- None --</option>
                    <?php
                    $teamdata[] = getTeams();
                    foreach ($teamdata[0] as $tid) {
                        echo "<option value=".$tid->Team_Id;
                        if($tid->Team_Id == $decodedContent->fkteamid){echo(" selected ");}
                        echo ">".$tid->Team_Name."</option>";
                    } ?>
                </select>
            </td>
        </tr>
        <tr>
            <td>Rolle:</td>
            <td><select name="rolle">
                    <option<?php if($decodedContent->fkrolesid == 0){ echo " selected ";} ?> value="0">--None--</option>
                    <option<?php if($decodedContent->fkrolesid == 1){ echo " selected ";} ?> value="1">Studerende</option>
                    <option<?php if($decodedContent->fkrolesid == 2){ echo " selected ";} ?> value="2">Underviser</option>
                    <option<?php if($decodedContent->fkrolesid == 3){ echo " selected ";} ?> value="3">Uddannelses Leder</option>
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
    $teamid = $_POST['klasse'];

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


    //header("Location: ".$_SERVER['REQUEST_URI']);

}
?>
</body>

</html>