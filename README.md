# IISTools
.Net Standard library helper for IIS config files

## IISRewriteMaps Usage

```C#
// read config xml
var rewriteMaps = IISRewriteMaps.Read("IISRewriteMaps.config");

// add new section
rewriteMaps.Maps.Add("hello map", new List<IISRewriteMapElement>
{
	new IISRewriteMapElement
	{
		Key = "/fromUrl",
		Value = "/toUrl"
	}
});

// save config xml
rewriteMaps.Write("new.config");