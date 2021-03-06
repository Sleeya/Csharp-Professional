﻿namespace FestivalManager.Entities.Factories
{
	using System;
    using System.Linq;
    using System.Reflection;
    using Contracts;
	using Entities.Contracts;

	public class SongFactory : ISongFactory
	{
		public ISong CreateSong(string name, TimeSpan duration)
        {
            var songType = Assembly.GetCallingAssembly().GetTypes()
               .Where(t => typeof(ISong).IsAssignableFrom(t))
               .FirstOrDefault();

            var song = (ISong)Activator.CreateInstance(songType, name, duration);

            return song;
        }
	}
}