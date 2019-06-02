# Podcast RSS Feed parser

NOTE: This project is a fork of https://github.com/rwhitmire/Podly.FeedParser - changed to work with .NET Standard/Core.

This project was originally forked from the [Quick and Dirty Feed Parser](https://github.com/Aaronontheweb/qdfeed) - lots of credit to @Aaronontheweb for this library. We re-purposed parts of the library to enable us to parse RSS feeds. The standard .NET library for RSS feed parsing lacks fault tolerance. This is a major problem with data as unstandardized and poorly formatted as RSS.

## Usage
```csharp
var factory = new HttpFeedFactory();
var feed = factory.CreateFeed(new Uri(podcast.FeedUrl));
```
