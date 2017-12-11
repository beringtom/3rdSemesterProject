<?php
/**
 * Created by PhpStorm.
 * User: rebelricco
 * Date: 07-12-2017
 * Time: 10:02
 */

function getSchedule($s)
{
    $getScheduleuri = "http://restfravaerservice.azurewebsites.net/service1.svc/Schedule/".$s;
    $Schedulecontent = file_get_contents($getScheduleuri);
    $decodedScheduleContent = json_decode($Schedulecontent);
    return $decodedScheduleContent;
    print_r($_SERVER['REQUEST_URI']);
}