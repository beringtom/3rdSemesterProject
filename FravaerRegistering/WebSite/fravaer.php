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
        <?php foreach ($decodedContent as $item){?>
        <option value="<?php echo($item->rid); ?>"><?php echo($item->firstname . " " . $item->lastname) ?></option>
        <?php } ?>
    </select>
    <input type="submit"value="Vis Fravær">
</form>



<?php
if(isset($_GET['rperson']))
{
    if(isset($_GET['edit']))
    {
        $editid = $_GET['edit'];

        $getresturitimeforedit = "http://restfravaerservice.azurewebsites.net/service1.svc/Time/Edit/".$editid;

        $contenttimeforedit = file_get_contents($getresturitimeforedit);

        $decodedContenttimeforedit= json_decode($contenttimeforedit);

        $datein = date_format(new DateTime($decodedContenttimeforedit->TimeRegistration_CheckIn),'Y-m-d\TH:i');
        $dateout = date_format(new DateTime($decodedContenttimeforedit->TimeRegistration_CheckOut),'Y-m-d\TH:i');
        ?>
        <table class="table table-bordered">
            <tr>
                <td>IN</td>
                <td>OUT</td>
            </tr>
            <tr>
                <form method="post">
                    <td><input type="datetime-local" name="timeinedit" value="<?php echo $datein; ?>"></td>
                    <td><input type="datetime-local" name="timeoutedit" value="<?php echo $dateout; ?>"></td>
                    <input type="hidden" value="<?php echo $decodedContenttimeforedit->TimeRegistration_Id;  ?>" name="timeid">
                    <td><input type="submit" name="editsubmit" value="Rediger"></td>
                </form>
            </tr>
        </table>

    <?php
        if(isset($_POST['editsubmit']))
        {
            $tinedit = date_format(new DateTime($_POST['timeinedit']),'d-m-Y H:i');
            $toutedit = date_format(new DateTime($_POST['timeoutedit']),'d-m-Y H:i');
            $timeid = $_POST['timeid'];


            $data = array("TimeRegistration_Id" => $timeid, "TimeRegistration_CheckIn" => $tinedit, "TimeRegistration_CheckOut" => $toutedit);
            $json_string = json_encode($data);


            $uri = "http://restfravaerservice.azurewebsites.net/service1.svc/Time/" . $timeid;
            //$uri = "http://localhost:7150/Service1.svc/Time/" . $timeid;
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
            header("Location: fravaer.php?rperson=".$_GET['rperson']);

        }
    }
    else
    {
    if(isset($_GET['opret']))
    {
        ?>
        <table class="table table-bordered">
            <tr>
                <td>IN</td>
                <td>OUT</td>
            </tr>
            <tr>
                <form method="post">
                    <td><input type="datetime-local" name="timein"></td>
                    <td><input type="datetime-local" name="timeout"></td>
                    <td>
                        <select name="room">
                            <?php
                            $getresturiroom = "http://restfravaerservice.azurewebsites.net/service1.svc/Room/";
                            //$getresturiroom = "http://localhost:7150/Service1.svc/Room/";

                            $contentroom = file_get_contents($getresturiroom);

                            $decodedContentroom = json_decode($contentroom);
                            foreach ($decodedContentroom as $room){?>
                                <option value="<?php echo($room->Room_Id); ?>"><?php echo($room->Room_Name); ?></option>
                            <?php } ?>
                        </select>
                    </td>
                    <td><input type="submit" name="opretsubmit" value="Opret"></td>
                </form>
            </tr>
        </table>
        <?php
        if(isset($_POST['opretsubmit']))
        {
            $tin = date_format(new DateTime($_POST['timein']),'d-m-Y H:i');
            $tout = date_format(new DateTime($_POST['timeout']),'d-m-Y H:i');
            $roomid = $_POST['room'];

            $data = array("TimeRegistration_CheckIn" => $tin, "TimeRegistration_CheckOut" => $tout, "FK_RegPersonId" => $_GET['rperson'], "FK_RoomId" => $roomid);
            $json_string = json_encode($data);
            print_r($json_string);
            $uri = "http://restfravaerservice.azurewebsites.net/service1.svc/Time/";

           // $uri = "http://localhost:7150/Service1.svc/Time/";
            $ch = curl_init($uri);
            curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
            curl_setopt($ch, CURLOPT_POSTFIELDS, $json_string);
            curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
            curl_setopt($ch, CURLOPT_HTTPHEADER, array(
                    'Content-Type: application/json',
                    'Content-Length: ' . strlen($json_string))
            );
            $jsondata = curl_exec($ch);
            header("Location: fravaer.php?rperson=".$_GET['rperson']);
        }
    }
    else{

        $getresturifortime = "http://restfravaerservice.azurewebsites.net/service1.svc/Time/".$_GET['rperson'];

        //$getresturifortime = "http://localhost:7150/Service1.svc/Time/".$_GET['rperson'];

        $contentfortime = file_get_contents($getresturifortime);

        $decodedContentfortime = json_decode($contentfortime);


        ?>

    <a href="?rperson=<?php echo($_GET['rperson']); ?>&opret">Opret</a>
    <table class="table table-bordered">
        <tr>
            <td>Check-In time</td>
            <td>Check-Out time</td>
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
}
?>

</table>





</body>
</html>
