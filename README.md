# Vectron.Extensions.Logging.Theming
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE.txt)
[![Build status](https://github.com/Vectron/Vectron.Extensions.Logging.Theming/actions/workflows/BuildTestDeploy.yml/badge.svg)](https://github.com/Vectron/Vectron.Extensions.Logging.Theming/actions)
[![NuGet](https://img.shields.io/nuget/v/Vectron.Extensions.Logging.Theming.svg)](https://www.nuget.org/packages/Vectron.Extensions.Logging.Theming)

Vectron.Extensions.Logging.Theming provides a single line log formatter (based on the SimpleConsoleFormatter in [Microsoft.Extensions.Logging.Console](https://github.com/dotnet/runtime/tree/main/src/libraries/Microsoft.Extensions.Logging.Console/src) that can use Themes to change colors.

## Changes with default simple formatter
1. The message will be single line, but preserves new lines in the message.
2. The Level text is changed to capitals and full word  
    trce -> TRACE  
    dbug -> DEBUG  
    info -> INFO  
    warn -> WARN  
    fail -> FAIL  
    crit -> CRIT  
3. implemented themes for coloring.  
    Change theme by changing the FormatterOptions.Theme options.  
    Buildin themes:

| Option | Description |
| --- | --- |
| MEL | Use the colors like 'Microsoft.Extensions.Logging.Console' |
| NLog | Use the colors like 'NLog' |
| Serilog | Use the colors like 'Serilog' |
| None | Don't colorize the output |

## Output examples:  
[Microsoft.Extensions.Logging level only](assets/MelLevelOnly.png)  
[Microsoft.Extensions.Logging Full line](assets/MelFullLine.png)  

[NLog level only](assets/NLogLevelOnly.png)  
[NLog Full line](assets/NLogFullLine.png)  

[Serilog level only](assets/SerilogLevelOnly.png)  
[Serilog Full line](assets/SerilogFullLine.png)  

## Authors
- [@Vectron](https://www.github.com/Vectron)
