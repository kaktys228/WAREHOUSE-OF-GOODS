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
    
    public partial class GOODS_ARIVAL
    {
        public int GOODS_ARIVAL_ID { get; set; }
        public int INVOICE_ID { get; set; }
        public int DELIVERY_ID { get; set; }
        public decimal PRICE { get; set; }
        public int COUNTS { get; set; }
    
        public virtual DELIVERY DELIVERY { get; set; }
        public virtual INVOICE INVOICE { get; set; }
    }
}
