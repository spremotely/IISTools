using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IISTools
{
    internal class IISRewriteMapsWriter
    {
        public void Write(string fileName, IISRewriteMaps maps)
        {
            StringBuilder builder = new StringBuilder();
            
            this.OpenRoot(builder);
            this.WriteMaps(builder, maps);
            this.CloseRoot(builder);
            
            File.WriteAllText(fileName, builder.ToString());
        }

        private void WriteMaps(StringBuilder builder, IISRewriteMaps maps)
        {
            foreach (var map in maps.Maps)
            {
                this.OpenMap(builder, map.Key);
                this.WriteElements(builder, map.Value);
                this.CloseMap(builder);
            }
        }

        private void WriteElements(StringBuilder builder, IList<IISRewriteMapElement> elements)
        {
            foreach (var element in elements)
            {
                this.WriteElement(builder, element);
            }
        }

        private void WriteElement(StringBuilder builder, IISRewriteMapElement element)
        {
            builder.AppendFormat("        <add key=\"{0}\" value=\"{1}\" />", element.From, element.To);
            builder.Append(Environment.NewLine);
        }

        private void OpenRoot(StringBuilder builder)
        {
            builder.Append("<rewriteMaps>");
            builder.Append(Environment.NewLine);
        }

        private void CloseRoot(StringBuilder builder)
        {
            builder.Append("</rewriteMaps>");
            builder.Append(Environment.NewLine);
        }

        private void OpenMap(StringBuilder builder, string name)
        {
            builder.AppendFormat("    <rewriteMap name=\"{0}\">", name);
            builder.Append(Environment.NewLine);
        }

        private void CloseMap(StringBuilder builder)
        {
            builder.Append("    </rewriteMap>");
            builder.Append(Environment.NewLine);
        }
    }
}
