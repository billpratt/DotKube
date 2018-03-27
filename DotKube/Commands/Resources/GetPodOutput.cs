using System;

namespace DotKube.Commands.Resources
{
    public class GetPodOutput
    {
        public string Name { get; set; }
        public string Ready { get; set; }
        public string Status { get; set; }
        public int Restarts { get; set; }
        public string Namespace { get; set; }

        public TimeSpan? Age { get; set; }

        public string AgeString
        {
            get
            {
                if (!Age.HasValue)
                    return "N/A";

                if (Age.Value.Days > 0)
                {
                    return $"{Age.Value.Days}d";
                }

                if (Age.Value.Hours > 0)
                {
                    return $"{Age.Value.Hours}h";
                }

                if (Age.Value.Minutes > 0)
                {
                    return $"{Age.Value.Minutes}m";
                }

                return $"{Age.Value.Seconds}s";
            }
        }
    }
}
