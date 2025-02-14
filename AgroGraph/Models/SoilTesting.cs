namespace AgroGraph.Models;

public class SoilTesting
{
    public SoilTesting(int numberOfTest/*double currentDistanceTraveled*/, double humidity, double temperature, double conductivity, double acidity, double nitrogen, double phosphorus, double potassium, double salinity, double totalDissolvedSolids)
    {
        NumberOfTest = numberOfTest;
        //CurrentDistanceTraveled = currentDistanceTraveled;
        Humidity = humidity;
        Temperature = temperature;
        Conductivity = conductivity;
        Acidity = acidity;
        Nitrogen = nitrogen;
        Phosphorus = phosphorus;
        Potassium = potassium;
        Salinity = salinity;
        TotalDissolvedSolids = totalDissolvedSolids;
    }

    public int NumberOfTest { get; set; }
    public double CurrentDistanceTraveled { get; set; }
    public double Humidity { get; set; }
    public double Temperature { get; set; }
    public double Conductivity { get; set; }
    public double Acidity { get; set; }
    public double Nitrogen { get; set; }
    public double Phosphorus { get; set; }
    public double Potassium { get; set; }
    public double Salinity { get; set; }
    public double TotalDissolvedSolids { get; set; }
}