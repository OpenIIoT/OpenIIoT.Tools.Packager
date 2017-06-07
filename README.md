<h1>OpenIIoT.Tools.Packager</h1>

[![Build status](https://ci.appveyor.com/api/projects/status/6wksxf5pw8og1w31?svg=true)](https://ci.appveyor.com/project/OpenIIoTAdmin/openiiot-tools-packager)
[![Build Status](https://travis-ci.org/OpenIIoT/OpenIIoT.Tools.Packager.svg?branch=master)](https://travis-ci.org/OpenIIoT/OpenIIoT.Tools.Packager)
[![codecov](https://codecov.io/gh/OpenIIoT/OpenIIoT.Tools.Packager/branch/master/graph/badge.svg)](https://codecov.io/gh/OpenIIoT/OpenIIoT.Tools.Packager)
[![License: AGPL v3](https://img.shields.io/badge/License-AGPL%20v3-blue.svg)](https://github.com/OpenIIoT/OpenIIoT.Tools.Packager/blob/master/LICENSE)

Allows creation and management of Package files for the OpenIIoT platform.

```
 ▄█████   █████    ▄▄███▄▄    ▄▄███▄▄  ▄█████  ██▄▄▄  █████▄   ▄█████
 ██   ▀  ██   ██ ▄█▀▀██▀▀█▄ ▄█▀▀██▀▀█▄ ██   ██ ██▀▀█▄ ██   ██  ██  ▀
 ██   ▄  ██   ██ ██  ██  ██ ██  ██  ██ ███████ ██  ██ ██   ██ ▀▀▀▀▀██
 █████▀   █████   █  ██  █   █  ██  █  ██   ██  █  █  █████▀  ▄█████▀
   ┌─────────────────────── ─ ─── ───────────────────── ── ───── ─ ───   ──
   │ > manifest         Generate a package manifest file.
   │ > extract-manifest Extract a package manifest file from a package.
   │ > package          Create a package file.
   │ > extract-package  Extract a package file.
   │ > trust            Add a trust to a signed package file.
   │ > verify           Verify the integrity of a package file.
   │
   │ ! use 'help <command>' to get more details about that command.
   └───────────────────── ── ─────────────── ─── ─ ─  ─   ─
```

## Manifest Generation

Prior to creation of a Package, a Manifest for the package must be created.  

```
   ▄▄███▄▄  ▄█████  ██▄▄▄   █    ▄████  ▄████   ▄█████    ██
 ▄█▀▀██▀▀█▄ ██   ██ ██▀▀█▄ ██  ▄██▄▄▄   ██      ██  ▀  ▀██████▄
 ██  ██  ██ ███████ ██  ██ ██   ██     ▀██▀▀   ▀▀▀▀▀██    ██
  █  ██  █  ██   ██  █  █  █    ██      ██████ ▄█████▀   ▄██▀
   ┌─────────────────────── ─ ─── ───────────────────── ── ───── ─ ───   ──
   │ > manifest
   │
   │ <-d|--directory <directory>>       The directory containing payload files.
   │ [-i|--include-resources]           Include non-binary/web files.
   │ [-h|--hash]                        Mark files to be hashed during packaging.
   │ [-m|--manifest <file>]             The output manifest file.
   │
   │ ! ex: 'manifest -hi -d "desktop\coolPlugin" -o "manifest.json"'
   └───────────────────── ── ─────────────── ─── ─ ─  ─   ─
```

Following generation of the Manifest it will be necessary to edit the generated 
file to update the default Package metadata.  Depending on the contents of the directory,
the Manifest should look something like the following:

```json
{
  "Title": "DefaultPlugin",
  "Version": "1.0.0",
  "Namespace": "OpenIIoT.Plugin",
  "Description": "Default plugin.",
  "Publisher": "OpenIIoT Team",
  "Copyright": "Copyright (c) 2017 OpenIIoT",
  "License": "AGPLv3",
  "Url": "http://github.com/openiiot",
  "Files": {
    "Binary": [
      {
        "Source": "binary.dll"
      }
    ],
    "WebIndex": [
      {
        "Source": "index.html"
      }
    ],
    "Resource": [
      {
        "Source": "resource.bmp"
      }
    ]
  }
}
```

Files are divided into three categories; binaries (files ending in .dll), 
web indexes (files named index.html), and resources (all other files).  Resource files will not be included unless the ```-i``` or ```--include-resources``` 
arguments are specified.

Any files listed in the manifest must be present when the Package is extracted or the Package will
be considered invalid.  

If the ```-h``` or ```--hash``` arguments are specified, files listed in the Manifest will contain an additional, empty ```Checksum``` property

```json
"Binary": [
  {
    "Source": "binary.dll",
    "Checksum": ""
  }
]
```

The ```Checksum``` property must remain empty until the Package is created; it will be computed and populated by the tool as part of the packaging process.