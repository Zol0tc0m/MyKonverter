using Newtonsoft.Json;
using System.Xml.Serialization;
using текстовый_конвертер;

List<Human> hum = new List<Human>();

Console.WriteLine("Какой файл используем?");
string path = Console.ReadLine();

if (path.EndsWith(".txt"))
{
    string[] lines = File.ReadAllLines(path);

    for (int i = 0; i < lines.Length; i += 3)
    {
        Human h = new Human();
        h.название = lines[i];
        h.длина = Convert.ToInt32(lines[i + 1]);
        h.ширина = Convert.ToInt32(lines[i + 2]);

        hum.Add(h);
    }
}
else if (path.EndsWith(".json")) 
{ 
    string text = File.ReadAllText(path);
    hum = JsonConvert.DeserializeObject<List<Human>>(text);
}
else if (path.EndsWith(".xml"))
{
    XmlSerializer xml = new XmlSerializer(typeof(List<Human>));
    using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
        hum = (List<Human>)xml.Deserialize(fs);
}
foreach (var item in hum)
{
    Console.WriteLine(item.название);
    Console.WriteLine(item.ширина);
    Console.WriteLine(item.длина);
}
Console.WriteLine("Куда и в какой формат вы хотите сохранить файл?");
path = Console.ReadLine();

if (path.EndsWith(".txt"))
{
    for (int i = 0; i < hum.Count(); i ++)
    {
        File.AppendAllText(path, hum[i].название +'\n');
        File.AppendAllText(path, hum[i].ширина.ToString() +'\n');
        File.AppendAllText(path, hum[i].длина.ToString() +'\n');
    }
}
else if (path.EndsWith(".xml"))
{
    XmlSerializer xml = new XmlSerializer(typeof(List<Human>));
    using (FileStream fs = new FileStream (path, FileMode.OpenOrCreate))
    {
        xml.Serialize(fs, hum);
    }
}
else if (path.EndsWith(".json"))
{
    string json = JsonConvert.SerializeObject(hum);
    File.WriteAllText(path, json);
}