//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Chronos.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.Employement = new HashSet<Employement>();
        }
        
        public string GetFullName {
            get
            {
                return EmplSurname + " " + EmplName + " " + FatherName;
            }
        }

        public string PasportData
        {
            get
            {
                return $"Паспорт: {PasportSeries} {PasportNumber}";
            }
        }

        public int id { get; set; }
        public string EmplName { get; set; }
        public string EmplSurname { get; set; }
        public string FatherName { get; set; }
        public string Tel { get;  set; }
        public string PasportSeries { get; set; }
        public string PasportNumber { get; set; }
        public int id_Status { get; set; }
        public int id_Position { get; set; }
    
        public virtual Position Position { get; set; }
        public virtual Status Status { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employement> Employement { get; set; }
    }
}