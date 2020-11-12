using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_CellphoneStore
{
    public class ProvinceMapped
    {
        public ProvinceMapped()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Nullable<int> TelephoneCode { get; set; }
        public string ZipCode { get; set; }
        public int CountryId { get; set; }
        public string CountryCode { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<bool> IsPublished { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
