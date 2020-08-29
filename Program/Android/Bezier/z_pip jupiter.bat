
which python
easy_install -U pip

@echo off
rem Рабочая /Users/Eugene
rem https://pypi.org/project/clrmagic/
rem scripts\pip3 install clrmagic

scripts\pip3 install ipykernel
@echo on

scripts\ipython kernel install --user --name=env
scripts\jupyter --version
scripts\jupyter --paths
scripts\jupyter kernelspec list

scripts\pip  --version
scripts\pip3 --version
python -m pip install --upgrade pip

rem python -m pip install jupyter --force-reinstall
scripts\pip install notebook
scripts\pip install qtconsole
scripts\pip install jupyterlab
scripts\pip install nbconvert
scripts\pip install ipywidgets
scripts\jupyter --version

@echo off
rem ######## Specifics packets to create PDFs ################
@echo on
scripts\pip3 install numpy
scripts\pip install scipy
rem scripts\pip install freetype-py
rem scripts\pip uninstall matplotlib
scripts\pip install matplotlib==3.2.1
scripts\pip install pandas
rem scripts\python -m pip install -U matplotlib
rem python -m pip install -U pip setuptools
rem scripts\pip install matplotlib --force-reinstall
python -c "import matplotlib;import pandas; print(matplotlib.__version__);print(pandas.__version__);"
rem scripts\pip install imutils
rem scripts\pip install scikit-image
rem scripts\pip install sympy
rem scripts\pip install opencv-python
rem scripts\pip install opencv-contrib-python
rem scripts\pip install git+git://github.com/mkrphys/ipython-tikzmagic.git --force-reinstall
@echo off
rem #https://github.com/mkrphys/ipython-tikzmagic
rem pip3 install git+git://github.com/mkrphys/ipython-tikzmagic.git
rem #The package requires a working LaTeX installation, ImageMagick and (pdf2svg ??? not installed).
rem #https://miktex.org/howto/install-miktex
rem #        basic-miktex-2.9.7269-x64.exe
rem #https://imagemagick.org/script/download.php
rem #        ImageMagick-7.0.9-17-Q16-x64-static.exe
rem -------- Specifics packets to create PDFs ----------------

python -c "import sys; print(sys.executable); print ('\n'.join(sys.path))"

@echo on
where python

@echo off
python -c "import platform; print(platform.python_version())"

@echo on
scripts\pip install --upgrade bezier

scripts\jupyter notebook --version
scripts\jupyter.exe notebook
rem scripts\jupyter-notebook
