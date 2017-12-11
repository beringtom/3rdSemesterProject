<?php
/**
 * Created by PhpStorm.
 * User: Jesper
 * Date: 01-12-2017
 * Time: 13:09
 */


function slet($full_uri)
{
    $ch = curl_init($full_uri);
    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "DELETE");
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
    $jsondata = curl_exec($ch);
    $theDelete = json_decode($jsondata, true);
}
?>