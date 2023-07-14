# Orbital Witness Tech Test


### Schedule of notices of leases parser

#### Running the application

##### Usage
```
Description:
  Convert from schedule of notices of leases to a format with entry information extracted

Usage:
  ScheduleOfNoticesOfLeasesParser [options]

Options:
  --input-file <input-file> (REQUIRED)    input file
  --output-file <output-file> (REQUIRED)  output file
  --version                               Show version information
  -?, -h, --help                          Show help and usage information
```

##### From dotnet
```
cd .\ScheduleOfNoticesOfLeasesParser\

dotnet run --input-file .\Data\schedule_of_notices_of_lease_examples.json --output-file test.json
```

##### From executable
```
cd .\ScheduleOfNoticesOfLeasesParser\bin\Debug\net6.0

dotnet run --input-file .\Data\schedule_of_notices_of_lease_examples.json --output-file test.json
```
#### From visual studio

* Project->Properties->Debug

* Open debug launch profiles UI

* Add similar to the snippet to Commandline Arg text box

```
--input-file "..\..\..\Data\schedule_of_notices_of_lease_examples.json" --output-file test.json
```

* Debug as usual

#### Design Approach
* Console app, due to time constraints and lack of further information. However its likely that the logic would likely be hosted in an API or an Azure Function / AWS Lambda in practice.

#### Assumptions
* Assumed that whilst row lengths vary that column positions remain fixed and used this as the basis of the algorithm to process the entries data.
  * However the failing tests suggest this assumption might break down.

#### Improvements
* Investigate failing tests
  * There are some scenarios which don't follow a fully columns based approach
* Efficiency improvements in mapping / parsing logic
    * Benchmark and optimise.
* Improved test coverage
  * Basic coverage of the core parsing logic but should extend to the rest of the application

