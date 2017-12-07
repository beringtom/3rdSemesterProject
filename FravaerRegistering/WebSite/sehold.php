<?php
/**
 * Created by PhpStorm.
 * User: Denderish
 * Date: 05/12/2017
 * Time: 12:17
 */
require ('Funktion/hentHoldFunktion.php');
require ('Funktion/seHoldFunktion.php');
require ('Funktion/hentScheduleFunktion.php');
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Fraværssystem - Se hold</title>
</head>
<body>

<table>
    <form method="get">
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
            </select>
        </td>
    </tr>
    <tr>
        <td><input type="submit" value="Find Lektioner" style="float:right;"></td>
    </tr>
    </form>
    <?php
    if(isset($_GET['klasse'])) {
        ?>

        <form method="get">
            <input type="hidden" name="klasse" value="<?php echo $_GET['klasse']; ?>">
        <tr>
            <td>Lektion: </td>
            <td>

            <select name="schedule">
                <option value="0">-- NONE --</option>
                <?php
                    $scheduledata[] = getSchedule($_GET['klasse']);
                    foreach ($scheduledata[0] as $time)
                    {?>
                        <option value="<?php echo $time->ScheduleTimefrom; ?>"><?php echo $time->ScheduleTimefrom; ?></option>
                    <?php
                    }
                ?>
            </select>

            </td>
        </tr>
        <tr>
            <td><input type="submit" value="Hent..."></td>
        </tr>
        </form>
        <?php
    }
    ?>
</table>
<!--
<?php
/*
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
*/
?>

-->


<?php

if (isset($_GET['schedule']))
{







}
?>

</body>
