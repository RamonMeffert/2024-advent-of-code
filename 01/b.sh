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

# Count number occurences in list_r
declare -a r_counts
for num in ${list_r[@]}; do
    if [ ! "${r_counts[$num]+abc}" ]; then
        r_counts[$num]=1;
    else
        orig=${r_counts[$num]}
        r_counts[$num]=$((orig+1));
    fi
done

# Calculate the total
total=0

for key in ${list_l[@]}; do
    # Find count of number in list L in list R
    [ "${r_counts[$key]+abc}" ] && count=${r_counts[$key]} || count=0;

    # Multiply them together
    new_value=$((key*count))

    # Add them to the total
    total=$((total+new_value))
done;

echo $total