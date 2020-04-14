python -m venv env

copy "z_pip jupiter.bat" "env\z.bat"
copy "*.ipynb" "env"
copy "*.py"  "env"
copy "*.mp4" "env"
copy "*.csv" "env"
copy "*.png" "env"


MKDIR  "env\images"
xcopy "images" "env\images\" /S /E

MKDIR  "env\data"
xcopy "data" "env\data\" /S /E


cd env
scripts\python --version
dir /al /s scripts\

scripts\activate

