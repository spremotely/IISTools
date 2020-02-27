using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace IISTools
{
    internal class IISRewriteMapsParser
    {
        public IISRewriteMaps Parse(string fileName)
        {
            var parsedMaps = new IISRewriteMaps();
            var document = XDocument.Load(fileName);

            var rewriteMaps = document.Root?
                .Elements()
                .Where(e => 
                    string.Equals(e.Name.LocalName, "rewriteMap", StringComparison.OrdinalIgnoreCase));

            if (rewriteMaps == null)
            {
                return null;
            }

            foreach (var rewriteMap in rewriteMaps)
            {
                var mapNameAttribute = rewriteMap.Attributes().FirstOrDefault(a => string.Equals(a.Name.LocalName, "name"));

                if (mapNameAttribute == null)
                {
                    continue;
                }

                var mapName = mapNameAttribute.Value;

                parsedMaps.Maps.Add(mapName, rewriteMap.Elements()
                    .Select(this.PrepareMapElements)
                    .ToList());
            }

            return parsedMaps;
        }

        private IISRewriteMapElement PrepareMapElements(XElement element)
        {
            return new IISRewriteMapElement
            {
                From = element.Attributes()
                    .FirstOrDefault(a => 
                        string.Equals(a.Name.LocalName, "key", StringComparison.OrdinalIgnoreCase))?.Value,

                To = element.Attributes()
                    .FirstOrDefault(a =>
                        string.Equals(a.Name.LocalName, "value", StringComparison.OrdinalIgnoreCase))?.Value
            };
        }
    }
}
