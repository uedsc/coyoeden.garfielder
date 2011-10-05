$path='..\index-min.html';

$toReplace_css='<link rel="stylesheet" href="css/style.css"/>';
$toReplace_js='<script type="text/javascript" src="js/release/mtuan.all.debug.js"></script>';

$path_css='..\css\style.css';
$path_js='..\js\release\mtuan.all.js';

$file=[IO.File]::ReadAllText($path);
$css='<style type="text/css">'+[IO.File]::ReadAllText($path_css)+'</style>';
$js='<script>'+[IO.File]::ReadAllText($path_js)+'</script>';

$file1=$file.Replace($toReplace_css,$css).Replace($toReplace_js,$js);

[IO.File]::WriteAllText($path,$file1);