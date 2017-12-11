<?php
/**
 * Created by PhpStorm.
 * User: rebelricco
 * Date: 07-12-2017
 * Time: 10:26
 */

function getSchedule()
{
    $getPTSuri = "http://restfravaerservice.azurewebsites.net/service1.svc/";
    $PTScontent = file_get_contents($getPTSuri);
    $decodedPTSContent = json_decode($PTScontent);
    return $decodedPTSContent;
    print_r($_SERVER['REQUEST_URI']);
}