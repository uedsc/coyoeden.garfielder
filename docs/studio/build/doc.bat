rem ;; path to the jsdoc toolkit
set JD_PATH=F:\work\tools\jsdoc-toolkit
set JS_PATH=..\js

java -jar %JD_PATH%\jsrun.jar %JD_PATH%\app\run.js -a -t=%JD_PATH%\templates\jsdoc %JS_PATH%\jf*.js

pause>nul