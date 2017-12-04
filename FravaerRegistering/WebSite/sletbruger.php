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
    <title>Fraværssystem - Slet bruger</title>
</head>
<body>

<?php
$getresturi = "http://restfravaerservice.azurewebsites.net/service1.svc/Person/";

$content = file_get_contents($getresturi);

$decodedContent = json_decode($content);
//print_r($decodedContent);
?>


<table>
    <tr>
        <td colspan="2"><h2>Slet Bruger</h2></td>
    </tr>
    <form method="post">
        <tr>
            <td></td>
            <td>ID:</td>
            <td>Fornavn:</td>
            <td>Efternavn:</td>
        </tr>
<?php
    foreach ($decodedContent as $row) {
        echo '<tr><td><input type="checkbox" name="sletuser[]" value="'.$row->rid.'"></td><td>';
        echo $row->studentid;
        echo '</td><td>';
        echo $row->firstname;
        echo '</td><td>';
        echo $row->lastname;
        echo '</td></tr>';
    }
?>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td><input type="submit" name="submit" value="Submit" style="float:right;"></td>
        </tr>
    </form>
</table>
</body>
</html>

<?php
require ('Funktion/sletPersonFunktion.php');
if(isset ($_POST['submit'])) {
    foreach ($_POST['sletuser'] as $rid) {
        echo($rid);
        slet("http://restfravaerservice.azurewebsites.net/service1.svc/person/" . $rid);
    }
    header("Location: ".$_SERVER['REQUEST_URI']);
}
?>