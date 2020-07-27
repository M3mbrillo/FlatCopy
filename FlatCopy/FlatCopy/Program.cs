using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace FlatCopy
{
    class Program
    {
        static void Main(string[] args)
        {
            // Usado originalmente para romper la recursividad de
            // relaciones en una entidad con Lazy Loading en EF
            // Pero se lo puedo aplicar en varias cosas mas...

            var example = new A
            {
                Id = 2345,
                P1 = 100,
                P2 = DateTime.Now,
            };

            example.P_A = example;

            var flatExample = example.GetFlatCopy(4); //Deep 4, read 4 time P_A

            Console.WriteLine(JsonSerializer.Serialize(flatExample));
        }
    }

    class A : IId
    {
        public int Id { get; set; }
        public int P1 { get; set; }
        public DateTime P2 { get; set; }

        public A P_A { get; set; }
    }
    
}
