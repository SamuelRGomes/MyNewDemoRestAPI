using MyDemoRestApi.Models.Dto;

namespace MyDemoRestApi.Data
{
    public class Store
    {
        public static List<MyDTO> villaList = new List<MyDTO>
        {
                new MyDTO{Id=1, Name="Pool View", Sqft=100,Occupancy=4},
                new MyDTO{Id=2, Name="Beach View", Sqft=300, Occupancy=3}

    };
    }
}
