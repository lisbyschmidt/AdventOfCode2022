# PowerShell

## How to run locally
Type for example:
```bash
part=part1 pwsh aoc.ps1
part=part2 pwsh aoc.ps1
```

## How to build and run with Docker
Type for example:
```bash
docker build -t day01 --platform=linux/x86-64 .
docker run -e part=part1 day01
docker run -e part=part2 day01
```