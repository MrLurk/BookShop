using System;
using SqlSugar;

namespace DbModels
{
    //如果实体类名称和表名不一致可以加上SugarTable特性指定表名
    [SugarTable("T_Book")]
    public class BookModel: DBModelBase
    {
        //指定主键和自增列，当然数据库中也要设置主键和自增列才会有效
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public decimal Price { get; set; }
    }
}
