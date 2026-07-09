<?php
    // Configuration
    $hostname = 'db5000765317.hosting-data.io';
    $username = 'dbu1074310';
    $password = 'Juego_271201';
    $database = 'dbs693369';

    $secretKey = "mySecretKey"; // Change this value to match the value stored in the client javascript below 

    try {
        $dbh = new PDO('mysql:host='. $hostname .';dbname='. $database, $username, $password);
    } catch(PDOException $e) {
        echo '<h1>An error has ocurred.</h1><pre>', $e->getMessage() ,'</pre>';
    }

	$Usuario = $_GET['usuario'];    
	$Mail = $_GET['mail'];   
	$Centro = $_GET['centro'];   
	$Juego = $_GET['juego'];   
	$Equipo = $_GET['equipo'];   
	$Puntos = $_GET['puntos'];   
	
	try{
        $query = ("INSERT into Score values(:Usuario,:Mail,:Centro,:Juego,:Equipo,:Puntos)");
         
        $sth= $dbh->prepare($query);
             
        $sth->bindParam(':Usuario',$Usuario);
        $sth->bindParam(':Mail',$Mail);
		$sth->bindParam(':Centro',$Centro);
        $sth->bindParam(':Juego',$Juego);
        $sth->bindParam(':Equipo',$Equipo);
        $sth->bindParam(':Puntos',$Puntos);
       
        $sth->execute();
    }
	catch(PDOException $e){
		error_log('PDOException -' . $e->getMessage(), 0);
        die('Error establishing connection with database');  
    }
    
    $dbh=null; //close connection
?>