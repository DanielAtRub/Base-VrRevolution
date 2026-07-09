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

    $sth = $dbh->query('SELECT * FROM Score WHERE centro LIKE "Sambil" ORDER BY puntos DESC LIMIT 16');
    $sth->setFetchMode(PDO::FETCH_ASSOC);

    $result = $sth->fetchAll();

    if(count($result) > 0) {
        foreach($result as $r) {
            //echo $r['usuario'], "\t", $r['mail'], "\t", $r['centro'], "\t", $r['juego'], "\t", $r['equipo'], "\t", $r['puntos'], "\n";
			echo $r['usuario'], "\t", $r['equipo'], "\t", $r['puntos'], "\n";
        }
    }
?>