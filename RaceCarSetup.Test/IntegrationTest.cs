using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace RaceCarSetup.Test
{
    [TestFixture]
    public class IntegrationTest
    {
        private const int TrackCount = 50;
        private const int CarConfigurationCount = 1000;
        private readonly Random random = new Random();

        [Test]
        public void RaceCarSetupIntegrationTest()
        {
            var tracks = InitTracks();

            var csp = new CarSetupProcessor(tracks);
            var setups = InitSetups();


            var processStart = DateTime.Now.TimeOfDay;
            Console.Out.WriteLine("PROCESS START: {0}", processStart);
            var results = csp.RankConfigurationsForTrack("Track_" + random.Next(0, TrackCount).ToString("0000"), setups);
            var processEnd = DateTime.Now.TimeOfDay;
            Console.Out.WriteLine("PROCESS END: {0}", processEnd);
            Console.Out.WriteLine("PROCESS DURATION: {0}", processEnd - processStart);

            PrintResults(results);
        }

        private void PrintResults(IEnumerable<RaceSimulationResult> results)
        {
            foreach (var result in results)
            {
                Console.Out.WriteLine("{0} - {1}", result.CarConfiguration.ConfigurationName, result.RaceTime);
            }
        }

        private CarConfiguration[] InitSetups()
        {
            var tracks = new CarConfiguration[CarConfigurationCount];
            for (var i = 0; i < CarConfigurationCount; i++)
            {
                tracks[i] = BuildCarConfiguration(i);
            }
            return tracks;
        }

        private CarConfiguration BuildCarConfiguration(int i)
        {
            return new CarConfiguration(ConfigurationName(i), BuildTrackPerformances());
        }

        private TrackPerformance[] BuildTrackPerformances()
        {
            var trackPerformances = new TrackPerformance[TrackCount];
            for (var i = 0; i < TrackCount; i++)
            {
                trackPerformances[i] = BuildTrackPerformance(i);
            }
            return trackPerformances;
        }

        private TrackPerformance BuildTrackPerformance(int i)
        {
            return new TrackPerformance()
            {
                AvgConsumption = (float)(1.0f + (random.NextDouble())),
                LapTime = (float)(75.0f + (random.NextDouble() * 5)),
                FuelCapacity = (float)(100.0f + (random.NextDouble() * 50)),
                TrackName = TrackName(i)
            };
        }


        private Track[] InitTracks()
        {
            var tracks = new Track[TrackCount];
            for (var i = 0; i < TrackCount; i++)
            {
                tracks[i] = BuildTrack(i);
            }
            return tracks;
        }

        private Track BuildTrack(int i)
        {
            return new Track()
            {
                LapDistance = (float)(4.0f + (random.NextDouble() * 3)),
                PitstopTime = (float)(3.0f + (random.NextDouble() * 2)),
                RaceLenght = 50 + i,
                TrackName = TrackName(i)
            };
        }


        private String TrackName(int i)
        {
            return "Track_" + i.ToString("0000");
        }

        private static string ConfigurationName(int i)
        {
            return "CarConfiguration_" + i.ToString("0000");
        }
    }
}
