using CURD_APP.Models;

namespace CURD_APP.Data
{
    public static class Data1
    {
        public static List<Model1> list1 = new List<Model1> {
            new Model1{Id=1,DishName="Apple",Weight=10.2},
            new Model1{Id = 2,DishName = "Banana", Weight = 5.2}
        };
    }
}
