using System;

namespace Backend_CSharp.Models
{
    public class Pagination
    {
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }

        public int MaximumThisPage { get { return CurrentPage * PerPage; } }
        public int MinimumThisPage { get { return MaximumThisPage - PerPage + 1; } }
        public int MaxPages { get { return (int)Math.Ceiling((double)Total / (double)PerPage); } }

        public Pagination(int currentPage, int perPage, int total)
        {
            CurrentPage = currentPage;
            PerPage = perPage;
            Total = total;
        }

        public override string ToString()
        {
            return $"{nameof(CurrentPage)}:{CurrentPage}" +
                $"\n{nameof(PerPage)}:{PerPage}" +
                $"\n{nameof(Total)}:{Total}" +
                $"\n{nameof(MinimumThisPage)}:{MinimumThisPage}" +
                $"\n{nameof(MaximumThisPage)}:{MaximumThisPage}" +
                $"\n{nameof(MaxPages)}:{MaxPages}";
        }
    }
}
