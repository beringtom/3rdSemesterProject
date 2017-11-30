<?php
/**
 * Created by PhpStorm.
 * User: BeringTom
 * Date: 28-11-2017
 * Time: 09:54
 */

?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Fraværssystem - Opret Bruger</title>
</head>
<body>
<table>
    <tr>
        <td colspan="2"><h2>Opret Bruger</h2></td>
    </tr>
    <form action="/opretbruger.php">
        <tr>
            <td>Fornavn:</td>
            <td><input type="text" name="firstname" value="First Name"></td>
        </tr>
        <tr>
            <td>Efternavn:</td>
            <td><input type="text" name="lastname" value="Last Name"></td>
        </tr>
        <tr>
            <td>Email:</td>
            <td><input type="text" name="email" value="username@edu.easj.dk"></td>
        </tr>
        <tr>
            <td>Brugernavn:</td>
            <td><input type="text" name="username" value="Username"></td>
        </tr>
        <tr>
            <td>Password:</td>
            <td><input type="text" name="password" value="eg. velkommen"></td>
        </tr>
        <tr>
            <td>Student ID:</td>
            <td><input type="text" name="studentid" value="eg. 48484848"></td>
        </tr>
        <tr>
            <td>Rolle:</td>
            <td><select name="rolle">
                    <option value="0">--None--</option>
                    <option value="1">Studerende</option>
                    <option value="2">Underviser</option>
                    <option value="3">Uddannelses Leder</option>
                </select>
            </td>
        </tr>
        <tr>
            <td></td>
            <td><input type="submit" value="Submit" style="float:right;"></td>
        </tr>
    </form>
</table>
</body>
</html>
