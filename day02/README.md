# Ruby

## How to run locally
Type for example:
```bash
part=part1 ruby aoc.ps1
part=part2 ruby aoc.ps1
```

## How to build and run with Docker
Type for example:
```bash
docker build -t day02 .
docker run -e part=part1 day02
docker run -e part=part2 day02
```