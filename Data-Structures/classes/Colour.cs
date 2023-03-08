namespace ColourNS
{
    /// <summary>
    /// Stores colour information (NON RGB)
    /// </summary>
    public class Colour
    {
        /// <summary>
        /// Stores the pattern of the colour as a string (ex. spotted)
        /// </summary>
        public string pattern { get; set; }

        /// <summary>
        /// Stores the primary colour as a string
        /// </summary>
        public string primary { get; set; }

        /// <summary>
        /// Stores the secondary colour as a string
        /// </summary>
        public string secondary { get; set; }

        /// <summary>
        /// Class contructor
        /// </summary>
        /// <param name="pattern">Pattern of the colours (ex. spotted)</param>
        /// <param name="primary">Primary colour </param>
        /// <param name="secondary">Secondary colour</param>
        public Colour(string pattern, string primary, string secondary)
        {
            this.pattern = pattern;
            this.primary = primary;
            this.secondary = secondary;
        }

        /// <summary>
        /// Gives a description of the colour in a string format
        /// </summary>
        /// <returns>A string containing the description</returns>
        public string Description()
        {
            return $"{primary}-{secondary} {pattern}";
        }
    }
}



