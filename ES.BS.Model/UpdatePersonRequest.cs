using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.BS.Model
{
    public class UpdatePersonRequest
    {
        public int? weight { get; set; }
        public string BuildingName { get; set; }
        public byte? fromFloor { get; set; }
        public byte? toFloor { get; set; }
    }
}
