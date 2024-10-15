#!/bin/bash

output_file="all_code.txt"
: > "$output_file"  # Clear or create a new file

# Find all .cs files in the current directory without recursion
find . -maxdepth 1 -name "*.cs" | while read -r file; do
  echo "=== $file ===" >> "$output_file"  # Write the file name
  # Remove unwanted characters before "namespace" and append content to the output file
  sed 's/^.*namespace/namespace/' "$file" >> "$output_file"
  echo -e "\n" >> "$output_file"  # Add a newline for separation
done

echo "All code has been saved to $output_file"
