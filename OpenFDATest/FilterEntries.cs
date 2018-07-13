using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenFDATest
{
   public class FilterEntries
    {
        public static string[] ListEntries()
        {
            string[] searchItems = new string[]
            {
                "purpose",
                "description",
                "warnings",
                "warnings_and_cautions",
                "adverse_reactions",
                "do_not_use",
                "pregnancy_or_breast_feeding",
                "questions",
                "ingredient",
                "effective_time"
            };
            return searchItems;
        }
    }
}
