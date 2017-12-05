<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Opret - Klasse</title>
</head>
<body>
    <table>
        <form method="post">
            <tr>
                <td>Hold navn:</td>
                <td><input type="text" name="holdnavn" placeholder="Holdnavn"></td>
            </tr>
            <tr>
                <td></td>
                <td><input type="submit" name="submit" value="Opret Hold" style="float:right;"></td>
            </tr>
        </form>
    </table>
</body>
</html>

<?php
require ('Funktion/opretHoldFunktion.php');
if(isset ($_POST['submit'])) {
        oprethold("http://restfravaerservice.azurewebsites.net/service1.svc/team/");
    //header("Location: ".$_SERVER['REQUEST_URI']);
}
?>