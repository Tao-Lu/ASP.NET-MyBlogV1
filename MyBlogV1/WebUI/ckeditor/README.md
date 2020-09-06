CKEditor 4
==========

Copyright (c) 2003-2020, CKSource - Frederico Knabben. All rights reserved.
https://ckeditor.com - See LICENSE.md for license information.

CKEditor 4 is a text editor to be used inside web pages. It's not a replacement
for desktop text editors like Word or OpenOffice, but a component to be used as
part of web applications and websites.

## Documentation

The full editor documentation is available online at the following address:
https://ckeditor.com/docs/

## Installation

Installing CKEditor is an easy task. Just follow these simple steps:

 1. **Download** the latest version from the CKEditor website:
    https://ckeditor.com. You should have already completed this step, but be
    sure you have the very latest version.
 2. **Extract** (decompress) the downloaded file into the root of your website.

**Note:** CKEditor is by default installed in the `ckeditor` folder. You can
place the files in whichever you want though.

## Checking Your Installation

The editor comes with a few sample pages that can be used to verify that
installation proceeded properly. Take a look at the `samples` directory.

To test your installation, just call the following page at your website:

	http://<your site>/<CKEditor installation path>/samples/index.html

For example:

	http://www.example.com/ckeditor/samples/index.html



put in View

in header:

<script src="~/ckeditor/ckeditor.js"></script>

in body:

```c#
@Html.TextAreaFor(model => model.Content, new { @id = "ckeditor", @class = "form-control", @row = "200" })
@Html.ValidationMessageFor(model => model.Content, "", new { @class = "text-danger" })

@*rich text editor replace a textarea*@
<script>CKEDITOR.replace("ckeditor");</script>
@*rich text editor ends*@
```





