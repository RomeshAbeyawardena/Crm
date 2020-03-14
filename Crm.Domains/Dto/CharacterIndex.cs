using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Dto
{
    public class CharacterIndex
    {
        public int Id { get; set; }
        public char Character { get; set; }
        public Hash Hash { get; set; }
        public int Index { get; set; }
    }
}
