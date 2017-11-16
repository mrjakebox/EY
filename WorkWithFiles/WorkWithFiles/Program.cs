using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//logger.Error("ERROR");
//logger.Debug("ERROR");
//logger.Fatal("ERROR");
//logger.Info("ERROR");
//logger.Trace("ERROR");
//logger.Warning("ERROR");
namespace WorkWithFiles
{
    class Program
    {
        static Logger logger;
        static string path = "files";

        static void Main(string[] args)
        {
            try
            {
                logger = new Logger(true); //LOG OR NOT
                logger.Info("Beginning of work");
                logger.Trace("Checking the existence of the path");
                DirectoryInfo dirInfo = new DirectoryInfo(path);

                if (!dirInfo.Exists)
                {
                    logger.Warning("Path not found");
                    try
                    {
                        dirInfo.Create();
                    }
                    catch
                    {
                        throw new CustomException("Could not create file");
                    }
                    logger.Trace("Path was created");
                }
                else
                    logger.Trace("Path exists");

                int countFiles;
                countFiles:
                Console.WriteLine("Enter the number of files");
                if (!int.TryParse(Console.ReadLine(), out countFiles))
                {
                    Console.WriteLine("Please try again");
                    logger.Warning("Is not a number");
                    goto countFiles;
                }

                int countLines;
                countLines:
                Console.WriteLine("Enter the number of lines in file");
                if (!int.TryParse(Console.ReadLine(), out countLines))
                {
                    Console.WriteLine("Please try again");
                    logger.Warning("Is not a number");
                    goto countLines;
                }
                CreateFilesAndFill(countFiles, countLines);
                Console.WriteLine("Files were created " + Directory.GetCurrentDirectory() + "\\" + path);

                string checkValue;
                checkValue:
                Console.WriteLine("Enter the string to exclude");
                checkValue = Console.ReadLine();
                if (checkValue.Contains(" "))
                {
                    Console.WriteLine("Please enter only one word");
                    goto checkValue;
                }

                string[] files = Directory.GetFiles(path);
                logger.Trace(String.Format("Receiving file headers ({0})", path));
                if (files.Count() == 0)
                    logger.Warning("Files not found");
                else
                    foreach (string s in files)
                    {
                        logger.Debug(s);
                    }

                ReadAllFiles(files, checkValue);

            }
            catch (CustomException ex)
            {
                logger.Error(ex.Message);
            }
            catch (Exception ex)
            {
                logger.Fatal(ex.StackTrace);

            }
        }

        static bool CreateFilesAndFill(int countFiles, int countLines)
        {
            string filepath;
            for (int i = 0; i < countFiles; i++)
            {
                filepath = Guid.NewGuid() + ".txt";
                if (!File.Exists(path + "/" + filepath))
                {
                    File.Create(path + "/" + filepath).Dispose();

                    logger.Trace(path + "/" + filepath + " was created");
                    for (int j = 0; j < countLines; j++)
                    {
                        using (TextWriter sw = new StreamWriter(path + "/" + filepath, true, System.Text.Encoding.Default))
                        {
                            sw.WriteLine(Line.Instance.GenerateLine());
                        }
                    }
                }
                else
                {
                    logger.Warning(path + "/" + filepath + " already exist");
                }
            }
            return true;
        }

        static void ReadAllFiles(string[] files, string checkValue)
        {
            string writePath = "output_" + checkValue + ".txt";
            foreach (var file in files)
            {
                using (StreamReader sr = new StreamReader(file, System.Text.Encoding.Default))
                {
                    string line;
                    using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                    {   //true - the new data will be added at the end to the existing data.
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (!line.Contains(checkValue))
                            {
                                sw.WriteLine(line);
                                CountingData.Counting(line);
                            }
                            else logger.Info("\'" + line + "\' " + "contains " + checkValue);
                        }
                    }
                }
            }
            Console.WriteLine("Сombined file: " + Directory.GetCurrentDirectory() + "\\" + writePath);

            string stat = "statistic.txt";
            using (StreamWriter sw = new StreamWriter(stat, true, System.Text.Encoding.Default))
            {
                sw.WriteLine("Суммы всех чисел А из общего файла: " + CountingData.SumA.ToString());
                sw.WriteLine("Количество строк, где число Б находится в диапазоне 3.0-5.0: " + CountingData.CountB.ToString());
                sw.WriteLine("50 строк, содержащих самые большие числа А в порядке убывания: ");
                foreach (int top in CountingData.List)
                    sw.WriteLine(top.ToString());
                Console.WriteLine("Statistics: " + Directory.GetCurrentDirectory() + "\\" + stat);

            }
        }  
    }
}