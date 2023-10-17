using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Stash.Project.Stash.Dictionary.Model
{
    public class DictionaryTable:BasicAggregateRoot<long>
    {
        public string Dictionary_Name { get; set; }=null!;
        public string Dictionary_Type { get;set; }=null!;
        public string Dictionary_PageType { get; set; }=null!;  
    }
}
