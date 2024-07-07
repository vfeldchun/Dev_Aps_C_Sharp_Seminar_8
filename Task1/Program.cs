

/// Объедините две предыдущих работы (практические работы 2 и 3): 
/// поиск файла и поиск текста в файле написав утилиту которая ищет файлы определенного расширения с указанным текстом. 
/// Рекурсивно. 
/// Пример вызова утилиты: utility.exe txt текст.

namespace Task1
{
    internal class Program
    {
        private const string Path = @"C:\Users\vladuz\Documents\CodeWar";
        private static List<string> SearchIn(string path, string extention)
        {
            List<string> result = new List<string>();

            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles();
            DirectoryInfo[] dirs = directory.GetDirectories();

            foreach (FileInfo file in files)
            {                
                if (file.Extension.Contains(extention))
                {
                    if (!file.Name.Contains(","))
                        result.Add(file.FullName);
                }
                    
            }

            foreach (var dir in dirs)
            {
                result.AddRange(SearchIn(dir.FullName, extention));
            }
            return result;
        }

        private static List<string> ReadFile(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                return reader.ReadToEnd().Split("\n").ToList();
            }
        }

        private static List<string> FindWord(string word, List<string> lines)
        {
            return lines.Where(line => line.Contains(word, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        private static void PrintResults(Dictionary<string, List<string>> dict)
        {
            foreach (var item in dict)
            {
                Console.WriteLine($"File: {item.Key}");
                foreach (var line in item.Value)
                {
                    Console.WriteLine(line);
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            Dictionary<string, List<string>> resultDict = new Dictionary<string, List<string>>();

            if (args.Length == 2) 
            {
                List<string> filesList = SearchIn(path: Path, extention: args[0]);
                foreach (string file in filesList)
                {
                    var text = ReadFile(file);
                    var resultOfSearch = FindWord(args[1], text);
                    if (resultOfSearch.Count > 0)
                    {
                        resultDict.Add(file, resultOfSearch);
                    }                   
                }
            }
            else
                Console.WriteLine("Формат использования утилиты:\n utility.exe <extention of files> <word for search>");

            PrintResults(resultDict);
        }
    }
}
