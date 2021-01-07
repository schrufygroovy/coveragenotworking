# About

For some unknown reason, the coverage is 0 % for my library project `Daxi.Libraries.MemoryStreamer` even though I know it IS hit by the tests.

## Calculating coverage

I am creating the coverage with following command:
```
dotnet test coveragenotworking.sln -p:CollectCoverage=true -p:CoverletOutputFormat="opencover" --logger:"nunit;LogFilePath=TestResult.xml"
```

The output looks like this then:

![screenshot of console](./output.png)

The controllers of the API are using a net standard library project. That library is part of the solution. Still the coverage of that library is 0 %. Why? When I debug the tests I see that the library code is reached. Also the controllers line that is accessing the library is marked as `covered`.