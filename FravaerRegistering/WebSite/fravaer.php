<?php
/**
 * Created by PhpStorm.
 * User: Beringtom
 * Date: 05-12-2017
 * Time: 13:57
 */


$getresturi = "http://restfravaerservice.azurewebsites.net/service1.svc/Person/";

$content = file_get_contents($getresturi);

$decodedContent = json_decode($content);
//print_r($_SERVER['REQUEST_URI']);
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Fraværssystem - Fravær</title>
    <link rel="stylesheet" href="stylesheet.css">
</head>
<body>
<?php include("menu.php") ?>



<form method="get">
    <select name="rperson">
        <option value="0">-- None --</option>
        <?php foreach ($decodedContent as $item){ ?>
        <option value="<?php echo($item->rid); ?>"><?php echo($item->firstname . " " . $item->lastname) ?></option>
        <?php } ?>
    </select>
    <input type="submit"value="Vis Fravær">
</form>



<?php
if(isset($_GET['rperson']))
{
    if(isset($_GET['opret']))
    {
        ?>
        <table>
            <tr>
                <td></td>
                <td></td>
            </tr>
        </table>



        <?php
    }
    else{

        $getresturifortime = "http://restfravaerservice.azurewebsites.net/service1.svc/Time/";

        $contentfortime = file_get_contents($getresturifortime);

        $decodedContentfortime = json_decode($contentfortime);


        ?>

    <a href="?rperson=<?php echo($_GET['rperson']); ?>&opret">Opret</a>
    <table>
        <tr>
            <td></td>
        </tr>
        <?php
        foreach ($decodedContentfortime as $time) {
            ?>
            <tr>
                <td><?php echo($time->TimeRegistration_CheckIn); ?></td>
                <td><?php echo($time->TimeRegistration_CheckOut); ?></td>
                <td><a href="?rperson=<?php echo($_GET['rperson']); ?>&edit=<?php echo($time->TimeRegistration_Id); ?>">Edit</a> </td>
            </tr>
            <?php
        }

    }
}
?>

</table>





</body>
</html>
