# Keyboard Madness
Simple program exercise given to me by an ex coworker sometime ago for fun. This is a F# port of my original program that was written Nim, where I was able to fix some bugs and add some additional functionality along with unit testing.

## Exercise details

```
"1" "2" "3" "4" "5" "6" "7" "8" "9" "0"
"Q" "W" "E" "R" "T" "Y" "U" "I" "O" "P"
"A" "S" "D" "F" "G" "H" "J" "K" "L" ";"
"Z" "X" "C" "V" "B" "N" "M" "," "." "?"
```

Given this keyboard layout, an starting point of (4, 2) "G" and string of instructions, print out any selected keys. Instructions are separated by a comma, with some instructions haveing a count with them, meaning to repeat to give an action by the number of count.

## Instructions
* "R" - Move Right, can also have a count like "R:3"
* "L" - Move Left, can also have a count like "L:3"
* "D" - Move Down, can also have a count like "D:2"
* "U" - Move Up, can also have a count like "U:2"
* "_" - Add a space to the selected keys
* "N" - Add a new line to the selected keys
* "S" - Select the key at that point

Any unknown instructions are ignored

Sample instruction looks like `R,S,U,L:3,S,D,R:6,S,S,U,S` which will output "HELLO"

## Running Unit Test
Run `dotnet test`

## Run Command line Application
Run `dotnet run --project KeyboardMadness R,S,U,L:3,S,D,R:6,S,S,U,S`

## Docker

### Build docker image and use local image
* Build with `docker build . -t keyboard-madness`
* Run wit `docker run keyboard-madness R,S,U,L:3,S,D,R:6,S,S,U,S`