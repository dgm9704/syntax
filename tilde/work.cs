#define FOO
//
// file for testing
//
namespace Syntax.Tilde
{
	using System;
    using System.Collections.Generic;
#if FOO
    using System.Collections.ObjectModel;
#else
	using System.Data.Linq
#endif
/*
	using System.IO
*/


    [Serializable]
    [XmlRoot(ElementName = "foo", Namespace = "http://example.com/foo")]
    public class Work : IEquatable<Work>
    {
        static AssemblyName assembly = Assembly.GetExecutingAssembly().GetName();

		string foo;

		foo =   "abcd{new FileInfo(\"/foo/bar\").Name.Lenght()}efgh";
		foo =  @"abcd{new FileInfo(""/foo/bar"").Name.Lenght()}efgh";

		foo =  $"abcd{new FileInfo( "/foo/bar").Name.Lenght()}efgh";
		foo =  $"abcd{new FileInfo(@"foo""bar").Name.Lenght()}efgh";
		foo =  $"abcd{new FileInfo( "foo\"bar").Name.Lenght()}efgh";
		foo =  $"abcd{new FileInfo($"/{foo}/{bar}").Name.Lenght()}efgh";

		string bar;
		bar =  "Alice \"Bob\" Cecilia";
		bar = $"Alice ""Bob"" Cecilia";

		bar = $@" "" {new Foo(@$"\")}  abc def ghi jkl ";

//		bar = $"Alice \"Bob\" Cecilia";
//		bar = "Alice ""Bob"" Cecilia";


		foo = $@"abcd{new FileInfo("/foo/bar").Name.Lenght()}efgh";

		foo = @$"abcd{new FileInfo("/foo/bar").Name.Lenght()}efgh";

		var a = 0;
		var aa = 123;
		var aaa = -123;
		var aaaa = +123
		var aaaaa = 123UL

		var b = 1.5;
		var c = 0xffff;




		bar = @"foo\"bar\"baz"

		var info = $"id=\"{id}\" version=\"{version}\" creationdate=\"{DateTime.Now:yyyy-MM-ddTHH:mm:ss:ffzzz}\"";

        internal static Dictionary<string, string> DefaultUnitNamespaces = new Dictionary<string, string>
        {
            ["iso4217"] = "http://www.xbrl.org/2003/iso4217",
        };

        public Entity Entity
        {
            get => entityField;
            set
            {
                entityField = value;
                entityField.Instance = this;
            }
        }

        public Period Period { get; set; } = new Period();

        [XmlArray("fIndicators", Namespace = "http://www.eurofiling.info/xbrl/ext/filing-indicators")]
        [XmlArrayItem("filingIndicator", Namespace = "http://www.eurofiling.info/xbrl/ext/filing-indicators")]
        public FilingIndicatorCollection FilingIndicators { get; set; } = new FilingIndicatorCollection();

        [XmlIgnore]
        public bool FilingIndicatorsSpecified
        => FilingIndicators != null && FilingIndicators.Any();

		#region stuff

        void ToXmlWriter(XmlWriter writer)
        {
            var ns = GetXmlSerializerNamespaces();



            writer.WriteProcessingInstruction("instance-generator", info);

            if (!string.IsNullOrEmpty(TaxonomyVersion))
                writer.WriteProcessingInstruction("taxonomy-version", TaxonomyVersion);

            foreach (var item in Comments)
                writer.WriteComment(item);

            Serializer.Serialize(writer, this, ns);
        }

        public XmlDocument ToXmlDocument()
        {
            var document = new XmlDocument();
            var declaration = document.CreateXmlDeclaration("1.0", "UTF-8", null);
            document.AppendChild(declaration);
            var nav = document.CreateNavigator();

            using (var writer = nav.AppendChild())
                ToXmlWriter(writer);

            return document;
        }

        public static Instance FromXml(string content)
        {
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(content);
                writer.Flush();

                stream.Position = 0;

                return FromStream(stream);
            }
        }

        public string ToXml()
        => ToXmlDocument().OuterXml;

        #endregion
    }
}