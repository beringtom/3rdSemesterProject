<?php
/**
 * Created by PhpStorm.
 * User: Bruger
 * Date: 01-12-2017
 * Time: 13:09
 */


function slet($full_uri)
{
    $ch = curl_init($full_uri);
// curl is good for more complex operations than just plain GET
    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "DELETE");
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
// TRUE to return the transfer as a string of the return value of curl_exec() instead of outputting it directly.
    $jsondata = curl_exec($ch);
    $theDelete = json_decode($jsondata, true);
    if ($theDelete == null) {
        $bookArray = false;
    } else {
        $bookArray = array($theDelete);

    }
}
?>