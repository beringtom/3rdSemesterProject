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
    <title>Fraværssystem - Slet bruger</title>
</head>
<body>
<table>
    <tr>
        <td colspan="2"><h2>Slet Bruger</h2></td>
    </tr>
    <form action="/sletbruger.php">
        <tr>
            <td></td>
            <td>Fornavn:</td>
            <td>Efternavn:</td>
        </tr>
        <tr>
            <td><input type="checkbox" name="sletuser[x]"></td>
            <td>Tom</td>
            <td>Bering Svensson</td>
        </tr>
        <tr>
            <td><input type="checkbox" name="sletuser[x]"></td>
            <td>Ricco</td>
            <td>Jørgensen</td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td><input type="submit" value="Submit" style="float:right;"></td>
        </tr>
    </form>
</table>
</body>
</html>
