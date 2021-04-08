using Cet.Core.Expression;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace SimpleExpressionParser2
{
    class Program
    {
        static IXSolverContext _ctx = new MySolverContext();

        static void Main(string[] args)
        {
            using StreamWriter output = new("demo.md");
            output.WriteLine("## Sample output");
            output.WriteLine();

            new Program()
            {
                _output = output,
            }
            .Demo();
        }

        private StreamWriter _output;

        private void Demo()
        {
            Test("3+5");
            Test("3+5 +10");
            Test("3+(5+1) + (2+3) +4");
            Test(" +7");
            Test(" + (4)");
            Test(" (+ +8)");
            Test("(+9) == + +9");
            Test("3==+3");
            Test("3==--3");
            Test("-(1-5)+(2+7)-10");
            Test("-10*2+(-2)*5/2");
            Test("1+0 / 0");
            Test("1+13 % 10");

            Test("2+3*5");
            Test("2*3-5");

            Test("(10 & 2) == 2");
            Test("12345 & 56");
            Test("(23 | 11) ^ ~66");

            Test("");
            Test("    ");
            Test("false  ");
            Test("  true");
            Test("  null  ");
            Test(" 123 ");
            Test("-456  ");
            Test(" +7 ");
            Test(" 3.151927 ");
            Test(" +2.718 ");
            Test(" .5 ");   //should throw
            Test(" -.5 ");   //should throw
            Test(" +5. ");   //should throw
            Test("myvar  ");
            Test("  w0rd");
            Test("_abc_def_");
            Test("my.multi.level.reference");
            Test(" 'incomplete string");   //should throw
            Test(" '");                     //should throw
            Test(" ''");
            Test("'' ");
            Test("'single-quoted string'");
            Test("\"double-quoted string\"");
            Test("'here is a \"nested string\"'");
            Test(@"'here is a ""nested escaped string""'");

            Test(" #  ");           //should throw
            Test(" #");             //should throw
            Test(" ##  ");          //should throw
            Test(" # #");           //should throw
            Test(" #à #  ");        //should throw
            Test(" #2007-04-05ù12:30-02:00#  ");   //should throw
            Test(" #2007-03-01T13:00:00Z# ");
            Test(" #2007-04-05T12:30-02:00#  ");
            Test(" #2007-04-05T12:30-02:00#== #2007-04-05T12:30-02:00# ");
            Test(" #2007-04-05T12:30-02:00# < #2007-04-05T12:31-02:00#");
            Test(" #2007-04-05T12:30-02:00# != 123 ");
            Test(" #2007-04-05T12:30-02:00# + 123 ");  //should throw
            Test(" #2007-04-05T12:30-02:00# #2007-04-05T12:30-02:00# ");  //should throw
            Test(" #2007-04-05T12:30-02:00##2007-04-05T12:30-02:00# ");   //should throw
            Test(" (#2007-04-05T12:30-02:00#)");
            Test(" #2007-04-05T12:30+02:00# == #2007-04-05T08:30:00-02:00#");
            Test(" #2007-04-05T12:30+02:00# == #2007-04-05T10:30:00Z#");
            Test(" #3000-04-05T12:30+02:00#");
            Test(" #2018-04-05T12:30:45.123456789+02:00#");
            Test(" #2007-04-05T12:30:45.123+02:00# > #2007-04-05T08:30:00-02:00#");

            Test("zero == zero  ");
            Test(" black != white");
            Test(" 12 < 45");
            Test("20 >4");
            Test("10<=100");
            Test("100   >=   1");

            Test("true && (1 < 2)");
            Test("(3 < 5) && true");
            Test("true && true");
            Test("false || (true) || (1 == 2)");
            Test("(1==1) && (2==2) && true");

            Test("!false==!!true");
            Test("to_be || !to_be");
            Test(" maccheroni || spaghetti || rigatoni");
            Test(" sex && drug && rock && roll   ");
            Test("!me || you && !they ");
            Test("a==b && c!=d");
            Test("pname match/abc/");
            Test("pname match /xyz/ig");
            Test("pname   match /(\\w+)\\s(\\w+)/");

            Test("(!me ||you)&&they");
            Test("!(a=='q') && (b!='x')");
            Test("(a || b) && (c || d) || (e && f)");
            Test("! (a && (b && c || d && e) || (g == h && j))");
            Test("!! (((a)==b) && ((((c && ((g)))))))");
        }

        private void Test(
            string text
            )
        {
            this._output.WriteLine($"Expression: `{text}`");
            this._output.WriteLine();
            try
            {
                //parsing
                XTreeNodeBase xtree = XTreeNodeBase.Parse(text);

                //compact serialization
                var cser = new XTreeCompactSerializer();
                cser.ShouldPad = true;
                string xstr = cser.Serialize(xtree, null) ?? string.Empty;

                this._output.WriteLine($"Tidy form: `{xstr}`");

                //xml serialization
                var xser = new XTreeXmlSerializer();
                XElement xelem = xser.Serialize(xtree, null) ?? new XElement("bingo");

                this._output.WriteLine("Serialized tree:");
                this._output.WriteLine("```");
                this._output.WriteLine(xelem);
                this._output.WriteLine("```");

                //evaluation (against the sample context)
                XSolverResult sr = xtree.Resolve(_ctx);
                this._output.WriteLine($"Result: `{sr.Error ?? sr.Data}`");

                //verify the compact serialization
                try
                {
                    XTreeNodeBase xtreeAlt = XTreeNodeBase.Parse(xstr);
                    XElement xelemAlt = xser.Serialize(xtreeAlt, null) ?? new XElement("bongo");
                    if (xelem.ToString() != xelemAlt.ToString())
                    {
                        //fail!
                    }
                }
                catch (Exception ex)
                {
                    this._output.WriteLine("Verify error: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                this._output.WriteLine("Test error: " + ex.Message);
            }
            this._output.WriteLine("***");
            this._output.WriteLine();
        }


        class MySolverContext : IXSolverContext
        {
            public IXReferenceSolver ReferenceSolver { get; } = new MyReferenceSolver();
        }

        class MyReferenceSolver : IXReferenceSolver
        {
            private Dictionary<string, object?> _bag = new()
            {
                ["myvar"] = "MyVar",
                ["w0rd"] = null,
                ["_abc_def_"] = Math.PI,
                ["zero"] = 0,
                ["black"] = "nero",
                ["white"] = "bianco",
                ["to_be"] = true,
                ["maccheroni"] = "stringa",
                ["spaghetti"] = 1234,
                ["rigatoni"] = false,
                ["sex"] = 789,
                ["drug"] = "droga",
                ["rock"] = true,
                ["roll"] = 3.14159,
                ["me"] = "",
                ["you"] = 1,
                ["they"] = true,
                ["a"] = 555,
                ["b"] = 555,
                ["c"] = true,
                ["d"] = null,
                ["e"] = "abc",
                ["f"] = -34,
                ["g"] = "",
                ["h"] = null,
                ["j"] = false,
                ["pname"] = "very long text which contains 'abcdefgh'...",
            };

            public XSolverResult GetValue(XTokenRefId token)
            {
                var refId = token.Data as string;
                if (string.IsNullOrEmpty(refId)) return XSolverResult.FromData(null);

                this._bag.TryGetValue(refId, out object? value);
                return XSolverResult.FromData(value);
            }
        }
    }
}