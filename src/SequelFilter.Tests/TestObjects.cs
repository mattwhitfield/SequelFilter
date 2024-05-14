using System.Globalization;

namespace SequelFilter.Tests
{
    public class World
    {
        public IEnumerable<Country> Countries => new[] { Country.US, Country.UK, Country.France };

        public RandomStatistics Statistics { get; } = new RandomStatistics();
    }

    public class RandomStatistics
    {
        public long Population => 8007821437;

        public long Websites => 1630322759;

        public long FieldOfDreams = 201;
    }

    public class Country
    {
        public static Country US { get; } = new Country("US", 332403650, true, new[] { "diaper", "math", "legos", "pan", "red", "read", "dream" }, new[] { new Subdivision("Washington", 12), new Subdivision("North Carolina", 33), new Subdivision("Florida", 55) });
        public static Country UK { get; } = new Country("UK", 68768944, true, new[] { "nappy", "maths", "lego", "pan", "red", "read", "dream" }, new[] { new Subdivision("England", 1), new Subdivision("Wales", 2), new Subdivision("Northern Ireland", 3), new Subdivision("Scotland", 4) });
        public static Country France { get; } = new Country("France", 65631038, false, new[] { "couche", "mathématiques", "légo", "poêle", "rouge", "lire", "reve" }, new[] { new Subdivision("Alsace", 88), new Subdivision("Brittany", 107), new Subdivision("Corsica", 215) });

        public Country(string countryName, int population, bool speaksEnglish, IEnumerable<string> words, IEnumerable<Subdivision> subdivisions)
        {
            CountryName = countryName;
            Population = new Population(population);
            SpeaksEnglish = speaksEnglish;
            Words = words;
            Subdivisions = subdivisions;
        }

        public string CountryName { get; }

        public Population Population { get; }

        public bool SpeaksEnglish { get; }

        public object? NullIfSpeaksEnglish => SpeaksEnglish ? null : new object();

        public IEnumerable<string> Words { get; }

        public IEnumerable<Subdivision> Subdivisions { get; }
    }

    public class Population
    {
        public Population(int population)
        {
            Int = population;
            Decimal = population + 0.02m;
            Double = population + 0.02d;
            String = Double.ToString(CultureInfo.InvariantCulture);
        }

        public int Int { get; }

        public decimal Decimal { get; }

        public double Double { get; }

        public string String { get; }
    }

    public class Subdivision
    {
        public Subdivision(string divisionName, int randomFactor)
        {
            DivisionName = divisionName;
            RandomFactor = randomFactor;
            RandomDecimal = randomFactor + 0.02m;
            RandomDouble = randomFactor + 0.02d;
        }

        public string DivisionName { get; }

        public int RandomFactor { get; }

        public decimal RandomDecimal { get; }

        public double RandomDouble { get; }
    }
}
