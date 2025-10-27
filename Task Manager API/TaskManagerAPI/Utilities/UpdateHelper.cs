namespace TaskManagerAPI.Utilities
{
    public static class UpdateHelper
    {
        public static string KeepOriginalIfEmpty(string? original, string? updated)
        {
            return string.IsNullOrWhiteSpace(updated) ? (original ?? "") : updated;
        }

        public static void UpdateIfNotEmpty(ref string? original, string? updated)
        {
            if (!string.IsNullOrWhiteSpace(updated))
                original = updated;
        }
    }
}
