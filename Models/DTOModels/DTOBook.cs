using System;

namespace DTOModels
{
    public class DTOBook : DTOModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public decimal Price { get; set; }
        public String DTOTEST { get; set; } = "测试！";
    }
}
