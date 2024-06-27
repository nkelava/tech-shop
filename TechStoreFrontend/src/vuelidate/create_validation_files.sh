#!/bin/bash

# Check if an argument is provided
if [ -z "$1" ]; then
  echo "Usage: $0 <name>"
  exit 1
fi

# Assign the argument to a variable
NAME=$1

# Create the directory and navigate into it
mkdir $NAME && cd $NAME

# Create the files with the specified naming pattern
touch ${NAME}.rules.js ${NAME}.state.js index.js

# Navigate back to the original directory
cd ..

echo "Created directory and files for $NAME"