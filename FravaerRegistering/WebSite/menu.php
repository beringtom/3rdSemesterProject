<?php
/**
 * Created by PhpStorm.
 * User: Beringtom
 * Date: 05-12-2017
 * Time: 13:19
 */
?>

<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="UTF-8">
        <title>Fraværssystem - Menu</title>
        <link rel="stylesheet" href="stylesheet.css">
    </head>
    <body>
        <div id="main">
            <div id="menu">
                <table width="100%">
                    <tr>
                        <td width="80%">
                            <table>
                                <tr>
                                    <td><a href="index.php">Forsiden</a></td>
                                    <td><a href="oprethold.php">Opret Hold</a></td>
                                    <td><a href="opretbruger.php">Opret Bruger</a></td>
                                    <td><a href="redigerbruger.php">Redigere Bruger</a></td>
                                    <td><a href="sletbruger.php">Slet Brugere</a></td>
                                </tr>
                            </table>
                        </td>
                        <td width="20%"><?php require 'login.php';?></td>
                    </tr>
                </table>
            </div>
            <div>
                <hr>
            </div>
            <div id="content">

            </div>
        </div>
    </body>
</html>