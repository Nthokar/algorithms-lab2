using System.Xml;

namespace lab_work2;

public class Main
{

    public static void Start()
    {
        var p = ""; var q = "";
        var text = "";
        var xDoc = new XmlDocument();
        xDoc.Load("lab.xml");
        var xRoot = xDoc.DocumentElement;
        foreach (XmlNode xNode in xRoot)
        {
    
            if (xNode.Name == "p")
                p = xNode.InnerText;
            if (xNode.Name == "q")
                q = xNode.InnerText;
            if (xNode.Name == "text")
                text = xNode.InnerText;
    
        }
        var rsa = new Rsa(new BigInt(p), new BigInt(q), text);
        Console.WriteLine(rsa.GetAnswer());
        Console.ReadKey();
    }
}