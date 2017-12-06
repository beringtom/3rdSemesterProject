<style type="text/css" media="screen">
  table, th, td {
   border: 1px solid black;
}
table {
    width: 100%;
}

th {
    height: 50px;
}
</style>
<?php
  session_start();
  require('oauth.php');
  require('outlook.php');
  
  $loggedIn = !is_null($_SESSION['access_token']);
  $redirectUri = 'http://localhost/KalenderAPI/authorize.php';
?>
<html>
  <head>
    <title>PHP Calendar API Tutorial</title>
  </head>
  <body>
    <?php 
      if (!$loggedIn) {
    ?>
      <!-- User not logged in, prompt for login -->
      <p>Please <a href="<?php echo oAuthService::getLoginUrl($redirectUri)?>">sign in</a> with your Office 365 or Outlook.com account.</p>
    <?php
      }
      else {
        $events = OutlookService::getEvents($_SESSION['access_token'], $_SESSION['user_email']);
    ?>
      <!-- User is logged in, do something here -->
      <h2>Your events</h2>
      
      <table>
        <tr>
          <th>Subject</th>
          <th>Start</th>
          <th>End</th>
        </tr>
        
        <?php foreach($events['value'] as $event) { ?>
          <tr>
            <td><?php echo $event['Subject'] ?></td>
            <td><?php echo $event['Start']['DateTime'] ?></td>
            <td><?php echo $event['End']['DateTime'] ?></td>
          </tr>
        <?php } ?>
      </table>
    <?php    
      }
    ?>
  </body>
</html>