<?php

    // Configuration
    $hostname = 'db5000765317.hosting-data.io';
    $username = 'dbu1074310';
    $password = 'Juego_271201';
    $database = 'dbs693369';

    try {
        $dbh = new PDO('mysql:host='. $hostname .';dbname='. $database, $username, $password);
    } catch(PDOException $e) {
        echo '<h1>An error has occurred.</h1><pre>', $e->getMessage() ,'</pre>';
    }

    $sth = $dbh->query('SELECT * FROM Score ORDER BY puntos DESC LIMIT 16');
    $sth->setFetchMode(PDO::FETCH_ASSOC);

    $result = $sth->fetchAll();

    if(count($result) > 0) {
        foreach($result as $r) {
            echo $r['usuario'], "\t", $r['mail'], "\t", $r['centro'], "\t", $r['juego'], "\t", $r['equipo'], "\t", $r['puntos'], "\n";
        }
    }
	
	/*
    // Send variables for the MySQL database class.
    $database = mysqli_connect('db5000765317.hosting-data.io', 'dbu1074310', 'Juego_271201', 'dbs693369') or die('Could not connect: ' . mysqli_error());

    $query = "SELECT * FROM `Score` ORDER by `puntos` DESC LIMIT 5";
    $result = mysqli_query($database, $query) or die('Query failed: ' . mysqli_error());
 
    $num_results = mysqli_num_rows($result);  
 
    for($i = 0; $i < $num_results; $i++)
    {
         $row = mysqli_fetch_array($result);
		 echo $row['usuario'], "\t", $row['mail'], "\t", $row['equipo'], "\t", $row['puntos'], "\n";
    }
	*/
?>