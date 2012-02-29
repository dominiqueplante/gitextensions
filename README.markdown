# Git Extensions

## Introduction

GitExtensions is many things, including:
* a shell extension
* a plugin for Visual Studio 2008 / 2010
* a standalone Git repository tool.

Some of the main features:
* it has support for Subversion repositories

## Supported Environments

Windows 7 (32 and 64 bit)
Mono (on Linux, and on a Mac)

## Mailing list

The GitExtensions mailing list can be found at [http://groups.google.com/group/gitextensions](http://groups.google.com/group/gitextensions)

## How to debug GitExtensions

The installer is built using WiX. You need to install WiX to build the installer. This can be downloaded here: [http://wix.sourceforge.net/](http://wix.sourceforge.net/).  Wix 3.5 works great with Visual Studio 2010.

Note: To build the WiX installer, you must do a Release build of the code, not a debug build.

When the installer is successfully built, you should see GitExtensions.msi in the subfolder Setup\bin\Release

If you do not want to build the installer, just open the solution and ignore the warning.

* Open the solution file (GitCommands.sln or GitCommands.VS2010.sln)
* Hit F5 to compile and run GitExtensions

## How to contribute code

* Login and set up an account with GitHub (you need an account and can create one for free)
* Fork the main repository from [GitHub](http://github.com/spdr870/gitextensions)
* Push your changes to your fork
* Send me a pull request

If you do not want to use GitHub, I also accept emailed patches (henk_westhuis@hotmail.com).  Make sure the patch is sent as an attachement and not in the body of the email.

## How to create the installer

* Download and install WiX [http://wix.sourceforge.net/](http://wix.sourceforge.net/)
* Open the solution file (GitCommands.sln or GitCommands.VS2010.sln)
* Compile SimpleShlExt in release for 64bit windows
* Compile SimpleShlExt in release for 32bit windows
* Compile entire solution in release, mixed platforms
* Run Setup\MakeInstallers.bat to build the installers

## Links

* Download page: [http://code.google.com/p/gitextensions/downloads/list](http://code.google.com/p/gitextensions/downloads/list)
* ChangeLog: [https://github.com/spdr870/gitextensions/blob/master/GitUI/Resources/ChangeLog.txt](https://github.com/spdr870/gitextensions/blob/master/GitUI/Resources/ChangeLog.txt)
* Source code: [http://github.com/spdr870/gitextensions](http://github.com/spdr870/gitextensions)
* Issue tracker: [http://github.com/spdr870/gitextensions/issues](http://github.com/spdr870/gitextensions/issues)
* Mailing list: [http://groups.google.com/group/gitextensions](http://groups.google.com/group/gitextensions)
