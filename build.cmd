@echo off

PUSHD %~dp0

echo Usage: build.cmd /p:MajorVersion=3 /p:MinorVersion=22 /p:BuildNumber=7213 /p:RevisionNumber=6

msbuild build.targets %*

POPD