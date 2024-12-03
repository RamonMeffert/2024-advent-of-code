#!/bin/bash

IFS=$'\n'

# Find valid mul(X,Y) operations, and transform them to the X*Y syntax
muls=($(grep -ioE 'mul\([0-9]+,[0-9]+\)' input | sed -E 's/mul\(([0-9]+),([0-9]+)\)/\1\*\2/'))

sum=0

for t in ${muls[@]}; do
    sum=$((sum+t))
done

echo $sum

unset IFS