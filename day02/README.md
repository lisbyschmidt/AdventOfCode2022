# Ruby

## How to run locally
Type for example:
```bash
part=part1 ruby aoc.rb
part=part2 ruby aoc.rb
```

## How to build and run with Docker
Type for example:
```bash
docker build -t day02 .
docker run --rm -e part=part1 day02
docker run --rm -e part=part2 day02
```