<h1>OpenIIoT.Tools.Packager</h1>

[![Build status](https://ci.appveyor.com/api/projects/status/6wksxf5pw8og1w31?svg=true)](https://ci.appveyor.com/project/OpenIIoTAdmin/openiiot-tools-packager)
[![Build Status](https://travis-ci.org/OpenIIoT/OpenIIoT.Tools.Packager.svg?branch=master)](https://travis-ci.org/OpenIIoT/OpenIIoT.Tools.Packager)
[![codecov](https://codecov.io/gh/OpenIIoT/OpenIIoT.Tools.Packager/branch/master/graph/badge.svg)](https://codecov.io/gh/OpenIIoT/OpenIIoT.Tools.Packager)
[![License: AGPL v3](https://img.shields.io/badge/License-AGPL%20v3-blue.svg)](https://github.com/OpenIIoT/OpenIIoT.Tools.Packager/blob/master/LICENSE)

Creates and manages Package files for the OpenIIoT platform.

## Commands 

View a list of available commands by executing the application with no parameters, or with ```help```.

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

Information about a specific command can be retrieved using ```help <command>```.

## Manifest Generation

Prior to creation of a Package, a Manifest for the Package must be created.  

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
web indexes (files named index.html), and resources (all other files).  Resource files will not be included unless 
the ```-i``` or ```--include-resources``` arguments are specified.

Any files listed in the manifest must be present when the Package is extracted or the Package will
be considered invalid.  Binaries and web index 

If the ```-h``` or ```--hash``` arguments are specified, files listed in the Manifest will contain an additional, 
empty ```Checksum``` property

```json
"Binary": [
  {
    "Source": "binary.dll",
    "Checksum": ""
  }
]
```

The ```Checksum``` property must remain empty until the Package is created; it will be computed and populated by 
the tool as part of the packaging process.

The generated Manifest will be output to the console if no Manifest file is specified using the ```-m``` or 
```--manifest``` arguments.

## Manifest Extraction

A Package's Manifest can be extracted without fully extracting the Package with the ```extract-manifest``` command.  

```
  ▄████   ██   ██    ██      █████ ▄█████  ▄█████     ██          ▄▄███▄▄  ▄█████  ██▄▄▄   █    ▄████  ▄████   ▄█████    ██
  ██       ██▄██▀ ▀██████▄  ██  ██ ██   ██ ██   ▀  ▀██████▄  ▄▄ ▄█▀▀██▀▀█▄ ██   ██ ██▀▀█▄ ██  ▄██▄▄▄   ██      ██  ▀  ▀██████▄
 ▀██▀▀     ▄█▀█▄     ██    ▄██▄▄█▀ ███████ ██   ▄     ██     ▀▀ ██  ██  ██ ███████ ██  ██ ██   ██     ▀██▀▀   ▀▀▀▀▀██    ██
  ██████ ▄██  ▀██   ▄██▀    ██  ██ ██   ██ █████▀    ▄██▀        █  ██  █  ██   ██  █  █  █    ██      ██████ ▄█████▀   ▄██▀
   ┌─────────────────────── ─ ─── ───────────────────── ── ───── ─ ───   ──
   │ > extract-manifest
   │
   │ <-p|--package <file>>      The input package file.
   │ [-m|--manifest <file>]     The output manifest file.
   │
   │ ! ex: 'extract-manifest -p "desktop\coolPlugin.opkg" -o "extractedManifest.json"'
   └───────────────────── ── ─────────────── ─── ─ ─  ─   ─
```

This command will either output the Manifest to the console or to a file if the ```-m``` or 
```--manifest``` arugments are specified.

## Package Creation

The Package command creates a Package using a directory and Manifest file as input.

```
   █████▄ ▄█████  ▄█████   ██ ▄█▀  ▄█████    ▄████▄  ▄████
  ██   ██ ██   ██ ██   ▀   ██▐█▀   ██   ██  ██    ▀  ██
 ▀██▀▀▀▀  ███████ ██   ▄  ▀████    ███████ ▀██ ▀▀█▄ ▀██▀▀
 ▄███▀    ██   ██ █████▀   ██ ▀█▄  ██   ██  ██████▀  ██████
   ┌─────────────────────── ─ ─── ───────────────────── ── ───── ─ ───   ──
   │ > package
   │
   │ <-d|--directory <directory>>       The directory containing payload files.
   │ <-m|--manifest <file>>             The input manifest file.
   │ <-p|--package <file>>              The output package file.
   │ [-s|--sign]                        Sign the package file.
   │ [-r|--private-key <file>]          The ASCII-armored PGP private key file.
   │ [-a|--passphrase <string>]         The password for the private key file.
   │ [-u|--keybase-username <name>]     The username of the keybase.io account containing the PGP keys used to create the digest.
   │
   │ ! ex: 'package -d "desktop\coolPlugin" -m "manifest.json" -p "coolPlugin.opkg"'
   │ ! ex: 'package -d "desktop\coolPlugin" -m "manifest.json" -p "coolPlugin.opkg" -s -r "privateKey.asc" -a MyPassword -u "someUser123'
   └───────────────────── ── ─────────────── ─── ─ ─  ─   ─
```

The Package is created in a temporary directory, then copied to the final destination specified by the ```-p``` or
```--package``` arguments.

If a Package is to be digitally signed, the ```-s``` or ```--sign``` arguments are to be provided along with the 
private key, passphrase and keybase username arguments.  The [keybase.io](https://keybase.io) account associated with the
private and public key pair must host the public key matching the specified private key as the primary key on the account.

## Package Extraction

Extract Packages with the ```extract-package``` command.  

```
  ▄████   ██   ██    ██      █████ ▄█████  ▄█████     ██          █████▄ ▄█████  ▄█████   ██ ▄█▀  ▄█████    ▄████▄  ▄████
  ██       ██▄██▀ ▀██████▄  ██  ██ ██   ██ ██   ▀  ▀██████▄  ▄▄  ██   ██ ██   ██ ██   ▀   ██▐█▀   ██   ██  ██    ▀  ██
 ▀██▀▀     ▄█▀█▄     ██    ▄██▄▄█▀ ███████ ██   ▄     ██     ▀▀ ▀██▀▀▀▀  ███████ ██   ▄  ▀████    ███████ ▀██ ▀▀█▄ ▀██▀▀
  ██████ ▄██  ▀██   ▄██▀    ██  ██ ██   ██ █████▀    ▄██▀       ▄███▀    ██   ██ █████▀   ██ ▀█▄  ██   ██  ██████▀  ██████
   ┌─────────────────────── ─ ─── ───────────────────── ── ───── ─ ───   ──
   │ > extract-package
   │
   │ <-p|--package <file>>              The input package file.
   │ <-d|--directory <directory>]       The output directory.
   │ [-o|--overwrite]                   Deletes the output directory prior to extraction, if it exists.
   │ [-v|--skip-verification]           Skips the verification step prior to extraction.
   │
   │ ! ex: 'extract-package -ov -p "desktop\coolPlugin.opkg" -d "desktop\extracted"
   └───────────────────── ── ─────────────── ─── ─ ─  ─   ─
```

If the output directory exists an error will be thrown, otherwise it can be overwritten with the ```-o``` or ```--overwrite``` arguments.

Packages are automatically verified prior to extraction.  To suppress verification, specify either the ```-v``` or ```--skip-verification``` argument.

## Trust Creation

A Trust creates a digital signature of a Package's signature digest.  This command is designed to be used by a member of 
the OpenIIoT team to verify that a Package has been reviewed.

```
    ██      █████ █   █   ▄█████    ██
 ▀██████▄  ██  ██ █   ██  ██  ▀  ▀██████▄
    ██    ▄██▄▄█▀ █   ██ ▀▀▀▀▀██    ██
   ▄██▀    ██  ██ █████  ▄█████▀   ▄██▀
   ┌─────────────────────── ─ ─── ───────────────────── ── ───── ─ ───   ──
   │ > trust
   │
   │ <-p|--package>     The package to trust.
   │ <-r|--private-key> The ASCII-armored PGP private key file.
   │ <-a|--password>    The password for the private key file.
   │
   │ ! ex: 'trust -p "coolPlugin.opkg" -r "privateKey.asc" -a MyPassword'
   └───────────────────── ── ─────────────── ─── ─ ─  ─   ─
```

The ```trust``` command accepts three required arguments; the Package to trust, the PGP private key file, and the password
for the private key.  Other keys may be used, however OpenIIoT will use the OpenIIoT public key to verify the package.

## Package Verification

Verify Package files with the ```verify``` command.

```
  █   █   ▄████    █████  █    ▄████ ▄█  ▄
 ██   ██  ██      ██  ██ ██  ▄██▄▄▄  ▀▀▀▀██
  █▄ ▄█  ▀██▀▀   ▄██▄▄█▀ ██   ██     ██  ██
   ▀█▀    ██████  ██  ██ █    ██      ████
   ┌─────────────────────── ─ ─── ───────────────────── ── ───── ─ ───   ──
   │ > verify
   │
   │ <-p|--package>     The package to verify.
   │
   │ ! ex: 'verify -p "coolPlugin.opkg"'
   └───────────────────── ── ─────────────── ─── ─ ─  ─   ─
```

This command verifies the Package in the following order:

* If the Package is trusted, the trust is validated.
* If the Package is signed, the signature is validated.
* The payload checksum is validated against the payload archive.
* The payload is extracted and each file is validated against the Manifest.

Verification results are printed to the console.