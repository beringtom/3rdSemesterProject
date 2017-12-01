<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Fraværssystem - Rediger Bruger</title>
</head>
<body>

<?php
$getresturi = "";

$content = file_get_contents($getresturi);

$decodedContent[] = json_decode($content);
?>
[{"FK_RolesId":3,
"FK_TeamId":1,
"Person_Email":"kal@edu.easj.dk",
"Person_FirstName":"Kal",
"Person_Id":2,
"Person_LastName":"Bo",
"Person_StudentId":"585485",
"Roles_Name":"Studerende",
"Team_Name":"ro16da2b3-3b"}]

<table>
    <tr>
        <td colspan="2"><h2>Rediger Bruger</h2></td>
    </tr>
    <form action="redigerbruger.php">
        <tr>
            <td>Fornavn:</td>
            <td><input type="text" name="firstname" value="<?php echo $decodedContent[4] ?>"></td>
        </tr>
        <tr>
            <td>Efternavn:</td>
            <td><input type="text" name="lastname" value="<?php echo $decodedContent[6] ?>"></td>
        </tr>
        <tr>
            <td>Email:</td>
            <td><input type="text" name="email" value="<?php echo $decodedContent[3] ?>"></td>
        </tr>
        <tr>
            <td>Brugernavn:</td>
            <td><input type="text" name="username" value="<?php echo $decodedContent[3] ?>"></td>
        </tr>
        <tr>
            <td>Password:</td>
            <td><input type="text" name="password" value="<?php echo $decodedContent[4] ?>"></td>
        </tr>
        <tr>
            <td>Student ID:</td>
            <td><input type="text" name="studentid" value="<?php echo $decodedContent[5] ?>"></td>
        </tr>
        <tr>
            <td>Rolle:</td>
            <td><select name="rolle">
                    <option<?php if($decodedContent[6] == 0){ echo "selected";} ?> value="0">--None--</option>
                    <option<?php if($decodedContent[6] == 1){ echo "selected";} ?> value="1">Studerende</option>
                    <option<?php if($decodedContent[6] == 2){ echo "selected";} ?> value="2">Underviser</option>
                    <option<?php if($decodedContent[6] == 3){ echo "selected";} ?> value="3">Uddannelses Leder</option>
                </select>
            </td>
        </tr>
        <tr>
            <td></td>
            <td><input type="submit" value="Submit" style="float:right;"></td>
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
    $teamid = 0;

    $data = array("FirstName" => $fname, "LastName" => $lname, "Email" => $email, "UserName" => $username, "Password" => $passw, "Rolle" => $rolle, "StidentID" => $studentid, "TeamID" => $teamid);
    $json_string = json_encode($data);


    $uri = "";
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