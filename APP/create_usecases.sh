#!/bin/bash

# List of entities
entities=("User" "CashFlow" "Contract" "Progress" "Stakeholders" "SubAccounts""Voucher")

# Directory to store use case files
use_case_dir="UseCases"

# Create the directory if it doesn't exist
mkdir -p $use_case_dir

# Loop through each entity and create use case files
for entity in "${entities[@]}"; do
  touch "$use_case_dir/Get${entity}.cs"
  touch "$use_case_dir/Create${entity}.cs"
  touch "$use_case_dir/Update${entity}.cs"
  touch "$use_case_dir/Delete${entity}.cs"
done

echo "Use case files created successfully."