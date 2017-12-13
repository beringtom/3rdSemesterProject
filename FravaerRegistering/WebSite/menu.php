<?php
require 'login.php';
?>

<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="UTF-8">
        <title>Fraværssystem - Menu</title>
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
        <link rel="stylesheet" href="stylesheet.css">
    </head>
    <body>
        <div id="main">
            <?php 
            if(isset($_SESSION['UserLoggedInRole']))
            {
                $rolle = $_SESSION['UserLoggedInRole'];
            }
            else if (!isset($_SESSION['UserLoggedInID']))
            {
                $rolle = 0;
            }
            ?>
            <div id="menu">
                <nav class="navbar navbar-default">
                    <div class="container-fluid">
                        <div class="navbar-header">
                            <a class="navbar-brand" href="#">Fraværs Registering</a>
                        </div>
                        <ul class="nav navbar-nav">
                            <li><a href="index.php">Forsiden</a></li>
                            <?php if ($rolle == 1 ) { ?>
                                 <li><a href="calendar.php">Kalender</a></li>
                            <li><a href="oprethold.php">Opret Hold</a></li>
                            <li><a href="opretbruger.php">Opret Bruger</a></li>
                            <li><a href="redigerbruger.php">Redigere Bruger</a></li>
                            <li><a href="sletbruger.php">Slet Brugere</a></li>
                            <li><a href="fravaer.php">Fravaer</a></li>

                            <?php }  if($rolle == 2) {?>
                            <li><a href="calendar.php">Kalender</a></li>
                            <li><a href="fravaer.php">Fravaer</a></li>

                            <?php } if($rolle == 3) {?>
                            <li><a href="calendar.php">Kalender</a></li>
                            <?php }  if($rolle == 0) {} ?>
                        </ul>
                        <ul class="nav navbar-nav navbar-right">
                            <li><?php require 'login.php';?></li>
                        </ul>
                    </div>
                </nav>
                <!--<table width="100%">
                    <tr>
                        <td width="80%">
                            <table>
                                <tr>
                                    <td><a href="index.php">Forsiden</a></td>
                                     <td><a href="calendar.php">Kalender</a></td>
                                    <td><a href="oprethold.php">Opret Hold</a></td>
                                    <td><a href="opretbruger.php">Opret Bruger</a></td>
                                    <td><a href="redigerbruger.php">Redigere Bruger</a></td>
                                    <td><a href="sletbruger.php">Slet Brugere</a></td>
                                </tr>
                            </table>
                        </td>
                        <td width="20%"><?php //require 'login.php';?></td>
                    </tr>
                </table>
            </div>
            <div>
                <hr>
            </div>-->
            <div id="content">

            </div>
        </div>
    </body>
</html>