// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System.Text.Json;
using System.Net;
using Bitcoin;

internal class Program
{
    private static void Main(string[] args)
    {
        BTC bitcoin = ObtenerDatosBitcoin();
        Console.WriteLine("\n=======TIPOS DE CAMBIO DEL BTC (bitcoin)======\n");
        Console.WriteLine($"En dólares: {bitcoin.bpi.USD.rate}");
        Console.WriteLine($"En euros: {bitcoin.bpi.EUR.rate}");
        Console.WriteLine($"En libras esterlinas: {bitcoin.bpi.GBP.rate}");


        Console.WriteLine("\n=========CARACTERÍSTICAS DEL DOLAR=========\n");
        Console.WriteLine($"Código: {bitcoin.bpi.USD.code}");
        Console.WriteLine($"Símbolo: {bitcoin.bpi.USD.symbol}");
        Console.WriteLine($"Tasa en relación al BTC: {bitcoin.bpi.USD.rate}");
        Console.WriteLine($"Descripción: {bitcoin.bpi.USD.description}");
    }

    private static BTC ObtenerDatosBitcoin(){
        var url = "https://api.coindesk.com/v1/bpi/currentprice.json";
        var peticion = (HttpWebRequest)WebRequest.Create(url);
        peticion.Method = "GET";
        peticion.ContentType = "application/json";
        peticion.Accept = "application/json";
        BTC bitcoin = null;

        try
        {
            using (WebResponse response = peticion.GetResponse()){
                using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return null;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string infoBTC = objReader.ReadToEnd();
                            bitcoin = JsonSerializer.Deserialize<BTC>(infoBTC);
                            objReader.Dispose();
                        }
                    }
            }
        }
        catch (WebException ex)
        {
            Console.WriteLine("Problemas de acceso a la API");
        }
        return bitcoin;
    }
}
