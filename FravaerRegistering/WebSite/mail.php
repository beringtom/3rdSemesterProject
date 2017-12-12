<?php
  session_start();
  require('KalenderAPI/oauth.php');
  require('KalenderAPI/outlook.php');
  $loggedIn = !is_null($_SESSION['access_token']);
  $redirectUri = 'http://localhost/FravaerRegistering/WebSite/KalenderAPI/authorize.php';
?>

<html>
	<head>
		<title>PHP Mail API Tutorial</title>
	</head>
  <body>
  	<form method="post">
		<input name="send" value="Send" style="float:right;" type="submit">
	</form>
    <?php 
      if (!$loggedIn) {
    ?>
     
      <!-- User not logged in, prompt for login -->
      <p>Please <a href="<?php echo oAuthService::getLoginUrl($redirectUri)?>">sign in</a> with your Office 365 or Outlook.com account.</p>
    <?php
      }
      else {
      	
      		 $messages = OutlookService::SendMail(oAuthService::getAccessToken($redirectUri), $_SESSION['user_email']);
      		 print_r($messages);
      	
       
         }
         ?>
  </body>
</html>

