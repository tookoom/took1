<?php

echo "Iniciando; ";

if( isset($_POST['submitted']) ) // si formulaire soumis
{
    echo "Submitted; ";

    $content_dir = 'Upload/'; // dossier où sera déplacé le fichier

    $tmp_file = $_FILES['uploadedFile']['tmp_name'];

    if( !is_uploaded_file($tmp_file) )
    {
        exit("Le fichier est introuvable");
    }

    // on vérifie maintenant l'extension
    //$type_file = $_FILES['uploadedFile']['type'];

    //if( !strstr($type_file, 'jpg') && !strstr($type_file, 'jpeg') && !strstr($type_file, 'bmp') && !strstr($type_file, 'gif') )
    //{
    //    exit("Le fichier n'est pas une image");
    //}

    // on copie le fichier dans le dossier de destination
    $name_file = $_FILES['uploadedFile']['name'];

    if( !move_uploaded_file($tmp_file, $content_dir . $name_file) )
    {
        exit("Impossible de copier le fichier dans $content_dir");
    }

    echo "Le fichier a bien été uploadé";
}
else
{
   echo "Not submitted; ";
}

?>
