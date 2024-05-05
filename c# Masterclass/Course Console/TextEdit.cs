using System;
using System.Text.RegularExpressions;

namespace Course_Console
{
    internal class TextEdit
    {
        public string Text { get; set; }
        private readonly string _textSource;

        public TextEdit()
        {
            _textSource = "D:\\Projects\\Assets\\TextFile.txt";
            Text = File.ReadAllText(_textSource);
        }

        public TextEdit(string textSource)
        {
            _textSource = textSource;
            Text = File.ReadAllText(_textSource);
        }

        public void ShowText()
        {
            Console.WriteLine(Text);
        }

        public void Assignment()
        {
            using (var sw = new StreamWriter("output.txt"))
            {
                using (var sr = new StreamReader(_textSource))
                {
                    string? line;
                    while ((line = sr.ReadLine()) is not null)
                    {
                        if (line.Contains("split"))
                        {
                            sw.Write(line.Split()[4] + " ");
                        }
                    }
                }
            }
        }

        public void Assignment2()
        {
            using (var sr = new StreamReader(_textSource))
            {
                string text = sr.ReadToEnd();
                string expresion = @"\d{2,3}";
                Regex reg = new Regex(expresion);
                MatchCollection collection = reg.Matches(text);
                //List<char> result = new();

                foreach (Match match in collection)
                {
                    char value = (char)Int32.Parse(match.Value);
                    //result.Add(value);
                    Console.WriteLine(value);
                }
            }
        }
    }
}
