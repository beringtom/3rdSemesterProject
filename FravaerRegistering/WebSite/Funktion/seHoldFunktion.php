<?php
/**
 * Created by PhpStorm.
 * User: Denderish
 * Date: 06/12/2017
 * Time: 13:12
 */

function getPersonsInTeam()
{
    $getteamsuri = "http://restfravaerservice.azurewebsites.net/service1.svc/person/team/".$_get['klasse'];
    $teamcontent = file_get_contents($getteamsuri);
    $decodedTeamContent = json_decode($teamcontent);
    return $decodedTeamContent;
    print_r($_SERVER['REQUEST_URI']);
}

?>