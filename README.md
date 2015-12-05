





### Options
* [filename]
* /replace < from >< to >
* /count
* /filter < phrase >
* /skip < noOfLines >
* /take < noOfLines >
* /console - outputs results to console
* /file < filename > - output results to a file 

to ilmerge
 & ILMerge.exe /targetplatform:"v4,$env:windir\Microsoft.NET\Framework$bitness\v4.0.30319"  /out:bf.exe BigFiles.exe Serilog.dll Serilog.FullNetFx.dll System.IO.Abstractions.dll
