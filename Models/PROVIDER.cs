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
    
    public partial class PROVIDER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PROVIDER()
        {
            this.INVOICEs = new HashSet<INVOICE>();
        }
    
        public int PROVIDER_ID { get; set; }
        public string ADRESS { get; set; }
        public string PHONE { get; set; }
        public string NAMES { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INVOICE> INVOICEs { get; set; }
    }
}