namespace Domain;

interface INegocio
{
    public double Value { get; set; }
    public string? ClientSector { get; set; }
    public DateTime NextPaymentDate { get; set; }
    public String DescricaoCategoria { get; set; }
}

public class Negocio : INegocio
{
    public double Value { get; set; }
    public string? ClientSector { get; set; }
    public DateTime NextPaymentDate { get; set; }
    public String DescricaoCategoria { get; set; }
}

