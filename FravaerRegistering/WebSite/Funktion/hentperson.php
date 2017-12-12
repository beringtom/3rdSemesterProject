<?php
function getPerson()
{
    $getpersonuri = "http://restfravaerservice.azurewebsites.net/service1.svc/person/";
    $personcontent = file_get_contents($getpersonuri);
    $decodedPersonContent = json_decode($personcontent);
    return $decodedPersonContent;
    print_r($_SERVER['REQUEST_URI']);
}
?>