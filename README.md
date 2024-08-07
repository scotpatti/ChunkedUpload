# ChunkedUpload

This is a simple Razor pages (.Net Core 8.0) application that uploads a single file in smaller chunks. Chunksize is set in the javascript function in /Upload/Index.cshtml.

This does require authentication using ASP.NET Identity core, so don't for get to:

```
PS> Update-Database
```

Use this code at your own risk.

