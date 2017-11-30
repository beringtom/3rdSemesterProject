<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Fraværssystem - Rediger Bruger</title>
</head>
<body>

</body>
    <table>
        <tr>
            <td colspan="2"><h2>Rediger Bruger</h2></td>
        </tr>
        <form action="redigerbruger.php">
            <tr>
                <td>Fornavn:</td>
                <td><input type="text" name="firstname" value=""></td>
            </tr>
            <tr>
                <td>Efternavn:</td>
                <td><input type="text" name="lastname" value=""></td>
            </tr>
            <tr>
                <td>Email:</td>
                <td><input type="text" name="email" value=""></td>
            </tr>
            <tr>
                <td>Brugernavn:</td>
                <td><input type="text" name="username" value=""></td>
            </tr>
            <tr>
                <td>Password:</td>
                <td><input type="text" name="password" value=""></td>
            </tr>
            <tr>
                <td>Student ID:</td>
                <td><input type="text" name="studentid" value=""></td>
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
</html>