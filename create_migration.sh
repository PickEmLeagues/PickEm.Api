#!/bin/bash

# Exit on error
set -e

# Check if a migration name was provided
if [ $# -eq 0 ]; then
    echo "Please provide a migration name"
    exit 1
fi

# Set the migration name
MIGRATION_NAME=$1

# Create the migration directory if it doesn't exist
mkdir -p ./DataAccess/Migrations
mkdir -p ./DataAccess/Scripts

# Get the last migration name if any exist
MIGRATIONS_LIST=$(dotnet ef migrations list --no-connect 2>/dev/null || echo "")
FIRST_MIGRATION=false
LAST_MIGRATION=""

# Check if the response contains "No migrations were found" or similar
if [[ "$MIGRATIONS_LIST" == *"No migrations were found"* ]]; then
    echo "This will be the first migration."
    FIRST_MIGRATION=true
elif [ -n "$MIGRATIONS_LIST" ] && [ "$(echo "$MIGRATIONS_LIST" | wc -l)" -gt 0 ]; then
    # Get the last line which should be the most recent migration
    MIGRATIONS_LIST=$(dotnet ef migrations list 2>/dev/null || echo "")
    LAST_MIGRATION=$(echo "$MIGRATIONS_LIST" | tail -n 1 | tr -d '\r')
    echo "Last migration: $LAST_MIGRATION"
else
    echo "This will be the first migration."
fi

# Create the migration
echo "Creating migration '$MIGRATION_NAME'..."
dotnet ef migrations add $MIGRATION_NAME -o ./DataAccess/Migrations

# Get the timestamp from the newly created migration file
TIMESTAMP=$(ls -t ./DataAccess/Migrations/*_${MIGRATION_NAME}.cs 2>/dev/null | head -n 1 | sed -n 's/.*Migrations\/\([0-9]*\)_.*/\1/p')

if [ -z "$TIMESTAMP" ]; then
    echo "Failed to extract timestamp from migration file."
    exit 1
fi

# Generate the Up script
echo "Generating Up script..."
if [ "$FIRST_MIGRATION" = false ]; then
    echo "Generating Up script from the last migration..."
    dotnet ef migrations script "$LAST_MIGRATION" "$MIGRATION_NAME" --output "./DataAccess/Scripts/${TIMESTAMP}_${MIGRATION_NAME}_Up.sql"
else
    # For the first migration, just generate from the beginning
    echo "Generating Up script from the beginning..."
    dotnet ef migrations script 0 "$MIGRATION_NAME" --output "./DataAccess/Scripts/${TIMESTAMP}_${MIGRATION_NAME}_Up.sql"
fi

# Generate the Down script only if this isn't the first migration
echo "Generating Down script..."
if [ "$FIRST_MIGRATION" = false ]; then
    echo "Generating Down script from the last migration..."
    dotnet ef migrations script "$MIGRATION_NAME" "$LAST_MIGRATION" --output "./DataAccess/Scripts/${TIMESTAMP}_${MIGRATION_NAME}_Down.sql"
else
    echo "Generating Down script from the beginning..."
    dotnet ef migrations script "$MIGRATION_NAME" 0 --output "./DataAccess/Scripts/${TIMESTAMP}_${MIGRATION_NAME}_Down.sql"
fi

echo "Migration '$MIGRATION_NAME' created and scripts generated successfully."