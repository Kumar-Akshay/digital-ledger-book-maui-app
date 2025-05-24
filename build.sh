#!/bin/zsh

echo "Cleaning build directory..."
dotnet clean

echo "Building DesiKhataApp..."
dotnet build

if [ $? -eq 0 ]; then
  echo "Build successful!"
else
  echo "Build failed!"
fi
