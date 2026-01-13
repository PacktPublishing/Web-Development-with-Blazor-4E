namespace BlazorWebAssemblyApp.InterfaceGeneratorDemo;
using AutomaticInterface;

[GenerateAutomaticInterface]
public class SampleService: ISampleService
{
    public double Multiply(double x, double y)
    {
        return x * y;
    }

    public int NiceNumber => 1337;
}
