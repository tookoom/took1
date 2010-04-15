<?php

print_r($_POST);
echo "<BR>"; 
print_r($_POST['reldir']);
echo "<BR>"; 
print_r($_POST['filename']);
echo "<BR>"; 
print_r($_POST['textdata']);
echo "<BR>"; 
echo "<BR>"; 

$uploaddir=$_POST['reldir'];
echo "uploaddir = $uploaddir<BR>";

$fname=$_POST['filename'];
echo "fname = $fname <BR>";

$textstring=$_POST['textdata'];
echo "textstring = $textstring <BR>";

if($fname == "")
 $uploadfile = $uploaddir . basename($_FILES['file']['name']);
else
 $uploadfile = $uploaddir . $fname;

 $fh = fopen($uploadfile, 'w+');
 
 $fh = fopen($uploadfile, 'w+');
 fwrite($fh, str_replace("\\", "", base64_decode($textstring)));
 fclose($fh);
 
 echo "File uploaded<BR>";
 $aux = "teste";
 echo $aux;
?>
