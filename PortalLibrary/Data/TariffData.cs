using System.Collections.Generic;
using PortalLibrary.Models;

namespace PortalLibrary.Data
{
    public class TariffData
    {
        private static List<Tarrif> tarrifData = new List<Tarrif>();

        private static List<Tarrif> CreateMockTarrifData()
        {
            List<Tarrif> mockTarrifData = new List<Tarrif>();

            var industryTarrif = new Tarrif
            {
                Id = "1",
                Name = "Industry Tarrif",
                PricePerUnit = 20
            };
            var smeTarrif = new Tarrif
            {
                Id = "2",
                Name = "SME Tarrif",
                PricePerUnit = 15
            };

            var houseHoldTarrif = new Tarrif
            {
                Id = "3",
                Name = "Household Tarrif",
                PricePerUnit = 10
            };

            mockTarrifData.Add(industryTarrif);
            mockTarrifData.Add(houseHoldTarrif);
            mockTarrifData.Add(smeTarrif);
            return mockTarrifData;
        }

        public static List<Tarrif> LoadMockTarrifData()
        {
            if (tarrifData.Count == 0)
            {
                tarrifData = CreateMockTarrifData();
            }
            return tarrifData;
        }

    }
}