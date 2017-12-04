<?php
/**
 * Created by PhpStorm.
 * User: BeringTom
 * Date: 28-11-2017
 * Time: 09:54
 */
session_start();
ob_start();
?>

<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="UTF-8">
        <title>Fraværssystem - login</title>
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous">
    </head>
    <body>
    <div class="page-header">
        <h1>Login</h1>
    </div>
            <form class="col-4" method="post">

                <label for="uname" class="form-group col-5">Username:</label>
                <input class="form-control col-7 float-right" id="uname" type="text" name="username" placeholder="brugernavn">

                <label for="pass" class="form-group col-5">Password</label>
                <input class="form-control col-7 float-right" id="pass" type="password" name="password" placeholder="password">

                <input class="btn btn-default float-right" type="submit" value="Submit" name="login">
            </form>
    <?php
    if(isset($_REQUEST['login']))
    {
        $username = $_REQUEST['username'];
        $password = $_REQUEST['password'];

        $data = array("Login_UserName" => $username, "Login_Password" => $password);
        $json_string = json_encode($data);


        $uri = "http://restfravaerservice.azurewebsites.net/service1.svc/Login/";
        $ch = curl_init($uri);


// curl is good for more complex operations than just plain GET
        curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
        curl_setopt($ch, CURLOPT_POSTFIELDS, $json_string);
        curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
// TRUE to return the transfer as a string of the return value of curl_exec() instead of outputting it directly.
        curl_setopt($ch, CURLOPT_HTTPHEADER, array(
                'Content-Type: application/json',
                'Content-Length: ' . strlen($json_string))
        );


        $jsondata = curl_exec($ch);
        $loginUser = json_decode($jsondata, true);

        print_r($loginUser);
        if($loginUser == true)
        {
            $_SESSION["UserLoggedIn"] = $loginUser
        }
    }
    ?>
        <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.3/umd/popper.min.js" integrity="sha384-vFJXuSJphROIrBnz7yo7oB41mKfc8JzQZiCq4NCceLEaO4IHwicKwpJf9c9IpFgh" crossorigin="anonymous"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/js/bootstrap.min.js" integrity="sha384-alpBpkh1PFOepccYVYDB4do5UnbKysX5WZXm3XxPqe5iKTfUKjNkCk9SaVuEZflJ" crossorigin="anonymous"></script>
    </body>
</html>
