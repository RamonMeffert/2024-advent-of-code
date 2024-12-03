#!/bin/bash

IFS=$'\n'

muls=($(sed -E 's/don\x27t\(\)/\nNO /g' input | \
sed -E 's/do\(\)/\nYES /g' | \
sed -E 's/^NO.*//g' | \
awk 'NF' | \
grep -ioE 'mul\([0-9]+,[0-9]+\)' | \
sed -E 's/mul\(([0-9]+),([0-9]+)\)/\1\*\2/'))

sum=0

for t in ${muls[@]}; do
    sum=$((sum+t))
done

echo $sum

unset IFS