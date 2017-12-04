<?php
/**
 * Created by PhpStorm.
 * User: Berin
 * Date: 04-12-2017
 * Time: 13:08
 */

function getTeams()
{
    $getteamsuri = "http://restfravaerservice.azurewebsites.net/service1.svc/teams/";
    $teamcontent = file_get_contents($getteamsuri);
    $decodedTeamContent = json_decode($teamcontent);
    return $decodedTeamContent;
    print_r($_SERVER['REQUEST_URI']);
}