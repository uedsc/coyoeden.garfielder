@echo off
rem ;; ======== S-Settings ===========

rem ;; where are the google closure compiler
set HTML_COMPILER=..\..\tools\yuicompressor246\build
set HTML_COMPILER_V=1.4.3
set IN_File=..\index.html
set OUT_File=..\index-min.html
rem ;; ======== E-Settings ===========


rem ;; ======== S-Processing ==========

echo  ------------------------------------------
echo  compressing index.html using htmlcompressor
echo  ------------------------------------------

java -jar "%HTML_COMPILER%\htmlcompressor-%HTML_COMPILER_V%.jar" --compress-css -o %OUT_File% %IN_File%
echo HTML Compressed!!

rem ;; ======== S-JS AND Css inline replacement ==========

echo  -----------------------------------------------
echo  Js and css inline replacement using powerscript
echo  -----------------------------------------------

powershell -ExecutionPolicy RemoteSigned -File build-html-replace.ps1
echo JS AND Css inline replaced!!

pause>nul