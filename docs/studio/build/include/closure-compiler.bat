echo  ------------------------------------------
echo  compiling using google closure compiler
echo  ------------------------------------------
java -jar "%CLOSURE_COMPILER%\compiler.jar" --js "%YUI_FOLDER_DEST%\%YUI_BUILD_NAME%.debug.%YUI_TYPE%" --js_output_file "%YUI_FOLDER_DEST%\%YUI_BUILD_NAME%.%YUI_TYPE%"
echo  DONE!
if "%YUI_DEBUG%"=="1" pause>nul