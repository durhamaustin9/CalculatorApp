#!/usr/bin/env bash

set -euo pipefail

script_dir="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
project_root="$(cd "$script_dir/.." && pwd)"
app_bundle="$project_root/artifacts/QuickCalc.app"
app_contents="$app_bundle/Contents"

case "$(uname -m)" in
  arm64) runtime_identifier="osx-arm64" ;;
  x86_64) runtime_identifier="osx-x64" ;;
  *) echo "Unsupported Mac architecture: $(uname -m)" >&2; exit 1 ;;
esac

rm -rf "$app_bundle"
mkdir -p "$app_contents/MacOS" "$app_contents/Resources"

dotnet publish "$project_root/src/CalculatorApp/CalculatorApp.csproj" \
  --configuration Release \
  --runtime "$runtime_identifier" \
  --self-contained false \
  --output "$app_contents/MacOS"

cp "$project_root/packaging/macos/Info.plist" "$app_contents/Info.plist"

echo "Created $app_bundle"
