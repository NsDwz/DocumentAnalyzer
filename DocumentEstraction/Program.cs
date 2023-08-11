// See https://aka.ms/new-console-template for more information

using AIBasedServises;



if (args.Length == 1)
{
    string path = args[0];
    
    var analizer = new DocumentRecognizerService("https://firststep.cognitiveservices.azure.com/", "0b6f1a4ed5a943a1a31f3b2a10c85031");
    string outPath = "result.json";
    string outKeyValuePath = "resultKeyValue.json";


    using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
    {

        using (StreamWriter outputFile = new StreamWriter(outPath))
        {

            var jsonString = analizer.ExtractDocumentInformation(fileStream);
            outputFile.Write(jsonString);
        }
    }

    using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
    {

        using (StreamWriter outputFile = new StreamWriter(outKeyValuePath))
        {

            var jsonString = analizer.ExtractDocumentKeyValueInformation(fileStream);
            outputFile.Write(jsonString);
        }
    }


}
else
{
    Console.WriteLine("Please specify one and only one document");
}





// analizer.ExtractDocumentInformationFromURI(
//     "https://wcblind.org/wp-content/uploads/2020/04/a-wi-real-id-with-a-star-in-the-upper-right-.jpeg");
