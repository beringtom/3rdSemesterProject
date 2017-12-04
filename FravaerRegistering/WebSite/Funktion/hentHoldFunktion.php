<?php
/**
 * Created by PhpStorm.
 * User: Berin
 * Date: 04-12-2017
 * Time: 13:08
 */

function getTeams()
{
    $getresturi = "http://restfravaerservice.azurewebsites.net/service1.svc/team/";
    $content = file_get_contents($getresturi);
    $decodedContent = json_decode($content);
    return $decodedContent;
    print_r($_SERVER['REQUEST_URI']);
}