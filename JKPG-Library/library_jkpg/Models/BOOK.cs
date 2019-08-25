//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace library_jkpg.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class BOOK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BOOK()
        {
            this.AUTHORs = new HashSet<AUTHOR>();
        }
    
        public string ISBN { get; set; }
        public string Title { get; set; }
        public Nullable<int> SignId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Publication Year")]
        public string PublicationYear { get; set; }
        [Display(Name = "Publication Info")]
        public string publicationinfo { get; set; }
        [Display(Name = "Pages")]
        public Nullable<short> pages { get; set; }
    
        public virtual CLASSIFICATION CLASSIFICATION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AUTHOR> AUTHORs { get; set; }
    }
}