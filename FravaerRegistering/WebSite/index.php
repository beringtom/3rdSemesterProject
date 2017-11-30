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
        <title>Fraværssystem - login</title>
    </head>
    <body>
        <table>
            <tr>
                <td colspan="2"><h2>Login</h2></td>
            </tr>
            <form action="/loginform.php">
            <tr>
                <td>Username:</td>
                <td><input type="text" name="username" value="brugernavn"></td>
            </tr>
            <tr>
                <td>Password</td>
                <td><input type="password" name="password" value="password"></td>
            </tr>
            <tr>
                <td></td>
                <td><input type="submit" value="Submit" style="float:right;"></td>
            </tr>
            </form>
        </table>
    </body>
</html>
