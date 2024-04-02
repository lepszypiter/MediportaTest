using System.ComponentModel.DataAnnotations.Schema;

namespace MediportaTest.Models;

public class Tag
{
    public long Id { get; set; }
    public string Name { get; set;}
    public long Count { get; set;}
    public double Percent { get; set; }
}