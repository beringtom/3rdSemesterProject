<?php
/**
 * Created by PhpStorm.
 * User: Berin
 * Date: 04-12-2017
 * Time: 12:33
 */


function oprethold($full_uri)
{
    $tname = $_POST['holdnavn'];
    echo ($tname);
    $data = array("Team_Name" => $tname);
    $json_string = json_encode($data);
    print_r($json_string);
    $uri = "http://restfravaerservice.azurewebsites.net/service1.svc/Team/";
    $ch = curl_init($uri);
    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
    curl_setopt($ch, CURLOPT_POSTFIELDS, $json_string);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($ch, CURLOPT_HTTPHEADER, array(
            'Content-Type: application/json',
            'Content-Length: ' . strlen($json_string))
    );
    $jsondata = curl_exec($ch);
}
