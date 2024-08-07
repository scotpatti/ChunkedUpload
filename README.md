# ChunkedUpload

This is a simple Razor pages (.Net Core 8.0) application that uploads a single file in smaller chunks. Chunksize is set in the javascript function in /Upload/Index.cshtml.

For this project to work you need to complete the following tasks:

 1. Create X1_JwtKey and X1_JwtIssuer environment variables (make the X1_JwtIssuer something like https://localhost:7134 or whatever the base url is that you are running from).
 2. Create the database. This won't work if you haven't done step one:

```
PS> Update-Database
```

Use this code at your own risk.

