namespace RaceCarSetup
{
    public class TrackRepository : ITrackRepository
    {
        private readonly Track[] tracks;

        public TrackRepository(Track[] tracks)
        {
            this.tracks = tracks;
        }
        
        public Track FindTrack(string trackName)
        {
            foreach (var track in tracks)
            {
                if (track.TrackName.Equals(trackName))
                {
                    return track;
                }
            }
            return null;
        }
    }
}