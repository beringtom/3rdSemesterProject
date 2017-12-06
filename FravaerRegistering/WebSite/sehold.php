<?php
/**
 * Created by PhpStorm.
 * User: Denderish
 * Date: 05/12/2017
 * Time: 12:17
 */
require ('Funktion/hentHoldFunktion.php');
require ('Funktion/seHoldFunktion.php');
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Fraværssystem - Se hold</title>
</head>
<body>

<table>
    <form method="post">
    <tr>
        <td colspan="2"><h2>Vælg Hold</h2></td>
    </tr>
    <tr>
        <td>Klasse:</td>
        <td><select name="klasse">
                <option value="0">-- None --</option>
                <?php
                $teamdata[] = getTeams();
                foreach ($teamdata[0] as $tid) {
                    echo "<option value=".$tid->Team_Id. ">".$tid->Team_Name."</option>";
                } ?>
            </select></td>
    </tr>
        <td>Dato:</td>
        <td><input type="date" name="datepicker" style="float:right"></td>
    <tr>
        <td><input type="submit" value="Submit" name="submit" style="float:right;"></td>
    </tr>
    </form>
</table>

<?php
if(isset($_POST['submit'])) {
    $persondata[] = getPersonsInTeam();
    ?>
    <table>
        <form>
            <tr>
                <td colspan="2"> Studerende i hold: <?php echo $persondata[0][0]->teamname ?></td>
            </tr>
            <?php foreach ($persondata[0] as $pd){
                 ?>
            <tr>
                <td> <?php echo $pd->firstname . " " . $pd->lastname ?> </td>
                <td><input type="checkbox" name="TilStede" value=""></td>
                <td><input type="checkbox" name="IkkeTilStede" value=""></td>
            </tr>
<?php } ?>
        </form>
    </table>
    <?php
}
?>
</body>
<?php

                    
?>

