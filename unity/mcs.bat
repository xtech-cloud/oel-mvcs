@echo off

set libpath=..\_bin\
::----------------------------------
set name=oelMVCS
set depref=
set libref=.\oelMVCS\Assets\3rd\uniAny-v1.1.dll;.\oelMVCS\Assets\3rd\uniError-v1.2.dll;.\oelMVCS\Assets\3rd\uniLogger-1.0.0.dll;.\oelMVCS\Assets\3rd\uniSimpleJSON-v1.0.dll
::----------------------------------
MD %libpath%
set outfile=%libpath%\%name%.dll
set srcpath=.\oelMVCS\Assets\XTC\oelMVCS\Scripts
call "%UNITY_ROOT%\Editor\Data\Mono\bin\smcs" -target:library -r:"%UNITY_ROOT%\Editor\Data\Managed\UnityEngine.dll";"%UNITY_ROOT%\Editor\Data\UnityExtensions\Unity\GUISystem\UnityEngine.UI.dll" -out:%outfile% -recurse:%srcpath%\*.cs -reference:%depref%;%libref%
echo FINISH
pause
exit