//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace курсовая_работа.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class GOODS_CARE
    {
        public int GOODS_CARE_ID { get; set; }
        public decimal PRICE { get; set; }
        public int COUNTS { get; set; }
        public int DELIVERY_ID { get; set; }
        public int CONSUMABLES_ID { get; set; }
    
        public virtual CONSUMABLE CONSUMABLE { get; set; }
        public virtual DELIVERY DELIVERY { get; set; }
    }
}