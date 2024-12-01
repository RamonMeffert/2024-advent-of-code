#!/bin/bash

# Read input file to var
input=$(< input);

# Set internal field separator to new line
IFS=$'\n';

# Read input as array
pairs=($input);

# Reset IFS
unset IFS;

declare -a list_l;
declare -a list_r;

for pair in "${pairs[@]}"; do
    pair_array=($pair);
    list_l+=(${pair_array[0]});
    list_r+=(${pair_array[1]});
done

# Set internal field separator to new line for sorting
IFS=$'\n';

# Sort the arrays: first one ascending
list_l=($(sort -n <<< "${list_l[*]}"))
list_r=($(sort -n <<< "${list_r[*]}"))

# Reset IFS
unset IFS;

sum=0

for ((i=0; i < ${#pairs[@]}; i++)) do
    l=${list_l[$i]};
    r=${list_r[$i]}
    [[ $l -gt $r ]] && dist=$((l-r)) || dist=$((r-l))
    sum=$((sum+dist))
done

echo $sum
