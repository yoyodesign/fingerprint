[![Build status](https://ci.appveyor.com/api/projects/status/qwbxt91rlanitmvj?svg=true)](https://ci.appveyor.com/project/MMasey/fingerprint)

# Fingerprint
A simple class with a method for handling cache busting on static files (.Net)

It can be used for static files that often require cache busting, such as css and javascript.

It works by taking a hash of the file requested, and using that to change the url, which in turn forces the file to be requested again.
The hash is also cached and updated when the file is updated, meaning it won't waste memory.

## Requirements

-  ISS URL Rewrite Module installed

The following code in the web.Config

```
<system.webServer>
  <rewrite>
    <rules>
      <rule name="fingerprint">
        <match url="([\S]+)(/v-[a-z0-9]+/)([\S]+)" />
        <action type="Rewrite" url="{R:1}/{R:3}" />
      </rule>
    </rules>
  </rewrite>
</system.webServer>
```

## How to use it

```
<link href="@Fingerprint.Tag("/css/style.css")" rel="stylesheet" />

// creates

<link href="/css/v-7f2a5d53fbc1758441013d2edbe5ec36/style.css" rel="stylesheet" />
```