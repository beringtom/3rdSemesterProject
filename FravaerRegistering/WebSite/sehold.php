<?php
/**
 * Created by PhpStorm.
 * User: Denderish
 * Date: 05/12/2017
 * Time: 12:17
 */
require ('Funktion/hentHoldFunktion.php');
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

</body>
<?php

                    
?>

