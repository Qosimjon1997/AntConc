using System.ComponentModel.DataAnnotations;

namespace AntConc
{
    public class ConcordanceHits
    {
        public ConcordanceHits()
        {
                
        }
        public int Id { get; set; }
        public string myWords { get; set; }
        public string myFile { get; set; }

    }
}
