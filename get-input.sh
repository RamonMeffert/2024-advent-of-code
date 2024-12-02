#!/bin/bash

# Make sure variable is supplied
if [ $# -ne 1 ]
  then echo 'Use: ./get_input.sh <PROBLEM_ID>';
  exit 1;
fi

# Validate user input
re='(^[1-9]$)|(^[1-2][0-9]$)|(^3[01]$)'
if ! [[ $1 =~ $re ]]
  then echo 'Problem ID must be a number between 1-31 without leading zeroes.';
  exit 1;
fi

# Format the problem id with a leading zero
problem_id=$(printf "%02d" $1);

# Use cURL to get the input for some problem
#           -sS: Silence output, but show errors
#            -f: Fail on HTTP errors
# --create-dirs: Create the output dir if it doesn't exist
#            -o: Save the output to a file
#            -b: Use the contents as a file as a cookie
curl_output=$(curl -sS -f --create-dirs -o $problem_id/input -b cookie https://adventofcode.com/2024/day/$1/input >&1 2>&1)

# If cURL returns an error exit code, tell us
if [ $? -ne 0 ]; then
  echo "Something went wrong receiving the input for problem $1:";
  echo "    $curl_output"
else
  echo "Done! Created ./$problem_id/input".
fi