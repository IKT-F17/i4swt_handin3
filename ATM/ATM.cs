using System.Collections.Generic;
using TransponderReceiver;

namespace ATM
{
    public class ATM
    {
        private ITrack _track;
        private ITrackFactory _trackFactory;
        private ITransponderReceiver _receiver;
        public Dictionary<string, ITrack> Global_TrackData;
        //private IAirSpace _airSpace;
        //private List<ISeperation> _seperations;
        //private ILog _log;

        public ATM(ITransponderReceiver receiver, ITrackFactory trackFactory, Dictionary<string, ITrack> tracks)
        {
            _trackFactory = trackFactory;
            _receiver = receiver;
            Global_TrackData = tracks;

            _receiver.TransponderDataReady += OnTrackData;
        }

        private void OnTrackData(object sender, RawTransponderDataEventArgs e)
        {
            var rawTrackData = e.TransponderData;

            foreach (var item in rawTrackData)
            {
                _track = _trackFactory.SpawnTrack(item);

                if (!Global_TrackData.ContainsKey(_track.Tag))
                {
                    Global_TrackData.Add(_track.Tag, _track);
                }
                else
                {
                    Global_TrackData[_track.Tag].updateTrack(_track);
                }
            }
        }
    }
}
