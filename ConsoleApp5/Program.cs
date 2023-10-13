using System;
using System.Text;

class Program
{
    static void Main()
    {
        string encodedText = @"
CkNvbmdyYXR6ISBZb3Ugc29sdmVkIHRoZSBzZWNvbmQgcHV6emxlLiBOb3cgaXQncyB0aW1l IGZvciB5b3UgdG8gYmUgaW5pdGlhdGVkCgpNYWtlIGEgcG9zdCByZXF1ZXN0IHRvIGNoYWxsZW5nZS5waG90aWVyLmNvbS9zdGFydCB3aXRoIHlvdXIgZW1haWwgYWRkcmVzcyBhcyBhIHBvc3QgcGFyYW1ldGVyIChieSB1c2luZyBhIHZlcnkgZWFzeSBrZXkgZm9yIHlvdXIgZW1haWwpLCB3aGljaCB5b3Ugc2hvdWxkIGd1ZXNz IHdpdGhvdXQgYSBwcm9ibGVtLgpJZiB5b3UgZW50ZXIgYSBjb3JyZWN0IGVtYWlsIGFkZHJlc3MgYW5kIGRvIGV2ZXJ5dGhpbmcgY29ycmVjdGx5LCB5b3Ugc2hvdWxkIGdldCB5b3VyIG5leHQgYXNzaWdubWVudC4KCkdvb2QgbHVjayEKClBob3RpZXIgVGVhbQo=
";

        byte[] decodedBytes = Convert.FromBase64String(encodedText);
        string decodedText = Encoding.UTF8.GetString(decodedBytes);

        Console.WriteLine(decodedText);
    }
}
