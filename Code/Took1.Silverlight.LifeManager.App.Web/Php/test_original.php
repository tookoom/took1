<?php
$uploaddir=$_POST['reldir'];
$fname=$_POST['filename'];
$textstring=$_POST['textdata'];
if($fname == "")
 $uploadfile = $uploaddir . basename($_FILES['file']['name']);
else
 $uploadfile = $uploaddir . $fname;

 $fh = fopen($uploadfile, 'w+');
 fwrite($fh, str_replace("\\", "", base64_decode($textstring)));
 fclose($fh);
 echo "OK";
?>
