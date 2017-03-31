# Csharp Generator


## Features
1. [x] [RepositoryUtils](./Utils/RepositoryUtils/README.MD): Generate entity classes from exsited database, surpported database includes: MsSQL, MySQL.

## Usages
> For !windows, build source code and run: dotnet CsharpGenerator.dll [command] [options].

``` bash
CsharpGenerator [command] [options]
```
command: see specific utils in [Features](#Features)

options:
``` bash
  -l, --loggerlevel         (Default: 2) 设置日志输出级别, 0-6

  --help                    Display this help screen.

  --version                 Display version information.
```

logger level:
``` c#
public enum ELoggerLevel
{
    Off = 0,
    Debug = 1,
    Info = 2,
    Warn = 3,
    Error = 4,
    FATAL = 5,
    All = 6,
}
```

## Acknowledgements
1. [CommandLine](https://github.com/gsscoder/commandline).
> The Command Line Parser Library offers CLR applications a clean and concise API for manipulating command line arguments and related tasks, such as defining switches, options and verb commands. It allows you to display a help screen with a high degree of customization and a simple way to report syntax errors to the end user.
2. [Pomelo.Data.MySql](https://github.com/PomeloFoundation/Pomelo.Data.MySql) by [PomeloFoundation](https://github.com/PomeloFoundation)
> Contains MySQL implementations of the System.Data.Common(Both .NET Core and .NET Framework) interfaces.
